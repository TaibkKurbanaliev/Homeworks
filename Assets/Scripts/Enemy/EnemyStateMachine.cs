using System;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;


public enum EnemyState { Search, Chase, Fight, Death}
public class EnemyStateMachine 
{
    private Player _target;
    private Transform _transform;
    private Rigidbody2D _rb;
    private EnemyConfig _config;
    private EnemyView _enemyView;
    private Health _health;
    private EnemyState _currentState = EnemyState.Chase;

    private float _attackTime;
    private Coroutine _deathCoroutine;

    public EnemyStateMachine(Player target, Transform transform, Rigidbody2D rb, EnemyConfig config, EnemyView enemyView, Health health)
    {
        _target = target;
        _transform = transform;
        _rb = rb;
        _config = config;
        _enemyView = enemyView;
        _health = health;
    }

    public void FixedUpdate()
    {
        if (_currentState != EnemyState.Death)
        {
            switch (_currentState)
            {
                case EnemyState.Search:
                    SearchPlayer();
                    break;
                case EnemyState.Fight:
                    Fight();
                    break;
                case EnemyState.Chase:
                    ChasingPlayer();
                    break;
                default:
                    throw new NotImplementedException();
            }
        } 
    }

    public void ChasingPlayer()
    {
        if (Vector2.Distance(_target.transform.position, _transform.position) <= _config.AttackRange)
        {
            _currentState = EnemyState.Fight;
            return;
        }
        else if (Vector2.Distance(_target.transform.position, _transform.position) >= _config.TrackingDistance)
        {
            _currentState = EnemyState.Search;
            return;
        }

        var dir = (_target.transform.position.x - _transform.position.x > 0) ? _config.MoveSpeed : -_config.MoveSpeed;
        _rb.linearVelocityX = Mathf.Lerp(_rb.linearVelocityX,
                                         _rb.linearVelocityX + dir,
                                         Time.fixedDeltaTime * _config.Acceleration);

        _rb.linearVelocityX = Mathf.Clamp(_rb.linearVelocityX, -_config.MoveSpeed, _config.MoveSpeed);
        _enemyView.SetChasing();
    }

    public void Fight()
    {
        _attackTime += Time.fixedDeltaTime;
        if (Vector2.Distance(_target.transform.position, _transform.position) > _config.AttackRange)
        {
            _currentState = EnemyState.Chase;
            _attackTime = 0;
            return;
        }

        _enemyView.SetFighting();
        
        if (_attackTime >= _config.AttackSpeed)
        {
            _target.TakeDamage(_config.Damage);
            _attackTime = 0;
        }
    }

    public void SearchPlayer()
    {
        if (Vector2.Distance(_target.transform.position, _transform.position) <= _config.TrackingDistance)
        {
            _currentState = EnemyState.Chase;
            return;
        }

        _enemyView.SetSearching();
    }
}
