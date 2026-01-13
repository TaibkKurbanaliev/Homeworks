using System;
using UnityEngine;

public class Health : IHealth
{
    private float _currentHealth;
    private ILoggerService _logger;

    public Health(float currentHealth, ILoggerService logger)
    {
        if (currentHealth <= 0)
            throw new ArgumentException();

        _currentHealth = currentHealth;
        _logger = logger;
    }

    public bool IsDied() => _currentHealth <= 0;

    public void ReduceHealth(float value)
    {
        if (IsDied() || value <= 0)
            return;

        _currentHealth -= value;
        _logger.Log("Take Damage. Current health - " + _currentHealth);

        if (IsDied())
            _logger.Log("Died");
    }

    public void RestoreHealth(float value)
    {
        if (IsDied() || value <= 0)
            return;

        _currentHealth += value;
        _logger.Log("Restore Health. Current health - " + _currentHealth);
    }
}
