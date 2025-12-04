using UnityEngine;

[CreateAssetMenu(fileName = "BulletConfig", menuName = "Scriptable Objects/BulletConfig")]
public class BulletConfig : ScriptableObject
{
    [field: SerializeField] public float Speed { get; private set; }
    [field: SerializeField] public float LifeTime { get; private set; }
    [field: SerializeField] public GameObject BulletPrefab { get; private set; }
}
