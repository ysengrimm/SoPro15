using System;
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
        public int XPos { get; set; }
        public int YPos { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public string Function { get; set; }
        public bool isVisible = true;
        private string buttonText = null;
        public int TextXPos { get; set; }
        public int TextYPos { get; set; }
        public float textScaleFactor { get; set; }

        private ButtonState_GUI button_State = ButtonState_GUI.Normal;

        public Button_GUI(int xPos, int yPos, int width, int height, string function)
        {
            this.XPos = xPos;
            this.YPos = yPos;
            this.Width = width;
            this.Height = height;
            this.Function = function;
        }

        public Button_GUI(int xPos, int yPos, int width, int height, string function, string buttonText, int textXPos, int textYPos, float textScaleFactor)
        {
            this.XPos = xPos;
            this.YPos = yPos;
            this.Width = width;
            this.Height = height;
            this.Function = function;
            this.ButtonText = buttonText;
            this.TextXPos = textXPos;
            this.TextYPos = textYPos;
            this.textScaleFactor = textScaleFactor;
        }

        public Button_GUI(int xPos, int yPos, int width, int height, string function, bool isVisible)
        {
            this.XPos = xPos;
            this.YPos = yPos;
            this.Width = width;
            this.Height = height;
            this.Function = function;
            this.isVisible = isVisible;
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

        public string onClick()
        {
            return this.Function;
        }

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
