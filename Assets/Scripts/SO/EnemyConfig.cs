using UnityEngine;

[CreateAssetMenu(fileName = "EnemyConfig", menuName = "SO/EnemyConfig")] 
public class EnemyConfig : ScriptableObject
{
    [field: SerializeField] public float Speed { get; private set; }
}
