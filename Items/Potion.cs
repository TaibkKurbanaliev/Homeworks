namespace Homework1.Items
{
    public abstract class Potion : Item, IEquipable
    {
        public bool IsUsed = false;

        protected Potion(string name, int cost, int effectValue) : base(name, cost)
        {
            EffectValue = effectValue;
        }

        protected int EffectValue { get; private set; }

        public void Equip(Player player)
        {
            player.EquipPotion(this);
        }

        public void UnEquip(Player player)
        {
            player.UnequipPotion(this);
        }

        public override void Use(Player character)
        {
            if (IsUsed)
                throw new Exception("The Poison was used!");

            IsUsed = true;
        }
    }
}
