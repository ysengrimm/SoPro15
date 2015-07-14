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
    
    class Menu_GUI
    {
        //EventHandler
        static void ButtonEventValue(object source, ButtonEvent_GUI e)
        {
            Console.WriteLine(e.ButtonFunction);
            switch (e.ButtonFunction)
            {
                case "nextState":
                    EmodiaQuest_Game.Gamestate_Game = GameStates_Overall.IngameScreen;
                    break;
                case "options":
                    EmodiaQuest_Game.Gamestate_Game = GameStates_Overall.OptionsScreen;
                    break;
                default:
                    Console.WriteLine("No such Function.");
                    break;
            }
        }

        private static Menu_GUI instance;

        private Menu_GUI() { }

        public static Menu_GUI Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Menu_GUI();
                }
                return instance;
            }
        }

        private Platform_GUI platform = new Platform_GUI();

        //private string functionCalled = null;

        public void loadContent(ContentManager Content)
        {
            this.platform.loadContent(Content);

            //this.platform.setBackground(Content, "Content_GUI/menu_background");
            this.platform.setBackground(Content, "Content_GUI/menu_full_small");

            this.platform.addButton(35, 60, 30, 8, "nextState", "Start Game");
            this.platform.addButton(35, 75, 30, 8, "options", "Options");

            this.platform.addLabel(50, 0, 20, "dice_big", "Menu", true);
            this.platform.addLabel(50, 30, 20, "monoFont_big", "Menu2", true);
            this.platform.addLabel(30, 50, 40, 20, "monoFont_big", "labelText");


            //this.platform.addPlainText(50.0f, 10.0f, "monoFont_big", "MAIN MENU", true);

            //EventHandler;
            platform.OnButtonValue += new GUI_Delegate_Button(ButtonEventValue);
        }

        

        public void update()
        {
            this.platform.update();
            //if ((this.functionCalled = this.platform.update()) != null)
            //    this.functionCall();
        }

        public void draw(SpriteBatch spritebatch)
        {
            this.platform.draw(spritebatch);
        }

        //private void functionCall()
        //{
        //    switch (this.functionCalled)
        //    {
        //        default:
        //            Console.WriteLine("No such Function.");
        //            break;
        //    }
        //    this.functionCalled = null;
        //}
    }
}
