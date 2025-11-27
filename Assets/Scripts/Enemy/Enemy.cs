using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Rigidbody2D _rb;

    private EntityView _enemyView;
    private IMoveStrategy _moveStrategy;
    private bool _isPaused = false;

    public void Init()
    {
        _enemyView = new EntityView(_animator);
    }

    private void OnEnable()
    {
        EventBus.Instance.OnGameEvent += HandleEvent;
    }

    private void OnDisable()
    {
        EventBus.Instance.OnGameEvent -= HandleEvent;
    }

    private void Update()
    {
        if (_isPaused)
            return;

        _enemyView.SetMoveDirection(_rb.linearVelocity);
    }

    private void FixedUpdate()
    {
        if (_isPaused)
        {
            _moveStrategy.Stop();
            return;
        }

        _moveStrategy.Move();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent(out Player player))
        {
            player.TakeDamage();
        }
    }

    public void SetMoveStrategy(IMoveStrategy moveStrategy)
    {
        _moveStrategy = moveStrategy;
        _enemyView.SetMoving(true);
    }

    private void HandleEvent(IEvent @event)
    {
        if (@event is GamePausedEvent pause)
            _isPaused = pause.IsPaused;
    }
}
