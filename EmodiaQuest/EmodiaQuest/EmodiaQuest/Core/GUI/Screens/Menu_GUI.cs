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

        // FAKE FAKE FAKE
        private bool exitValue = false;

        public void loadContent(ContentManager Content)
        {
            this.platform.loadContent(Content);

            this.platform.setBackground(Content, "Content_GUI/menu_background");

            // Beware: Hardcoded values...
            this.platform.addButton(240, 240, 300, 60, "nextState");
            this.platform.addButton(240, 340, 300, 60, "quit");

            this.platform.addPlainText(170, 30, "monoFont_big", "MAIN MENU");

            this.platform.addPlainText(296, 248, "monoFont_small", "New game");
            this.platform.addPlainText(288, 348, "monoFont_small", "Quit game");
            


        }
        // FAKE FAKE FAKE
        public bool update()
        {
            if ((this.functionCalled = this.platform.update()) != null)
                this.functionCall();
            return this.exitValue;
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
                case "quit":
                    this.exitValue = true;
                    break;
                default:
                    Console.WriteLine("No such Function.");
                    break;
            }
        }
    }
}
