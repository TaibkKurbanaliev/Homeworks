using UnityEngine;

public interface IMovementService
{
    void Move(Vector2 targetPoint);
    void SetMovementMultiplier(float multiplier);
}
