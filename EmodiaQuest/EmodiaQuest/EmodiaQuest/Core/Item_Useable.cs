using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmodiaQuest.Core
{
    class Item_Useable
    {
        public int hitPoints = 0;

        public string itemName = null;
        public Item_Useable(string itemName, int hitPoints)
        {
            this.itemName = itemName;
            this.hitPoints = hitPoints;
        }
    }

    // "Leichter Heiltrank", 20
    // "Normaler Heiltrank", 50
    // "Starker Heiltrank", 100
}
