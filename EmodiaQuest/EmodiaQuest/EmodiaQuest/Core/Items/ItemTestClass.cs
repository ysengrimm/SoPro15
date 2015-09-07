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
            Quests.Add(new Item(ItemClass.Quest, "Diebesgut"));
            Quests.Add(new Item(ItemClass.Quest, "Kochbuch"));
            Quests.Add(new Item(ItemClass.Quest, "Schatztruhe"));
            Quests.Add(new Item(ItemClass.Quest, "Sack"));
            Quests.Add(new Item(ItemClass.Quest, "Messing"));
            Quests.Add(new Item(ItemClass.Quest, "Eisen"));
            Quests.Add(new Item(ItemClass.Quest, "Truhe"));

            //Lvl 1
            Helmets.Add(new Item(1, ItemClass.Helmet, 0, 30, 0, 0, "LederKappe"));
            Helmets.Add(new Item(1, ItemClass.Helmet, 0, 20, 0, 0, "StoffKappe"));
            Helmets.Add(new Item(1, ItemClass.Helmet, 0, 40, 0, 0, "MetallKappe"));

            //Lvl 2
            Helmets.Add(new Item(2, ItemClass.Helmet, 5, 50, 0, 0, "LederKappe"));
            Helmets.Add(new Item(2, ItemClass.Helmet, 5, 80, 0, 0, "rostigerSchrottHelm"));
            Helmets.Add(new Item(2, ItemClass.Helmet, 5, 90, 0, 0, "rostigerSchrottHelm"));

            //Lvl 3
            Helmets.Add(new Item(3, ItemClass.Helmet, 10, 100, 0, 0, "rostigerSchrottHelm"));
            Helmets.Add(new Item(3, ItemClass.Helmet, 10, 200, 0, 0, "speziellerHelm"));
            Helmets.Add(new Item(3, ItemClass.Helmet, 10, 150, 0, 0, "speziellerRostigerHelm"));

            //Lvl 4
            Helmets.Add(new Item(4, ItemClass.Helmet, 20, 50, 0, 0, "LederKappe"));
            Helmets.Add(new Item(4, ItemClass.Helmet, 20, 400, 0, 0, "speziellerSignierterHelm"));
            Helmets.Add(new Item(4, ItemClass.Helmet, 20, 200, 0, 0, "speziellerHelm"));

            //Lvl 1
            Boots.Add(new Item(1, ItemClass.Boots, 0, 30, 0, 0, "Schuhe"));
            Boots.Add(new Item(1, ItemClass.Boots, 0, 20, 0, 0, "alteSchuhe"));

            //Lvl 2
            Boots.Add(new Item(2, ItemClass.Boots, 5, 30, 0, 0, "Schuhe"));
            Boots.Add(new Item(2, ItemClass.Boots, 5, 20, 0, 0, "alteSchuhe"));
            Boots.Add(new Item(2, ItemClass.Boots, 5, 50, 0, 0, "moderneSchuhe"));

            //Lvl 3
            Boots.Add(new Item(3, ItemClass.Boots, 10, 30, 0, 0, "Schuhe"));
            Boots.Add(new Item(3, ItemClass.Boots, 10, 25, 0, 0, "alteSchuhe"));
            Boots.Add(new Item(3, ItemClass.Boots, 10, 55, 0, 0, "moderneSchuhe"));

            //Lvl 4
            Boots.Add(new Item(4, ItemClass.Boots, 20, 50, 0, 0, "Schuhe"));
            Boots.Add(new Item(4, ItemClass.Boots, 20, 60, 0, 0, "alteSchuhe"));
            Boots.Add(new Item(4, ItemClass.Boots, 20, 80, 0, 0, "moderneSchuhe"));

            //Lvl 1
            Armors.Add(new Item(1, ItemClass.Armor, 0, 40, 40, 0, "StoffHemd"));
            Armors.Add(new Item(1, ItemClass.Armor, 0, 60, 60, 0, "neuesStoffhemd"));
            Armors.Add(new Item(1, ItemClass.Armor, 0, 35, 20, 0, "altesStoffhemd"));

            //Lvl 2
            Armors.Add(new Item(2, ItemClass.Armor, 5, 70, 0, 0, "neuesStoffhemd"));
            Armors.Add(new Item(2, ItemClass.Armor, 5, 80, 0, 0 , "leichteRuestung"));
            Armors.Add(new Item(2, ItemClass.Armor, 5, 100, 0, 0, "leichteModerneRuestung"));

            //Lvl 3
            Armors.Add(new Item(3, ItemClass.Armor, 10, 140, 0, 0, "leichteModerneRuestung"));
            Armors.Add(new Item(3, ItemClass.Armor, 10, 160, 0, 0, "leichteRuestung"));
            Armors.Add(new Item(3, ItemClass.Armor, 10, 180, 0, 0, "alteSchwereRuestung"));

            //Lvl 4
            Armors.Add(new Item(4, ItemClass.Armor, 20, 250, 0, 0, "alteSchwereRuestung"));
            Armors.Add(new Item(4, ItemClass.Armor, 20, 300, 0, 0, "moderneSchwereRuestung"));
            Armors.Add(new Item(4, ItemClass.Armor, 20, 350, 0, 0, "schwereRuestung"));

            //Lvl 0
            Weapons.Add(new Item(0, ItemClass.Weapon, 0, 60, 70, 0, 0, false, "Stock"));
            Weapons.Add(new Item(0, ItemClass.Weapon, 0, 40, 90, 0, 0, false, "Hammer"));
            Weapons.Add(new Item(0, ItemClass.Weapon, 0, 100, 105, 0, 0, true, "Gewehr"));

            //Lvl 1
            Weapons.Add(new Item(1, ItemClass.Weapon, 3, 70, 150, 0, 0, true, "EinfachesGewehr"));
            Weapons.Add(new Item(1, ItemClass.Weapon, 3, 100, 200, 0, 0, true, "Gewehr"));
            Weapons.Add(new Item(1, ItemClass.Weapon, 3, 160, 180, 0, 0, true, "SpeziellesGewehr"));

            //Lvl 2
            Weapons.Add(new Item(2, ItemClass.Weapon, 7, 100, 120, 0, 0, false, "Schwert"));
            Weapons.Add(new Item(2, ItemClass.Weapon, 7, 100, 121, 0, 0, false, "Schwert"));
            Weapons.Add(new Item(2, ItemClass.Weapon, 7, 100, 122, 0, 0, false, "Schwert"));

            //Lvl 3
            Weapons.Add(new Item(3, ItemClass.Weapon, 12, 100, 200, 0, 0, false, "Schwert"));
            Weapons.Add(new Item(3, ItemClass.Weapon, 12, 100, 201, 0, 0, false, "Schwert"));
            Weapons.Add(new Item(3, ItemClass.Weapon, 12, 100, 202, 0, 0, false, "Schwert"));

            //Lvl 4
            Weapons.Add(new Item(4, ItemClass.Weapon, 25, 242, 242, 0, 0, false, "Schwert"));
            Weapons.Add(new Item(4, ItemClass.Weapon, 25, 42, 42, 0, 0, false, "Schwert"));
            Weapons.Add(new Item(4, ItemClass.Weapon, 25, 420, 42, 0, 0, false, "Schwert"));


        }

        public List<Item> ItemGeneratorMonster(int currentHeroLevel, NPCs.EnemyType monsterType)
        {
            int heroItemLvl = 0;
            if (currentHeroLevel >= 0)
                heroItemLvl = 1;
            if (currentHeroLevel > 3)
                heroItemLvl = 2;
            if (currentHeroLevel > 7)
                heroItemLvl = 3;
            if (currentHeroLevel > 11)
                heroItemLvl = 4;

            List<Item> availableList = new List<Item>();
            availableList = getAvailable(heroItemLvl);

            List<Item> giveList = new List<Item>();

            int droppedItemsCount = random.Next(3) + 1;
            int availableItemsCount = availableList.Count;

            for (int i = 0; i < droppedItemsCount; i++)
            {
                int pickedItem = random.Next(availableItemsCount - 1);
                giveList.Add(availableList[pickedItem]);
            }

            // computer the magic values
            foreach (var item in giveList)
            {
                dieMagic(item);
            }

            return giveList;
            
        }


        public List<Item> ItemGeneratorMerchant(int currentHeroLevel, string npcName)
        {
            int heroItemLvl = 0;
            if (currentHeroLevel >= 0)
                heroItemLvl = 1;
            if (currentHeroLevel > 3)
                heroItemLvl = 2;
            if (currentHeroLevel > 7)
                heroItemLvl = 3;
            if (currentHeroLevel > 11)
                heroItemLvl = 4;

            int chooseItemDie = 0;
            int healingPots = 0;

            switch (heroItemLvl)
            {
                case 1:
                    chooseItemDie = random.Next(3) + 1;
                    healingPots = 1;
                    break;
                case 2:
                    chooseItemDie = random.Next(1, 5) + 1;
                    healingPots = 2;
                    break;
                case 3:
                    chooseItemDie = random.Next(4, 9) + 1;
                    healingPots = 3;
                    break;
                case 4:
                    chooseItemDie = random.Next(4, 9) + 1;
                    healingPots = 3;
                    break;
                default:
                    Console.WriteLine("Wrong heroItemLevelChoosen.");
                    break;
            }
            if (npcName == "Konstantin")
            {
                chooseItemDie -= 3;
            }
            if (chooseItemDie < 1)
                chooseItemDie = 1;

            //for (int i = 0; i < chooseItemDie; i++)
            //{

            //}

            List<Item> availableList = new List<Item>();
            availableList = getAvailable(heroItemLvl);

            List<Item> giveList = new List<Item>();




            switch (chooseItemDie)
            {
                case 1:
                    if (npcName == "Yorlgon")
                    {
                        giveList.Add(getOneItem(availableList, ItemClass.Weapon));
                    }
                    else
                    {
                        giveList.Add(getOneItem(availableList, ItemClass.Armor));
                    }
                    break;
                case 2:
                    if (npcName == "Yorlgon")
                    {
                        giveList.Add(getOneItem(availableList, ItemClass.Weapon));
                        giveList.Add(getOneItem(availableList, ItemClass.Armor));
                    }
                    else
                    {
                        giveList.Add(getOneItem(availableList, ItemClass.Armor));
                        giveList.Add(getOneItem(availableList, ItemClass.Helmet));
                    }
                    break;
                case 3:
                    if (npcName == "Yorlgon")
                    {
                        giveList.Add(getOneItem(availableList, ItemClass.Weapon));
                        giveList.Add(getOneItem(availableList, ItemClass.Armor));
                        giveList.Add(getOneItem(availableList, ItemClass.Helmet));
                    }
                    else
                    {
                        giveList.Add(getOneItem(availableList, ItemClass.Armor));
                        giveList.Add(getOneItem(availableList, ItemClass.Helmet));
                        giveList.Add(getOneItem(availableList, ItemClass.Boots));
                    }
                    break;
                case 4:
                    if (npcName == "Yorlgon")
                    {
                        giveList.Add(getOneItem(availableList, ItemClass.Weapon));
                        giveList.Add(getOneItem(availableList, ItemClass.Armor));
                        giveList.Add(getOneItem(availableList, ItemClass.Helmet));
                        giveList.Add(getOneItem(availableList, ItemClass.Boots));
                    }
                    else
                    {
                        giveList.Add(getOneItem(availableList, ItemClass.Armor));
                        giveList.Add(getOneItem(availableList, ItemClass.Helmet));
                        giveList.Add(getOneItem(availableList, ItemClass.Boots));
                        giveList.Add(getOneItem(availableList, ItemClass.Armor));
                    }
                    break;
                case 5:
                    if (npcName == "Yorlgon")
                    {
                        giveList.Add(getOneItem(availableList, ItemClass.Weapon));
                        giveList.Add(getOneItem(availableList, ItemClass.Armor));
                        giveList.Add(getOneItem(availableList, ItemClass.Helmet));
                        giveList.Add(getOneItem(availableList, ItemClass.Boots));
                        giveList.Add(getOneItem(availableList, ItemClass.Weapon));
                    }
                    else
                    {
                        giveList.Add(getOneItem(availableList, ItemClass.Armor));
                        giveList.Add(getOneItem(availableList, ItemClass.Helmet));
                        giveList.Add(getOneItem(availableList, ItemClass.Boots));
                        giveList.Add(getOneItem(availableList, ItemClass.Armor));
                        giveList.Add(getOneItem(availableList, ItemClass.Helmet));
                    }
                    break;
                case 6:
                    if (npcName == "Yorlgon")
                    {
                        giveList.Add(getOneItem(availableList, ItemClass.Weapon));
                        giveList.Add(getOneItem(availableList, ItemClass.Armor));
                        giveList.Add(getOneItem(availableList, ItemClass.Helmet));
                        giveList.Add(getOneItem(availableList, ItemClass.Boots));
                        giveList.Add(getOneItem(availableList, ItemClass.Weapon));
                        giveList.Add(getOneItem(availableList, ItemClass.Armor));
                    }
                    else
                    {
                        giveList.Add(getOneItem(availableList, ItemClass.Armor));
                        giveList.Add(getOneItem(availableList, ItemClass.Helmet));
                        giveList.Add(getOneItem(availableList, ItemClass.Boots));
                        giveList.Add(getOneItem(availableList, ItemClass.Armor));
                        giveList.Add(getOneItem(availableList, ItemClass.Helmet));
                        giveList.Add(getOneItem(availableList, ItemClass.Boots));
                    }
                    break;
                case 7:
                    giveList.Add(getOneItem(availableList, ItemClass.Weapon));
                    giveList.Add(getOneItem(availableList, ItemClass.Armor));
                    giveList.Add(getOneItem(availableList, ItemClass.Helmet));
                    giveList.Add(getOneItem(availableList, ItemClass.Boots));
                    giveList.Add(getOneItem(availableList, ItemClass.Weapon));
                    giveList.Add(getOneItem(availableList, ItemClass.Armor));
                    giveList.Add(getOneItem(availableList, ItemClass.Helmet));
                    break;
                case 8:
                    giveList.Add(getOneItem(availableList, ItemClass.Weapon));
                    giveList.Add(getOneItem(availableList, ItemClass.Armor));
                    giveList.Add(getOneItem(availableList, ItemClass.Helmet));
                    giveList.Add(getOneItem(availableList, ItemClass.Boots));
                    giveList.Add(getOneItem(availableList, ItemClass.Weapon));
                    giveList.Add(getOneItem(availableList, ItemClass.Armor));
                    giveList.Add(getOneItem(availableList, ItemClass.Helmet));
                    giveList.Add(getOneItem(availableList, ItemClass.Boots));
                    break;
                case 9:
                    giveList.Add(getOneItem(availableList, ItemClass.Weapon));
                    giveList.Add(getOneItem(availableList, ItemClass.Armor));
                    giveList.Add(getOneItem(availableList, ItemClass.Helmet));
                    giveList.Add(getOneItem(availableList, ItemClass.Boots));
                    giveList.Add(getOneItem(availableList, ItemClass.Weapon));
                    giveList.Add(getOneItem(availableList, ItemClass.Armor));
                    giveList.Add(getOneItem(availableList, ItemClass.Helmet));
                    giveList.Add(getOneItem(availableList, ItemClass.Boots));
                    giveList.Add(getOneItem(availableList, ItemClass.Weapon));
                    break;
                default:
                    Console.WriteLine("Wrong number in chooseItemDie");
                    break;
            }

            // computer the magic values
            foreach (var item in giveList)
            {
                dieMagic(item);
            }

            // Put in the healing pots
            if (npcName == "Konstantin")
            {
                switch (heroItemLvl)
                {
                    case 1:
                        giveList.Add(Useables[0]);
                        break;
                    case 2:
                        giveList.Add(Useables[0]);
                        giveList.Add(Useables[1]);
                        break;
                    case 3:
                        giveList.Add(Useables[0]);
                        giveList.Add(Useables[1]);
                        giveList.Add(Useables[2]);
                        break;
                    case 4:
                        giveList.Add(Useables[0]);
                        giveList.Add(Useables[1]);
                        giveList.Add(Useables[2]);
                        break;
                    default:
                        break;
                }
            }


            return giveList;

        }



        public void ItemGeneratorMonster(int currentHeroLevel)
        {
            // + monstertyp!
        }

        private void dieMagic(Item item)
        {
            int itemLvl = item.Lvl;
            int magicNumber;
            int magicCount = 0;
            magicNumber = random.Next(10);
            while (magicNumber == 0)
            {
                int magicStrength = (random.Next(3) + 1) * itemLvl;
                int property = random.Next(3);
                switch (property)
                {
                    case 0:
                        item.StrengthPlus += magicStrength;
                        break;
                    case 1:
                        item.SkillPlus += magicStrength;
                        break;
                    case 2:
                        item.IntelligencePlus += magicStrength;
                        break;
                    default:
                        break;
                }
                magicNumber = random.Next(6);
                magicCount++;
                if (magicCount > 5)
                    break;
            }
        }

        private Item getOneItem(List<Item> available, ItemClass itemClass)
        {
            List<Item> possible = new List<Item>();
            int chosenItem = 0;
            int chooseItemCount = 0;
            foreach (var item in available)
            {
                if (item.Class.ToString() == itemClass.ToString())
                {
                    possible.Add(item);
                    chooseItemCount++;
                }
            }
            chosenItem = random.Next(chooseItemCount);
            return possible[chosenItem];
        }

        private List<Item> getAvailable(int heroItemLvl)
        {
            List<Item> available = new List<Item>();

            foreach (var item in Helmets)
            {
                if (heroItemLvl >= item.Lvl)
                {
                    available.Add(item);
                }
            }
            foreach (var item in Armors)
            {
                if (heroItemLvl >= item.Lvl)
                {
                    available.Add(item);
                }
            }
            foreach (var item in Boots)
            {
                if (heroItemLvl >= item.Lvl)
                {
                    available.Add(item);
                }
            }
            foreach (var item in Weapons)
            {
                if (heroItemLvl >= item.Lvl)
                {
                    available.Add(item);
                }
            }
            return available;
        }

        //int dieValue = random.Next(6) + 1;






















    }
}
