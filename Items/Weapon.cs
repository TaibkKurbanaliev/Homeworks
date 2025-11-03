using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework1.Items
{
    public class Weapon : Item, IEquipable
    {
        public Weapon(string name, int cost, int damage) : base(name, cost)
        {
            Damage = damage;
        }

        public int Damage { get; private set; }

        public void Equip(Player player)
        {
            player.EquipWeapon(this);
        }

        public void UnEquip(Player player)
        {
            player.UnequipWeapon(this);
        }

        public override void Use(Player character)
        {
        }
    }
}
