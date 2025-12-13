using System;
using UnityEngine;

public class EnemyDieState : IState
{
    private Enemy _enemy;

    public EnemyDieState(Enemy enemy)
    {
        _enemy = enemy;
    }

    public void Enter()
    {
        _enemy.View.PlayDieAnim();
        _enemy.View.OnEnemyDiedAnimEnded += OnDiedAnimEnded;
        EventBus.Instance.TriggerEvent(new EnemyDiedEvent("Died"));
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

    private void OnDiedAnimEnded()
    {
        _enemy.Agent.enabled = true;
        _enemy.Collider.enabled = true;
        _enemy.gameObject.SetActive(false);
    }
}
