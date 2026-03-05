using UnityEngine;

[CreateAssetMenu(fileName = "WeaponConfig", menuName = "Configs/WeaponConfig")]
public class WeaponConfig : ScriptableObject
{
    [field: SerializeField] public float FireRate { get; private set; }
    [field: SerializeField] public float Damage { get; private set; }
    [field: SerializeField] public float ReloadTime { get; private set; }
    [field: SerializeField] public int NumberOfBullets { get; private set; }
}
