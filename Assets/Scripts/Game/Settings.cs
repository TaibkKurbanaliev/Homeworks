using UnityEngine;

[CreateAssetMenu(fileName = "Settings", menuName = "Scriptable Objects/Settings")]
public class Settings : ScriptableObject
{
    [field: SerializeField] public float MouseSensetive { get; private set; }
    [field: SerializeField] public bool IsSoundOn { get; private set; } = true;
    [field: SerializeField] public float Volume { get; private set; }
}
