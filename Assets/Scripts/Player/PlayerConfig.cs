using UnityEngine;

[CreateAssetMenu(fileName = "PlayerMovementConfig", menuName = "Player/PlayerMovementConfig")]
public class PlayerConfig : ScriptableObject
{
    [field: SerializeField] public float Acceleration { get; private set; } 
    [field: SerializeField] public float Deceleration { get; private set; } 
    [field: SerializeField] public float Speed { get; private set; }
    [field: SerializeField] public float Health { get; private set; }
    [field: SerializeField] public float Damage { get; private set; }
    [field: SerializeField] public float AttackRange { get; private set; }
    [field: SerializeField] public float AttackTime { get; private set; }
}
