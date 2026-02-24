using UnityEngine;

public abstract class AirborneState : MovementState
{
    protected AirborneStateConfig Config { get; private set; }
    public AirborneState(Player player, IStateSwitcher stateSwitcher, StatesData statesData, AirborneStateConfig config) 
        : base(player, stateSwitcher, statesData)
    {
        Config = config;
    }

    public override void Enter()
    {
        base.Enter();

        Data.HorizontalSpeed = Config.AirHorizontalSpeed;
        Player.Rigidbody.linearDamping = 0f;
    }

    public override void Update()
    {
        base.Update();

        if (Data.IsGrounded)
            StateSwitcher.SwitchState<WalkingState>();
    }
}
