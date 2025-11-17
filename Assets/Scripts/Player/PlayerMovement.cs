using UnityEngine;

public enum Direction { Left = 180, Right = 0}

public class PlayerMovement
{
    private Transform _transform;
    private PlayerConfig _config;
    private Rigidbody2D _rb;

    private Vector2 _currentVelocity;
    private Vector2 _previousDirection;
    private float _stopOffset = 0.01f;

    public bool IsMoving => _currentVelocity.x > _stopOffset;

    public PlayerMovement(Rigidbody2D rb, PlayerConfig config, Transform transform)
    {
        _rb = rb;
        _config = config;
        _transform = transform;
    }

    public void Move(Vector2 direction)
    {
        if (direction != Vector2.zero)
        {
            _rb.linearVelocityX = Mathf.Lerp(_rb.linearVelocityX,
                                             _rb.linearVelocityX + direction.x * _transform.right.x,
                                             Time.fixedDeltaTime * _config.Acceleration);
            _rb.linearVelocityX = Mathf.Clamp(_rb.linearVelocityX, -_config.Speed, _config.Speed);

            if (direction != _previousDirection)
            {
                _transform.rotation = new Quaternion(_transform.rotation.x,
                                                     direction.x > 0 ? (float)Direction.Right : (float)Direction.Left,
                                                     _transform.rotation.z,
                                                     _transform.rotation.w);
                _rb.linearVelocityX = 0;
                _previousDirection = direction;
            }
        }
        else
        {
            _rb.linearVelocityX = Mathf.Lerp(_rb.linearVelocityX,
                                             0f,
                                             Time.fixedDeltaTime * _config.Deceleration);
        }
    }
}
