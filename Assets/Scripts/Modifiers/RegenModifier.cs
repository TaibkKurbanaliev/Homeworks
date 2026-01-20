using UnityEngine;

public class RegenModifier : IGameModifier
{
    private RegenModifierConfig _config;
    private IHealth _health;
    private float _timer;

    public RegenModifier(IHealth health, RegenModifierConfig config)
    {
        _health = health;
        _config = config;
    }

    public void OnEnterGameplay()
    {
        _timer = 0f;
    }

    public void OnExitGameplay()
    {
    }

    public void Tick(float deltaTime)
    {
        _timer += deltaTime;

        if (_timer >= _config.Delay)
        {
            Debug.Log("Healed");
            _health.RestoreHealth(_config.RegenValue);
            _timer = 0f;
        }
    }
}
