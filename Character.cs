using Homework1.Items;

namespace Homework1
{
    public class Character : IDamageable
    {

        protected int MaxHealth;
        protected int BaseDamage;

        public Character(string name, int health, int damage)
        {
            Name = name;
            Health = health;
            MaxHealth = health;
            BaseDamage = damage;
        }

        public string Name { get; private set; }
        public int Health { get; protected set; }
        public bool IsAlive => Health > 0;

        public virtual int Damage => BaseDamage;

        public void ShowStats()
        {
            Console.WriteLine($"{Name}: текущее здоровье - {Health}\n Общий урон - {Damage}");
        }

        public virtual void Attack(IDamageable target)
        {
            target.TakeDamage(Damage);
        }

        public virtual void TakeDamage(int damage)
        {
            if (damage <= 0)
                return;

            Health -= damage;
            Console.WriteLine($"{Name} получил {damage} урона");
        }
    }
}
