using UnityEngine;

public abstract class Event
{   
    public string Description { get; private set; }

    protected Event(string description)
    {
        Description = description;
    }
}
