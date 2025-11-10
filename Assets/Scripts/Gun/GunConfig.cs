using UnityEngine;

[CreateAssetMenu(fileName = "GunConfig", menuName = "Gun/GunConfig")]
public class GunConfig : ScriptableObject
{
    [field: SerializeField] public float ProjectileSpeed { get; private set; }
    [field: SerializeField] public float RealoadTime { get; private set; }
    [field: SerializeField] public float Damage { get; private set; }
}
