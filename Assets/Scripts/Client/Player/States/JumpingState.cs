using UnityEngine;

public class JumpingState : AirborneState
{
    public JumpingState(Player player, IStateSwitcher stateSwitcher, StatesData statesData, AirborneStateConfig config) : base(player, stateSwitcher, statesData, config)
    {
    }

    public override void Enter()
    {
        base.Enter();

        var velocity = Player.Rigidbody.linearVelocity;
        velocity.y = 0f;
        Player.Rigidbody.linearVelocity = velocity;

        Player.Rigidbody.AddForce(
            Vector3.up * Config.JumpForce,
            ForceMode.VelocityChange
        );
    }

    public override void Update()
    {
        if (Player.Rigidbody.linearVelocity.y < 0)
        {
            StateSwitcher.SwitchState<FallingState>();
            return;
        }    

        base.Update();
    }
}
