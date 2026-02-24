using System;
using UnityEngine;

public class WalkingState : MovementState
{
    private WalkingStateConfig _cfg;

    public WalkingState(Player player, IStateSwitcher stateSwitcher, StatesData statesData, WalkingStateConfig cfg) 
        : base(player, stateSwitcher, statesData)
    {
        _cfg = cfg;
    }

    public override void Enter()
    {
        base.Enter();

        Data.HorizontalSpeed = _cfg.Speed;
        Player.Rigidbody.linearDamping = _cfg.Drag;
        Player.Input.Jumped += OnJumpPressed;
    }

    public override void Exit()
    {
        base.Exit();

        Player.Input.Jumped -= OnJumpPressed;
    }

    public override void Update()
    {
        base.Update();

        if (!Data.IsGrounded)
            StateSwitcher.SwitchState<FallingState>();
    }

    private void OnJumpPressed()
    {
        StateSwitcher.SwitchState<JumpingState>();
    }
}
