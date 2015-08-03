using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmodiaQuest.Core.GUI
{
    public class InventoryItem_GUI
    {
        public float XPosRelative { get; set; }
        public float YPosRelative { get; set; }
        public int XPos { get; set; }
        public int YPos { get; set; }
        public float WidthRelative { get; set; }
        public float HeightRelative { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public bool isEmpty = true;
        public bool IsEmpty { get { return this.isEmpty; } set { this.isEmpty = value; } }

        public InventoryItem_GUI(float xPosRelative, float yPosRelative, int xPos, int yPos, float widthRelative, float heightRelative, int width, int height)
        {
            this.XPosRelative = xPosRelative;
            this.YPosRelative = yPosRelative;
            this.XPos = xPos;
            this.YPos = yPos;
            this.WidthRelative = widthRelative;
            this.HeightRelative = heightRelative;
            this.Width = width;
            this.Height = height;
        }
    }
}
