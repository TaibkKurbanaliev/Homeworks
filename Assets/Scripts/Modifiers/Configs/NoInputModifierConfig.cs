using UnityEngine;

[CreateAssetMenu(fileName = "NoInputModifierConfig", menuName = "ModifierConfig/NoInputModifierConfig")]
public class NoInputModifierConfig : ScriptableObject
{
    [field: SerializeField] public float NotWorkingTime { get; private set; }
    [field: SerializeField] public float ReloadTime { get; private set; }
}
