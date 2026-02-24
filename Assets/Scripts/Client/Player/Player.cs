using JetBrains.Annotations;
using Mirror;
using UnityEngine;

[RequireComponent(typeof(PlayerView))]
public class Player : NetworkBehaviour
{
    [SerializeField] private PlayerConfig _config;

    private StateMachine _stateMachine;

    [field: SerializeField] public PlayerView PlayerView;
    [field: SerializeField] public Rigidbody Rigidbody;
    [field: SerializeField] public Transform CameraTarget;

    public InputSystem_Actions Actions { get; private set; }

    private void Awake()
    {
        Actions = new InputSystem_Actions();
        Actions.Enable();
        _stateMachine = new StateMachine();
        var data = new StatesData();
        _stateMachine.AddState(new WalkingState(this, _stateMachine, data, _config.WalkingStateConfig));
        _stateMachine.SwitchState<WalkingState>();
    }

    protected override void OnValidate()
    {
        if (PlayerView == null)
            PlayerView = GetComponent<PlayerView>();
    }

    private void Update()
    {
        /*if (!isOwned)
            return;*/

        _stateMachine.Update();
    }

    private void FixedUpdate()
    {
        /*if (!isOwned)
            return;*/

        _stateMachine.FixedUpdate();
    }

    public void Init(InstanceInfo info)
    {
        RpcInit(info);
    }

    [ClientRpc]
    private void RpcInit(InstanceInfo info)
    {
        PlayerView.Init(info);
    }
}
