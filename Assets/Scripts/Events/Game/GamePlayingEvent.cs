using UnityEngine;

public class GamePlayingEvent : IEvent
{
    private string _description;

    public GamePlayingEvent(string description)
    {
        _description = description;
    }

    public string GetDescription()
    {
        return _description;
    }
}
