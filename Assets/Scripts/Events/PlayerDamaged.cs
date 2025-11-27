using UnityEngine;

public class PlayerDamaged : IEvent
{
    private string _description;

    public PlayerDamaged(string description, int remainingLives)
    {
        _description = description;
        RemainingLives = remainingLives;
    }

    public int RemainingLives { get; private set; }

    public string GetDescription()
    {
        return _description;
    }
}
