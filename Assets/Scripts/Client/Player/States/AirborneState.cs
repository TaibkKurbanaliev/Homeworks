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
    }

    public override void Update()
    {
        base.Update();

        if (Player.CharacterController.isGrounded)
            StateSwitcher.SwitchState<WalkingState>();
    }
}
