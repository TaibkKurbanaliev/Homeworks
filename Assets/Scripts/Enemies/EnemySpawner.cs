using System.Collections.Generic;
using System.Linq;
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
    private List<Enemy> _enemies = new();

    public void Init(Player target)
    {
        _target = target;

        for (int i = 0; i < _numberOfEnemiesPerRound * 2; i++)
        {
            var zombiePrefab = Random.Range(0f, 100f) < _heavyChance ? _heavyZombiePrefab : _fastZombiePrefab;

            var enemy = Instantiate(zombiePrefab, _spawnPoints[Random.Range(0, _spawnPoints.Count)].position, Quaternion.identity);
            enemy.Init(_target);
            enemy.gameObject.SetActive(false);
            _enemies.Add(enemy);
        }
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
        _enemies.Shuffle();
        await Task.Delay((int)(_waveDelay * 1000f), _cts.Token);
        _currentNumberOfEnemies = 0;

        while (!token.IsCancellationRequested )
        {
            _currentNumberOfEnemies++;
            var enemy = _enemies.FirstOrDefault(e => !e.gameObject.activeSelf);
            enemy.Respawn(_spawnPoints[Random.Range(0, _spawnPoints.Count)].position);
            enemy.gameObject.SetActive(true);

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

public static class ListExtensions
{
    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}