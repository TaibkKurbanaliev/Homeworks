using System;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyChasingState : IState
{
    private IStateSwitcher _stateMachine;
    private Enemy _enemy;

    public EnemyChasingState(IStateSwitcher stateMachine, Enemy enemy)
    {
        _stateMachine = stateMachine;
        _enemy = enemy;
    }

    public void Enter()
    {
        _enemy.EnemyView.SetChasing();
        _enemy.EventManager.TriggerEvent(new GameEvent(EventType.StartChasing, DateTime.Now, "Enemy start chase!!!"));
    }

    public void Exit()
    {
        _enemy.Rb.linearVelocityX = 0f;
    }

    public void FixedUpdate()
    {
        var dir = (_enemy.Target.transform.position.x - _enemy.transform.position.x > 0) 
                   ? _enemy.Config.MoveSpeed : -_enemy.Config.MoveSpeed;

        _enemy.Rb.linearVelocityX = Mathf.Lerp(_enemy.Rb.linearVelocityX,
                                         _enemy.Rb.linearVelocityX + dir,
                                         Time.fixedDeltaTime * _enemy.Config.Acceleration);

        _enemy.Rb.linearVelocityX = Mathf.Clamp(_enemy.Rb.linearVelocityX, 
                                                -_enemy.Config.MoveSpeed, 
                                                _enemy.Config.MoveSpeed);
    }

    public void Update()
    {
        if (Vector2.Distance(_enemy.Target.transform.position, _enemy.transform.position) <= _enemy.Config.AttackRange)
            _stateMachine.SwitchState<EnemyFightState>();
        else if (Vector2.Distance(_enemy.Target.transform.position, _enemy.transform.position) >= _enemy.Config.TrackingDistance)
            _stateMachine.SwitchState<EnemySearchState>();
    }
}
