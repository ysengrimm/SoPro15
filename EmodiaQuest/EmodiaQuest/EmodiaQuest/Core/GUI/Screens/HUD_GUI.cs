using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace EmodiaQuest.Core.GUI.Screens
{
    class HUD_GUI
    {
        private static HUD_GUI instance;

        private HUD_GUI() { }

        public static HUD_GUI Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new HUD_GUI();
                }
                return instance;
            }
        }

        private Platform_GUI platform = new Platform_GUI();

        public void loadContent(ContentManager Content)
        {
            this.platform.loadContent(Content);
            this.platform.backgroundOff();

            this.platform.addPlainImage(0, 100 - 100 * 0.189f * 1.777f, 100, 100 * 0.189f * 1.777f, "HUD_small");
        }

        public void update()
        {
            this.platform.update();
        }

        public void draw(SpriteBatch spritebatch)
        {
            this.platform.draw(spritebatch);
        }
    }
}
