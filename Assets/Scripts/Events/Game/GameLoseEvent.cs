using UnityEngine;

public class GameLoseEvent : GamePausedEvent
{
    public GameLoseEvent(string description) : base(description, true)
    {
    }
}
