using System;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class Health : IHealth
{
    public event Action Died;
    public event Action<float> HealthChanged;

    private float _currentHealth;
    private float _maxHealth;
    private ILoggerService _logger;

    public Health(float currentHealth, ILoggerService logger)
    {
        if (currentHealth <= 0)
            throw new ArgumentException();

        _currentHealth = currentHealth;
        _maxHealth = currentHealth;
        _logger = logger;
    }


    public bool IsDied() => _currentHealth <= 0;

    public void ReduceHealth(float value)
    {
        if (IsDied() || value <= 0)
            return;

        _currentHealth -= value;
        HealthChanged?.Invoke(_currentHealth);
        _logger.Log("Take Damage. Current health - " + _currentHealth);

        if (IsDied())
        {
            _logger.Log("Died");
            Died?.Invoke();
        }
    }

    public void RestoreHealth(float value)
    {
        if (IsDied() || value <= 0)
            return;


        _currentHealth = Mathf.Clamp(_currentHealth + value, 0, _maxHealth);
        HealthChanged?.Invoke(_currentHealth);
        _logger.Log("Restore Health. Current health - " + _currentHealth);
    }

    public void Reset()
    {
        _currentHealth = _maxHealth;
        HealthChanged?.Invoke(_currentHealth);
    }
}
