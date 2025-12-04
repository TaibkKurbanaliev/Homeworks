using UnityEngine;

[CreateAssetMenu(fileName = "EnemyConfig", menuName = "Scriptable Objects/EnemyConfig")]
public class EnemyConfig : ScriptableObject
{
    [field: SerializeField] public float Speed { get; private set; }
    [field: SerializeField] public float Health { get; private set; }
    [field: SerializeField] public float Damage { get; private set; }
    [field: SerializeField] public float AttackRange { get; private set; }
}
