using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace EmodiaQuest.Core.GUI
{
    class PlainText_GUI
    {
        public int XPos { get; set; }
        public int YPos { get; set; }
        public SpriteFont SpriteFont { get; set; }
        public string Text { get; set; }

        public PlainText_GUI(int xPos, int yPos, SpriteFont spriteFont, string text)
        {
            this.XPos = xPos;
            this.YPos = yPos;
            this.SpriteFont = spriteFont;
            this.Text = text;
        }
    }
}
