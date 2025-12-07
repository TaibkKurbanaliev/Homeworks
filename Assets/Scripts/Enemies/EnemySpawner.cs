using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<Transform> _spawnPoints;
    [SerializeField] private Enemy _fastZombiePrefab;
    [SerializeField] private Enemy _heavyZombiePrefab;
    [SerializeField] private float _spawnDelay;
    [SerializeField] private float _waveDelay;
    [SerializeField] private float _heavyChance;
    [SerializeField] private int _numberOfEnemiesPerRound;

    private int _currentNumberOfEnemies = 0;
    private int _numberOfKilledEnemies = 0;
    private Player _target; 
    private CancellationTokenSource _cts;

    public void Init(Player target)
    {
        _target = target;   
    }

    private void OnEnable()
    {
        EventBus.Instance.AddListener<NewWaveEvent>(OnNewWaveSpawned);
        EventBus.Instance.AddListener<EnemyDiedEvent>(OnEnemyDied);
        EventBus.Instance.AddListener<LoseEvent>(OnLose);
        _cts = new CancellationTokenSource();
    }

    private void OnDisable()
    {
        EventBus.Instance.RemoveListener<NewWaveEvent>(OnNewWaveSpawned);
        EventBus.Instance.RemoveListener<EnemyDiedEvent>(OnEnemyDied);
        EventBus.Instance.RemoveListener<LoseEvent>(OnLose);
        _cts.Cancel();
    }

    private async Task StartSpawn(CancellationToken token = default)
    {
        await Task.Delay((int)(_waveDelay * 1000f), _cts.Token);
        _currentNumberOfEnemies = 0;

        while (!token.IsCancellationRequested )
        {
            var zombiePrefab = Random.Range(0f, 100f) < _heavyChance ? _heavyZombiePrefab : _fastZombiePrefab;

            var enemy = Instantiate(zombiePrefab, _spawnPoints[Random.Range(0, _spawnPoints.Count)].position, Quaternion.identity);
            enemy.Init(_target);
            _currentNumberOfEnemies++;

            await Task.Delay((int)(_spawnDelay * 1000), token);

            if (_currentNumberOfEnemies == _numberOfEnemiesPerRound)
                break;
        }
    }
    private void OnNewWaveSpawned(NewWaveEvent @event)
    {
        _ = StartSpawn(_cts.Token);
    }

    private void OnLose(LoseEvent _)
    {
        _cts.Cancel();
    }

    private void OnEnemyDied(EnemyDiedEvent _)
    {
        _numberOfKilledEnemies++;

        if (_numberOfKilledEnemies == _numberOfEnemiesPerRound)
        {
            EventBus.Instance.TriggerEvent(new AllEnemiesDiedEvent());
            _numberOfKilledEnemies = 0;
        }
            
    }
}
