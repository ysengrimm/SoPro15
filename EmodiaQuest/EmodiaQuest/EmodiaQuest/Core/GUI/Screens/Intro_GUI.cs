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
    public class Intro_GUI
    {
        private static Intro_GUI instance;

        private Intro_GUI() { }

        public static Intro_GUI Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Intro_GUI();
                }
                return instance;
            }
        }


        private Platform_GUI platform = new Platform_GUI();

        public void loadContent(ContentManager Content)
        {
            this.platform.loadContent(Content);


            this.platform.addLabel(50, 0, 20, "dice_big", "Willkommen", "menu", true);

            this.platform.addButton(0, 0, 100, 100, "clickToPlay", false);

            //EventHandler;
            platform.OnButtonValue += new GUI_Delegate_Button(ButtonEventValue);
        }

        public void update()
        {
            this.platform.update();
        }

        public void draw(SpriteBatch spritebatch)
        {
            this.platform.draw(spritebatch);
        }

        //EventHandler
        static void ButtonEventValue(object source, ButtonEvent_GUI e)
        {
            switch (e.ButtonFunction)
            {
                case "clickToPlay":
                    EmodiaQuest.Core.GUI.Screens.Menu_GUI.Instance.showIntro = false;
                    EmodiaQuest_Game.Gamestate_Game = GameStates_Overall.IngameScreen;
                    break;
                default:
                    Console.WriteLine("No such Function.");
                    break;
            }
        }
    }
}
