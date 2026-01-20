using UnityEngine;

public class RegenModifier : IGameModifier
{
    private float _delay = 1f;
    private float _regenValue = 1f;

    private float _timer;

    private IHealth _health;

    public RegenModifier(IHealth health)
    {
        _health = health;
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

        if (_timer >= _delay)
        {
            Debug.Log("Healed");
            _health.RestoreHealth(_regenValue);
            _timer = 0f;
        }
    }
}
