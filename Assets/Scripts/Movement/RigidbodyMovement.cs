using UnityEngine;

public class RigidbodyMovement : IMovementService
{
    private Rigidbody _rigidbody;
    private float _movementMultiplier = 1f;

    public RigidbodyMovement(Rigidbody rigidbody)
    {
        _rigidbody = rigidbody;
    }

    public void Move(Vector2 direction)
    {
        _rigidbody.AddForce(new Vector3(direction.x, 0f, direction.y) * _movementMultiplier, ForceMode.VelocityChange);
    }

    public void SetMovementMultiplier(float multiplier)
    {
        _movementMultiplier = multiplier;
    }
}
