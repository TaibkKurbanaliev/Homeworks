using UnityEngine;

[CreateAssetMenu(fileName = "WeaponConfig", menuName = "Scriptable Objects/WeaponConfig")]
public class WeaponConfig : ScriptableObject
{
    [field: SerializeField] public Sprite Icon { get; private set; }
    [field: SerializeField] public AudioClip FireSound { get; private set; }
    [field: SerializeField] public AudioClip ReloadSound { get; private set; }
    [field: SerializeField] public float Damage { get; private set; }
    [field: SerializeField, Range(0f, 5f)] public float ReloadTime { get; private set; }
    [field: SerializeField, Range(0f, 1f)] public float Spread { get; private set; }
    [field: SerializeField, Range(1, 60)] public int BulletsPerSecond { get; private set; }
    [field: SerializeField, Range(1, 35)] public int BulletStore { get; private set; }
}
