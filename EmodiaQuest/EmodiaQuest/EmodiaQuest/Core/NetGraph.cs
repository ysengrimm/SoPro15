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
        float frameRate = 0;
        private string frameString = "0";
        private string playerXString = "0";
        private string playerYString = "0";
        private string activePlayerState = "0";
        private string lastPlayerState = "0";
        private string isBlending = "0";
        private string activeBlendDuration = "0";
        

        public void LoadContent(ContentManager content)
        {

            backgroundForFrames = content.Load<Texture2D>("Content_GUI/pixel_black");
            monoFont_small = content.Load<SpriteFont>("Content_GUI/monoFont_small");

            //Measure Fontsize
            //Console.WriteLine(monoFont_small.MeasureString("1"));
        }

        public void Update(GameTime gameTime, float playerX, float playerY, string activePlayerState, string lastPlayerState)
        {
            // get frames
            frameRate = 1 / (float)gameTime.ElapsedGameTime.TotalSeconds;
            // set the frames string to the frame value
            frameString = frameRate.ToString();
            playerXString = playerX.ToString();
            playerYString = playerY.ToString();
            this.activePlayerState = activePlayerState;
            this.lastPlayerState = lastPlayerState;
            this.activeBlendDuration = Player.Instance.activeBlendTime.ToString();
            this.isBlending = Player.Instance.isBlending.ToString();
        }

        public void Draw(SpriteBatch spritebatch)
        {
            // Beware: Hardcoded values
            spritebatch.Begin();
            // Add draws
            //spritebatch.Draw(backgroundForFrames, new Rectangle(Settings.Instance.Resolution.X - 180, 0, 180, 40*3), new Color(0, 0, 0, 80));
            spritebatch.Draw(backgroundForFrames, new Rectangle((int) (Settings.Instance.Resolution.X - 230), 0, 230, 42 * 7), new Color(0, 0, 0, 80));
            spritebatch.DrawString(monoFont_small, frameString, new Vector2(Settings.Instance.Resolution.X - 178, 0), Color.White);
            spritebatch.DrawString(monoFont_small, "F:", new Vector2(Settings.Instance.Resolution.X - 218, 0), Color.White);
            spritebatch.DrawString(monoFont_small, "x:", new Vector2(Settings.Instance.Resolution.X - 218, 40), Color.White);
            spritebatch.DrawString(monoFont_small, "y:", new Vector2(Settings.Instance.Resolution.X - 218, 80), Color.White);
            spritebatch.DrawString(monoFont_small, "A: ", new Vector2(Settings.Instance.Resolution.X - 218, 120), Color.White);
            spritebatch.DrawString(monoFont_small, "L:", new Vector2(Settings.Instance.Resolution.X - 218, 160), Color.White);
            spritebatch.DrawString(monoFont_small, "D:", new Vector2(Settings.Instance.Resolution.X - 218, 200), Color.White);
            spritebatch.DrawString(monoFont_small, "B:", new Vector2(Settings.Instance.Resolution.X - 218, 240), Color.White);
            spritebatch.DrawString(monoFont_small, playerXString, new Vector2(Settings.Instance.Resolution.X - 178, 40), Color.White);
            spritebatch.DrawString(monoFont_small, playerYString, new Vector2(Settings.Instance.Resolution.X - 178, 80), Color.White);
            spritebatch.DrawString(monoFont_small, activePlayerState, new Vector2(Settings.Instance.Resolution.X - 178, 120), Color.White);
            spritebatch.DrawString(monoFont_small, lastPlayerState, new Vector2(Settings.Instance.Resolution.X - 178, 160), Color.White);
            spritebatch.DrawString(monoFont_small, activeBlendDuration, new Vector2(Settings.Instance.Resolution.X - 178, 200), Color.White);
            spritebatch.DrawString(monoFont_small, isBlending, new Vector2(Settings.Instance.Resolution.X - 178, 240), Color.White);
            spritebatch.End();
        }

    }
}
