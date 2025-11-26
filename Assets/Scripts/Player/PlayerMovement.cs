using UnityEngine;

public class PlayerMovement 
{
    private Rigidbody2D _rb;
    private float _speed;

    public PlayerMovement(Rigidbody2D rb, float speed)
    {
        _rb = rb;
        _speed = speed;
    }

    public void Move(Vector2 Input)
    {
        _rb.linearVelocity = Input * _speed;
    }
}
