using UnityEngine;

public class ChaseStrategy : IMoveStrategy
{
    private Player _target;
    private Rigidbody2D _rb;
    private float _speed;

    public ChaseStrategy(Player target, Rigidbody2D rb, float speed)
    {
        _target = target;
        _rb = rb;
        _speed = speed;
    }

    public void Move()
    {
        var targetDir = (_target.transform.position - _rb.transform.position).normalized;
        _rb.linearVelocity = targetDir * _speed;
    }

    public void Stop()
    {
        _rb.linearVelocity = Vector2.zero;
    }
}
