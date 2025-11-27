using UnityEngine;

public class GameInitEvent : IEvent
{
    private string _description;

    public GameInitEvent(string description)
    {
        _description = description;
    }

    public string GetDescription()
    {
        return _description;
    }
}
