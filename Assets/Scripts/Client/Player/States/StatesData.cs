using System;
using UnityEngine;

public class StatesData
{
    public Vector2 Input;

    private float _speed;
    public float Speed
    {
        get => _speed; 
        set
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException($"The field {nameof(_speed)} mustn't be negative");

            _speed = value;
        }
    }
}
