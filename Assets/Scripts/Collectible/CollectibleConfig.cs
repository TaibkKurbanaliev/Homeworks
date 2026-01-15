using UnityEngine;

[CreateAssetMenu(fileName = "CollectibleConfig", menuName = "Scriptable Objects/CollectibleConfig")]
public class CollectibleConfig : ScriptableObject
{
    [field: SerializeField] public float RotationSpeed { get; private set; }
    [field: SerializeField] public int Reward { get; private set; }
}
