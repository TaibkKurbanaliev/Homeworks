using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoveState : IState
{
    private IStateSwitcher _stateSwitcher;
    private Player _player;

    private Vector2 _input;
    private Vector2 _previousInput;
    private float _speedOffset = 0.001f;

    public MoveState(IStateSwitcher stateSwitcher, Player player)
    {
        _stateSwitcher = stateSwitcher;
        _player = player;
    }

    public void Enter()
    {
        _player.PlayerView.SetMovementAnimation();
        _player.InputActions.Player.Attack.started += OnAttack;
    }

    public void Exit()
    {
        _player.InputActions.Player.Attack.started -= OnAttack;
    }

    public void FixedUpdate()
    {
        if (_input != Vector2.zero)
        {
            _player.Rigidbody.linearVelocityX = Mathf.Lerp(_player.Rigidbody.linearVelocityX,
                                             _player.Rigidbody.linearVelocityX + _input.x,
                                             Time.fixedDeltaTime * _player.Config.Acceleration);
            _player.Rigidbody.linearVelocityX = Mathf.Clamp(_player.Rigidbody.linearVelocityX, -_player.Config.Speed, _player.Config.Speed);

            if (_input != _previousInput)
            {
                _player.transform.rotation = new Quaternion(_player.transform.rotation.x,
                                                     _input.x > 0 ? (float)Direction.Right : (float)Direction.Left,
                                                     _player.transform.rotation.z,
                                                     _player.transform.rotation.w);
                _player.Rigidbody.linearVelocityX = 0;
                _previousInput = _input;
            }
        }
        else
        {
            _player.Rigidbody.linearVelocityX = Mathf.Lerp(_player.Rigidbody.linearVelocityX,
                                             0f,
                                             Time.fixedDeltaTime * _player.Config.Deceleration);
        }
    }

    public void Update()
    {
        _input = _player.InputActions.Player.Move.ReadValue<Vector2>();

        if (_player.Rigidbody.linearVelocityX <= _speedOffset && _player.Rigidbody.linearVelocityX >= -_speedOffset)
            _stateSwitcher.SwitchState<IdleState>();
    }

    private void OnAttack(InputAction.CallbackContext context)
    {
        _stateSwitcher.SwitchState<AttackState>();
    }
}
