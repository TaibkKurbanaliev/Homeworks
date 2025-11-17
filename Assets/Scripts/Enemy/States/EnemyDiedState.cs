using System;
using UnityEngine;

public class EnemyDiedState : IState
{
    private Enemy _enemy;

    public EnemyDiedState(Enemy enemy)
    {
        _enemy = enemy;
    }

    public void Enter()
    {
        _enemy.EnemyView.SetDeath();
        _enemy.EventManager.TriggerEvent(new GameEvent(EventType.Died, DateTime.Now, "Enemy Died!!!"));
    }

    public void Exit()
    {
    }

    public void FixedUpdate()
    {
    }

    public void Update()
    {
    }
}
