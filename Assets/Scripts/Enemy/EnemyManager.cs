using System;
using UnityEngine;

public class EnemyManager : IDisposable
{
    private Enemy _prefab;
    private Transform _spawnPoint;
    private EventManager _eventManager;
    private Player _target;

    public EnemyManager(Enemy prefab, Transform spawnPoint, EventManager eventManager, Player target)
    {
        _prefab = prefab;
        _spawnPoint = spawnPoint;
        _eventManager = eventManager;
        _target = target;
        _eventManager.OnGameEvent += HandleEvent;
    }

    private void HandleEvent(GameEvent e)
    {
        if (e.Type == EventType.ItemPicked)
        {
            var enemy = GameObject.Instantiate(_prefab, _spawnPoint.position, Quaternion.identity);
            enemy.Init(_target, _eventManager);
            enemy.gameObject.SetActive(true);
            _eventManager.TriggerEvent(new GameEvent(EventType.EnemySpotted, DateTime.Now, $"{_prefab.name} spotted!"));
        }   
    }

    public void Dispose()
    {
        _eventManager.OnGameEvent -= HandleEvent;
    }
}
