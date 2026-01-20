using UnityEngine;

public class SpeedModifier : IGameModifier
{
    private SpeedModifierConfig _config;
    private IMovementService _movementService;

    public SpeedModifier(IMovementService movementService, SpeedModifierConfig config)
    {
        _movementService = movementService;
        _config = config;
    }

    public void OnEnterGameplay()
    {
        _movementService.SetMovementMultiplier(_config.SpeedMultiplier);
    }

    public void OnExitGameplay()
    {
        _movementService.SetMovementMultiplier(1 / _config.SpeedMultiplier);
    }

    public void Tick(float deltaTime)
    {
    }
}
