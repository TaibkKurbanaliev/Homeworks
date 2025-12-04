using System;
using UnityEngine;

[Serializable]
public class WalkingConfig
{
    [field: SerializeField] public float Speed;
    [field: SerializeField] public float Acceleration;
    [field: SerializeField] public float Deceleration;
}
