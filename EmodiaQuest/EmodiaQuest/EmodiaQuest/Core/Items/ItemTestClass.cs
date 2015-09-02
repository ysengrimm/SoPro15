using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmodiaQuest.Core.Items
{
    public class ItemTestClass
    {
        private static ItemTestClass instance;

        private ItemTestClass() { }

        public static ItemTestClass Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ItemTestClass();
                }
                return instance;
            }
        }

        // The Random Generator
        Random random = new Random();

        //public List<Item> Items = new List<Item>();

        public List<Item> Quests = new List<Item>();
        public List<Item> Useables = new List<Item>();
        public List<Item> Helmets = new List<Item>();
        public List<Item> Armors = new List<Item>();
        public List<Item> Boots = new List<Item>();
        public List<Item> Weapons = new List<Item>();

        public void loadContent()
        {

            Useables.Add(new Item(0, ItemClass.Useable, 50));
            Useables.Add(new Item(0, ItemClass.Useable, 80));
            Useables.Add(new Item(0, ItemClass.Useable, 120));

            Quests.Add(new Item(ItemClass.Quest, "buchQuest"));
            Quests.Add(new Item(ItemClass.Quest, "diamontQuest"));
            Quests.Add(new Item(ItemClass.Quest, "rowenErzQuest"));
            Quests.Add(new Item(ItemClass.Quest, "leg10MonsterUmQuest"));
            Quests.Add(new Item(ItemClass.Quest, "leg15MonsterUmQuest"));
            Quests.Add(new Item(ItemClass.Quest, "leg3SteinmonsterumQuest"));

            //Lvl 1
            Helmets.Add(new Item(0, ItemClass.Helmet, 0, 1, 0, 0));
            Helmets.Add(new Item(0, ItemClass.Helmet, 0, 2, 0, 0));
            Helmets.Add(new Item(0, ItemClass.Helmet, 0, 3, 0, 0));

            //Lvl 2
            Helmets.Add(new Item(2, ItemClass.Helmet, 5, 5, 0, 0));
            Helmets.Add(new Item(2, ItemClass.Helmet, 5, 7, 0, 0));
            Helmets.Add(new Item(2, ItemClass.Helmet, 5, 8, 0, 0));

            //Lvl 3
            Helmets.Add(new Item(3, ItemClass.Helmet, 10, 10, 0, 0));
            Helmets.Add(new Item(3, ItemClass.Helmet, 10, 12, 0, 0));
            Helmets.Add(new Item(3, ItemClass.Helmet, 10, 15, 0, 0));

            //Lvl 4
            Helmets.Add(new Item(4, ItemClass.Helmet, 20, 20, 0, 0));
            Helmets.Add(new Item(4, ItemClass.Helmet, 20, 25, 0, 0));
            Helmets.Add(new Item(4, ItemClass.Helmet, 20, 30, 0, 0));

            //Lvl 1
            Boots.Add(new Item(1, ItemClass.Boots, 0, 1, 0, 0));
            Boots.Add(new Item(1, ItemClass.Helmet, 0, 2, 0, 0));

            //Lvl 2
            Boots.Add(new Item(2, ItemClass.Boots, 5, 4, 0, 0));
            Boots.Add(new Item(2, ItemClass.Boots, 5, 5, 0, 0));
            Boots.Add(new Item(2, ItemClass.Boots, 5, 7, 0, 0));

            //Lvl 3
            Boots.Add(new Item(3, ItemClass.Boots, 10, 8, 0, 0));
            Boots.Add(new Item(3, ItemClass.Boots, 10, 10, 0, 0));
            Boots.Add(new Item(3, ItemClass.Boots, 10, 12, 0, 0));

            //Lvl 4
            Boots.Add(new Item(4, ItemClass.Boots, 20, 15, 0, 0));
            Boots.Add(new Item(4, ItemClass.Boots, 20, 18, 0, 0));
            Boots.Add(new Item(4, ItemClass.Boots, 20, 22, 0, 0));

            //Lvl 1
            Armors.Add(new Item(1, ItemClass.Armor, 0, 2, 0, 0));
            Armors.Add(new Item(1, ItemClass.Armor, 0, 4, 0, 0));
            Armors.Add(new Item(1, ItemClass.Armor, 0, 5, 0, 0));

            //Lvl 2
            Armors.Add(new Item(2, ItemClass.Armor, 5, 7, 0, 0));
            Armors.Add(new Item(2, ItemClass.Armor, 5, 8, 0, 0));
            Armors.Add(new Item(2, ItemClass.Armor, 5, 10, 0, 0));

            //Lvl 3
            Armors.Add(new Item(3, ItemClass.Armor, 10, 14, 0, 0));
            Armors.Add(new Item(3, ItemClass.Armor, 10, 16, 0, 0));
            Armors.Add(new Item(3, ItemClass.Armor, 10, 18, 0, 0));

            //Lvl 4
            Armors.Add(new Item(4, ItemClass.Armor, 20, 25, 0, 0));
            Armors.Add(new Item(4, ItemClass.Armor, 20, 30, 0, 0));
            Armors.Add(new Item(4, ItemClass.Armor, 20, 35, 0, 0));

            //Lvl 1
            Weapons.Add(new Item(1, ItemClass.Weapon, 0, 1, 3, 0, 0));
            Weapons.Add(new Item(1, ItemClass.Weapon, 0, 1, 4, 0, 0));
            Weapons.Add(new Item(1, ItemClass.Weapon, 0, 1, 5, 0, 0));

            //Lvl 2
            Weapons.Add(new Item(2, ItemClass.Weapon, 3, 3, 6, 0, 0));
            Weapons.Add(new Item(2, ItemClass.Weapon, 3, 3, 7, 0, 0));
            Weapons.Add(new Item(2, ItemClass.Weapon, 3, 3, 8, 0, 0));

            //Lvl 3
            Weapons.Add(new Item(3, ItemClass.Weapon, 7, 8, 12, 0, 0));
            Weapons.Add(new Item(3, ItemClass.Weapon, 7, 8, 14, 0, 0));
            Weapons.Add(new Item(3, ItemClass.Weapon, 7, 8, 16, 0, 0));

            //Lvl 4
            Weapons.Add(new Item(4, ItemClass.Weapon, 12, 14, 20, 0, 0));
            Weapons.Add(new Item(4, ItemClass.Weapon, 12, 14, 24, 0, 0));
            Weapons.Add(new Item(4, ItemClass.Weapon, 12, 14, 28, 0, 0));

            //Lvl 5
            Weapons.Add(new Item(5, ItemClass.Weapon, 20, 25, 40, 0, 0));
            Weapons.Add(new Item(5, ItemClass.Weapon, 20, 25, 45, 0, 0));
            Weapons.Add(new Item(5, ItemClass.Weapon, 20, 25, 50, 0, 0));


        }


        public void ItemGeneratorMerchant(int currentHeroLevel, bool dungeonDone)
        {
            int chooseItemDie;
            int healingPots;
            if (currentHeroLevel < 4)
            {
                chooseItemDie = random.Next(3) + 1;
                healingPots = 1;
            }
            else if (currentHeroLevel < 10)
            {
                chooseItemDie = random.Next(1, 5) + 1;
                healingPots = 2;
            }
            else
            {
                chooseItemDie = random.Next(4, 9) + 1;
                healingPots = 3;
            }

            for (int i = 0; i < chooseItemDie; i++)
            {

            }

            switch (chooseItemDie)
            {
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;
                case 5:
                    break;
                case 6:
                    break;
                case 7:
                    break;
                case 8:
                    break;
                case 9:
                    break;
                default:
                    Console.WriteLine("Wrong number in chooseItemDie");
                    break;
            }

        }



        public void ItemGeneratorMonster(int currentHeroLevel)
        {
            // + monstertyp!
        }



        //int dieValue = random.Next(6) + 1;






















    }
}
