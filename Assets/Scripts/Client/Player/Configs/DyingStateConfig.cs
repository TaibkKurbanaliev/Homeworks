using System;
using UnityEngine;

[Serializable]
public class DyingStateConfig
{
    [field: SerializeField] public float SpectatorSpeed { get; private set; }
    [field: SerializeField] public float Sensetive { get; private set; }
}
