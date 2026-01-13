using UnityEngine;

public class RigidbodyMovement : IMovementService
{
    private Rigidbody _rigidbody;

    public RigidbodyMovement(Rigidbody rigidbody)
    {
        _rigidbody = rigidbody;
    }

    public void Move(Vector2 direction)
    {
        _rigidbody.AddForce(new Vector3(direction.x, 0f, direction.y), ForceMode.VelocityChange);
    }
}
