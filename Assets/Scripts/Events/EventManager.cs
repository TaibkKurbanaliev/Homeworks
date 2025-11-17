using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public delegate void GameEventHandler(GameEvent e);

public class EventManager 
{
    public event GameEventHandler OnGameEvent;

    private List<GameEvent> _events = new();

    public void TriggerEvent(GameEvent e)
    {
        OnGameEvent?.Invoke(e);
        _events.Add(e);
    }

    public List<GameEvent> FindAllBattleStartEvents()
    {
        var battleStartsEvents = _events.Where(e => e.Type == EventType.BattleStart).ToList();
        return battleStartsEvents;
    }

    public int CountPlayerItemPicks()
    {
        return _events.Count(e => e.Type == EventType.ItemPicked);
    }

    public EventType? ShowMostFrequentEvent()
    {
        if (_events == null || _events.Count == 0) 
            return null;

        return _events.GroupBy(e => e.Type).OrderByDescending(type => type.Count()).First()?.Key;
    }

    public List<GameEvent> LastFiveEventsPeriodTime()
    {
        return _events.TakeLast(5).ToList();
    }
}
