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
        public float XPosRelative { get; set; }
        public float YPosRelative { get; set; }
        public int XPos { get; set; }
        public int YPos { get; set; }
        public float WidthRelative { get; set; }
        public float HeightRelative { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public string Function { get; set; }
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

        public Slider_GUI(float xPosRelative, float yPosRelative, int xPos, int yPos, float widthRelative, float heightRelative, int width, int height, int minValue, int maxValue, int currentValue, string function)
        {
            this.XPosRelative = xPosRelative;
            this.YPosRelative = yPosRelative;
            this.XPos = xPos;
            this.YPos = yPos;
            this.WidthRelative = widthRelative;
            this.HeightRelative = heightRelative;
            this.Width = width;
            this.Height = height;
            this.CurrentValue = minValue;
            this.MinValue = minValue;
            this.MaxValue = maxValue;
            this.CurrentValue = currentValue;
            this.Function = function;
            setSliderStartPosition(xPos, yPos, width, height);
        }

        public string onRelease()
        {
            return this.Function;
        }

        public void setSliderStartPosition(int XPos, int YPos, int Width, int Height)
        {
            this.SliderWidth = (int)(Height * 0.6);
            this.SliderHeight = (int)(Height * 0.6);

            //this.SliderPosX = XPos + (int)(Width * 0.06) - SliderWidth / 4;
            this.SliderPosX = XPos + (int)(Width * 0.02);
            this.SliderPosY = YPos + (int)(Height * 0.2);

            this.SliderMinX = SliderPosX;
            //this.SliderMaxX = this.XPos + this.Width - SliderWidth - SliderWidth / 4;
            this.SliderMaxX = XPos + Width - (int)(Width * 0.02) - SliderWidth;

            // For each subtraction to get a width, you need to add one!
            this.FactorX = (SliderMaxX-SliderMinX+1) / (float)(MaxValue - MinValue + 1);   
         
            // TRY
            int eValue = CurrentValue;
            float factorXY = 1 / (float)(MaxValue - MinValue);

            int sliderWidth = SliderMaxX - SliderMinX;
            SliderPosX = (int)(SliderMinX + sliderWidth * (factorXY) * (float)(eValue - MinValue));
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