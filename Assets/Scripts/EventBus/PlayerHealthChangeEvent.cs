using UnityEngine;

public class PlayerHealthChangeEvent : Event
{   
    public float Health { get; private set; }

    public PlayerHealthChangeEvent(string description, float health) : base(description)
    {
        Health = health;
    }
}
