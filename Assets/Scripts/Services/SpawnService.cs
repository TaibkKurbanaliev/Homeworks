using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnService : IDisposable
{
    private int _spawnDelay;
    private List<EnemyConfig> _configs;
    private Enemy _prefab;
    private List<Transform> _spawnPoints;
    private Player _target;

    private bool _isSpawningStopped = false;

    public SpawnService(int spawnDelay, Enemy prefab, List<Transform> spawnPoints, Player target, List<EnemyConfig> configs)
    {
        _spawnDelay = spawnDelay;
        _prefab = prefab;
        _spawnPoints = spawnPoints;
        _target = target;
        EventBus.Instance.OnGameEvent += HandleEvent;
        _configs = configs;
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

            SetupEnemy(newSpawnPoint, enemy);

            EventBus.Instance.TriggerEvent(new EnemySpawnedEvent($"{nameof(enemy)} was spawned!!!"));
            await Task.Delay(_spawnDelay, token);
        }
    }

    private void SetupEnemy(Transform newSpawnPoint, Enemy enemy)
    {
        var randomCFG = _configs[Random.Range(0, _configs.Count)];

        if (randomCFG.Type == StrategyType.MoveToTarget)
        {
            enemy.SetMoveStrategy(new ChaseStrategy(_target, enemy.GetComponent<Rigidbody2D>(), randomCFG.Speed));
        }
        else
        {
            var startPoint = newSpawnPoint;
            Transform endPoint = _spawnPoints.Count == 1 ? startPoint : null;

            while (endPoint == null)
            {
                var randomPoint = _spawnPoints[Random.Range(0, _spawnPoints.Count)];

                if (randomPoint != startPoint)
                    endPoint = randomPoint;
            }

            enemy.SetMoveStrategy(new PatrolStrategy(startPoint, endPoint, randomCFG.Speed, enemy.GetComponent<Rigidbody2D>()));
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
