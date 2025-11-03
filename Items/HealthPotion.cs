using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework1.Items
{
    public class HealthPotion : Potion
    {
        public HealthPotion(string name, int cost, int effectValue) : base(name, cost, effectValue)
        {
        }

        public override void Use(Player character)
        {
            base.Use(character);

            character.Heal(EffectValue);
        }
    }
}
