using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace EmodiaQuest.Core.GUI
{
    public class PlainText_GUI
    {
        public int XPos { get; set; }
        public int YPos { get; set; }
        public SpriteFont SpriteFont { get; set; }
        public string Text { get; set; }
        public bool Centered { get; set; }
        public Vector2 Size { get; set; }

        public PlainText_GUI(int xPos, int yPos, SpriteFont spriteFont, string text, bool Centered, Vector2 Size)
        {
            this.XPos = xPos;
            this.YPos = yPos;
            this.SpriteFont = spriteFont;
            this.Text = text;
            this.Centered = Centered;
            this.Size = Size;
        }
    }
}
