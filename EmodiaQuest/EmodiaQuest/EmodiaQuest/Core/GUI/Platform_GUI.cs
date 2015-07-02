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

        //MainwindowRes
        public static Vector2 MainWindowSize { get; set; } 
        public static int MainWindowWidth { get; set; }
        public static int MainwindowHeight { get; set; }


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

        // Fontsizes and Scales
        private float fontFactor_dice_big = 0.41458f;
        private float fontFactor_monoFont_big = 0.2083f;
        private float fontFactor_monoFont_small = 0.0979f;
        private float scaleFactor_dice_big;
        private float scaleFactor_monoFont_big;
        private float scaleFactor_monoFont_small;

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
            
            
            // Set resolution
            // This gets called for every Menu. Should change
            setResolution();
            setFontSize();

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
            foreach (Slider_GUI sl in sliders)
            {
                int sliderhandle = mouseHandle.X - sl.SliderWidth / 2;
                if (mouseHandle.LeftButton == ButtonState.Pressed)
                {
                    
                    if (Slider_GUI.isInside(mouseHandle.X, mouseHandle.Y, sl.SliderPosX, sl.SliderPosY, sl.SliderWidth, sl.SliderHeight))
                    {
                        sl.Slider_State = SliderState_GUI.Pressed;
                        //if (pushed_name == sl.Function)
                        //{
                            
                        //    //bb.Button_State = ButtonState_GUI.Pressed;
                        //    // Direkte Übergabe? Ne, da das mit Grafik-Stuff schief geht
                        //}
                        if (mouseHandle_Old.LeftButton == ButtonState.Released)
                        {
                            pushed_name = sl.Function;
                        }
                    }

                    if (pushed_name == sl.Function)
                    {    
                        sl.SliderPosX = sliderhandle;
                        if (sliderhandle <= sl.SliderMinX)
                            sl.SliderPosX = sl.SliderMinX;
                        else if (sliderhandle >= sl.SliderMaxX)
                            sl.SliderPosX = sl.SliderMaxX;
                    }                        
                }

                if (mouseHandle.LeftButton == ButtonState.Released && mouseHandle_Old.LeftButton == ButtonState.Pressed && pushed_name == sl.Function)
                {
                    pushed_name = null;
                    sl.Slider_State = SliderState_GUI.Normal;
                    //Console.WriteLine(monoFont_small.MeasureString("A").Y);
                    //Console.WriteLine(monoFont_big.MeasureString("MAIN MENU").X);
                    //Console.WriteLine((int)((sl.SliderPosX - sl.SliderMinX) / sl.FactorX));
                }
            }

            mouseHandle_Old = mouseHandle;
            return this.functionCalled;
        }

        public void draw(SpriteBatch spritebatch)
        {
            spritebatch.Begin();

            // Beware: Hardcoded values...
            //spritebatch.Draw(background, new Rectangle(0, 0, 800, 480), Color.White);
            // Windows-Standard-Size ist 800x480

            if (backgroundEnabled)
            {
                spritebatch.Draw(overlay, new Rectangle(0, 0, MainWindowWidth, MainwindowHeight), Color.White);
                spritebatch.Draw(background, new Rectangle(0, 0, MainWindowWidth, MainwindowHeight), drawColor);
            }




            //spritebatch.Draw(overlay, new Rectangle(0, 0, 800, 480), Color.White*0.7f);


            foreach (Button_GUI bb in buttons)
            {
                if (bb.isVisible)
                {
                    if (bb.Button_State == ButtonState_GUI.Normal)
                        spritebatch.Draw(button_n, new Rectangle(bb.XPos, bb.YPos, bb.Width, bb.Height), drawColor);
                    else if (bb.Button_State == ButtonState_GUI.MouseOver)
                        spritebatch.Draw(button_m, new Rectangle(bb.XPos, bb.YPos, bb.Width, bb.Height), drawColor);
                    else if (bb.Button_State == ButtonState_GUI.Pressed)
                        spritebatch.Draw(button_p, new Rectangle(bb.XPos, bb.YPos, bb.Width, bb.Height), drawColor);
                }
                if (bb.ButtonText != null)
                {
                    //spritebatch.DrawString(monoFont_small, bb.ButtonText, new Vector2(pt.XPos, pt.YPos), drawColor, 0.0f, new Vector2(0.0f, 0.0f), 2.7f, SpriteEffects.None, 0.0f);
                }
            }
            foreach (PlainImage_GUI pi in pimages)               
                spritebatch.Draw(plainImage, new Rectangle(pi.XPos, pi.YPos, pi.Width, pi.Height), pi.Color);

            foreach (PlainText_GUI pt in ptexts)
                spritebatch.DrawString(pt.SpriteFont, pt.Text, new Vector2(pt.XPos, pt.YPos), drawColor);
                //spritebatch.DrawString(pt.SpriteFont, pt.Text, new Vector2(pt.XPos, pt.YPos), drawColor, 0.0f, new Vector2(0.0f,0.0f), 1.86f, SpriteEffects.None, 0.0f);
            foreach (Slider_GUI sl in sliders)
            {
                spritebatch.Draw(slider_background, new Rectangle(sl.XPos, sl.YPos, sl.Width, sl.Height), drawColor);
                if (sl.Slider_State == SliderState_GUI.Normal)
                    spritebatch.Draw(slider_foreground_normal, new Rectangle(sl.SliderPosX, sl.SliderPosY, sl.SliderWidth, sl.SliderHeight), drawColor);
                else
                    spritebatch.Draw(slider_foreground_pressed, new Rectangle(sl.SliderPosX, sl.SliderPosY, sl.SliderWidth, sl.SliderHeight), drawColor);
            }


            spritebatch.End();
        }

        public void setResolution()
        {
            MainWindowSize = EmodiaQuest.Core.Settings.Instance.Resolution;
            MainWindowWidth = (int)EmodiaQuest.Core.Settings.Instance.Resolution.X;
            MainwindowHeight = (int)EmodiaQuest.Core.Settings.Instance.Resolution.Y;
        }

        public void setFontSize()
        {
            this.scaleFactor_dice_big = fontFactor_dice_big / MainWindowSize.Y;
            this.scaleFactor_monoFont_big = scaleFactor_monoFont_big / MainWindowSize.Y;
            this.scaleFactor_monoFont_small = scaleFactor_monoFont_small / MainWindowSize.Y;
            //fontFactor_dice_big
            //fontSize_dice_big = dice_big.MeasureString("A");
            //fontSize_monoFont_big = monoFont_big.MeasureString("A");
            //fontSize_monoFont_small = monoFont_small.MeasureString("A");
        }

        public void addButton(float xPos, float yPos, float width, float height, string name)
        {
            int xPosAbs = (int)(MainWindowSize.X * xPos * 0.01);
            int yPosAbs = (int)(MainWindowSize.Y * yPos * 0.01);
            int widthAbs = (int)(MainWindowSize.X * width * 0.01);
            int heightAbs = (int)(MainWindowSize.Y * height * 0.01);
            buttons.Add(new Button_GUI(xPosAbs, yPosAbs, widthAbs, heightAbs, name));
        }
        public void addButton(float xPos, float yPos, float width, float height, string name, string buttonText)
        {
            int xPosAbs = (int)(MainWindowSize.X * xPos * 0.01);
            int yPosAbs = (int)(MainWindowSize.Y * yPos * 0.01);
            int widthAbs = (int)(MainWindowSize.X * width * 0.01);
            int heightAbs = (int)(MainWindowSize.Y * height * 0.01);
            //int textX = (int)(xPos + (width-monoFont_small.MeasureString(buttonText).X)/2);
            //int textY = (int)monoFont_small.MeasureString(buttonText).Y;
            buttons.Add(new Button_GUI(xPosAbs, yPosAbs, widthAbs, heightAbs, name, buttonText));
        }
        public void addButton(float xPos, float yPos, float width, float height, string name, bool isVisible)
        {
            int xPosAbs = (int)(MainWindowSize.X * xPos * 0.01);
            int yPosAbs = (int)(MainWindowSize.Y * yPos * 0.01);
            int widthAbs = (int)(MainWindowSize.X * width * 0.01);
            int heightAbs = (int)(MainWindowSize.Y * height * 0.01);
            buttons.Add(new Button_GUI(xPosAbs, yPosAbs, widthAbs, heightAbs, name, isVisible));
        }

        public void addPlainText(float xPos, float yPos, string chooseFont, string text)
        {
            int xPosAbs = (int)(MainWindowSize.X * xPos * 0.01);
            int yPosAbs = (int)(MainWindowSize.Y * yPos * 0.01);
            switch (chooseFont)
            {
                case "dice_big":
                    ptexts.Add(new PlainText_GUI(xPosAbs, yPosAbs, dice_big, text));
                    break;
                case "monoFont_big":
                    ptexts.Add(new PlainText_GUI(xPosAbs, yPosAbs, monoFont_big, text));
                    break;
                case "monoFont_small":
                    ptexts.Add(new PlainText_GUI(xPosAbs, yPosAbs, monoFont_small, text));
                    break;
                default:
                    Console.WriteLine("No such font");
                    break;
            }
        }

        public void addPlainImage(float xPos, float yPos, float width, float height, Color color)
        {
            int xPosAbs = (int)(MainWindowSize.X * xPos * 0.01);
            int yPosAbs = (int)(MainWindowSize.Y * yPos * 0.01);
            int widthAbs = (int)(MainWindowSize.X * width * 0.01);
            int heightAbs = (int)(MainWindowSize.Y * height * 0.01);
            pimages.Add(new PlainImage_GUI(xPosAbs, yPosAbs, widthAbs, heightAbs, color));
        }

        public void addSlider(float xPos, float yPos, float width, float height, int minValue, int maxValue, string name)
        {
            int xPosAbs = (int)(MainWindowSize.X * xPos * 0.01);
            int yPosAbs = (int)(MainWindowSize.Y * yPos * 0.01);
            int widthAbs = (int)(MainWindowSize.X * width * 0.01);
            int heightAbs = (int)(MainWindowSize.Y * height * 0.01);
            sliders.Add(new Slider_GUI(xPosAbs, yPosAbs, widthAbs, heightAbs, minValue, maxValue, name));
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
