using System;
using UnityEngine;

public class StatesData
{
    public bool IsGrounded;
    public Vector2 Input;

    private float _horizontalSpeed;
    public float HorizontalSpeed
    {
        get => _horizontalSpeed; 
        set
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException($"The field {nameof(_horizontalSpeed)} mustn't be negative");

            _horizontalSpeed = value;
        }
    }
}
