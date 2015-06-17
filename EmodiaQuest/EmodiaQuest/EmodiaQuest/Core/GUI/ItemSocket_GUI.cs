using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmodiaQuest.Core.GUI
{
    class ItemSocket_GUI
    {
        public int XPos { get; set; }
        public int YPos { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public string ItemName { get; set; }

        public ItemSocket_GUI(int xPos, int yPos)
        {
            this.XPos = xPos;
            this.YPos = yPos;
            this.ItemName = null;
        }

        public ItemSocket_GUI(int xPos, int yPos, string itemName)
        {
            this.XPos = xPos;
            this.YPos = yPos;
            this.ItemName = itemName;
        }

        public bool ItemIsPicked { get; set; }
        public bool ItemOn { get; set; }

        public void itemBought()
        {

        }
    }
}
