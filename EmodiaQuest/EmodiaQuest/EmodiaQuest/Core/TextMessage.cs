using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EmodiaQuest.Core.NPCs;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using EmodiaQuest.Core.GUI;

namespace EmodiaQuest.Core
{
    public class TextMessage
    {
        // Message list
        private List<TextMessage_GUI> textMessages = new List<TextMessage_GUI>();

        // Fonts
        private SpriteFont monoFont_small;

        // Pictures
        Texture2D pixel_white;
        Texture2D pixel_black;

        // Background Color
        //Color backgroundColor = new Color(0, 0, 0, 50);
        Color backgroundColor = new Color(0, 0, 0, 50);

        // Sizes
        int xAbs = 0;
        int yAbs = 0;
        int widthAbs = 0;
        int heightAbs = 0;

        float textScaleFactor = 0;
        int textX = 0;
        int textY = 0;

        // Heightfactor. This divides the current screen size
        float heightFactor = 20;

        // SpriteFontSize
        Vector2 spriteFontSize = new Vector2(0, 0);

        // Movement of the messages
        float movement = 0;


        // Add method
        public void NewMessage(string message)
        {
            int messageCount = textMessages.Count();

            int mX, mY, mWidth, mHeight, mTextXPos, mTextYPos;
            string mText;
            Color mBackgroundColor;

            if (messageCount < 1)
            {
                mX = this.xAbs;
                mY = this.yAbs;
                mWidth = this.widthAbs;
                mHeight = this.heightAbs;
                mTextXPos = this.textX;
                mTextYPos = this.textY;
                mText = message;
                mBackgroundColor = this.backgroundColor;
            }
            else
            {
                mX = this.xAbs;
                mY = textMessages.Last().YPos + this.heightAbs;
                mWidth = this.widthAbs;
                mHeight = this.heightAbs;
                mTextXPos = this.textX;
                mTextYPos = textMessages.Last().TextYPos + this.heightAbs;
                mText = message;
                mBackgroundColor = this.backgroundColor;
            }

            //int mX = this.xAbs;
            //int mY = this.yAbs + this.heightAbs * messageCount;
            //int mWidth = this.widthAbs;
            //int mHeight = this.heightAbs;
            //int mTextXPos = this.textX;
            //int mTextYPos = this.textY + this.heightAbs * messageCount;
            //string mText = message;
            //Color mBackgroundColor = this.backgroundColor;



            textMessages.Add(new TextMessage_GUI(mX, mY, mWidth, mHeight, mTextXPos, mTextYPos, mText, mBackgroundColor));

        }

        public void NewMessage(string message, Color Color)
        {
            int messageCount = textMessages.Count();

            int mX, mY, mWidth, mHeight, mTextXPos, mTextYPos;
            string mText;
            Color mBackgroundColor, mColor;

            if(messageCount < 1)
            {
                mX = this.xAbs;
                mY = this.yAbs;
                mWidth = this.widthAbs;
                mHeight = this.heightAbs;
                mTextXPos = this.textX;
                mTextYPos = this.textY;
                mText = message;
                mBackgroundColor = this.backgroundColor;
                mColor = Color;
            }
            else
            {
                mX = this.xAbs;
                mY = textMessages.Last().YPos + this.heightAbs;
                mWidth = this.widthAbs;
                mHeight = this.heightAbs;
                mTextXPos = this.textX;
                mTextYPos = textMessages.Last().TextYPos + this.heightAbs;
                mText = message;
                mBackgroundColor = this.backgroundColor;
                mColor = Color;
            }

            //int mX = this.xAbs;
            //int mY = this.yAbs + this.heightAbs * messageCount;
            //int mWidht = this.widthAbs;
            //int mHeight = this.heightAbs;
            //int mTextXPos = this.textX;
            //int mTextYPos = this.textY + this.heightAbs * messageCount;
            //string mText = message;
            //Color mBackgroundColor = this.backgroundColor;           
            //Color mColor = Color;

            textMessages.Add(new TextMessage_GUI(mX, mY, mWidth, mHeight, mTextXPos, mTextYPos, mText, mBackgroundColor, mColor));

        }

        public void updateMessageDisplaySize(int x, int y)
        {

            // Clear list
            textMessages.Clear();

            float newX = (float)x;
            float newY = (float)y;

            this.yAbs = (int)(newY * 0.15);

            this.widthAbs = (int)newX;
            this.heightAbs = (int)(newY / heightFactor);

            this.textScaleFactor = (heightAbs * 0.9f) / spriteFontSize.Y;

            this.textX = (int)(widthAbs * 0.08f);

            this.textY = yAbs + (int)(heightAbs * 0.05f);

        }




        public void loadContent(ContentManager Content)
        {
            // Font load and measurements
            monoFont_small = Content.Load<SpriteFont>("Content_GUI/monoFont_small");
            pixel_white = Content.Load<Texture2D>("Content_GUI/pixel_white");
            pixel_black = Content.Load<Texture2D>("Content_GUI/pixel_black");

            spriteFontSize = monoFont_small.MeasureString("A");

            // Update message main size
            float newX = (float)Settings.Instance.Resolution.X;
            float newY = (float)Settings.Instance.Resolution.Y;

            this.yAbs = (int)(newY * 0.05);

            this.widthAbs = (int)newX;
            this.heightAbs = (int)(newY / heightFactor);

            this.textScaleFactor = (heightAbs * 0.9f) / spriteFontSize.Y;

            this.textX = (int)(widthAbs * 0.08f);

            this.textY = yAbs + (int)(heightAbs * 0.05f);
        }

        public void update(GameTime gameTime)
        {
            // See if there are messages
            if (textMessages.Count() > 0)
            {
                var delta = (float)gameTime.ElapsedGameTime.TotalSeconds;

                // Compute the movement
                movement += (0.1f * textScaleFactor * (delta * 200));
                if (movement > 1)
                {
                    foreach (TextMessage_GUI ms in textMessages)
                    {
                        ms.YPos--;
                        ms.TextYPos--;
                    }

                    // Compute the fading
                    textMessages.First().BackgroundColor.A -= 2;

                    textMessages.First().TextColor.R -= 10;
                    textMessages.First().TextColor.G -= 10;
                    textMessages.First().TextColor.B -= 10;
                    textMessages.First().TextColor.A -= 10;

                    // Kicks the item out of list if not visible anymore
                    if (textMessages.First().BackgroundColor.A < 5)
                    {
                        textMessages.RemoveAt(0);
                    }

                    movement -= 1;
                }
            }
        }


        public void draw(SpriteBatch spritebatch)
        {
            foreach (TextMessage_GUI ms in textMessages)
            {
                spritebatch.Begin();
                spritebatch.Draw(pixel_black, new Rectangle(0, ms.YPos, ms.Width, ms.Height), ms.BackgroundColor);
                spritebatch.DrawString(monoFont_small, "o", new Vector2(ms.CirclePosition, ms.TextYPos), ms.CircleColor, 0.0f, new Vector2(0.0f, 0.0f), textScaleFactor, SpriteEffects.None, 0.0f);
                spritebatch.DrawString(monoFont_small, ms.Text, new Vector2(ms.TextXPos, ms.TextYPos), ms.TextColor, 0.0f, new Vector2(0.0f, 0.0f), textScaleFactor, SpriteEffects.None, 0.0f);               
                spritebatch.End();
            }
        }
















        private static TextMessage instance;

        private TextMessage() { }

        public static TextMessage Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new TextMessage();
                }
                return instance;
            }
        }




    }
}
