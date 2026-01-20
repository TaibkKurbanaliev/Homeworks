using UnityEngine;

[CreateAssetMenu(fileName = "RegenModifierConfig", menuName = "ModifierConfig/RegenModifierConfig")]
public class RegenModifierConfig : ScriptableObject
{
    [field: SerializeField] public float Delay { get; private set; }
    [field: SerializeField] public float RegenValue { get; private set; }
}
