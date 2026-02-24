using System;
using UnityEngine;

public interface IInput
{
    event Action Jumped;

    Vector2 Move();
    Vector2 Look();
}
