using System;
using UnityEngine;

public class EnemyMoveState : IState
{
    private Enemy _enemy;
    private IStateSwitcher _stateSwitcher;

    public EnemyMoveState(Enemy enemy, IStateSwitcher stateSwitcher)
    {
        _enemy = enemy;
        _stateSwitcher = stateSwitcher;
    }

    public void Enter()
    {
        _enemy.Health.OnDied += OnDied;
    }


    public void Exit()
    {
        _enemy.Health.OnDied -= OnDied;
    }

    public void FixedUpdate()
    {
    }

    public void Update()
    {
        Move();

        var distance = Vector3.Distance(_enemy.transform.position, _enemy.Target.transform.position);

        if (distance <= _enemy.Config.AttackRange)
        {
            _stateSwitcher.SwitchState<EnemyAttackState>();
        }
    }

    private void Move()
    {
        _enemy.Agent.SetDestination(_enemy.Target.transform.position);
        _enemy.View.SetSpeed(_enemy.Agent.velocity.magnitude);
    }

    private void OnDied()
    {
        _stateSwitcher.SwitchState<EnemyDieState>();
    }
}
