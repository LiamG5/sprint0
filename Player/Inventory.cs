using sprint0.Interfaces;
using sprint0;
using sprint0.PlayerStates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using System.Security;

namespace sprint0.Classes
{
    public static class Inventory
    {
        
        private static int maxHealth = 6;
        private static int health = 6;
        private static int bombs = 0;
        private static int keys = 0;
        private static int rupees = 80;
        private static bool sword = true;
        private static bool bow = false;   
        private static bool boomerang = false;
        private static bool compass = false;
        private static bool map = false;
        private static bool[] keyCollected = new bool[6];

       
        static Inventory()
        {
           
            Reset();
        }

        
        public static void Reset()
        {
            maxHealth = 6;
            health = 6;
            bombs = 0;
            keys = 0;
            rupees = 80;
            sword = true;
            bow = false;
            boomerang = false;
            compass = false;
            map = false;
        }

        // Health methods
        public static int GetHealth()
        {
            return health;
        }

        public static void SetHealth(int value)
        {
            health = value;
        }

        
        public static int GetMaxHealth()
        {
            return maxHealth;
        }

        public static void SetMaxHealth(int value)
        {
            maxHealth = value;
        }

        
        public static int GetBombs()
        {
            return bombs;
        }

        public static void SetBombs(int value)
        {
            bombs = value;
        }

        public static void AddBombs(int count)
        {
            bombs += count;
        }

        public static bool UseBomb()
        {
            if (bombs > 0)
            {
                bombs--;
                return true;
            }
            return false;
        }

        
        public static int GetKeys()
        {
            return keys;
        }

        public static void SetKeys(int value)
        {
            keys = value;
        }

        public static void AddKeys(int count)
        {
            keys += count;
        }

        public static bool UseKey()
        {
            if (keys > 0)
            {
                keys--;
                return true;
            }
            return false;
        }

        public static int GetRupees()
        {
            return rupees;
        }

        public static void SetRupees(int value)
        {
            rupees = value;
        }

        public static void AddRupees(int amount)
        {
            rupees += amount;
        }

        public static bool SpendRupees(int amount)
        {
            if (rupees >= amount)
            {
                rupees -= amount;
                return true;
            }
            return false;
        }

        
        public static bool HasSword()
        {
            return sword;
        }

        public static void SetSword(bool value)
        {
            sword = value;
        }

        public static void AcquireSword()
        {
            sword = true;
        }

        
        public static bool HasBow()
        {
            return bow;
        }

        public static void SetBow(bool value)
        {
            bow = value;
        }

        public static void AcquireBow()
        {
            bow = true;
        }

        
        public static bool HasBoomerang()
        {
            return boomerang;
        }

        public static void SetBoomerang(bool value)
        {
            boomerang = value;
        }

        public static void AcquireBoomerang()
        {
            boomerang = true;
        }

        
        public static bool HasCompass()
        {
            return compass;
        }

        public static void SetCompass(bool value)
        {
            compass = value;
        }

        public static void AcquireCompass()
        {
            compass = true;
        }

        
        public static bool HasMap()
        {
            return map;
        }

        public static void SetMap(bool value)
        {
            map = value;
        }

        public static void AcquireMap()
        {
            map = true;
        }

        
        public static void AddHealth(int amount)
        {
            health = System.Math.Min(health + amount, maxHealth);
        }

        public static void TakeDamage(int damage)
        {
            health = System.Math.Max(0, health - damage);
        }

        public static bool IsDead()
        {
            return health <= 0;
        }

        public static void RestoreFullHealth()
        {
            health = maxHealth;
        }
    }
}