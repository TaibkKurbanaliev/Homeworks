using UnityEngine;

[CreateAssetMenu(fileName = "GrenadeConfig", menuName = "Configs/GrenadeConfig")]
public class GrenadeConfig : ScriptableObject
{
    [field: SerializeField] public float TimeToExplosion { get; private set; }
    [field: SerializeField] public float TimeToDestroy { get; private set; }
    [field: SerializeField] public float ThrowForce { get; private set; }
    [field: SerializeField] public float ExplosionRadius { get; private set; }
    [field: SerializeField] public float Damage { get; private set; }

}
