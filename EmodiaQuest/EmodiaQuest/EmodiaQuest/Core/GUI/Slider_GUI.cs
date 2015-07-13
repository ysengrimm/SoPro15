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
        public int SliderMinX { get; set; }
        public int SliderMaxX { get; set; }
        public int SliderPosX { get; set; }
        public int SliderPosY { get; set; }
        public int SliderWidth { get; set; }
        public int SliderHeight { get; set; }
        public int MaxValue { get; set; }
        public int MinValue { get; set; }
        public int CurrentValue { get; set; }
        public float FactorX { get; set; }

        private SliderState_GUI slider_State = SliderState_GUI.Normal;

        public Slider_GUI(int xPos, int yPos, int width, int height, int minValue, int maxValue, string function)
        {
            this.XPos = xPos;
            this.YPos = yPos;
            this.Width = width;
            this.Height = height;
            this.CurrentValue = minValue;
            this.MinValue = minValue;
            this.MaxValue = maxValue;
            this.Function = function;
            setSliderStartPosition(xPos, yPos, width, height);
        }

        public string onRelease()
        {
            return this.Function;
        }

        private void setSliderStartPosition(int XPos, int YPos, int Width, int Height)
        {
            this.SliderWidth = (int)(Height * 0.6);
            this.SliderHeight = (int)(Height * 0.6);

            //this.SliderPosX = XPos + (int)(Width * 0.06) - SliderWidth / 4;
            this.SliderPosX = XPos + (int)(Width * 0.02);
            this.SliderPosY = YPos + (int)(Height * 0.2);

            this.SliderMinX = SliderPosX;
            //this.SliderMaxX = this.XPos + this.Width - SliderWidth - SliderWidth / 4;
            this.SliderMaxX = this.XPos + this.Width - (int)(this.Width * 0.02) - SliderWidth;

            // For each subtraction to get a width, you need to add one!
            this.FactorX = (SliderMaxX-SliderMinX+1) / (float)(MaxValue - MinValue + 1);            
        }

        public static bool isInside(int mouse_xPos, int mouse_yPos, int slider_xPos, int slider_yPos, int slider_width, int slider_height)
        {
            if (mouse_xPos >= slider_xPos)
            {
                if (mouse_xPos <= slider_xPos + slider_width)
                {
                    if (mouse_yPos >= slider_yPos)
                    {
                        if (mouse_yPos <= slider_yPos + slider_height)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public SliderState_GUI Slider_State
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
    }
}