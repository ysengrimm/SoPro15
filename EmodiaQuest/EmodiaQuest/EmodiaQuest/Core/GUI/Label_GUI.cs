using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace EmodiaQuest.Core.GUI
{
    public class Label_GUI
    {
        public int XPos { get; set; }
        public int YPos { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int TextXPos { get; set; }
        public int TextYPos { get; set; }
        public SpriteFont SpriteFont { get; set; }
        public string LabelText { get; set; }
        public float TextScaleFactor { get; set; }

        public Label_GUI(int xPos, int yPos, int width, int height, SpriteFont spriteFont, string labelText, float textScaleFactor)
        {
            this.XPos = xPos;
            this.YPos = yPos;
            this.Width = width;
            this.Height = height;
            this.SpriteFont = spriteFont;
            this.LabelText = labelText;
            this.TextScaleFactor = textScaleFactor;
        }

        public Label_GUI(int xPos, int yPos, int width, int height, SpriteFont spriteFont, string labelText, int textXPos, int textYPos, float textScaleFactor)
        {
            this.XPos = xPos;
            this.YPos = yPos;
            this.Width = width;
            this.Height = height;
            this.SpriteFont = spriteFont;
            this.LabelText = labelText;
            this.TextXPos = textXPos;
            this.TextYPos = textYPos;
            this.TextScaleFactor = textScaleFactor;
        }
    }
}
