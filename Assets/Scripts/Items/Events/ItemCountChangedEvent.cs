using UnityEngine;

public struct ItemCountChangedEvent : IEvent
{
    public ItemType Item;
    public int Count;
}
