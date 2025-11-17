using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;


public class Player : MonoBehaviour, IDamagable
{
    [SerializeField] private Animator _animator;
    [SerializeField] private LayerMask _itemMask;

    private EventManager _eventManager;
    private Health _health;
    private StateMachine _stateMachine;

    [field: SerializeField] public LayerMask Target { get; private set; }
    [field: SerializeField] public Rigidbody2D Rigidbody { get; private set; }
    [field: SerializeField] public PlayerConfig Config { get; private set; }

    public PlayerInputActions InputActions { get; private set; }
    public PlayerView PlayerView { get; private set; }

    public void Init(PlayerInputActions inputActions, EventManager eventManager)
    {
        InputActions = inputActions;
        PlayerView = new(_animator);
        _health = new(Config.Health);

        _eventManager = eventManager;

        _stateMachine = new StateMachine();
        _stateMachine.AddState(new IdleState(_stateMachine, this));
        _stateMachine.AddState(new AttackState(_stateMachine, this));
        _stateMachine.AddState(new MoveState(_stateMachine, this));
        _stateMachine.SwitchState<IdleState>();
    }

    private void OnEnable()
    {
        _eventManager.OnGameEvent += HandleGameEvent;
    }

    private void Update() =>
        _stateMachine.Update();

    private void FixedUpdate() =>
        _stateMachine.FixedUpdate();

    public void TakeDamage(float damage)
    {
        _health.ReduceHealth(damage);
    }

    private void HandleGameEvent(GameEvent e)
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((_itemMask & (1 << collision.gameObject.layer)) != 0)
        {
            _eventManager.TriggerEvent(new GameEvent(EventType.ItemPicked, DateTime.Now, $"{collision.name} picked!!!"));
            Destroy(collision.gameObject);
        }
    }
}
