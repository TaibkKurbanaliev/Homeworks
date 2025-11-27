using UnityEngine;

public class GamePausedEvent : IEvent
{
    private string _description;

    public GamePausedEvent(string description, bool paused)
    {
        _description = description;
        IsPaused = paused;
    }

    public bool IsPaused {  get; private set; }

    public string GetDescription()
    {
        return _description;
    }
}
