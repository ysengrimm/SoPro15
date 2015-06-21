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
        private List<PlainText_GUI> ptexts = new List<PlainText_GUI>();
        private List<ItemSocket_GUI> sockets = new List<ItemSocket_GUI>();
        private List<PlainImage_GUI> pimages = new List<PlainImage_GUI>();
        private List<Slider_GUI> sliders = new List<Slider_GUI>();
        private MouseState mouseHandle;
        private MouseState mouseHandle_Old;
        private string pushed_name = null;
        private string functionCalled = null;
        

        //private short alphaValue = 255;
        public Color drawColor = Color.White;
        public bool backgroundEnabled = true;
        private Color overlayColor = Color.White;
        private float overlayValue = 0;
        private bool overlayBool = true;
        

        // Button Textures
        private Texture2D button_n;
        private Texture2D button_m;
        private Texture2D button_p;

        // ItemSocket Textures
        private Texture2D itemSocket;

        // Background Textures
        private Texture2D background;
        private Texture2D overlay;

        // PlainImages Textures
        private Texture2D plainImage;

        // Slider Textures
        private Texture2D slider_background;
        private Texture2D slider_foreground_normal;
        private Texture2D slider_foreground_pressed;

        // Fonts
        private SpriteFont monoFont_big;
        private SpriteFont dice_big;
        private SpriteFont monoFont_small;

        public void loadContent(ContentManager Content)
        {
            // Button Content
            button_n = Content.Load<Texture2D>("Content_GUI/button_normal");
            button_m = Content.Load<Texture2D>("Content_GUI/button_mouseOver");
            button_p = Content.Load<Texture2D>("Content_GUI/button_pressed");

            // ItemSocket Content
            itemSocket = Content.Load<Texture2D>("Content_GUI/itemSocket");

            // Background Content
            background = Content.Load<Texture2D>("Content_GUI/pixel_black");
            overlay = Content.Load<Texture2D>("Content_GUI/pixel_white");

            // PlainImage Content
            plainImage = Content.Load<Texture2D>("Content_GUI/pixel_black");

            // Slider Content
            slider_background = Content.Load<Texture2D>("Content_GUI/slider_background");
            slider_foreground_normal = Content.Load<Texture2D>("Content_GUI/slider_foreground_normal");
            slider_foreground_pressed = Content.Load<Texture2D>("Content_GUI/slider_foreground_pressed");

            // Load Fonts
            monoFont_big = Content.Load<SpriteFont>("Content_GUI/monoFont_big");
            dice_big = Content.Load<SpriteFont>("Content_GUI/diceFont_big");
            monoFont_small = Content.Load<SpriteFont>("Content_GUI/monoFont_small");
            //Console.WriteLine(monoFont_big.MeasureString("12345"));
            
        }

        public void setBackground(ContentManager Content, string name)
        {
            this.background = Content.Load<Texture2D>(name);
        }

        public void backgroundOff()
        {
            this.backgroundEnabled = false;
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
            //foreach (Slider_GUI sl in sliders)
            //{
            //    if (Button_GUI.isInside(mouseHandle.X, mouseHandle.Y, bb.XPos, bb.YPos, bb.Width, bb.Height))
            //    {
            //        if (mouseHandle.LeftButton == ButtonState.Pressed)
            //        {
            //            if (pushed_name == bb.Function)
            //            {
            //                bb.Button_State = ButtonState_GUI.Pressed;
            //            }
            //            if (mouseHandle_Old.LeftButton == ButtonState.Released)
            //            {
            //                pushed_name = bb.Function;
            //            }
            //        }
            //        else
            //            bb.Button_State = ButtonState_GUI.MouseOver;

            //        if (mouseHandle.LeftButton == ButtonState.Released && mouseHandle_Old.LeftButton == ButtonState.Pressed && pushed_name == bb.Function)
            //        {
            //            this.functionCalled = bb.onClick();
            //        }
            //    }
            //    else
            //        bb.Button_State = ButtonState_GUI.Normal;
            //}

            mouseHandle_Old = mouseHandle;
            return this.functionCalled;
        }

        public void draw(SpriteBatch spritebatch)
        {
            spritebatch.Begin();

            // Beware: Hardcoded values...
            //spritebatch.Draw(background, new Rectangle(0, 0, 800, 480), Color.White);
            // Windows-Standard-Size ist 800x480

            if(backgroundEnabled)
            {
                spritebatch.Draw(overlay, new Rectangle(0, 0, 800, 480), Color.White);
                spritebatch.Draw(background, new Rectangle(0, 0, 800, 480), drawColor);
            }
            
            

            
            //spritebatch.Draw(overlay, new Rectangle(0, 0, 800, 480), Color.White*0.7f);
            

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
            foreach (PlainImage_GUI pi in pimages)
                spritebatch.Draw(plainImage, new Rectangle(pi.XPos, pi.YPos, pi.Width, pi.Height), pi.Color);

            foreach (PlainText_GUI pt in ptexts)
                spritebatch.DrawString(pt.SpriteFont, pt.Text, new Vector2(pt.XPos, pt.YPos), drawColor);

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

        public void addPlainText(int xPos, int yPos, string chooseFont, string text)
        {
            switch (chooseFont)
            {
                case "dice_big":
                    ptexts.Add(new PlainText_GUI(xPos, yPos, dice_big, text));
                    break;
                case "monoFont_big":
                    ptexts.Add(new PlainText_GUI(xPos, yPos, monoFont_big, text));
                    break;
                case "monoFont_small":
                    ptexts.Add(new PlainText_GUI(xPos, yPos, monoFont_small, text));
                    break;
                default:
                    Console.WriteLine("No such font");
                    break;
            }
        }

        public void addPlainImage(int xPos, int yPos, int width, int height, Color color)
        {
            pimages.Add(new PlainImage_GUI(xPos, yPos, width, height, color));
        }

        public void addSlider(int xPos, int yPos, int width, int height, string name)
        {
            sliders.Add(new Slider_GUI(xPos, yPos, width, height, name));
        }
                

        public void breathing()
        {
            if (this.overlayBool)
                this.overlayValue += 0.01f;
            else
                this.overlayValue -= 0.01f;
            if (this.overlayValue > 0.88)
                this.overlayBool = false;
            if (this.overlayValue < 0.22)
                this.overlayBool = true;
            this.drawColor = Color.White * overlayValue;
        }





    }


}
