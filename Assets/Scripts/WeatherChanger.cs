using System;
using UnityEngine;

public class WeatherChanger : IDisposable
{
    private GameObject _dayBackground;
    private GameObject _nightBackground;
    private EventManager _eventManager;

    public WeatherChanger(EventManager eventManager, GameObject dayBackground, GameObject nightBackground)
    {
        _eventManager = eventManager;
        _eventManager.OnGameEvent += HandleEvent;
        _dayBackground = dayBackground;
        _nightBackground = nightBackground;
    }

    public void Dispose()
    {
        _eventManager.OnGameEvent -= HandleEvent;
    }

    private void HandleEvent(GameEvent e)
    {
        if (e.Type == EventType.Died)
        {
            _dayBackground.SetActive(true);
            _nightBackground.SetActive(false);
            _eventManager.TriggerEvent(new GameEvent(EventType.WeatherChanged, DateTime.Now, "Weather changed."));
        }
    } 
}
