using System;
using UnityEngine;

[Serializable]
public class PlayStateConfig
{
    [field: SerializeField] public float RespawnDelay { get; private set; }
    [field: SerializeField] public float ItemsRespawnDelay { get; private set; }
}
