using UnityEngine;

public class FallingState : AirborneState
{
    public FallingState(Player player, IStateSwitcher stateSwitcher, StatesData statesData, AirborneStateConfig config) : base(player, stateSwitcher, statesData, config)
    {
    }
}
