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

            this.platform.setBackground(Content, "Content_GUI/menu_full_small");

            // headline
            this.platform.addLabel(50, 10, 20, "monoFont_big", "Credits", "credits", true);

            this.platform.addButton(38, 85, 24, 8, "menu", "Menue");

            platform.addPlainImage(5, 5, 20, 20, "acagamics", "pixel_red");
            platform.updatePlainImagePicture("acagamics", "other/acagamics");

            //EventHandler;
            platform.OnButtonValue += new GUI_Delegate_Button(this.ButtonEventValue);
        }

        public void update()
        {
            this.platform.update();
        }

        public void draw(SpriteBatch spritebatch)
        {
            this.platform.draw(spritebatch);
        }

        // EventHandler
        void ButtonEventValue(object source, ButtonEvent_GUI e)
        {
            switch (e.ButtonFunction)
            {
                case "menu":
                    EmodiaQuest_Game.Gamestate_Game = GameStates_Overall.MenuScreen;
                    break;
                default:
                    Console.WriteLine("No such Function.");
                    break;
            }
        }

    }
}
