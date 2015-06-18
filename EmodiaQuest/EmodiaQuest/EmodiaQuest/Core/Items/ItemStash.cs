using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmodiaQuest.Core
{
    class ItemStash
    {
        private static ItemStash instance;
        private ItemStash() { }
        public static ItemStash Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ItemStash();
                }
                return instance;
            }
        }

        private List<Item_Armor> armors = new List<Item_Armor>();
        private List<Item_Quest> quests = new List<Item_Quest>();
        private List<Item_Useable> usuables = new List<Item_Useable>();
        private List<Item_Weapon> weapons = new List<Item_Weapon>();

        public void addArmor(string itemName, int requiredStrength, int strength, int tenacity)
        {
            armors.Add(new Item_Armor(itemName, requiredStrength, strength, tenacity));
        }



    }


    
}
