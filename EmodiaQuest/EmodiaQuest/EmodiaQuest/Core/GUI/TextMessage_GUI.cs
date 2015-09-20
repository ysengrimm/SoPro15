using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace EmodiaQuest.Core.GUI
{
    public class TextMessage_GUI
    {
        public int XPos { get; set; }
        public int YPos { get; set; }

        public int Width { get; set; }
        public int Height { get; set; }

        //public string Function { get; set; }

        public string Text { get; set; }
        public Color BackgroundColor = new Color(0, 0, 0, 50);
        public Color CircleColor = Color.White;
        public int CirclePosition = 0;
        public Color TextColor = Color.White;
        private float circleOverrule = 255;

        //public bool IsVisible = true;

        public int TextXPos { get; set; }
        public int TextYPos { get; set; }

        //public bool isClickable = true;

        // Constructor
        public TextMessage_GUI(int XPos, int YPos, int Width, int Height, int TextXPos, int TextYPos, string Text, Color BackgroundColor)
        {
            this.XPos = XPos;
            this.YPos = YPos;
            this.Width = Width;
            this.Height = Height;
            this.TextXPos = TextXPos;
            this.TextYPos = TextYPos;
            this.Text = Text;           
            this.BackgroundColor = BackgroundColor;
            this.CirclePosition = (int)(TextXPos * 0.5f);
        }

        public TextMessage_GUI(int XPos, int YPos, int Width, int Height, int TextXPos, int TextYPos, string Text, Color BackgroundColor, Color CircleColor)
        {
            this.XPos = XPos;
            this.YPos = YPos;
            this.Width = Width;
            this.Height = Height;
            this.TextXPos = TextXPos;
            this.TextYPos = TextYPos;
            this.Text = Text;
            this.BackgroundColor = BackgroundColor;
            this.CirclePosition = (int)(TextXPos * 0.5f);
            this.CircleColor = CircleColor;
        }
    }
}
