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

        private string functionCalled = null;

        public void loadContent(ContentManager Content)
        {
            this.platform.loadContent(Content);

            //this.platform.setBackground(Content, "Content_GUI/menu_background");
            this.platform.setBackground(Content, "Content_GUI/menu_full_small");

            // Beware: Hardcoded values...
            //this.platform.addButton(30, 50, 40, 15, "nextState");
            this.platform.addButton(35, 50, 30, 15, "nextState", "Start Game");
            //this.platform.addButton(30, 70, 40, 15, "quit");
            this.platform.addButton(35, 75, 30, 15, "options", "Options");

            //this.platform.addPlainText(50.0f, 10.0f, "monoFont_big", "MAIN MENU", true);

            //this.platform.addSlider(30, 85, 40, 15, 0, 100, "testslide");

            
        }

        


        // FAKE FAKE FAKE
        public void update()
        {
            if ((this.functionCalled = this.platform.update()) != null)
                this.functionCall();
        }

        public void draw(SpriteBatch spritebatch)
        {
            this.platform.draw(spritebatch);
        }

        private void functionCall()
        {
            switch (this.functionCalled)
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
            this.functionCalled = null;
        }
    }
}
