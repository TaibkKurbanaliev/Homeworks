using UnityEngine;

[CreateAssetMenu(fileName = "SpeedModifierConfig", menuName = "ModifierConfig/SpeedModifierConfig")]
public class SpeedModifierConfig : ScriptableObject
{
    [field: SerializeField] public float SpeedMultiplier { get; private set; }
}
