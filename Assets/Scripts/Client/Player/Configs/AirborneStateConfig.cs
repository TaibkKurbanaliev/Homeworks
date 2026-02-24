using System;
using UnityEngine;

[Serializable]
public class AirborneStateConfig
{
    [field: SerializeField] public float JumpForce { get; private set; }
    [field: SerializeField] public float AirHorizontalSpeed { get; private set; }
}
