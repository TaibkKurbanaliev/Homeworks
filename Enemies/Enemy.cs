using Homework1.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework1.Enemies
{
    public class Enemy : Character
    {
        public Enemy(string name, int health, int damage, int diedBonus) : base(name, health, damage)
        {
            DiedBonus = diedBonus;
        }

        public int DiedBonus { get; private set; }
    }
}
