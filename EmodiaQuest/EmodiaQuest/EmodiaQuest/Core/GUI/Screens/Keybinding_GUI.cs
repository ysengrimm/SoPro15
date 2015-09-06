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
            this.platform.setBackground(Content, "Content_GUI/menu_background");

            this.platform.addButton(35, 85, 30, 8, "changeToMainMenu", "Hauptmenue");

            this.platform.addLabel(50, 10, 20, "dice_big", "Tastenbelegung", "Credits", true);

            int first = 34;
            int sec = 54;
            this.platform.addLabel(first, 35, 6, "monoFont_small", "Laufen:", "Menu", false);
            this.platform.addLabel(first, 40, 6, "monoFont_small", "Reden:", "Menu", false);
            this.platform.addLabel(first, 45, 6, "monoFont_small", "Schlagen:", "Menu", false);
            this.platform.addLabel(first, 50, 6, "monoFont_small", "Schiessen:", "Menu", false);
            this.platform.addLabel(first, 55, 6, "monoFont_small", "Inventar:", "Menu", false);
            this.platform.addLabel(first, 60, 6, "monoFont_small", "Optionen:", "Menu", false);
            this.platform.addLabel(first, 65, 6, "monoFont_small", "Musik An/Aus:", "Menu", false);
            this.platform.addLabel(first, 70, 6, "monoFont_small", "Spiel beenden:", "Menu", false);

            this.platform.addLabel(sec, 35, 6, "monoFont_small", "WASD", "Menu", false);
            this.platform.addLabel(sec, 40, 6, "monoFont_small", "E", "Menu", false);
            this.platform.addLabel(sec, 45, 6, "monoFont_small", "Linke Maustaste", "Menu", false);
            this.platform.addLabel(sec, 50, 6, "monoFont_small", "Rechte Maustaste", "Menu", false);
            this.platform.addLabel(sec, 55, 6, "monoFont_small", "I", "Menu", false);
            this.platform.addLabel(sec, 60, 6, "monoFont_small", "O", "Menu", false);
            this.platform.addLabel(sec, 65, 6, "monoFont_small", "P", "Menu", false);
            this.platform.addLabel(sec, 70, 6, "monoFont_small", "ESC", "Menu", false);

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
