using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnService : IDisposable
{
    private int _spawnDelay;
    private Enemy _prefab;
    private List<Transform> _spawnPoints;
    private Player _target;

    private bool _isSpawningStopped = false;

    public SpawnService(int spawnDelay, Enemy prefab, List<Transform> spawnPoints, Player target)
    {
        _spawnDelay = spawnDelay;
        _prefab = prefab;
        _spawnPoints = spawnPoints;
        _target = target;
        EventBus.Instance.OnGameEvent += HandleEvent;
    }

    public void Dispose()
    {
        EventBus.Instance.OnGameEvent -= HandleEvent;
    }

    public async Task SpawnEnemyAsync(CancellationToken token = default)
    {
        while (!token.IsCancellationRequested)
        {
            if (_isSpawningStopped)
            {
                await Task.Yield();
                continue;
            }
                
            var newSpawnPoint = _spawnPoints[Random.Range(0, _spawnPoints.Count)];
            var enemy = GameObject.Instantiate(_prefab, newSpawnPoint.position, Quaternion.identity);
            enemy.Init();
            enemy.SetMoveStrategy(new ChaseStrategy(_target, enemy.GetComponent<Rigidbody2D>(), enemy.Cfg.Speed));
            EventBus.Instance.TriggerEvent(new EnemySpawnedEvent($"{nameof(enemy)} was spawned!!!"));
            await Task.Delay(_spawnDelay, token);
        }
    }

    private void HandleEvent(IEvent @event)
    {
        if (@event is GamePausedEvent pause)
        {
            _isSpawningStopped = pause.IsPaused;
        }
    }
}
