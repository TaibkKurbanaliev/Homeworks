using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class Spawner : MonoBehaviour 
{
    [SerializeField] private float _spawnDelay;
    [SerializeField] private Collectible _prefab;
    [SerializeField] private List<Transform> _spawnPoints;

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
        yield return new WaitForSeconds(_spawnDelay);
        var newCoin = _coinFactory.Create(_prefab, _spawnPoints[coinIndex].position);
        newCoin.OnCollected += OnCoinCollected;
        _coins[coinIndex] = newCoin;
    }
}
