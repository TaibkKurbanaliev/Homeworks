using System;
using UnityEngine;

public class EnemyFightState : IState
{
    private IStateSwitcher _stateMachine;
    private Enemy _enemy;

    private float _delay;

    public EnemyFightState(IStateSwitcher stateMachine, Enemy enemy)
    {
        _stateMachine = stateMachine;
        _enemy = enemy;
    }

    public void Enter()
    {
        _enemy.EnemyView.SetFighting();
        _enemy.EventManager.TriggerEvent(new GameEvent(EventType.BattleStart, DateTime.Now, "Battle started!"));
    }

    public void Exit()
    {
    }

    public void FixedUpdate()
    {

    }

    public void Update()
    {
        if (_delay >= _enemy.Config.AttackSpeed)
        {
            var hit = Physics2D.Raycast(_enemy.transform.position,
                                        _enemy.transform.right,
                                        _enemy.Config.AttackRange,
                                        _enemy.Config.TargetMask);

            if (hit.collider != null && hit.collider.TryGetComponent(out IDamagable damagable))
                damagable.TakeDamage(_enemy.Config.Damage);

            _delay = 0;
        }

        _delay += Time.deltaTime;

        if (Vector2.Distance(_enemy.Target.transform.position, _enemy.transform.position) > _enemy.Config.AttackRange)
            _stateMachine.SwitchState<EnemySearchState>();
    }
}
