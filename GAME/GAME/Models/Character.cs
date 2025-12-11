namespace GAME.Models
{
    public enum RaceType
    {
        Human,
        Elf,
        Orc,
        Beast
    }

    public enum WeaponType
    {
        // Human
        Shotgun,
        SniperRifle,
        // Elf
        FireStaff,
        EarthStaff,
        AirStaff,
        WaterStaff,
        // Orc
        Axe,
        Hammer,
        // Beast
        Fists,
        Sword
    }

    public class Character
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public RaceType Race { get; set; }
        public WeaponType Weapon { get; set; }
        public int MaxHealth { get; set; }
        public int CurrentHealth { get; set; }
        public int BaseAttackDamage { get; set; }
        public int BleeedingTurnsRemaining { get; set; } = 0;
        public int BleeedingDamagePerTurn { get; set; } = 0;

        public Character()
        {
        }

        public Character(RaceType race, WeaponType weapon, string name = "")
        {
            Race = race;
            Weapon = weapon;
            Name = name;
            InitializeStats();
        }

        private void InitializeStats()
        {
            switch (Race)
            {
                case RaceType.Human:
                    MaxHealth = 100;
                    BaseAttackDamage = 3;
                    break;
                case RaceType.Elf:
                    MaxHealth = Weapon == WeaponType.WaterStaff ? 115 : 100;
                    BaseAttackDamage = 2;
                    break;
                case RaceType.Orc:
                    MaxHealth = 100;
                    BaseAttackDamage = 4;
                    break;
                case RaceType.Beast:
                    MaxHealth = 100;
                    BaseAttackDamage = 5;
                    break;
            }
            CurrentHealth = MaxHealth;
        }

        public int GetAttackDamage(int distance)
        {
            Random random = new Random();
            int damage = 0;

            switch (Weapon)
            {
                case WeaponType.Shotgun:
                    damage = random.Next(1, 6);
                    damage += (int)(damage * 0.1); // 10% bonus
                    break;
                case WeaponType.SniperRifle:
                    if (distance > 0)
                        damage = random.Next(10, 21); // 10-20 at distance
                    else
                        damage = random.Next(1, 6);
                    break;
                case WeaponType.FireStaff:
                    damage = random.Next(3, 8);
                    damage += (int)(damage * 0.15); // 15% bonus
                    break;
                case WeaponType.EarthStaff:
                    damage = random.Next(2, 7);
                    damage += (int)(damage * 0.1); // 10% bonus
                    break;
                case WeaponType.AirStaff:
                    damage = random.Next(2, 6);
                    if (distance > 0)
                        damage += (int)(damage * 0.15); // 5-15% bonus at distance
                    break;
                case WeaponType.WaterStaff:
                    damage = random.Next(2, 6);
                    damage += (int)(damage * 0.2); // 20% bonus
                    break;
                case WeaponType.Axe:
                    damage = random.Next(1, 6);
                    break;
                case WeaponType.Hammer:
                    damage = random.Next(2, 8);
                    break;
                case WeaponType.Fists:
                    damage = random.Next(20, 31);
                    break;
                case WeaponType.Sword:
                    damage = random.Next(1, 11);
                    break;
            }

            return Math.Max(1, damage);
        }

        public int GetHealAmount()
        {
            Random random = new Random();
            int damageToHeal = MaxHealth - CurrentHealth;

            switch (Race)
            {
                case RaceType.Human:
                    return (int)(damageToHeal * random.NextDouble() * 0.5); // 40-50%
                case RaceType.Elf:
                    if (Weapon == WeaponType.WaterStaff)
                        return (int)(damageToHeal * (0.75 + random.NextDouble() * 0.15)); // 75-90%
                    else
                        return (int)(damageToHeal * 0.65); // 65%
                case RaceType.Orc:
                    return (int)(damageToHeal * (0.25 + random.NextDouble() * 0.2)); // 25-45%
                case RaceType.Beast:
                    return (int)(damageToHeal * 0.5); // 50%
                default:
                    return 0;
            }
        }

        public void TakeDamage(int damage)
        {
            CurrentHealth -= damage;
            if (CurrentHealth < 0)
                CurrentHealth = 0;
        }

        public void Heal(int amount)
        {
            CurrentHealth += amount;
            if (CurrentHealth > MaxHealth)
                CurrentHealth = MaxHealth;
        }

        public bool IsAlive => CurrentHealth > 0;

        public void ApplyBleeding()
        {
            if (BleeedingTurnsRemaining > 0)
            {
                TakeDamage(BleeedingDamagePerTurn);
                BleeedingTurnsRemaining--;
            }
        }
    }
}
