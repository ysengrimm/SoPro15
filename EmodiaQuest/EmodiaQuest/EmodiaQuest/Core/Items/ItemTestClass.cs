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

        //public List<Item> Items = new List<Item>();

        public List<Item> Quests = new List<Item>();
        public List<Item> Useables = new List<Item>();
        public List<Item> Helmets = new List<Item>();
        public List<Item> Armors = new List<Item>();
        public List<Item> Boots = new List<Item>();
        public List<Item> Weapons = new List<Item>();

        public void loadContent()
        {

            Useables.Add(new Item(ItemClass.Useable, 50));
            Useables.Add(new Item(ItemClass.Useable, 80));

            Quests.Add(new Item(ItemClass.Quest, "bookQuest"));

            Helmets.Add(new Item(ItemClass.Helmet, 10, 5, 0, 0));

            Armors.Add(new Item(ItemClass.Armor, 8, 5, 0, 0));

            Boots.Add(new Item(ItemClass.Boots, 5, 5, 0, 0));

            Weapons.Add(new Item(ItemClass.Weapon, 5, 2, 6, 0, 0));





        }




























    }
}
