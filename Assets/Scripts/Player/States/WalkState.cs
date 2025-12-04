using UnityEngine;

public class WalkState : FireState
{
    private WalkingConfig _config;
    public WalkState(Player player, IStateSwitcher switcher) : base(player, switcher)
    {
        _config = player.Config.WalkingConfig;
    }

    public override void Enter()
    {
        base.Enter();

        Speed = _config.Speed;
        Acceleration = _config.Acceleration;
        Deceleration = _config.Deceleration;
    }

    public override void Update()
    {
        base.Update();

        if (Player.Controller.velocity.magnitude <= Player.Controller.minMoveDistance)
        {
            Switcher.SwitchState<IdleState>();
        }
    }
}
