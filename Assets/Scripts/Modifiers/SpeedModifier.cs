using UnityEngine;

public class SpeedModifier : IGameModifier
{
    private float _multiplier = 1.5f;
    private IMovementService _movementService;

    public SpeedModifier(IMovementService movementService)
    {
        _movementService = movementService;
    }

    public void OnEnterGameplay()
    {
        _movementService.SetMovementMultiplier(_multiplier);
    }

    public void OnExitGameplay()
    {
        _movementService.SetMovementMultiplier(1/_multiplier);
    }

    public void Tick(float deltaTime)
    {
    }
}
