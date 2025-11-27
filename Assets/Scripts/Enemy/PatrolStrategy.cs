using UnityEngine;

public class PatrolStrategy : IMoveStrategy
{
    private Transform _startPoint;
    private Transform _endPoint;
    private Rigidbody2D _rigidbody;
    private float _speed;
    private float _stopOffset = 0.01f;
    public PatrolStrategy(Transform startPoint, Transform endPoint, float speed, Rigidbody2D rigidbody)
    {
        _startPoint = startPoint;
        _endPoint = endPoint;
        _speed = speed;
        _rigidbody = rigidbody;
        Debug.Log((_rigidbody.transform.position - _endPoint.transform.position));
    }

    public void Move()
    {
        var targetDir = (_endPoint.transform.position - _rigidbody.transform.position ).normalized;
        _rigidbody.linearVelocity = targetDir * _speed;

        if (Vector3.Distance(_rigidbody.transform.position, _endPoint.position) <= _stopOffset)
        {
            var temp = _endPoint;
            _endPoint = _startPoint;
            _startPoint = temp;
        }
    }

    public void Stop()
    {
        _rigidbody.linearVelocity = Vector2.zero;
    }
}
