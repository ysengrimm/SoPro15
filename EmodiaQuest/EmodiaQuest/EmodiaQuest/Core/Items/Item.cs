using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmodiaQuest.Core.Items
{
    public class Item
    {
        public int Lvl = 0;
        public string Name;
        public ItemClass Class;
        public int ID;
        public int RequiredStrength = 0;
        public int Armor = 0;
        public int MinDamage = 0;
        public int MaxDamage = 0;
        public int StrengthPlus = 0;
        public int SkillPlus = 0;
        public float IntelligencePlus = 0;
        public int HitPointsPlus = 0;
        public bool IsRangedWeapon = false;

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
        public Item(int Lvl, ItemClass itemClass, int hitpoints)
        {
            useableCount++;

            this.ID = useableCount;
            this.Lvl = Lvl;
            this.Class = itemClass;
            this.HitPointsPlus = hitpoints;
        }

        // Armor-Items ( Helmet, Armor, Boots )
        public Item(int Lvl, ItemClass itemClass, int requiredStrength, int armor, int strengthPlus, int skillPlus, string name)
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
            this.Lvl = Lvl;
            this.Class = itemClass;
            this.RequiredStrength = requiredStrength;
            this.Armor = armor;
            this.StrengthPlus = strengthPlus;
            this.SkillPlus = skillPlus;
            this.Name = name;

        }

        // Weapon-Items ( Helmet, Armor, Boots )
        public Item(int Lvl, ItemClass itemClass, int requiredStrength, int minDamage, int maxDamage, int strengthPlus, int skillPlus, bool isRangedWeapon, string name)
        {
            weaponCount++;

            this.ID = weaponCount;
            this.Lvl = Lvl;
            this.Class = itemClass;
            this.RequiredStrength = requiredStrength;
            this.MinDamage = minDamage;
            this.MaxDamage = maxDamage;
            this.StrengthPlus = strengthPlus;
            this.SkillPlus = skillPlus;
            this.IsRangedWeapon = isRangedWeapon;
            this.Name = name;
        }

    }
}
