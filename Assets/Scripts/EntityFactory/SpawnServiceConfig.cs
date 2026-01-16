using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpawnServiceConfig", menuName = "Scriptable Objects/SpawnServiceConfig")]
public class SpawnServiceConfig : ScriptableObject
{
    [field: SerializeField] public float SpawnDelay { get; private set; }
    [field: SerializeField] public List<Vector3> SpawnPoints { get; private set; }
    [field: SerializeField] public Collectible Prefab { get; private set; }
}
