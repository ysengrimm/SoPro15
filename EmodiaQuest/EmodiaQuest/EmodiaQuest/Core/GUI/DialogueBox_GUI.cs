using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace EmodiaQuest.Core.GUI
{
    public class DialogueBox_GUI
    {
        public float XPosRelative { get; set; }
        public float YPosRelative { get; set; }
        public int XPos { get; set; }
        public int YPos { get; set; }
        public float WidthRelative { get; set; }
        public float HeightRelative { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int TextXPos { get; set; }
        public int TextYPos { get; set; }
        public SpriteFont SpriteFont { get; set; }
        public string LabelText { get; set; }
        public string LabelName { get; set; }
        public float TextScaleFactor { get; set; }
        public bool Centered { get; set; }
        public bool IsVisible = true;


        public DialogueBox_GUI(float xPosRelative, float yPosRelative, int xPos, int yPos, float widthRelative, float heightRelative, int width, int height, SpriteFont spriteFont, string labelText, string labelName, int textXPos, int textYPos, float textScaleFactor)
        {
            this.XPosRelative = xPosRelative;
            this.YPosRelative = yPosRelative;
            this.XPos = xPos;
            this.YPos = yPos;
            this.WidthRelative = widthRelative;
            this.HeightRelative = heightRelative;
            this.Width = width;
            this.Height = height;
            this.SpriteFont = spriteFont;
            this.LabelText = labelText;
            this.LabelName = labelName;
            this.TextXPos = textXPos;
            this.TextYPos = textYPos;
            this.TextScaleFactor = textScaleFactor;
        }
    }
}
