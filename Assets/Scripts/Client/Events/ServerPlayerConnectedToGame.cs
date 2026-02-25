using UnityEngine;

public struct ServerPlayerConnectedToGame : IEvent
{
    public InstanceInfo InstanceInfo;
    public PlayerInfo PlayerInfo;
}
