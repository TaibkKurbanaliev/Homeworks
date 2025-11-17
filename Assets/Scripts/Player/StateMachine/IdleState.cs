using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class IdleState : IState
{
    private IStateSwitcher _stateSwitcher;
    private Player _player;

    public IdleState(IStateSwitcher stateSwitcher, Player player)
    {
        _stateSwitcher = stateSwitcher;
        _player = player;
    }

    public void Enter()
    {
        _player.PlayerView.SetIdleAnimation();
        _player.InputActions.Player.Attack.started += OnAttack;
    }

    public void Exit()
    {
        _player.InputActions.Player.Attack.started -= OnAttack;
    }

    public void FixedUpdate()
    {
    }

    public void Update()
    {
        var input = _player.InputActions.Player.Move.ReadValue<Vector2>();

        if (input != Vector2.zero)
            _stateSwitcher.SwitchState<MoveState>();
    }

    private void OnAttack(InputAction.CallbackContext context)
    {
        _stateSwitcher.SwitchState<AttackState>();
    }
}
