using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmodiaQuest.Core.Items
{
    public class Item
    {
        public string Name;
        public ItemClass Class;
        public int ID;
        public int RequiredStrength;
        public int Armor;
        public int MinDamage;
        public int MaxDamage;
        public int StrengthPlus;
        public int TenacityPlus;
        public int HitPointsPlus;

        // ID-Counter to separate items
        private static int questCount = 10000;
        private static int useableCount = 20000;
        private static int helmetCount = 30000;
        private static int armorCount = 40000;
        private static int bootCount = 50000;
        private static int weaponCount = 60000;

        // Quest-Items
        public Item(ItemClass itemClass, string name)
        {
            questCount++;

            this.ID = questCount;
            this.Class = itemClass;
            this.Name = name;
        }

        // Useable-Items
        public Item(ItemClass itemClass, int hitpoints)
        {
            useableCount++;

            this.ID = useableCount;
            this.Class = itemClass;
            this.HitPointsPlus = hitpoints;
        }

        // Armor-Items ( Helmet, Armor, Boots )
        public Item(ItemClass itemClass, int requiredStrength, int armor, int strengthPlus, int tenacityPlus)
        {
            switch (itemClass)
            {
                case ItemClass.Helmet:
                    helmetCount++;
                    this.ID = helmetCount;
                    break;
                case ItemClass.Armor:
                    armorCount++;
                    this.ID = armorCount;
                    break;
                case ItemClass.Boots:
                    bootCount++;
                    this.ID = bootCount;
                    break;
                default:
                    Console.WriteLine("Wrong ItemClass chosen.");
                    break;
            }

            this.Class = itemClass;
            this.RequiredStrength = requiredStrength;
            this.Armor = armor;
            this.StrengthPlus = strengthPlus;
            this.TenacityPlus = tenacityPlus;

        }

        // Weapon-Items ( Helmet, Armor, Boots )
        public Item(ItemClass itemClass, int requiredStrength, int minDamage, int maxDamage, int strengthPlus, int tenacityPlus)
        {
            weaponCount++;

            this.ID = weaponCount;
            this.Class = itemClass;
            this.RequiredStrength = requiredStrength;
            this.MinDamage = minDamage;
            this.MaxDamage = maxDamage;
            this.StrengthPlus = strengthPlus;
            this.TenacityPlus = tenacityPlus;
        }

    }
}
