using UnityEngine;

public class BulletsAmountChangeEvent : Event
{
    public int NumberOfBullets {  get; private set; }

    public BulletsAmountChangeEvent(int numberOfBullets ,string description = "") : base(description)
    {
        NumberOfBullets = numberOfBullets;
    }
}
