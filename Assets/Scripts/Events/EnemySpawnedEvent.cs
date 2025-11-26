using UnityEngine;

public class EnemySpawnedEvent : IEvent
{
    public string _description;

    public EnemySpawnedEvent(string description)
    {
        _description = description;
    }

    public string GetDescription()
    {
        return _description;
    }
}
