using Mirror;
using System;
using System.Collections.Generic;
using UniExtension;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemFactory", menuName = "Factory/ItemFactory")]
public class ItemFactory : ScriptableObject
{
    [SerializeField] private Heal _healPrefab;
    [SerializeField] private Grenade _granadePrefab;
    private List<Transform> _spawnPoints;

    public void Init(List<Transform> spawnPoints)
    {
        _spawnPoints = spawnPoints;
    }

    [Server]
    public PickupItem Get(ItemType type)
    {
        var point = _spawnPoints.GetRandomElement();

        switch (type)
        {
            case ItemType.Heal:
                var heal = Instantiate(_healPrefab, point.position, Quaternion.identity);
                return heal;
            case ItemType.Grenade:
                var granade = Instantiate(_granadePrefab, point.position, Quaternion.identity);
                return granade;
            default:
                throw new NotImplementedException();
        }
    }
}
