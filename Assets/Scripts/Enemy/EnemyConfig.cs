using UnityEngine;

[CreateAssetMenu(fileName = "EnemyConfig", menuName = "Enemy/EnemyConfig")]
public class EnemyConfig : ScriptableObject
{
    [field: SerializeField] public float MoveSpeed { get; private set; }
    [field: SerializeField] public float Acceleration { get; private set; }
    [field: SerializeField] public float TrackingDistance { get; private set; }
    [field: SerializeField] public float AttackRange { get; private set; }
    [field: SerializeField] public float AttackSpeed { get; private set; }
    [field: SerializeField] public float Damage { get; private set; }
    [field: SerializeField] public float Health { get; private set; }
    [field: SerializeField] public LayerMask TargetMask { get; private set; }
}
