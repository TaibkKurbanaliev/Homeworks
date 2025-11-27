using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public delegate void OnGameEvent(IEvent @event);

public class EventBus 
{
    public event OnGameEvent OnGameEvent;
    private List<IEvent> _events = new();

    private static EventBus _instance;
    public static EventBus Instance
    {
        get => _instance ?? (_instance = new EventBus());
    }
    public IReadOnlyCollection<IEvent> Events => _events;

    private EventBus(){}

    public void TriggerEvent(IEvent @event)
    {
        _events.Add(@event);
        OnGameEvent?.Invoke(@event);
    }

    public int GetNumberOfEvents<T>()
    {
        return _events.Count(ev => ev is T);
    }
}
