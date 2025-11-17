using System;
using UnityEngine;


public enum EventType { BattleStart, Died, StartChasing, ItemPicked, WeatherChanged, EnemySpotted }

public class GameEvent
{
    public GameEvent(EventType type, DateTime time, string desctiption)
    {
        Type = type;
        Time = time;
        Desctiption = desctiption;
    }

    public EventType Type {  get; private set; }
    public DateTime Time { get; private set; }
    public string Desctiption { get; private set; }
}
