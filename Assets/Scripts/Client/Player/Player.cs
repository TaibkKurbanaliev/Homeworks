using Mirror;
using System;
using UnityEngine;

[RequireComponent(typeof(PlayerView), typeof(CharacterController), typeof(CapsuleCollider))]
public class Player : NetworkBehaviour
{
    [SerializeField] private PlayerConfig _config;

    private StateMachine _stateMachine;
    [SyncVar(hook = nameof(OnInstanceInfoChanged))] private InstanceInfo _instanceInfo;

    [field: SerializeField] public PlayerView PlayerView { get; private set; }
    [field: SerializeField] public PlayerInfo PlayerInfo { get; private set; }
    [field: SerializeField] public CharacterController CharacterController { get; private set; }
    [field: SerializeField] public Transform CameraTarget { get; private set; }
    [field: SerializeField] public CapsuleCollider Collider { get; private set; }
    [field: SerializeField] public Settings Settings { get; private set; }

    public IInput Input { get; private set; }

    [Server]
    public void Init(InstanceInfo info)
    {
        _instanceInfo = info;
    }

    public override void OnStartServer()
    {
        base.OnStartServer();

        EventBus<ServerPlayerConnectedToGame>.Raise(new ServerPlayerConnectedToGame { PlayerInfo = PlayerInfo, 
                                                                                      InstanceInfo = _instanceInfo });
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
    }


    private void OnInstanceInfoChanged(InstanceInfo oldValue, InstanceInfo newValue)
    {
        PlayerView.Init(newValue);
    }
}
