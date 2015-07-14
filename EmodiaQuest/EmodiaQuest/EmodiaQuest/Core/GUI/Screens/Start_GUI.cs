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
    class Start_GUI
    {
        //EventHandler
        static void ButtonEventValue(object source, ButtonEvent_GUI e)
        {
            switch (e.ButtonFunction)
            {
                case "clickToPlay":
                    EmodiaQuest_Game.Gamestate_Game = GameStates_Overall.MenuScreen;
                    break;
                default:
                    Console.WriteLine("No such Function.");
                    break;
            }
        }

        private static Start_GUI instance;

        private Start_GUI() { }

        public static Start_GUI Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Start_GUI();
                }
                return instance;
            }
        }

        private Platform_GUI platform = new Platform_GUI();   

        public void loadContent(ContentManager Content)
        {
            this.platform.loadContent(Content);

            //int h = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            //int w = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            //mainWindowWidth = (int)EmodiaQuest.Core.Settings.Instance.Resolution.X;
            //mainwindowHeight = (int)EmodiaQuest.Core.Settings.Instance.Resolution.Y;

            //this.platform.addButton(0, 0, mainWindowWidth, mainwindowHeight, "clickToPlay", false);
            this.platform.addButton(0, 0, 100, 100, "clickToPlay", false);
        
            //this.platform.addPlainText(50.0f, 20.0f, "dice_big", "EMODIA", true);
            //this.platform.addPlainText(50.0f, 20.0f, "dice_big", "\nQUEST", true);
            this.platform.addLabel(50, 15, 25, "dice_big", "EMODIA", true);
            this.platform.addLabel(50, 40, 25, "dice_big", "QUEST", true);
            

            //EventHandler;
            platform.OnButtonValue += new GUI_Delegate_Button(ButtonEventValue);
        }

        public void update()
        {
            this.platform.update();
            this.platform.breathing();
        }

        public void draw(SpriteBatch spritebatch)
        {
            this.platform.draw(spritebatch);
        }
    }
}
