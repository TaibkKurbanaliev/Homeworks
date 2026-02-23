using UnityEngine;

[CreateAssetMenu(fileName = "GameConfig", menuName = "Scriptable Objects/GameConfig")]
public class GameConfig : ScriptableObject
{
    [field: SerializeField] public float AllPlayersWaitingTime { get; private set; }
    [field: SerializeField] public float StartGameWaitingTime { get; private set; }
}
