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
        _enemy.Agent.enabled = false;
        _enemy.Collider.enabled = false;
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
}
