using UnityEngine;

[CreateAssetMenu(fileName = "WeaponConfig", menuName = "Scriptable Objects/WeaponConfig")]
public class WeaponConfig : ScriptableObject
{
    [field: SerializeField] public float Damage { get; private set; }
    [field: SerializeField, Range(0f, 1f)] public float Spread { get; private set; }
    [field: SerializeField, Range(1, 60)] public int BulletsPerSecond { get; private set; }
    [field: SerializeField, Range(1, 35)] public int BulletStore { get; private set; }
}
