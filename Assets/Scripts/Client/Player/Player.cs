using Mirror;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(PlayerView), typeof(CharacterController), typeof(CapsuleCollider))]
public class Player : NetworkBehaviour
{
    public event Action<Player> ServerDied;

    [SerializeField] private PlayerConfig _config;

    private StateMachine _stateMachine;
    [SyncVar(hook = nameof(OnInstanceInfoChanged))] private InstanceInfo _instanceInfo;

    [field: SerializeField] public PlayerView PlayerView { get; private set; }
    [field: SerializeField] public PlayerInfo PlayerInfo { get; private set; }
    [field: SerializeField] public CharacterController CharacterController { get; private set; }
    [field: SerializeField] public Transform CameraTarget { get; private set; }
    [field: SerializeField] public Transform CameraGhostTarget { get; private set; }
    [field: SerializeField] public CapsuleCollider Collider { get; private set; }
    [field: SerializeField] public Settings Settings { get; private set; }
    [field: SerializeField] public Weapon Weapon { get; private set; }
    [field: SerializeField] public HealthComponent Health { get; private set; }
    [field: SerializeField] public ItemsController ItemsController { get; private set; }

    public IInput Input { get; private set; }

    private void Awake()
    {
        Health.Died += OnDied;
        Health.ServerDied += OnServerDied;
    }

    private void OnDestroy()
    {
        Health.Died -= OnDied;
        Health.ServerDied -= OnServerDied;
    }

    [Server]
    public void Init(InstanceInfo info)
    {
        _instanceInfo = info;
    }

    [Server]
    public void Respawn(Vector3 position)
    {
        var conn = NetworkManagerExt.LocalPlayers.FirstOrDefault(player =>
                                                          player.Value.GetComponent<ClientInstance>().CurrentPlayer == this).Key;
        if (conn != null)
        {
            TargetRpcRespawn(conn, position);
            Health.Reset();
        }
    }

    [TargetRpc]
    private void TargetRpcRespawn(NetworkConnection conn, Vector3 position)
    {
        if (!isOwned)
            return;

        _stateMachine.SwitchState<WalkingState>();
        transform.position = position;
        PlayerView.SetFPView();
        CameraGhostTarget.transform.localPosition = Vector3.zero;
    }

    public override void OnStartServer()
    {
        base.OnStartServer();

        EventBus<ServerPlayerConnectedToGame>.Raise(new ServerPlayerConnectedToGame { NetID = netId,
                                                                                      PlayerInfo = PlayerInfo, 
                                                                                      InstanceInfo = _instanceInfo });
    }

    public override void OnStopServer()
    {
        base.OnStopServer();

        EventBus<ServerPlayerDisconnected>.Raise(new ServerPlayerDisconnected { NetID = netId });
    }

    public override void OnStartAuthority()
    {
        base.OnStartAuthority();

        var data = new StatesData();

        CharacterController.enabled = true;
        PlayerView.SetFPView();

        EventBus<PlayerConnectedToGame>.Raise(new PlayerConnectedToGame { Player = this });

        _stateMachine = new StateMachine();
        _stateMachine.AddState(new FallingState(this, _stateMachine, data, _config.AirborneStateConfig));
        _stateMachine.AddState(new JumpingState(this, _stateMachine, data, _config.AirborneStateConfig));
        _stateMachine.AddState(new WalkingState(this, _stateMachine, data, _config.WalkingStateConfig));
        _stateMachine.AddState(new DyingState(this, _stateMachine, _config.DyingStateConfig));
        _stateMachine.SwitchState<WalkingState>();

    }

    protected override void OnValidate()
    {
        if (PlayerView == null)
            PlayerView = GetComponent<PlayerView>();

        if (CharacterController == null)
            CharacterController = GetComponent<CharacterController>();

        if (Collider == null)
            Collider = GetComponent<CapsuleCollider>();
    }

    private void Update()
    {
        if (!isOwned)
            return;

        _stateMachine.Update();
    }

    private void FixedUpdate()
    {
        if (!isOwned)
            return;

        _stateMachine.FixedUpdate();
    }

    public void SetInput(IInput input)
    {
        Input = input;
        ItemsController.Init(input);
    }

    private void OnDied()
    {
        EventBus<DiedEvent>.Raise(new DiedEvent { Player = this });
    }

    private void OnServerDied()
    {
        ServerDied?.Invoke(this);
    }

    private void OnInstanceInfoChanged(InstanceInfo oldValue, InstanceInfo newValue)
    {
        PlayerView.Init(newValue);
    }
}
