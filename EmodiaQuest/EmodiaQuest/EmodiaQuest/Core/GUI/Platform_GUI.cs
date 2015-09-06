﻿using System;
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
        // Selfcontaining List
        public static List<Platform_GUI> platforms = new List<Platform_GUI>();



        // Eventhandler
        public event GUI_Delegate_Slider OnSliderValue;
        public event GUI_Delegate_Button OnButtonValue;

        private List<Button_GUI> buttons = new List<Button_GUI>();
        private List<PlainText_GUI> ptexts = new List<PlainText_GUI>();
        private List<ItemSocket_GUI> sockets = new List<ItemSocket_GUI>();
        private List<PlainImage_GUI> pimages = new List<PlainImage_GUI>();
        private List<Slider_GUI> sliders = new List<Slider_GUI>();
        private List<Label_GUI> labels = new List<Label_GUI>();
        private List<DialogueBox_GUI> dialogues = new List<DialogueBox_GUI>();

        private List<InventoryItem_GUI> invItems = new List<InventoryItem_GUI>();

        // Items
        public List<Items.Item> CharItems = new List<Items.Item>();

        public static List<SpriteFonts_GUI> fonts = new List<SpriteFonts_GUI>();

        private MouseState mouseHandle;
        private MouseState mouseHandle_Old;
        private string pushed_name_button = null;
        private string pushed_name_slider = null;
        //private string functionCalled = null;

        // MainwindowRes
        public static IntVector2 MainWindowSize { get; set; }
        public static int MainWindowWidthInt { get; set; }
        public static int MainWindowHeightInt { get; set; }


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
        private Texture2D HUD_small;
        private Texture2D pixel_black;
        private Texture2D pixel_white;
        private Texture2D pixel_red;

        private Texture2D icon_armor;
        private Texture2D icon_boot;
        private Texture2D icon_helmet;
        private Texture2D icon_quest;
        private Texture2D icon_weapon;
        private Texture2D icon_useable;

        // Slider Textures
        private Texture2D slider_background;
        private Texture2D slider_foreground_normal;
        private Texture2D slider_foreground_pressed;

        // Label Textures
        private Texture2D label;

        // Dialogue Textures
        private Texture2D dialogue;

        // Test Textures
        private Texture2D test1;
        private Texture2D test2;

        // Item Content
        private Texture2D itemPicture;

        // Fonts
        private SpriteFont monoFont_big;
        private SpriteFont dice_big;
        private SpriteFont monoFont_small;

        public static float OverallFontScale;

        ContentManager platformContent;

        // Fontsizes and Scales
        //private float fontFactor_dice_big = 0.41458f;
        //private float fontFactor_monoFont_big = 0.2083f;
        //private float fontFactor_monoFont_small = 0.0979f;
        //private float scaleFactor_dice_big;
        //private float scaleFactor_monoFont_big;
        //private float scaleFactor_monoFont_small;

        public void loadContent(ContentManager Content)
        {
            this.platformContent = Content;

            // Button Content
            //button_n = Content.Load<Texture2D>("Content_GUI/button_normal");
            button_n = Content.Load<Texture2D>("Content_GUI/button_normal");
            button_m = Content.Load<Texture2D>("Content_GUI/button_mouseOver");
            button_p = Content.Load<Texture2D>("Content_GUI/button_pressed");

            // ItemSocket Content
            itemSocket = Content.Load<Texture2D>("Content_GUI/itemSocket");

            // Background Content
            background = Content.Load<Texture2D>("Content_GUI/pixel_black");
            overlay = Content.Load<Texture2D>("Content_GUI/pixel_white");

            // PlainImage Content
            HUD_small = Content.Load<Texture2D>("Content_GUI/HUD_small");
            pixel_black = Content.Load<Texture2D>("Content_GUI/pixel_black");
            pixel_white = Content.Load<Texture2D>("Content_GUI/pixel_white");
            pixel_red = Content.Load<Texture2D>("Content_GUI/pixel_red");

            icon_armor = Content.Load<Texture2D>("fbxContent/items/icon_armor");
            icon_boot = Content.Load<Texture2D>("fbxContent/items/icon_boot");
            icon_helmet = Content.Load<Texture2D>("fbxContent/items/icon_helmet");
            icon_quest = Content.Load<Texture2D>("fbxContent/items/icon_quest");
            icon_weapon = Content.Load<Texture2D>("fbxContent/items/icon_weapon");
            icon_useable = Content.Load<Texture2D>("fbxContent/items/icon_useable");



            // Slider Content
            slider_background = Content.Load<Texture2D>("Content_GUI/slider_background_new");
            slider_foreground_normal = Content.Load<Texture2D>("Content_GUI/slider_foreground_normal");
            slider_foreground_pressed = Content.Load<Texture2D>("Content_GUI/slider_foreground_pressed");

            // Label Content
            label = Content.Load<Texture2D>("Content_GUI/label");

            // Dialogue Content
            dialogue = Content.Load<Texture2D>("Content_GUI/label");

            // Test Content
            test1 = Content.Load<Texture2D>("Content_GUI/test1");
            test2 = Content.Load<Texture2D>("Content_GUI/test2");

            // Item Content
            itemPicture = Content.Load<Texture2D>("Content_GUI/itemSocket");

            //fonts.Add()
            //dice_big = Content.Load<SpriteFont>("Content_GUI/diceFont_big");
            //monoFont_big = Content.Load<SpriteFont>("Content_GUI/monoFont_big");            
            //monoFont_small = Content.Load<SpriteFont>("Content_GUI/monoFont_small");

            dice_big = Content.Load<SpriteFont>("Content_GUI/diceFont_big");
            monoFont_big = Content.Load<SpriteFont>("Content_GUI/monoFont_big");
            monoFont_small = Content.Load<SpriteFont>("Content_GUI/monoFont_small");

            //Console.WriteLine(monoFont_big.MeasureString("12345"));

            // Add platform to its own platform List
            platforms.Add(this);


        }

        public void setBackground(ContentManager Content, string name)
        {
            this.background = Content.Load<Texture2D>(name);
        }

        public void backgroundOff()
        {
            this.backgroundEnabled = false;
        }



        public void update()
        {
            //functionCalled = null;
            //mouseHandle = Controls_GUI.Instance.Mouse_GUI;
            mouseHandle = Mouse.GetState();


            foreach (Button_GUI bb in buttons)
            {
                if (bb.isClickable)
                {
                    if (Button_GUI.isInside(mouseHandle.X, mouseHandle.Y, bb.XPos, bb.YPos, bb.Width, bb.Height))
                    {
                        if (mouseHandle.LeftButton == ButtonState.Pressed)
                        {
                            if (pushed_name_button == bb.Function)
                            {
                                bb.Button_State = ButtonState_GUI.Pressed;
                            }
                            if (mouseHandle_Old.LeftButton == ButtonState.Released)
                            {
                                pushed_name_button = bb.Function;
                            }
                        }
                        else
                            bb.Button_State = ButtonState_GUI.MouseOver;

                        if (mouseHandle.LeftButton == ButtonState.Released && mouseHandle_Old.LeftButton == ButtonState.Pressed && pushed_name_button == bb.Function)
                        {
                            Jukebox.Instance.PlayAudioMouseFeedback();
                            //Console.WriteLine("Sound!");

                            //this.functionCalled = bb.onClick();
                            if (OnButtonValue != null)
                            {
                                OnButtonValue(this, new ButtonEvent_GUI(bb.Function));
                            }
                        }
                    }
                    else
                        bb.Button_State = ButtonState_GUI.Normal;
                }
            }

            foreach (InventoryItem_GUI ii in invItems)
            {

            }

            foreach (Slider_GUI sl in sliders)
            {
                int sliderhandle = mouseHandle.X - sl.SliderWidth / 2;
                if (mouseHandle.LeftButton == ButtonState.Pressed)
                {

                    if (Slider_GUI.isInside(mouseHandle.X, mouseHandle.Y, sl.SliderPosX, sl.SliderPosY, sl.SliderWidth, sl.SliderHeight))
                    {
                        if (pushed_name_slider == sl.Function)
                        {
                            sl.Slider_State = SliderState_GUI.Pressed;
                        }

                        //if (pushed_name == sl.Function)
                        //{

                        //    //bb.Button_State = ButtonState_GUI.Pressed;
                        //    // Direkte Übergabe? Ne, da das mit Grafik-Stuff schief geht
                        //}
                        if (mouseHandle_Old.LeftButton == ButtonState.Released)
                        {
                            pushed_name_slider = sl.Function;
                        }
                    }

                    if (pushed_name_slider == sl.Function)
                    {
                        sl.SliderPosX = sliderhandle;
                        if (sliderhandle <= sl.SliderMinX)
                            sl.SliderPosX = sl.SliderMinX;
                        else if (sliderhandle >= sl.SliderMaxX)
                            sl.SliderPosX = sl.SliderMaxX;

                        int eValue = (int)((sl.SliderPosX - sl.SliderMinX) / sl.FactorX);
                        float factorXY = 1 / (float)(sl.MaxValue - sl.MinValue);

                        int sliderWidth = sl.SliderMaxX - sl.SliderMinX;
                        //Console.WriteLine("here:");
                        //Console.WriteLine(factorXY);
                        //Console.WriteLine(sliderWidth);
                        //Console.WriteLine(sl.SliderMinX);
                        sl.SliderPosX = (int)(sl.SliderMinX + sliderWidth * (factorXY) * (float)(eValue - sl.MinValue));
                        //if (sl.SliderPosX <= sl.SliderMinX)
                        //    sl.SliderPosX = sl.SliderMinX;
                        //else if (sl.SliderPosX >= sl.SliderMaxX)
                        //    sl.SliderPosX = sl.SliderMaxX;

                    }
                }

                if (mouseHandle.LeftButton == ButtonState.Released && mouseHandle_Old.LeftButton == ButtonState.Pressed && pushed_name_slider == sl.Function)
                {
                    pushed_name_slider = null;
                    sl.Slider_State = SliderState_GUI.Normal;
                    //Console.WriteLine(monoFont_small.MeasureString("A").Y);
                    //Console.WriteLine(monoFont_big.MeasureString("MAIN MENU").X);
                    //Console.WriteLine("Hier:");
                    //Console.WriteLine(sl.MaxValue);
                    //Console.WriteLine(sl.SliderPosX);
                    //Console.WriteLine(sl.SliderMaxX);
                    int eValue = (int)((sl.SliderPosX - sl.SliderMinX) / sl.FactorX);
                    if (sl.SliderPosX <= sl.SliderMinX)
                        eValue = sl.MinValue;
                    // That's a little cheat right there, but who cares. It works just fine. (The +1)
                    if (sl.SliderPosX + 1 >= sl.SliderMaxX)
                        eValue = sl.MaxValue;
                    //Console.WriteLine(eValue);
                    sl.CurrentValue = eValue;
                    if (OnSliderValue != null)
                    {
                        OnSliderValue(this, new SliderEvent_GUI(eValue, sl.Function));
                    }

                    //Console.WriteLine((int)((sl.SliderPosX - sl.SliderMinX) / sl.FactorX));
                }
            }
            if (mouseHandle.LeftButton == ButtonState.Released && mouseHandle_Old.LeftButton == ButtonState.Pressed)
            {
                pushed_name_button = null;
                pushed_name_slider = null;
            }

            mouseHandle_Old = mouseHandle;
        }

        public void draw(SpriteBatch spritebatch)
        {
            spritebatch.Begin();

            // Beware: Hardcoded values...
            //spritebatch.Draw(background, new Rectangle(0, 0, 800, 480), Color.White);
            // Windows-Standard-Size ist 800x480

            if (backgroundEnabled)
            {
                spritebatch.Draw(overlay, new Rectangle(0, 0, MainWindowWidthInt, MainWindowHeightInt), Color.White);
                spritebatch.Draw(background, new Rectangle(0, 0, MainWindowWidthInt, MainWindowHeightInt), drawColor);
            }

            foreach (PlainImage_GUI pi in pimages)
                if (pi.IsVisible)
                {
                    spritebatch.Draw(pi.Image, new Rectangle(pi.XPos, pi.YPos, pi.Width, pi.Height), drawColor);
                }


            //spritebatch.Draw(overlay, new Rectangle(0, 0, 800, 480), Color.White*0.7f);
            foreach (Label_GUI ls in labels)
            {
                if (ls.IsVisible)
                {
                    if (ls.Width < 0.01f)
                    {
                        spritebatch.DrawString(ls.SpriteFont, ls.LabelText, new Vector2(ls.XPos, ls.YPos), drawColor, 0.0f, new Vector2(0.0f, 0.0f), ls.TextScaleFactor, SpriteEffects.None, 0.0f);
                    }
                    else
                    {
                        spritebatch.Draw(label, new Rectangle(ls.XPos, ls.YPos, ls.Width, ls.Height), drawColor);
                        spritebatch.DrawString(ls.SpriteFont, ls.LabelText, new Vector2(ls.TextXPos, ls.TextYPos), drawColor, 0.0f, new Vector2(0.0f, 0.0f), ls.TextScaleFactor, SpriteEffects.None, 0.0f);
                    }
                }
            }

            foreach (DialogueBox_GUI db in dialogues)
            {
                if (db.IsVisible)
                {
                    spritebatch.Draw(dialogue, new Rectangle(db.XPos, db.YPos, db.Width, db.Height), drawColor);
                    spritebatch.DrawString(db.SpriteFont, db.LabelText, new Vector2(db.TextXPos, db.TextYPos), drawColor, 0.0f, new Vector2(0.0f, 0.0f), db.TextScaleFactor, SpriteEffects.None, 0.0f);
                }
            }



            foreach (Button_GUI bb in buttons)
            {
                if (bb.IsVisible)
                {
                    if (bb.Button_State == ButtonState_GUI.Normal)
                        if (bb.PathOfPicture.Length > 2)
                            spritebatch.Draw(itemPicture, new Rectangle(bb.XPos, bb.YPos, bb.Width, bb.Height), drawColor);
                        else
                            spritebatch.Draw(button_n, new Rectangle(bb.XPos, bb.YPos, bb.Width, bb.Height), drawColor);
                    else if (bb.Button_State == ButtonState_GUI.MouseOver)
                        spritebatch.Draw(button_m, new Rectangle(bb.XPos, bb.YPos, bb.Width, bb.Height), drawColor);
                    else if (bb.Button_State == ButtonState_GUI.Pressed)
                        spritebatch.Draw(button_p, new Rectangle(bb.XPos, bb.YPos, bb.Width, bb.Height), drawColor);
                    if (bb.ButtonText != null)
                    {
                        spritebatch.DrawString(monoFont_small, bb.ButtonText, new Vector2(bb.TextXPos, bb.TextYPos), drawColor, 0.0f, new Vector2(0.0f, 0.0f), bb.TextScaleFactor, SpriteEffects.None, 0.0f);
                    }
                }

            }
            foreach (InventoryItem_GUI ii in invItems)
            {
                spritebatch.Draw(itemSocket, new Rectangle(ii.XPos, ii.YPos, ii.Width, ii.Height), drawColor);
            }


            foreach (PlainText_GUI pt in ptexts)
                spritebatch.DrawString(pt.SpriteFont, pt.Text, new Vector2(pt.XPos, pt.YPos), drawColor, 0.0f, new Vector2(0.0f, 0.0f), OverallFontScale, SpriteEffects.None, 0.0f);
            //spritebatch.DrawString(pt.SpriteFont, pt.Text, new Vector2(pt.XPos, pt.YPos), drawColor);                
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


        public void addButton(float xPos, float yPos, float width, float height, string name, string buttonText)
        {
            int xPosAbs = (int)(MainWindowSize.X * xPos * 0.01);
            int yPosAbs = (int)(MainWindowSize.Y * yPos * 0.01);
            int widthAbs = (int)(MainWindowSize.X * width * 0.01);
            int heightAbs = (int)(MainWindowSize.Y * height * 0.01);

            Vector2 spriteFontSize = monoFont_small.MeasureString(buttonText);

            // 0.7 Factor is the scale for the text for now to make it fit in the box
            float textScaleFactor = (MainWindowSize.Y * height * 0.01f * 0.7f) / spriteFontSize.Y;
            //Console.WriteLine(textScaleFactor);
            //int textX = (int)(((MainWindowSize.X * xPos * 0.01f)) + (MainWindowSize.X * width * 0.01f / 2) - (textScaleFactor * spriteFontSize.X / 2) - (MainWindowSize.X*0.01));
            //int textY = (int)(((MainWindowSize.Y * yPos * 0.01f)) + (MainWindowSize.Y * height * 0.01f / 2) - (textScaleFactor * spriteFontSize.Y / 2) + (MainWindowSize.Y * 0.01));

            int textX = (int)(((MainWindowSize.X * xPos * 0.01f)) + (MainWindowSize.X * width * 0.01f / 2) - (textScaleFactor * spriteFontSize.X / 2));

            // In Y-Direction there is a small subtraction on the textScaleFactor to put the text more in the lower middle
            int textY = (int)(((MainWindowSize.Y * yPos * 0.01f)) + (MainWindowSize.Y * height * 0.01f / 2) - ((textScaleFactor - 0.15f) * spriteFontSize.Y / 2));
            //int textY = (int)((MainWindowSize.Y * yPos * 0.01f) - (textScaleFactor * spriteFontSize.Y) / 2);
            buttons.Add(new Button_GUI(xPos, yPos, xPosAbs, yPosAbs, width, height, widthAbs, heightAbs, name, buttonText, textX, textY, textScaleFactor));
        }
        public void addButton(float xPos, float yPos, float width, float height, string name, bool isVisible)
        {
            int xPosAbs = (int)(MainWindowSize.X * xPos * 0.01);
            int yPosAbs = (int)(MainWindowSize.Y * yPos * 0.01);
            int widthAbs = (int)(MainWindowSize.X * width * 0.01);
            int heightAbs = (int)(MainWindowSize.Y * height * 0.01);
            buttons.Add(new Button_GUI(xPos, yPos, xPosAbs, yPosAbs, width, height, widthAbs, heightAbs, name, isVisible));
        }
        public void addButton(float xPos, float yPos, float width, float height, string name, bool isVisible, string pathOfPicture)
        {
            int xPosAbs = (int)(MainWindowSize.X * xPos * 0.01);
            int yPosAbs = (int)(MainWindowSize.Y * yPos * 0.01);
            int widthAbs = (int)(MainWindowSize.X * width * 0.01);
            int heightAbs = (int)(MainWindowSize.Y * height * 0.01);
            buttons.Add(new Button_GUI(xPos, yPos, xPosAbs, yPosAbs, width, height, widthAbs, heightAbs, name, isVisible, pathOfPicture));
        }



        public void addPlainText(float xPos, float yPos, string chooseFont, string text, bool centered)
        {
            int xPosAbs;
            int yPosAbs;


            Vector2 spriteFontSize;

            //float textScaleFactor = (MainWindowSize.Y * height * 0.01f * 0.7f) / spriteFontSize.Y;

            // Oh my god this is so ugly. I can't believe it myself.
            switch (chooseFont)
            {
                case "dice_big":
                    spriteFontSize = dice_big.MeasureString(text);
                    if (centered)
                    {
                        //int a =   (int)(((MainWindowSize.X * xPos * 0.01f)) + (MainWindowSize.X * width * 0.01f / 2) - (textScaleFactor * spriteFontSize.X / 2));

                        xPosAbs = (int)((MainWindowSize.X * xPos * 0.01) - spriteFontSize.X * OverallFontScale / 2);
                        yPosAbs = (int)(MainWindowSize.Y * yPos * 0.01);
                        //yPosAbs = (int)((MainWindowSize.Y * yPos * 0.01) - spriteFontSize.Y * OverallFontScale / 2);
                    }
                    else
                    {
                        xPosAbs = (int)(MainWindowSize.X * xPos * 0.01);
                        yPosAbs = (int)(MainWindowSize.Y * yPos * 0.01);
                    }
                    ptexts.Add(new PlainText_GUI(xPosAbs, yPosAbs, dice_big, text, centered, spriteFontSize));
                    break;
                case "monoFont_big":
                    spriteFontSize = monoFont_big.MeasureString(text);
                    if (centered)
                    {
                        xPosAbs = (int)((MainWindowSize.X * xPos * 0.01) - spriteFontSize.X * OverallFontScale / 2);
                        yPosAbs = (int)(MainWindowSize.Y * yPos * 0.01);
                    }
                    else
                    {
                        xPosAbs = (int)(MainWindowSize.X * xPos * 0.01);
                        yPosAbs = (int)(MainWindowSize.Y * yPos * 0.01);
                    }
                    ptexts.Add(new PlainText_GUI(xPosAbs, yPosAbs, monoFont_big, text, centered, spriteFontSize));
                    break;
                case "monoFont_small":
                    spriteFontSize = monoFont_small.MeasureString(text);
                    if (centered)
                    {
                        xPosAbs = (int)((MainWindowSize.X * xPos * 0.01) - spriteFontSize.X * OverallFontScale / 2);
                        yPosAbs = (int)(MainWindowSize.Y * yPos * 0.01);
                    }
                    else
                    {
                        xPosAbs = (int)(MainWindowSize.X * xPos * 0.01);
                        yPosAbs = (int)(MainWindowSize.Y * yPos * 0.01);
                    }
                    ptexts.Add(new PlainText_GUI(xPosAbs, yPosAbs, monoFont_small, text, centered, spriteFontSize));
                    break;
                default:
                    Console.WriteLine("No such font");
                    break;
            }

        }

        public void addPlainImage(float xPos, float yPos, float width, float height, string name, string image)
        {
            int xPosAbs = (int)(MainWindowSize.X * xPos * 0.01);
            int yPosAbs = (int)(MainWindowSize.Y * yPos * 0.01);
            int widthAbs = (int)(MainWindowSize.X * width * 0.01);
            int heightAbs = (int)(MainWindowSize.Y * height * 0.01);
            Texture2D plTexture;

            switch (image)
            {
                case "HUD_small":
                    plTexture = HUD_small;
                    break;
                case "pixel_white":
                    plTexture = pixel_white;
                    break;
                case "pixel_black":
                    plTexture = pixel_black;
                    break;
                case "pixel_red":
                    plTexture = pixel_red;
                    break;
                case "test1":
                    plTexture = test1;
                    break;
                case "test2":
                    plTexture = test2;
                    break;
                case "icon_armor":
                    plTexture = icon_armor;
                    break;
                case "icon_boot":
                    plTexture = icon_boot;
                    break;
                case "icon_helmet":
                    plTexture = icon_helmet;
                    break;
                case "icon_quest":
                    plTexture = icon_quest;
                    break;
                case "icon_weapon":
                    plTexture = icon_weapon;
                    break;
                case "icon_useable":
                    plTexture = icon_useable;
                    break;
                default:
                    plTexture = pixel_black;
                    break;
            }
            pimages.Add(new PlainImage_GUI(xPos, yPos, xPosAbs, yPosAbs, width, height, widthAbs, heightAbs, name, plTexture));
        }

        public void addSlider(float xPos, float yPos, float width, float height, int minValue, int maxValue, int startValue, string name)
        {
            int xPosAbs = (int)(MainWindowSize.X * xPos * 0.01);
            int yPosAbs = (int)(MainWindowSize.Y * yPos * 0.01);
            int widthAbs = (int)(MainWindowSize.X * width * 0.01);
            int heightAbs = (int)(MainWindowSize.Y * height * 0.01);
            sliders.Add(new Slider_GUI(xPos, yPos, xPosAbs, yPosAbs, width, height, widthAbs, heightAbs, minValue, maxValue, startValue, name));
        }

        public void addLabel(float xPos, float yPos, float height, string labelFont, string labelText, string labelName, bool centered)
        {
            int xPosAbs = (int)(MainWindowSize.X * xPos * 0.01);
            int yPosAbs = (int)(MainWindowSize.Y * yPos * 0.01);
            //int widthAbs = (int)(MainWindowSize.X * width * 0.01);
            int heightAbs = (int)(MainWindowSize.Y * height * 0.01);

            SpriteFont lFont;
            switch (labelFont)
            {
                case "dice_big":
                    lFont = dice_big;
                    break;
                case "monoFont_big":
                    lFont = monoFont_big;
                    break;
                case "monoFont_small":
                    lFont = monoFont_small;
                    break;
                default:
                    lFont = dice_big;
                    break;
            }

            //Vector2 spriteFontSize = monoFont_big.MeasureString(labelText);
            Vector2 spriteFontSize = lFont.MeasureString(labelText);

            float textScaleFactor = (MainWindowSize.Y * height * 0.01f) / spriteFontSize.Y;
            if (centered)
            {
                xPosAbs -= (int)(spriteFontSize.X * textScaleFactor / 2);
            }

            //labels.Add(new Label_GUI(xPosAbs, yPosAbs, 0, heightAbs, monoFont_big, labelText, textScaleFactor, centered));
            labels.Add(new Label_GUI(xPos, yPos, xPosAbs, yPosAbs, 0, height, 0, heightAbs, lFont, labelText, labelName, textScaleFactor, centered));

        }
        public void addLabel(float xPos, float yPos, float width, float height, string labelFont, string labelText, string labelName)
        {
            int xPosAbs = (int)(MainWindowSize.X * xPos * 0.01);
            int yPosAbs = (int)(MainWindowSize.Y * yPos * 0.01);
            int widthAbs = (int)(MainWindowSize.X * width * 0.01);
            int heightAbs = (int)(MainWindowSize.Y * height * 0.01);

            SpriteFont lFont;
            switch (labelFont)
            {
                case "dice_big":
                    lFont = dice_big;
                    break;
                case "monoFont_big":
                    lFont = monoFont_big;
                    break;
                case "monoFont_small":
                    lFont = monoFont_small;
                    break;
                default:
                    lFont = dice_big;
                    break;
            }

            Vector2 spriteFontSize = lFont.MeasureString(labelText);


            // 0.9 Factor is the scale for the text for now to make it fit in the box
            float textScaleFactor = (MainWindowSize.Y * height * 0.01f * 0.8f) / spriteFontSize.Y;

            int textX = (int)(((MainWindowSize.X * xPos * 0.01f)) + (MainWindowSize.X * width * 0.01f / 2) - (textScaleFactor * spriteFontSize.X / 2));

            // In Y-Direction there is a small subtraction on the textScaleFactor to put the text more in the lower middle
            int textY = (int)(((MainWindowSize.Y * yPos * 0.01f)) + (MainWindowSize.Y * height * 0.01f / 2) - ((textScaleFactor - 0.15f) * spriteFontSize.Y / 2));

            labels.Add(new Label_GUI(xPos, yPos, xPosAbs, yPosAbs, width, height, widthAbs, heightAbs, lFont, labelText, labelName, textX, textY, textScaleFactor));
        }

        public void addDialogue(float xPos, float yPos, float width, float height, string dialogFont, string labelText, string labelName)
        {
            int xPosAbs = (int)(MainWindowSize.X * xPos * 0.01);
            int yPosAbs = (int)(MainWindowSize.Y * yPos * 0.01);
            int widthAbs = (int)(MainWindowSize.X * width * 0.01);
            int heightAbs = (int)(MainWindowSize.Y * height * 0.01);

            SpriteFont dFont;
            switch (dialogFont)
            {
                case "dice_big":
                    dFont = dice_big;
                    break;
                case "monoFont_big":
                    dFont = monoFont_big;
                    break;
                case "monoFont_small":
                    dFont = monoFont_small;
                    break;
                default:
                    dFont = dice_big;
                    break;
            }

            Vector2 spriteFontSize = dFont.MeasureString("A");


            // 1.0f means the factor to make the fontSize smaller is 1 right know
            //float textScaleFactor = (MainWindowSize.Y * height * 0.01f * 1.0f) / spriteFontSize.Y;
            //float textScaleFactor = spriteFontSize.Y;
            float textScaleFactor = (MainWindowSize.Y * 10 * 0.01f) / spriteFontSize.Y;

            //int textX = (int)(((MainWindowSize.X * xPos * 0.01f)) + (MainWindowSize.X * width * 0.01f / 2) - (textScaleFactor * spriteFontSize.X / 2));
            int textX = (int)(MainWindowSize.X * (xPos + 1) * 0.01f);

            // In Y-Direction there is a small subtraction on the textScaleFactor to put the text more in the lower middle
            //int textY = (int)(((MainWindowSize.Y * yPos * 0.01f)) + (MainWindowSize.Y * height * 0.01f / 2) - ((textScaleFactor - 0.15f) * spriteFontSize.Y / 2));
            int textY = (int)(MainWindowSize.Y * (yPos + 1) * 0.01f);

            dialogues.Add(new DialogueBox_GUI(xPos, yPos, xPosAbs, yPosAbs, width, height, widthAbs, heightAbs, dFont, labelText, labelName, textX, textY, textScaleFactor));
        }

        public void addInventoryItem(float xPos, float yPos, float width, float height)
        {
            int xPosAbs = (int)(MainWindowSize.X * xPos * 0.01);
            int yPosAbs = (int)(MainWindowSize.Y * yPos * 0.01);
            int widthAbs = (int)(MainWindowSize.X * width * 0.01);
            int heightAbs = (int)(MainWindowSize.Y * height * 0.01);

            invItems.Add(new InventoryItem_GUI(xPos, yPos, xPosAbs, yPosAbs, width, height, widthAbs, heightAbs));
        }

        public void updateLabel(string labelName, string newLabeltext)
        {
            //foreach(var n in people.Where(p=>p.sex == male))
            //foreach (Label_GUI ls in labels)
            //{
            //    if()
            //}
            foreach (Label_GUI ls in labels.Where(n => n.LabelName == labelName))
            {
                ls.LabelText = newLabeltext;
                if (ls.Width < 0.01f)
                {
                    ls.XPos = (int)(MainWindowSize.X * ls.XPosRelative * 0.01);
                    ls.YPos = (int)(MainWindowSize.Y * ls.YPosRelative * 0.01);
                    ls.Height = (int)(MainWindowSize.Y * ls.HeightRelative * 0.01);

                    Vector2 spriteFontSize = ls.SpriteFont.MeasureString(ls.LabelText);

                    ls.TextScaleFactor = (MainWindowSize.Y * ls.HeightRelative * 0.01f) / spriteFontSize.Y;
                    if (ls.Centered)
                    {
                        ls.XPos -= (int)(spriteFontSize.X * ls.TextScaleFactor / 2);
                    }
                }
                else
                {
                    ls.XPos = (int)(MainWindowSize.X * ls.XPosRelative * 0.01);
                    ls.YPos = (int)(MainWindowSize.Y * ls.YPosRelative * 0.01);
                    ls.Width = (int)(MainWindowSize.X * ls.WidthRelative * 0.01);
                    ls.Height = (int)(MainWindowSize.Y * ls.HeightRelative * 0.01);

                    Vector2 spriteFontSize = ls.SpriteFont.MeasureString(ls.LabelText);

                    ls.TextScaleFactor = (MainWindowSize.Y * ls.HeightRelative * 0.01f * 0.8f) / spriteFontSize.Y;

                    ls.TextXPos = (int)(((MainWindowSize.X * ls.XPosRelative * 0.01f)) + (MainWindowSize.X * ls.WidthRelative * 0.01f / 2) - (ls.TextScaleFactor * spriteFontSize.X / 2));
                    ls.TextYPos = (int)(((MainWindowSize.Y * ls.YPosRelative * 0.01f)) + (MainWindowSize.Y * ls.HeightRelative * 0.01f / 2) - ((ls.TextScaleFactor - 0.15f) * spriteFontSize.Y / 2));
                }
            }
        }

        public void updatePlainImage(string imageName, float newX, float newY, float newWidth, float newHeight)
        {
            foreach (PlainImage_GUI pi in pimages.Where(n => n.Function == imageName))
            {
                pi.XPos = (int)(MainWindowSize.X * newX * 0.01);
                pi.YPos = (int)(MainWindowSize.Y * newY * 0.01);
                pi.Width = (int)(MainWindowSize.X * newWidth * 0.01);
                pi.Height = (int)(MainWindowSize.Y * newHeight * 0.01);
            }
        }
        public void updatePlainImage(string imageName, bool visibility)
        {
            foreach (PlainImage_GUI pi in pimages.Where(n => n.Function == imageName))
            {
                pi.IsVisible = visibility;
            }
        }

        public void updateButtonPicture(string buttonName, string newPicturePath)
        {
            foreach (Button_GUI bb in buttons.Where(n => n.Function == buttonName))
            {
                bb.PathOfPicture = newPicturePath; // Is doing quiet nothing right know
                itemPicture = this.platformContent.Load<Texture2D>(newPicturePath);
            }
        }

        public Vector2 getButtonPosition(string buttonName)
        {
            Vector2 result = new Vector2(0, 0);
            foreach (Button_GUI bb in buttons.Where(n => n.Function == buttonName))
            {
                result.X = bb.XPosRelative;
                result.Y = bb.YPosRelative;
            }
            return result;
        }
        public void updateButtonVisibility(string buttonName, bool visibility)
        {
            foreach (Button_GUI bb in buttons.Where(n => n.Function == buttonName))
            {
                bb.IsVisible = visibility;
            }
        }
        public void updateButtonClickability(string buttonName, bool isClickable)
        {
            foreach (Button_GUI bb in buttons.Where(n => n.Function == buttonName))
            {
                bb.isClickable = isClickable;
            }
        }
        public void updateButtonPosition(string buttonName, float xPos, float yPos)
        {
            foreach (Button_GUI bb in buttons.Where(n => n.Function == buttonName))
            {
                bb.XPosRelative = xPos;
                bb.YPosRelative = yPos;
            }
        }
        public void updateButtonText(string buttonName, string buttonText)
        {
            foreach (Button_GUI bb in buttons.Where(n => n.Function == buttonName))
            {
                bb.ButtonText = buttonText;
            }
        }
        public Vector2 getLabelPosition(string labelName)
        {
            Vector2 result = new Vector2(0, 0);
            foreach (Label_GUI ls in labels.Where(n => n.LabelName == labelName))
            {
                result.X = ls.XPosRelative;
                result.Y = ls.YPosRelative;
            }
            return result;
        }
        public void updateLabelVisibility(string labelName, bool visibility)
        {
            foreach (Label_GUI ls in labels.Where(n => n.LabelName == labelName))
            {
                ls.IsVisible = visibility;
            }
        }
        public void updateLabelPosition(string labelName, float xPos, float yPos)
        {
            foreach (Label_GUI ls in labels.Where(n => n.LabelName == labelName))
            {
                ls.XPosRelative = xPos;
                ls.YPosRelative = yPos;
            }
        }

        // Updates the resolutions in every platform instance
        public static void updateAllResolutions(int x, int y)
        {
            foreach (Platform_GUI pltfrms in platforms)
            {
                pltfrms.updateResolution(x, y);
            }
        }

        // Updates this platform resolution
        public void updateResolution(int x, int y)
        {
            MainWindowWidthInt = x;
            MainWindowHeightInt = y;
            MainWindowSize = new IntVector2(x, y);

            foreach (Button_GUI bb in buttons)
            {
                if (bb.ButtonText == null)
                {
                    bb.XPos = (int)(MainWindowSize.X * bb.XPosRelative * 0.01);
                    bb.YPos = (int)(MainWindowSize.Y * bb.YPosRelative * 0.01);
                    bb.Width = (int)(MainWindowSize.X * bb.WidthRelative * 0.01);
                    bb.Height = (int)(MainWindowSize.Y * bb.HeightRelative * 0.01);
                }
                else
                {
                    bb.XPos = (int)(MainWindowSize.X * bb.XPosRelative * 0.01);
                    bb.YPos = (int)(MainWindowSize.Y * bb.YPosRelative * 0.01);
                    bb.Width = (int)(MainWindowSize.X * bb.WidthRelative * 0.01);
                    bb.Height = (int)(MainWindowSize.Y * bb.HeightRelative * 0.01);

                    Vector2 spriteFontSize = monoFont_small.MeasureString(bb.ButtonText);

                    bb.TextScaleFactor = (MainWindowSize.Y * bb.HeightRelative * 0.01f * 0.7f) / spriteFontSize.Y;

                    bb.TextXPos = (int)(((MainWindowSize.X * bb.XPosRelative * 0.01f)) + (MainWindowSize.X * bb.WidthRelative * 0.01f / 2) - (bb.TextScaleFactor * spriteFontSize.X / 2));
                    bb.TextYPos = (int)(((MainWindowSize.Y * bb.YPosRelative * 0.01f)) + (MainWindowSize.Y * bb.HeightRelative * 0.01f / 2) - ((bb.TextScaleFactor - 0.15f) * spriteFontSize.Y / 2));
                }
            }
            foreach (InventoryItem_GUI ii in invItems)
            {
                ii.XPos = (int)(MainWindowSize.X * ii.XPosRelative * 0.01);
                ii.YPos = (int)(MainWindowSize.Y * ii.YPosRelative * 0.01);
                ii.Width = (int)(MainWindowSize.X * ii.WidthRelative * 0.01);
                ii.Height = (int)(MainWindowSize.Y * ii.HeightRelative * 0.01);
            }

            foreach (Label_GUI ls in labels)
            {
                if (ls.Width < 0.01f)
                {
                    ls.XPos = (int)(MainWindowSize.X * ls.XPosRelative * 0.01);
                    ls.YPos = (int)(MainWindowSize.Y * ls.YPosRelative * 0.01);
                    ls.Height = (int)(MainWindowSize.Y * ls.HeightRelative * 0.01);

                    Vector2 spriteFontSize = ls.SpriteFont.MeasureString(ls.LabelText);

                    ls.TextScaleFactor = (MainWindowSize.Y * ls.HeightRelative * 0.01f) / spriteFontSize.Y;
                    if (ls.Centered)
                    {
                        ls.XPos -= (int)(spriteFontSize.X * ls.TextScaleFactor / 2);
                    }
                }
                else
                {
                    ls.XPos = (int)(MainWindowSize.X * ls.XPosRelative * 0.01);
                    ls.YPos = (int)(MainWindowSize.Y * ls.YPosRelative * 0.01);
                    ls.Width = (int)(MainWindowSize.X * ls.WidthRelative * 0.01);
                    ls.Height = (int)(MainWindowSize.Y * ls.HeightRelative * 0.01);

                    Vector2 spriteFontSize = ls.SpriteFont.MeasureString(ls.LabelText);

                    ls.TextScaleFactor = (MainWindowSize.Y * ls.HeightRelative * 0.01f * 0.8f) / spriteFontSize.Y;

                    ls.TextXPos = (int)(((MainWindowSize.X * ls.XPosRelative * 0.01f)) + (MainWindowSize.X * ls.WidthRelative * 0.01f / 2) - (ls.TextScaleFactor * spriteFontSize.X / 2));
                    ls.TextYPos = (int)(((MainWindowSize.Y * ls.YPosRelative * 0.01f)) + (MainWindowSize.Y * ls.HeightRelative * 0.01f / 2) - ((ls.TextScaleFactor - 0.15f) * spriteFontSize.Y / 2));
                }
            }
            foreach (DialogueBox_GUI db in dialogues)
            {
                db.XPos = (int)(MainWindowSize.X * db.XPosRelative * 0.01);
                db.YPos = (int)(MainWindowSize.Y * db.YPosRelative * 0.01);
                db.Width = (int)(MainWindowSize.X * db.WidthRelative * 0.01);
                db.Height = (int)(MainWindowSize.Y * db.HeightRelative * 0.01);

                Vector2 spriteFontSize = db.SpriteFont.MeasureString("A");

                db.TextScaleFactor = (MainWindowSize.Y * 10 * 0.01f) / spriteFontSize.Y;

                db.TextXPos = (int)(MainWindowSize.X * (db.XPosRelative + 1) * 0.01f);
                db.TextYPos = (int)(MainWindowSize.Y * (db.YPosRelative + 1) * 0.01f);
            }

            foreach (PlainImage_GUI pi in pimages)
            {
                pi.XPos = (int)(MainWindowSize.X * pi.XPosRelative * 0.01);
                pi.YPos = (int)(MainWindowSize.Y * pi.YPosRelative * 0.01);
                pi.Width = (int)(MainWindowSize.X * pi.WidthRelative * 0.01);
                pi.Height = (int)(MainWindowSize.Y * pi.HeightRelative * 0.01);
            }
            foreach (Slider_GUI sl in sliders)
            {
                sl.XPos = (int)(MainWindowSize.X * sl.XPosRelative * 0.01);
                sl.YPos = (int)(MainWindowSize.Y * sl.YPosRelative * 0.01);
                sl.Width = (int)(MainWindowSize.X * sl.WidthRelative * 0.01);
                sl.Height = (int)(MainWindowSize.Y * sl.HeightRelative * 0.01);
                sl.setSliderStartPosition(sl.XPos, sl.YPos, sl.Width, sl.Height);
            }
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
