using System;
using UnityEngine;

public class StatesData
{
    public Vector3 Velocity;
    public Vector2 MoveInput;
    public Vector2 LookInput;
    public bool IsGrounded;

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
