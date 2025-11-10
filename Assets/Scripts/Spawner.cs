using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    public event Action TargetSpawned;
    public event Action TargetDestroyed;

    [SerializeField] private Target _targetObject;
    [SerializeField] private float _timeBetweenSpawning;
    [SerializeField] private bool _canSpawning = true;
    [SerializeField] private float _spawnSpread = 1f;

    private void Start()
    {
        StartCoroutine(SpawnObject());
    }

    private IEnumerator SpawnObject()
    {
        while (_canSpawning)
        {
            var spawnPosition = new Vector3( Random.Range(-_spawnSpread, _spawnSpread), 
                                        Random.Range(0, _spawnSpread), 
                                        Random.Range(-_spawnSpread, _spawnSpread));

            var instance = Instantiate(_targetObject, spawnPosition, _targetObject.transform.rotation);
            var behaviours = (LifeTimeBehaviour[])Enum.GetValues(typeof(LifeTimeBehaviour));
            instance.Init(behaviours[Random.Range(0, behaviours.Length)]);
            instance.Hitted += OnTargetHit;

            TargetSpawned?.Invoke();
            yield return new WaitForSeconds(_timeBetweenSpawning);
        }
    }

    private void OnTargetHit()
    {
        TargetDestroyed?.Invoke();
    }
}
