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
    public class Credits_GUI
    {
        private static Credits_GUI instance;

        private Credits_GUI() { }

        public static Credits_GUI Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Credits_GUI();
                }
                return instance;
            }
        }

        private Platform_GUI platform = new Platform_GUI();

        public void loadContent(ContentManager Content)
        {
            this.platform.loadContent(Content);

            // headline
            this.platform.addLabel(50, 10, 20, "monoFont_big", "Credits", "credits", true);
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
