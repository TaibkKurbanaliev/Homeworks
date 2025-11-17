using System;
using UnityEngine;

public class Health
{
    public event Action Died;

    private float _healthValue;

    public float HealthValue
    {
        get { return _healthValue; }
        set
        {
            if (value <= 0) 
                throw new ArgumentOutOfRangeException(nameof(value));

            _healthValue = value;
        }
    }

    public Health(float value)
    {
        HealthValue = value;
    }

    public void ReduceHealth(float value)
    {
        if (value <= 0 || _healthValue <= 0)
            return;

        _healthValue -= value;

        if (_healthValue <= 0)
            Died?.Invoke();
    }
}
