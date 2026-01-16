using System;
using UnityEngine;

public interface IHealth
{
    event Action Died;
    event Action<float> HealthChanged;

    void Reset();
    void ReduceHealth(float value);
    void RestoreHealth(float value);
    bool IsDied();
}
