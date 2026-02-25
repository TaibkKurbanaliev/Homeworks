using UnityEngine;

public class JumpingState : AirborneState
{
    public JumpingState(Player player, IStateSwitcher stateSwitcher, StatesData statesData, AirborneStateConfig config) : base(player, stateSwitcher, statesData, config)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Data.Velocity.y = Config.JumpForce;
    }

    public override void Update()
    {
        if (Player.CharacterController.velocity.y < 0)
        {
            StateSwitcher.SwitchState<FallingState>();
            return;
        }    

        base.Update();
    }
}
