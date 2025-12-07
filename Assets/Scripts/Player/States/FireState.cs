using UnityEngine;

public abstract class FireState : MovementState
{
    public FireState(Player player, IStateSwitcher switcher) : base(player, switcher)
    {
    }

    public override void Update()
    {
        base.Update();

        if (Player.Input.Player.Attack.IsInProgress())
        {
            Player.CurrentWeapon.Shoot();
        }    
    }
}
