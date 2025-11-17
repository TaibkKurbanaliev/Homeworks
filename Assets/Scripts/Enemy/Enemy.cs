using System;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamagable
{
    private StateMachine _stateMachine;

    [field: SerializeField] public Animator Animator { get; private set; }
    [field: SerializeField] public EnemyConfig Config { get; private set; }
    [field: SerializeField] public Rigidbody2D Rb { get; private set; }

    public Health Health { get; private set;  }
    public EnemyView EnemyView { get; private set; }
    public Player Target { get; private set; }
    public EventManager EventManager { get; private set; }

    public void Init(Player target, EventManager eventManager)
    {
        EnemyView = new(Animator);
        Health = new Health(Config.Health);
        Target = target;

        EventManager = eventManager;

        _stateMachine = new StateMachine();
        _stateMachine.AddState(new EnemySearchState(_stateMachine, this));
        _stateMachine.AddState(new EnemyFightState(_stateMachine, this));
        _stateMachine.AddState(new EnemyChasingState(_stateMachine, this));
        _stateMachine.AddState(new EnemyDiedState(this));
        _stateMachine.SwitchState<EnemySearchState>();
    }

    public void OnEnable()
    {
        Health.Died += OnDied;
    }

    private void OnDisable()
    {
        Health.Died -= OnDied;
    }

    private void Update()
    {
        _stateMachine.Update();
    }

    private void FixedUpdate()
    {
        _stateMachine.FixedUpdate();
    }

    public void TakeDamage(float damage)
    {
        Health.ReduceHealth(damage);
    }

    public void OnDiedAnimEnd()
    {
        Destroy(gameObject);
    }

    private void OnDied()
    {
        _stateMachine.SwitchState<EnemyDiedState>();
    }
}
