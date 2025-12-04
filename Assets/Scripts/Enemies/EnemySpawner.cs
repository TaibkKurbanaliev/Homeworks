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
    [SerializeField] private int _numberOfZombiesPerRound;

    private int _currentNumber = 0;
    private Player _target; 

    public void Init(Player target)
    {
        _target = target;   
    }

    private void Start()
    {
        _ = StartSpawn(destroyCancellationToken);
    }

    private async Task StartSpawn(CancellationToken token = default)
    {
        while (!token.IsCancellationRequested)
        {
            var zombiePrefab = Random.Range(0f, 100f) < _heavyChance ? _heavyZombiePrefab : _fastZombiePrefab;

            var enemy = Instantiate(zombiePrefab, _spawnPoints[Random.Range(0, _spawnPoints.Count)].position, Quaternion.identity);
            enemy.Init(_target);
            _currentNumber++;

            await Task.Delay((int)(_spawnDelay * 1000), token);
            if (_currentNumber == _numberOfZombiesPerRound)
            {
                _currentNumber = 0;
                await Task.Delay((int)(_waveDelay * 1000), token);
            }
        }
    }
    
}
