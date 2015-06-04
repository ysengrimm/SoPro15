using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace EmodiaQuest.Core.GUI
{
    public class Platform_GUI
    {
        public SpriteBatch menuSpritebatch;
        private List<Button_GUI> buttons = new List<Button_GUI>();
        private MouseState mouseHandle;
        private MouseState mouseHandle_Old;
        private string pushed_name = null;
        private Texture2D button_n;
        private Texture2D button_m;
        private Texture2D button_p;
        private Texture2D background;

        public void loadContent(ContentManager Content)
        {
            button_n = Content.Load<Texture2D>("button_normal");
            button_m = Content.Load<Texture2D>("button_mouseOver");
            button_p = Content.Load<Texture2D>("button_pressed");

            background = Content.Load<Texture2D>("white_Pixel");

        }

        public void setBackground(ContentManager Content, string name)
        {
            this.background = Content.Load<Texture2D>(name);
        }

        

        public void update()
        {
            mouseHandle = Controls_GUI.Instance.Mouse_GUI;

            foreach (Button_GUI bb in buttons)
            {
                if (Button_GUI.isInside(mouseHandle.X, mouseHandle.Y, bb.XPos, bb.YPos, bb.Width, bb.Height))
                {

                    if (mouseHandle.LeftButton == ButtonState.Pressed)
                    {

                        if (pushed_name == bb.Name)
                        {
                            bb.Button_State = ButtonState_GUI.Pressed;
                        }
                        if (mouseHandle_Old.LeftButton == ButtonState.Released)
                        {
                            pushed_name = bb.Name;
                        }

                    }
                    else
                        bb.Button_State = ButtonState_GUI.MouseOver;

                    if (mouseHandle.LeftButton == ButtonState.Pressed && mouseHandle_Old.LeftButton == ButtonState.Pressed && pushed_name == bb.Name)
                    {
                        //button.click
                    }



                }
                else
                    bb.Button_State = ButtonState_GUI.Normal;

            }
            mouseHandle_Old = mouseHandle;
        }

        public void draw(SpriteBatch spritebatch)
        {
            spritebatch.Begin();
            spritebatch.Draw(background, new Rectangle(0, 0, 800, 480), Color.White);

            foreach (Button_GUI bb in buttons)
            {
                if (bb.Button_State == ButtonState_GUI.Normal)
                    spritebatch.Draw(button_n, new Rectangle(bb.XPos, bb.YPos, bb.Width, bb.Height), Color.White);
                else if (bb.Button_State == ButtonState_GUI.MouseOver)
                    spritebatch.Draw(button_m, new Rectangle(bb.XPos, bb.YPos, bb.Width, bb.Height), Color.White);
                else if (bb.Button_State == ButtonState_GUI.Pressed)
                    spritebatch.Draw(button_p, new Rectangle(bb.XPos, bb.YPos, bb.Width, bb.Height), Color.White);
            }
            spritebatch.End();
        }

        public void addButton(int xPos, int yPos, int width, int height, string name)
        {
            buttons.Add(new Button_GUI(xPos, yPos, width, height, name));
        }


    }


}
