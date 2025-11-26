using UnityEngine;

public class ItemPickedEvent : IEvent
{
    private string _description;

    public int Score { get; private set; }

    public ItemPickedEvent(int score, string description)
    {
        Score = score;
        _description = description;
    }

    public string GetDescription()
    {
        return _description;
    }
}
