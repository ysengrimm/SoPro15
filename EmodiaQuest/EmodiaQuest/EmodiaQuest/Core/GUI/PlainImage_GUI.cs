using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace EmodiaQuest.Core.GUI
{
    class PlainImage_GUI
    {
        public int XPos { get; set; }
        public int YPos { get; set; }
        private Color color = Color.Black;

        public PlainImage_GUI(int xPos, int yPos, Color color)
        {
            this.XPos = xPos;
            this.YPos = yPos;
            this.color = color;
        }
    }
}
