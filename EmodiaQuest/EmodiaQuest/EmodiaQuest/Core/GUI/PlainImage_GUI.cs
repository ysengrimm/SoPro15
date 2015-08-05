using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EmodiaQuest.Core.GUI
{
    class PlainImage_GUI
    {
        public float XPosRelative { get; set; }
        public float YPosRelative { get; set; }
        public int XPos { get; set; }
        public int YPos { get; set; }
        public float WidthRelative { get; set; }
        public float HeightRelative { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public string Function { get; set; }
        public Texture2D Image { get; set; }
        private bool isVisible = true;
        public bool IsVisible { get { return this.isVisible; } set { this.isVisible = value; } }

        public PlainImage_GUI(float xPosRelative, float yPosRelative, int xPos, int yPos, float widthRelative, float heightRelative, int width, int height, string function, Texture2D image)
        {
            this.XPosRelative = xPosRelative;
            this.YPosRelative = yPosRelative;
            this.XPos = xPos;
            this.YPos = yPos;
            this.WidthRelative = widthRelative;
            this.HeightRelative = heightRelative;
            this.Width = width;
            this.Height = height;
            this.Function = function;
            this.Image = image;
        }

    }
}
