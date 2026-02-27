using System;
using UnityEngine;

[Serializable]
public struct ServerPlayerConnectedToGame : IEvent
{
    public uint NetID;
    public InstanceInfo InstanceInfo;
    public PlayerInfo PlayerInfo;
}
