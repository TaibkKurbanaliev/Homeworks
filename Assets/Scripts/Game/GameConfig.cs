using UnityEngine;

[CreateAssetMenu(fileName = "GameConfig", menuName = "Configs/GameConfig")]
public class GameConfig : ScriptableObject
{
    [field: SerializeField] public float AllPlayersWaitingTime { get; private set; }
    [field: SerializeField] public float StartGameWaitingTime { get; private set; }
}
