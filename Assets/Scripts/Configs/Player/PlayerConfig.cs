using UnityEngine;

[CreateAssetMenu(fileName = "PlayerMovementConfig", menuName = "Scriptable Objects/PlayerMovementConfig")]
public class PlayerConfig : ScriptableObject
{
    [field: SerializeField] public float Health { get; private set; }
    [field: SerializeField] public WalkingConfig WalkingConfig { get; private set; }
}
