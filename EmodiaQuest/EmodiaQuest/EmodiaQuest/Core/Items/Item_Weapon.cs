using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmodiaQuest.Core
{
    class Item_Weapon : Item_ValueParent
    {
        int minDam = 0;
        int maxDam = 0;
        int requiredStrength = 0;
        
        public Item_Weapon(string itemName, int minDam, int maxDam, int requiredStrength, int strength, int tenacity)
        {
            this.itemName = itemName;
            this.minDam = minDam;
            this.maxDam = maxDam;
            this.requiredStrength = requiredStrength;
            this.strength = strength;
            this.tenacity = tenacity;
        }

        // "Leichtes Holzschwert", 0, 2, 3, 0, 0
        // "Schweres Holzschwert", 0, 4, 5, 0, 0

        // "Leichtes Kupferschwert", 2, 6, 6, 0, 0
        // "Schweres Kupferschwert", 4, 8, 8, 0, 0

        // "Leichtes Eisenschwert", 6, 12, 10, 0, 0
        // "Schweres Eisenschwert", 6, 14, 12, 0, 0

        // "Leichtes Rowenschwert", 10, 15, 16, 0, 0
        // "Schweres Rowenschwert", 30, 50, 35, 0, 0
    }
}
