using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace EmodiaQuest.Core.GUI
{
    class Button_GUI
    {
        public int XPos { get; set; }
        public int YPos { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public string Name { get; set; }
        public int Score { get; set; }

        private static Texture2D button_n;
        private static Texture2D button_m;
        private static Texture2D button_p;

        private ButtonState_GUI button_State = ButtonState_GUI.Normal;

        public Button_GUI(int xPos, int yPos, int width, int height, string name)
        {
            this.XPos = xPos;
            this.YPos = yPos;
            this.Width = width;
            this.Height = height;
            this.Name = name;
        }

        public static void loadContent(ContentManager Content)
        {
            button_n = Content.Load<Texture2D>("button_normal");
            button_m = Content.Load<Texture2D>("button_mouseOver");
            button_p = Content.Load<Texture2D>("button_pressed");
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
    }
}
