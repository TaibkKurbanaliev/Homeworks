using System;
using UnityEngine;

public class EnemyAttackState : IState
{
    private Enemy _enemy;
    private IStateSwitcher _stateSwitcher;
    private bool _isAttackStarted;

    public EnemyAttackState(Enemy enemy, IStateSwitcher stateSwitcher)
    {
        _enemy = enemy;
        _stateSwitcher = stateSwitcher;
    }

    public void Enter()
    {
        Vector3 direction = _enemy.Target.transform.position - _enemy.transform.position;
        direction.y = 0f;
        _enemy.transform.rotation = Quaternion.LookRotation(direction);
        _enemy.View.Attack();
        _enemy.View.OnAttackAnimEnded += OnAttackAnimEnded;
        _enemy.Health.OnDied += OnDied;
        _isAttackStarted = true;
    }

    public void Exit()
    {
        _enemy.View.OnAttackAnimEnded -= OnAttackAnimEnded;
        _enemy.Health.OnDied -= OnDied;
    }

    public void FixedUpdate()
    {
    }

    public void Update()
    {
        Vector3 direction = _enemy.Target.transform.position - _enemy.transform.position;
        direction.y = 0f;
        _enemy.transform.rotation = Quaternion.LookRotation(direction);
    }

    public void Hit(Vector3 hitNormal = default)
    {
        if (_isAttackStarted)
        {
            _isAttackStarted = false;
            _enemy.Target.TakeDamage(_enemy.Config.Damage, hitNormal);
        }
    }

    private void OnAttackAnimEnded()
    {
        var distance = Vector3.Distance(_enemy.transform.position, _enemy.Target.transform.position);

        if (distance > _enemy.Config.AttackRange)
        {
            _stateSwitcher.SwitchState<EnemyMoveState>();
            return;
        }

        _enemy.View.Attack();
        _isAttackStarted = true;
    }

    private void OnDied()
    {
        _stateSwitcher.SwitchState<EnemyDieState>();
    }
}
