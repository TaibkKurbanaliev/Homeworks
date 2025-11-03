using Homework1.Enemies;
using Homework1.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework1
{
    public class Game
    {
        enum PlayerAction
        {
            Attack = 1,
            UsePotion,
            SelectWeapon,
            SelectPotion,
            ShowStatus,
            Exit = 0
        }

        private Player _player;
        private Enemy _enemy;

        private bool _isRunning = true;

        public void Start()
        {
            var rand = new Random();
            var minHealth = 90;
            var maxHealth = 110;
            var minDamage = 5;
            var maxDamage = 15;

            _player = new Player("Воин", rand.Next(minHealth, maxHealth), rand.Next(minDamage, maxDamage), new Weapon("Меч", 0, 5));
            
            _player.AddItem(new Weapon("Топор", 10, 10));
            _player.AddItem(new Weapon("Копье", 5, 7));
            _player.AddItem(new HealthPotion("Зелье здоровья", 5, 10));
            _player.AddItem(new DamagePotion("Зелье силы", 5, 5, 3));
            _enemy = new Enemy("Зомби", rand.Next(minHealth, maxHealth), rand.Next(minDamage, maxDamage), 5);

        }

        public void Update()
        {
            while (_isRunning)
            {
                _player.ShowStats();
                _enemy.ShowStats();

                Console.WriteLine("\nВыбери действие:");
                Console.WriteLine("1. Атаковать");
                Console.WriteLine("2. Использовать зелье");
                Console.WriteLine("3. Выбрать оружие");
                Console.WriteLine("4. Выбрать зелье");
                Console.WriteLine("5. Показать статус");
                Console.WriteLine("0. Выход из игры");
                Console.Write("> ");

                if (int.TryParse(Console.ReadLine(), out int choice) &&
                    Enum.IsDefined(typeof(PlayerAction), choice))
                {
                    Console.Clear();
                    PlayerChoice(choice);
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Неправильный ввод!!!");
                }

                if (!_enemy.IsAlive)
                {
                    Console.WriteLine("Вы победили!");
                    _isRunning = false;
                }
                else if (!_player.IsAlive)
                {
                    Console.WriteLine("Вы проиграли");
                    _isRunning = false;
                }
            }

            Console.ReadLine();
        }

        private void PlayerChoice(int choice)
        {
            PlayerAction action = (PlayerAction)choice;

            switch (action)
            {
                case PlayerAction.Attack:
                    Attack();

                    if (_enemy.IsAlive)
                        _enemy.Attack(_player);

                    break;

                case PlayerAction.UsePotion:
                    if (!_player.HasPotion)
                    {
                        Console.WriteLine("Выберите зелье!");
                        break;
                    }
                    
                    UsePotion();

                    if (_enemy.IsAlive)
                        _enemy.Attack(_player);

                    break;

                case PlayerAction.SelectWeapon:
                    SelectWeapon();
                    break;

                case PlayerAction.SelectPotion:
                    SelectPotion();
                    break;

                case PlayerAction.ShowStatus:
                    ShowStatus();
                    break;

                case PlayerAction.Exit:
                    _isRunning = false;
                    Console.WriteLine("Выход из игры...");
                    break;
            }
        }

        private void SelectPotion()
        {
            var potions = _player.Items.Where(potion => potion is Potion).Cast<Potion>().ToList();

            if (potions.Count == 0)
            {
                Console.WriteLine("Нет доступных зелей");
                return;
            }

            for (int i = 0; i < potions.Count; i++)
            {
                Console.WriteLine($"{i} - {potions[i].Name}");
            }

            Console.Write("Выберите активное зелье - ");

            var input = Console.ReadLine();

            if (int.TryParse(input, out var potionNumber) && potionNumber >= 0 && potionNumber < potions.Count)
            {
                potions[potionNumber].Equip(_player);
            }
            else
            {
                Console.WriteLine("Неправильный ввод");
            }
        }

        private void ShowStatus()
        {
            _player.ShowStats();
            _player.ShowItems();
        }

        private void SelectWeapon()
        {
            var weapons = _player.Items.Where(weapon => weapon is Weapon).Cast<Weapon>().ToList();

            if (weapons.Count == 0)
            {
                Console.WriteLine("Нет доступных орудий");
                return;
            }

            for (int i = 0; i < weapons.Count; i++)
            {
                Console.WriteLine($"{i} - {weapons[i].Name} кол-во урона - {weapons[i].Damage}");
            }

            Console.Write("Введите номер оружия который вы хотите выбрать - ");
            var input = Console.ReadLine();

            if (int.TryParse( input, out var weaponNumber) && weaponNumber >= 0 && weaponNumber < weapons.Count)
            {
                weapons[weaponNumber].Equip(_player);
            }
            else
            {
                Console.WriteLine("Неправильный ввод");
            }
        }

        private void UsePotion()
        {
            _player.DecreaseBuffs();
            _player.UsePotion();
        }

        private void Attack()
        {
            _player.DecreaseBuffs();
            _player.Attack(_enemy);
        }
    }
}
