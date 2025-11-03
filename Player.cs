using Homework1.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework1
{
    public class Player : Character
    {
        private List<Item> _items = new();
        private List<Buff> _buffs = new();


        private Weapon _currentWeapon;
        private Potion _currentPotion;

        public bool HasPotion => _currentPotion != null;

        public IEnumerable<Item> Items => _items;

        public Player(string name, int health, int damage, Weapon currentWeapon) : base(name, health, damage)
        {
            _currentWeapon = currentWeapon;
        }

        public override int Damage
        {
            get
            {
                var damage = BaseDamage;
                foreach (var buff in _buffs)
                    damage = buff.ApplyBuff(damage);

                return damage + _currentWeapon.Damage;
            }
        }

        public void ShowItems()
        {
            Console.Write("Оружие - ");
            Console.WriteLine(_currentWeapon != null ? _currentWeapon.Name : "отсутствует");
            Console.Write("Зелье - ");
            Console.WriteLine(_currentPotion != null ? _currentPotion.Name : "отсутствует");
            Console.WriteLine($"Предметы в инвентаре:");

            foreach (var item in _items)
            {
                Console.WriteLine(item.Name);
            }
        }

        public void AddItem(Item item)
        {
            _items.Add(item);
        }

        public void RemoveItem(Item item)
        {
            _items.Remove(item);
        }

        public void Heal(int effectValue)
        {
            if (effectValue <= 0)
                throw new ArgumentOutOfRangeException(nameof(effectValue));

            var previousHelath = Health;
            Health = Math.Clamp(Health + effectValue, 0, MaxHealth);
            Console.Write($"восстановленно {Health - previousHelath} здоровья");
        }

        public void UsePotion()
        {
            Console.WriteLine($"{Name} использовал зелье - ");
            _currentPotion.Use(this);
            _currentPotion = null;
        }

        public void AddBuff(Buff buff)
        {
            _buffs.Add(buff);
            buff.Finished += OnBuffFinished;
            Console.Write($"получено +{buff.Value} урона на {buff.TimesToApplying} хода");
        }

        private void OnBuffFinished(Buff buff)
        {
            buff.Finished -= OnBuffFinished;
            _buffs.Remove(buff);
        }

        public void EquipWeapon(Weapon weapon)
        {
            _items.Remove(weapon);
            UnequipWeapon(_currentWeapon);
            _currentWeapon = weapon;
            Console.WriteLine($"{Name} надел {_currentWeapon.Name} (+{_currentWeapon.Damage} к урону)");
        }

        public void UnequipWeapon(Weapon weapon)
        {
            _items.Add(weapon);
            _currentWeapon = default;
        }

        public void EquipPotion(Potion potion)
        {
            _items.Remove(potion);
            UnequipPotion(_currentPotion);
            _currentPotion = potion;
            Console.WriteLine($"Используется зелье - {potion.Name}");
        }

        public void UnequipPotion(Potion potion)
        {
            if (_currentPotion == null)
                return;

            _items.Add(potion);
            _currentPotion = default;
        }
        
        public void DecreaseBuffs()
        {
            foreach (Buff buff in _buffs)
            {
                buff.DecreaseBuff();
            }
        }
    }
}
