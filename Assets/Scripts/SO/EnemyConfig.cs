using UnityEngine;

public enum StrategyType { MoveToTarget, Patrol, }

[CreateAssetMenu(fileName = "EnemyConfig", menuName = "SO/EnemyConfig")] 
public class EnemyConfig : ScriptableObject
{
    [field: SerializeField] public float Speed { get; private set; }
    [field: SerializeField] public StrategyType Type { get; private set; } 
}
