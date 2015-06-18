using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmodiaQuest.Core
{
    class Item_Armor : Item_ValueParent
    {
        public int requiredStrength = 0;
        public Item_Armor(string itemName, int requiredStrength, int strength, int tenacity)
        {
            this.itemName = itemName;
            this.requiredStrength = requiredStrength;
            this.strength = strength;
            this.tenacity = tenacity;
        }
    }

    // "Leichter Stoff", 0, 0, 3
    // "Schwerer Stoff", 5, 0, 5

    // "Leichtes Leder", 8, 0, 8
    // "Schweres Leder", 10, 0, 10

    // "Leichtes Eisen", 15, 0, 12
    // "Schweres Eisen", 20, 0, 15

    // "Leichtes Rowen", 30, 0, 20
    // "Schweres Rowen", 50, 0, 25


}
