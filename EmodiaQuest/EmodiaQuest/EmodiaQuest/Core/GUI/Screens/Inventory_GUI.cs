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
    public class Inventory_GUI
    {
        private static Inventory_GUI instance;

        private Inventory_GUI() { }

        public static Inventory_GUI Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Inventory_GUI();
                }
                return instance;
            }
        }

        // key click handling
        private KeyboardState lastKeyboardState;
        private KeyboardState currentKeyboardState;

        private Platform_GUI platform = new Platform_GUI();

        public void loadContent(ContentManager Content)
        {
            // Initialize KeyboardState
            lastKeyboardState = Keyboard.GetState();

            this.platform.loadContent(Content);

            this.platform.setBackground(Content, "Content_GUI/inventory_background");

            this.platform.addLabel(15, 0, 10, "monoFont_big", "QuestLog", "questlog", true);
            this.platform.addLabel(15, 40, 10, "monoFont_big", "QuestText", "questtext", true);
            this.platform.addLabel(50, 0, 10, "monoFont_big", "Storyboard", "story", true);
            this.platform.addLabel(50, 40, 10, "monoFont_big", "Stats", "stats", true);
            this.platform.addLabel(85, 0, 10, "monoFont_big", "Inventory", "inventory", true);

            this.platform.addPlainImage(0, 100 - 100 * 0.189f * 1.777f, 100, 100 * 0.189f * 1.777f, "HUD_small");
        }

        public void update()
        {
            // Keyboard update. Keyboard is save
            currentKeyboardState = Keyboard.GetState();

            this.platform.update();

            // Get Keyboard input to change overall GameState
            if (currentKeyboardState.IsKeyDown(Keys.I) && !lastKeyboardState.IsKeyDown(Keys.I))
            {
                EmodiaQuest_Game.Gamestate_Game = GameStates_Overall.IngameScreen;
            }

            // Update Keyboard State
            lastKeyboardState = currentKeyboardState;
        }

        public void draw(SpriteBatch spritebatch)
        {
            this.platform.draw(spritebatch);
        }

    }
}
