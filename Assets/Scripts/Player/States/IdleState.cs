using UnityEngine;

public class IdleState : FireState
{
    public IdleState(Player player, IStateSwitcher switcher) : base(player, switcher)
    {
    }

    public override void Enter()
    {
        base.Enter();

        Speed = 0;
        Acceleration = 0;
        Deceleration = 0;
    }

    public override void Update()
    {
        base.Update();

        if (Player.Input.Player.Move.ReadValue<Vector2>() != Vector2.zero)
        {
            Switcher.SwitchState<WalkState>();
        }
    }
}
