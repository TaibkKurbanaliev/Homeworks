using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Collider))]
public class Enemy : MonoBehaviour, IDamagable
{
    private StateMachine _stateMachine;

    [field: SerializeField] public EnemyView View { get; private set; }
    [field: SerializeField] public NavMeshAgent Agent { get; private set; }
    [field: SerializeField] public EnemyConfig Config { get; private set; }
    public Player Target { get; private set; }
    public Health Health { get; private set; }
    public Collider Collider { get; private set; }

    public void Init(Player target)
    {
        Target = target;
        Health = new(Config.Health);
        Collider = GetComponent<Collider>();

        _stateMachine = new StateMachine();
        _stateMachine.AddState(new EnemyMoveState(this, _stateMachine));
        _stateMachine.AddState(new EnemyAttackState(this, _stateMachine));
        _stateMachine.AddState(new EnemyDieState(this));
        _stateMachine.SwitchState<EnemyMoveState>();
    }

    private void Update() => _stateMachine.Update();
    private void FixedUpdate() => _stateMachine.FixedUpdate();

    public void TakeDamage(float damage, Vector3 hitNormal)
    {
        if (damage <= 0) return;

        View.PlayHitEffect(hitNormal);
        Health.ReduceHealth(damage);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_stateMachine.CurrentState is EnemyAttackState attackState)
        {
            attackState.Hit();
        }
    }
}
