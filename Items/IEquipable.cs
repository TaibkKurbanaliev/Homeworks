using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework1.Items
{
    public interface IEquipable
    {
        void Equip(Player player);
        void UnEquip(Player player);
    }
}
