using System;
using System.Collections.Generic;
using UnityEngine;

public class EventBus
{
    private Dictionary<Type, List<object>> _subscribers = new();
    
    private static EventBus _instance;
    public static EventBus Instance
    {
        get => _instance ?? (_instance = new EventBus());
    }

    private EventBus() { }

    public void TriggerEvent<T>(T @event) where T : Event
    {
        if (_subscribers.ContainsKey(typeof(T)))
        {
            var copySubscribers = new List<object>(_subscribers[typeof(T)]);
            foreach (var obj in copySubscribers)
            {
                var subscriber = obj as Action<T>;
                subscriber?.Invoke(@event);
            }
        }
    }

    public void AddListener<T>(Action<T> handler) where T : Event
    {
        if (_subscribers.ContainsKey(typeof(T)))
        {
            _subscribers[typeof(T)].Add(handler);
        }
        else
        {
            _subscribers[typeof(T)] = new List<object> { handler };
        }
    }

    public void RemoveListener<T>(Action<T> handler) where T : Event
    {
        if (_subscribers.ContainsKey(typeof(T)))
        {
            _subscribers[typeof(T)].Remove(handler);
        }
        else
        {
            Debug.LogError($"Doesn't contain Event {typeof(T)}");
        }
    }
}
