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
    public class Keybinding_GUI
    {
        private static Keybinding_GUI instance;

        private Keybinding_GUI() { }

        public static Keybinding_GUI Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Keybinding_GUI();
                }
                return instance;
            }
        }

        private Platform_GUI platform = new Platform_GUI();

        public void loadContent(ContentManager Content)
        {

            this.platform.loadContent(Content);

            // Hier einfach den String ersetzen durch den, den du brauchst
            this.platform.setBackground(Content, "Content_GUI/inventory_background");

            this.platform.addButton(35, 75, 30, 8, "changeToMainMenu", "Main Menu");

            platform.OnButtonValue += new GUI_Delegate_Button(this.ButtonEventValue);
        }

        public void update()
        {
            this.platform.update();

            // Get Keyboard input to change overall GameState
            if (EmodiaQuest.Core.GUI.Controls_GUI.Instance.keyClicked(Keys.O))
                EmodiaQuest_Game.Gamestate_Game = GameStates_Overall.IngameScreen;
        }

        public void draw(SpriteBatch spritebatch)
        {

            this.platform.draw(spritebatch);
        }


        void ButtonEventValue(object source, ButtonEvent_GUI e)
        {
            switch (e.ButtonFunction)
            {
                case "changeToMainMenu":
                    EmodiaQuest_Game.Gamestate_Game = GameStates_Overall.MenuScreen;
                    break;
                default:
                    Console.WriteLine("Function name does not exist");
                    break;
            }
        }
    }
}
