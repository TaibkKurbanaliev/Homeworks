using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class Spawner : MonoBehaviour, IPauseEntity
{
    [SerializeField] private SpawnServiceConfig _config;

    private bool _isPaused = false;
    private IEntityFactory<Collectible> _coinFactory;
    private ICollectibleService _service;
    private List<Collectible> _coins = new();

    public void Construct(IEntityFactory<Collectible> coinFactory, ICollectibleService collectible)
    {
        _coinFactory = coinFactory;
        _service = collectible;

        foreach (var coin in _coins)
            coin.OnCollected -= OnCoinCollected;
        
        _coins.Clear();

        for (int i = 0; i < _config.SpawnPoints.Count; i++)
        {
            var newCoin = _coinFactory.Create(_config.Prefab, _config.SpawnPoints[i]);
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
        _service.Collect();
        StartCoroutine(SpawnCoin(index));
    }

    public IEnumerator SpawnCoin(int coinIndex)
    {
        var currentSpawnTime = 0f;

        while (currentSpawnTime < _config.SpawnDelay)
        {
            if (!_isPaused)
                currentSpawnTime += Time.deltaTime;

            yield return null;
        }

        var newCoin = _coinFactory.Create(_config.Prefab, _config.SpawnPoints[coinIndex]);
        newCoin.OnCollected += OnCoinCollected;
        _coins[coinIndex] = newCoin;
    }

    public void Pause(bool isPaused)
    {
        _isPaused = isPaused;
    }
}
