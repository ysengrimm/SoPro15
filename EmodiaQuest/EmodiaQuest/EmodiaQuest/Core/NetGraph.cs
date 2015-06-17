using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using EmodiaQuest.Core.GUI;

namespace EmodiaQuest.Core
{
    class NetGraph
    {
        private static NetGraph instance;
        private NetGraph() { }
        public static NetGraph Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new NetGraph();
                }
                return instance;
            }
        }

        private Texture2D backgroundForFrames;
        private SpriteFont monoFont_small;
        float frameRate;
        private string frames = "0";
        

        public void loadContent(ContentManager Content)
        {

            backgroundForFrames = Content.Load<Texture2D>("Content_GUI/pixel_black");
            monoFont_small = Content.Load<SpriteFont>("Content_GUI/monoFont_small");
        }

        public void update(GameTime gameTime)
        {
            frameRate = 1 / (float)gameTime.ElapsedGameTime.TotalSeconds;
            frames = frameRate.ToString();
        }

        public void draw(SpriteBatch spritebatch)
        {
            // Beware: Hardcoded values
            spritebatch.Begin();
            spritebatch.Draw(backgroundForFrames, new Rectangle(800 - 180, 0, 180, 40), new Color(0, 0, 0, 80));
            spritebatch.DrawString(monoFont_small, frames, new Vector2(800 - 178, 0), Color.White);
            spritebatch.End();
        }

    }
}
