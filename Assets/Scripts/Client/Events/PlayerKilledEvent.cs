using UnityEngine;

public struct PlayerKilledEvent : IEvent
{
    public Player Killer;
}
