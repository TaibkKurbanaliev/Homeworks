using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework1.Items
{
    public class DamagePotion : Potion
    {
        private int _actionTime;

        public DamagePotion(string name, int cost, int effectValue, int actionTime) : base(name, cost, effectValue)
        {
            _actionTime = actionTime;
        }

        public override void Use(Player player)
        {
            base.Use(player);

            player.AddBuff(new Buff(EffectValue, _actionTime));
        }
    }
}
