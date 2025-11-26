using UnityEngine;

[CreateAssetMenu(fileName = "GameConfig", menuName = "SO/GameConfig")]
public class GameConfig : ScriptableObject
{
    [field: SerializeField] public float PlayerSpeed {  get; private set; }
    [field: SerializeField] public int SpawnFrequencyInMiliseconds {  get; private set; }
    [field: SerializeField] public int TargetScore {  get; private set; }
}
