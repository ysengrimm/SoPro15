﻿using System;
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
        public int SliderPosX { get; set; }
        public int SliderPosY { get; set; }
        public int SliderWidth { get; set; }
        public int SliderHeight { get; set; }

        private SliderState_GUI slider_State = SliderState_GUI.Normal;

        public Slider_GUI(int xPos, int yPos, int width, int height, string function)
        {
            this.XPos = xPos;
            this.YPos = yPos;
            this.Width = width;
            this.Height = height;
            this.Function = function;
            setSliderStartPosition(xPos, yPos, width, height);
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
            this.SliderPosX = XPos + (int)(Width * 0.06);
            this.SliderPosY = YPos + (int)(Height * 0.2);
            this.SliderWidth = (int)(Height * 0.6);
            this.SliderHeight = (int)(Height * 0.6);
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