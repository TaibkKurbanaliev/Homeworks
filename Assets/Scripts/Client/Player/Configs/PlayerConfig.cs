using Mirror;
using System;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerConfig", menuName = "Configs/PlayerConfig")]
public class PlayerConfig : ScriptableObject
{
    [field: SerializeField] public WalkingStateConfig WalkingStateConfig { get; private set; }
    [field: SerializeField] public AirborneStateConfig AirborneStateConfig { get; private set; }
    [field: SerializeField] public DyingStateConfig DyingStateConfig { get; private set; }


    public void Init(StatMediator statMediator)
    {
        AirborneStateConfig.Init(statMediator);
        WalkingStateConfig.Init(statMediator);
    }
}
