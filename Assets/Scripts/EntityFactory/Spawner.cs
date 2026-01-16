using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class Spawner : MonoBehaviour, IPauseEntity
{
    [SerializeField] private float _spawnDelay;
    [SerializeField] private Collectible _prefab;
    [SerializeField] private List<Transform> _spawnPoints;

    private bool _isPaused = false;
    private IEntityFactory<Collectible> _coinFactory;
    private List<Collectible> _coins = new();

    public void Construct(IEntityFactory<Collectible> coinFactory)
    {
        _coinFactory = coinFactory;

        for (int i = 0; i < _spawnPoints.Count; i++)
        {
            var newCoin = _coinFactory.Create(_prefab, _spawnPoints[i].position);
            newCoin.OnCollected += OnCoinCollected;
            _coins.Add(newCoin);
        }
    }

    private void OnDisable()
    {
        foreach (var coin in _coins)
            coin.OnCollected -= OnCoinCollected;
    }

    private void OnCoinCollected(Collectible coin)
    {
        coin.OnCollected -= OnCoinCollected;
        var index = _coins.IndexOf(coin);
        StartCoroutine(SpawnCoin(index));
    }

    public IEnumerator SpawnCoin(int coinIndex)
    {
        var currentSpawnTime = 0f;

        while (currentSpawnTime < _spawnDelay)
        {
            if (!_isPaused)
                currentSpawnTime += Time.deltaTime;

            yield return null;
        }

        var newCoin = _coinFactory.Create(_prefab, _spawnPoints[coinIndex].position);
        newCoin.OnCollected += OnCoinCollected;
        _coins[coinIndex] = newCoin;
    }

    public void Pause(bool isPaused)
    {
        _isPaused = isPaused;
    }
}
