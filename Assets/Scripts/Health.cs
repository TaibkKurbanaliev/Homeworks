using System;
using UnityEngine;

public class Health
{
    public event Action OnDied;

    public Health(float maxHealth)
    {
        MaxHealth = maxHealth;
        Init();
    }


    public float MaxHealth { get; private set; }
    public float CurrentHealth { get; private set; }
    public float HealthPercent => IsAlive ? (CurrentHealth / MaxHealth) * 100f : 0f;

    public bool IsAlive => CurrentHealth > 0;

    public void Init()
    {
        CurrentHealth = MaxHealth;
    }

    public void ReduceHealth(float value)
    {
        if (value <= 0) 
            throw new ArgumentException(nameof(value));
        if (!IsAlive)
            return;

        CurrentHealth -= value;

        if (!IsAlive)
            OnDied?.Invoke();
    }
}
