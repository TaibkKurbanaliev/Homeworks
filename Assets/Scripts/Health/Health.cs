using System;
using UnityEngine;

public class Health : IHealth
{
    private float _currentHealth;

    public Health(float currentHealth)
    {
        if (currentHealth <= 0)
            throw new ArgumentException();

        _currentHealth = currentHealth;
    }

    public bool IsDied() => _currentHealth <= 0;

    public void ReduceHealth(float value)
    {
        if (IsDied() || value <= 0)
            return;

        _currentHealth -= value;
    }

    public void RestoreHealth(float value)
    {
        if (IsDied() || value <= 0)
            return;

        _currentHealth += value;
    }
}
