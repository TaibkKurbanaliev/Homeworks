using UnityEngine;

public class SwapWeaponEvent : Event
{
    public Sprite Icon { get; private set; }
    public int CurrentBullets { get; private set; }

    public SwapWeaponEvent(Sprite icon, int currentBullets, string description = "SwapWeapon") : base(description)
    {
        Icon = icon;
        CurrentBullets = currentBullets;
    }
}
