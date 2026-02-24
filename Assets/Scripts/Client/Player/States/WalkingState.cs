using UnityEngine;

public class WalkingState : MovementState
{
    private WalkingStateConfig _cfg;

    public WalkingState(Player player, IStateSwitcher stateSwitcher, StatesData statesData, WalkingStateConfig cfg) 
        : base(player, stateSwitcher, statesData)
    {
        _cfg = cfg;
    }

    public override void Enter()
    {
        base.Enter();

        Data.Speed = _cfg.Speed;
        Player.Rigidbody.linearDamping = _cfg.Drag;
    }
}
