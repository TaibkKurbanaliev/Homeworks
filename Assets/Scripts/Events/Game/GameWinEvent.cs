using UnityEngine;

public class GameWinEvent : GamePausedEvent
{
    public GameWinEvent(string description) : base(description, true)
    {
    }
}
