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
        private List<Button_GUI> buttons = new List<Button_GUI>();
        private MouseState mouseHandle;
        private MouseState mouseHandle_Old;
        private string pushed_name = null;
        private string functionCalled = null;

        //private short alphaValue = 255;
        public Color drawColor = Color.White;

        // Button Textures
        private Texture2D button_n;
        private Texture2D button_m;
        private Texture2D button_p;

        // Background Textures
        private Texture2D background;

        public void loadContent(ContentManager Content)
        {
            // Button Content
            button_n = Content.Load<Texture2D>("Content_GUI/button_normal");
            button_m = Content.Load<Texture2D>("Content_GUI/button_mouseOver");
            button_p = Content.Load<Texture2D>("Content_GUI/button_pressed");

            // Background Content
            background = Content.Load<Texture2D>("Content_GUI/pixel_white");


            
        }

        public void setBackground(ContentManager Content, string name)
        {
            this.background = Content.Load<Texture2D>(name);
        }



        public string update()
        {
            functionCalled = null;
            mouseHandle = Controls_GUI.Instance.Mouse_GUI;

            foreach (Button_GUI bb in buttons)
            {
                if (Button_GUI.isInside(mouseHandle.X, mouseHandle.Y, bb.XPos, bb.YPos, bb.Width, bb.Height))
                {

                    if (mouseHandle.LeftButton == ButtonState.Pressed)
                    {

                        if (pushed_name == bb.Function)
                        {
                            bb.Button_State = ButtonState_GUI.Pressed;
                        }
                        if (mouseHandle_Old.LeftButton == ButtonState.Released)
                        {
                            pushed_name = bb.Function;
                        }

                    }
                    else
                        bb.Button_State = ButtonState_GUI.MouseOver;

                    if (mouseHandle.LeftButton == ButtonState.Released && mouseHandle_Old.LeftButton == ButtonState.Pressed && pushed_name == bb.Function)
                    {
                        this.functionCalled = bb.onClick();
                    }



                }
                else
                    bb.Button_State = ButtonState_GUI.Normal;

            }
            mouseHandle_Old = mouseHandle;
            return this.functionCalled;
        }

        public void draw(SpriteBatch spritebatch)
        {
            spritebatch.Begin();

            // Beware: Hardcoded values...
            //spritebatch.Draw(background, new Rectangle(0, 0, 800, 480), Color.White);
            spritebatch.Draw(background, new Rectangle(0, 0, 800, 480), drawColor);

            foreach (Button_GUI bb in buttons)
            {
                if(bb.isVisible)
                {
                    if (bb.Button_State == ButtonState_GUI.Normal)
                        spritebatch.Draw(button_n, new Rectangle(bb.XPos, bb.YPos, bb.Width, bb.Height), drawColor);
                    else if (bb.Button_State == ButtonState_GUI.MouseOver)
                        spritebatch.Draw(button_m, new Rectangle(bb.XPos, bb.YPos, bb.Width, bb.Height), drawColor);
                    else if (bb.Button_State == ButtonState_GUI.Pressed)
                        spritebatch.Draw(button_p, new Rectangle(bb.XPos, bb.YPos, bb.Width, bb.Height), drawColor);
                }
            }
            spritebatch.End();
        }

        public void addButton(int xPos, int yPos, int width, int height, string name)
        {
            buttons.Add(new Button_GUI(xPos, yPos, width, height, name));
        }
        public void addButton(int xPos, int yPos, int width, int height, string name, bool isVisible)
        {
            buttons.Add(new Button_GUI(xPos, yPos, width, height, name, isVisible));
        }

        // This doesn't actually lighten anything up. It just pumps the alpha to the max
        public void lightenUp()
        {
            if (drawColor.A < 254)
                drawColor.A++;
        }



    }


}
