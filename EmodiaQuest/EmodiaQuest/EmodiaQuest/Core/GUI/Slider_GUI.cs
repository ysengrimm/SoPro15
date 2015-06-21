using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace EmodiaQuest.Core.GUI
{
    class Slider_GUI
    {
        // Slider geht von 6 bis 94 Prozent
        // Und Breite ist 7 Mal die Hoehe
        public int XPos { get; set; }
        public int YPos { get; set; }
        public string Function { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int sliderPosX { get; set; }
        public int sliderPosY { get; set; }
        public int sliderWidth { get; set; }
        public int sliderHeight { get; set; }

        private SliderState_GUI slider_State = SliderState_GUI.Normal;

        public Slider_GUI(int xPos, int yPos, int width, int height, string function)
        {
            this.XPos = xPos;
            this.YPos = yPos;
            this.Width = width;
            this.Height = height;
            this.Function = function;
        }

        public string onRelease()
        {
            return this.Function;
        }

        public SliderState_GUI Button_State
        {
            get
            {
                return this.slider_State;
            }
            set
            {
                this.slider_State = value;
            }
        }

        private void setSliderStartPosition(int XPos, int YPos, int Width, int Height)
        {

        }
    }
}
