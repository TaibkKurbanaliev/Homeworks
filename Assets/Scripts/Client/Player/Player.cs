using JetBrains.Annotations;
using Mirror;
using UnityEngine;

[RequireComponent(typeof(PlayerView), typeof(Rigidbody), typeof(CapsuleCollider))]
public class Player : NetworkBehaviour
{
    [SerializeField] private PlayerConfig _config;

    private StateMachine _stateMachine;

    [field: SerializeField] public PlayerView PlayerView { get; private set; }
    [field: SerializeField] public Rigidbody Rigidbody { get; private set; }
    [field: SerializeField] public Transform CameraTarget { get; private set; }
    [field: SerializeField] public CapsuleCollider Collider { get; private set; }

    public IInput Input { get; private set; }

    public override void OnStartAuthority()
    {
        base.OnStartAuthority();
        var data = new StatesData();
        Input = new NewInputSystem();
        _stateMachine = new StateMachine();
        _stateMachine.AddState(new FallingState(this, _stateMachine, data, _config.AirborneStateConfig));
        _stateMachine.AddState(new JumpingState(this, _stateMachine, data, _config.AirborneStateConfig));
        _stateMachine.AddState(new WalkingState(this, _stateMachine, data, _config.WalkingStateConfig));
        _stateMachine.SwitchState<WalkingState>();
        EventBus<PlayerConnectedToGame>.Raise(new PlayerConnectedToGame { Player = this });
    }

    protected override void OnValidate()
    {
        if (PlayerView == null)
            PlayerView = GetComponent<PlayerView>();
        
        if (Rigidbody == null)
            Rigidbody = GetComponent<Rigidbody>();
        
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

    public void Init(InstanceInfo info)
    {
        RpcInit(info);
        PlayerView.Init(info);
    }

    [ClientRpc]
    private void RpcInit(InstanceInfo info)
    {
        PlayerView.Init(info);
    }
}
