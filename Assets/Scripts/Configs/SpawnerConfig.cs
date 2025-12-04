using UnityEngine;

[CreateAssetMenu(fileName = "SpawnerConfig", menuName = "Scriptable Objects/SpawnerConfig")]
public class SpawnerConfig : ScriptableObject
{
    [field: SerializeField] public float SpawnDelay { get; private set; }
    [field: SerializeField] public float SpawnBetweenRoundDelay { get; private set; }
    [field: SerializeField] public float NumberOf { get; private set; }
}
