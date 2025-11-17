using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemySearchState : IState
{
    private IStateSwitcher _stateMachine;
    private Enemy _enemy;

    public EnemySearchState(IStateSwitcher stateMachine, Enemy enemy)
    {
        _stateMachine = stateMachine;
        _enemy = enemy;
    }

    public void Enter()
    {
        _enemy.EnemyView.SetSearching();
    }

    public void Exit()
    {
        
    }

    public void FixedUpdate()
    {
        
    }

    public void Update()
    {
        SearchPlayer();
    }

    private void SearchPlayer()
    {
        if (Vector2.Distance(_enemy.Target.transform.position, _enemy.transform.position) <= _enemy.Config.AttackRange)
            _stateMachine.SwitchState<EnemyFightState>();
        else if (Vector2.Distance(_enemy.transform.position, _enemy.Target.transform.position) <= _enemy.Config.TrackingDistance)
            _stateMachine.SwitchState<EnemyChasingState>();
    }
}
