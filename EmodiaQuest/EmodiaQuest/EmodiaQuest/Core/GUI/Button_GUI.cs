﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace EmodiaQuest.Core.GUI
{
    class Button_GUI
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
        public bool IsVisible = true;
        public int TextXPos { get; set; }
        public int TextYPos { get; set; }
        public float TextScaleFactor { get; set; }
        public bool isClickable = true;

        private string buttonText = null;
        private ButtonState_GUI button_State = ButtonState_GUI.Normal;

        public Button_GUI(float xPosRelative, float yPosRelative, int xPos, int yPos, float widthRelative, float heightRelative, int width, int height, string function, string buttonText, int textXPos, int textYPos, float textScaleFactor)
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
            this.ButtonText = buttonText;
            this.TextXPos = textXPos;
            this.TextYPos = textYPos;
            this.TextScaleFactor = textScaleFactor;
        }

        public Button_GUI(float xPosRelative, float yPosRelative, int xPos, int yPos, float widthRelative, float heightRelative, int width, int height, string function, bool isVisible)
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
            this.IsVisible = isVisible;
        }

        public static bool isInside(int mouse_xPos, int mouse_yPos, int button_xPos, int button_yPos, int button_width, int button_height)
        {
            if (mouse_xPos >= button_xPos)
            {

                if (mouse_xPos <= button_xPos + button_width)
                {
                    if (mouse_yPos >= button_yPos)
                    {
                        if (mouse_yPos <= button_yPos + button_height)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        //public string onClick()
        //{
        //    return this.Function;
        //}

        public ButtonState_GUI Button_State
        {
            get
            {
                return this.button_State;
            }
            set
            {
                this.button_State = value;
            }
        }

        public string ButtonText
        {
            get
            {
                return this.buttonText;
            }
            set
            {
                this.buttonText = value;
            }
        }

    }
}
