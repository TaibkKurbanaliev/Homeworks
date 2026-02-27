using System;
using UnityEngine;

public interface IInput
{
    event Action Jumped;
    event Action TabOpenned;
    event Action TabClosed;

    Vector2 Move();
    Vector2 Look();
    bool IsFire();
}
