using sprint0.Interfaces;
using sprint0;
using sprint0.PlayerStates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace sprint0.Classes
{
    public class Inventory
    {
        private static int maxHealth;
        private static int health;
        private static int bombs;
        private static int keys;
        private static int rupees;
        private static bool sword;
        private static bool bow;   
        private static bool boomerang;
        private static bool compass;
        private static bool map;
        

        public Inventory()
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

        public int GetHealth()
        {
            return health;
        }

        public void SetHealth(int value)
        {
            health = value;
        }


        public int GetMaxHealth()
        {
            return maxHealth;
        }

        public void SetMaxHealth(int value)
        {
            maxHealth = value;
        }


        public int GetBombs()
        {
            return bombs;
        }

        public void SetBombs(int value)
        {
            bombs = value;
        }


        public int GetKeys()
        {
            return keys;
        }

        public void SetKeys(int value)
        {
            keys = value;
        }


        public int GetRupees()
        {
            return rupees;
        }

        public void SetRupees(int value)
        {
            rupees = value;
        }

    
        public bool HasSword()
        {
            return sword;
        }

        public void SetSword(bool value)
        {
            sword = value;
        }


        public bool HasBow()
        {
            return bow;
        }

        public void SetBow(bool value)
        {
            bow = value;
        }


        public bool HasBoomerang()
        {
            return boomerang;
        }

        public void SetBoomerang(bool value)
        {
            boomerang = value;
        }

        public bool HasCompass()
        {
            return compass;
        }

        public void SetCompass(bool value)
        {
            compass = value;
        }

        public bool HasMap()
        {
            return map;
        }

        public void SetMap(bool value)
        {
            map = value;
        }
    }
}