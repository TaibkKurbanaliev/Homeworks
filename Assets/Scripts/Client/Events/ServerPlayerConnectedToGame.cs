using System;
using UnityEngine;

[Serializable]
public struct ServerPlayerConnectedToGame : IEvent
{
    public InstanceInfo InstanceInfo;
    public PlayerInfo PlayerInfo;
}
