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

        private Platform_GUI platform = new Platform_GUI();

        public void loadContent(ContentManager Content)
        {

            this.platform.loadContent(Content);

            this.platform.setBackground(Content, "Content_GUI/inventory_background");

            this.platform.addLabel(15, 0, 10, "monoFont_big", "QuestLog", "questlog", true);
            this.platform.addLabel(15, 40, 10, "monoFont_big", "QuestText", "questtext", true);
            this.platform.addLabel(50, 0, 10, "monoFont_big", "Storyboard", "story", true);
            this.platform.addLabel(50, 40, 10, "monoFont_big", "Stats", "stats", true);
            this.platform.addLabel(85, 0, 10, "monoFont_big", "Inventory", "inventory", true);

            // ItemSockets (Look out to only use 16 to 9 or it will not be a square!)
            // First row
            this.platform.addInventoryItem(69, 42, 4.5f, 8f);
            this.platform.addInventoryItem(69 + 4.7f, 42, 4.5f, 8f);
            this.platform.addInventoryItem(69 + 4.7f * 2, 42, 4.5f, 8f);
            this.platform.addInventoryItem(69 + 4.7f * 3, 42, 4.5f, 8f);
            this.platform.addInventoryItem(69 + 4.7f * 4, 42, 4.5f, 8f);
            this.platform.addInventoryItem(69 + 4.7f * 5, 42, 4.5f, 8f);

            // Second row
            this.platform.addInventoryItem(69, 42 + 8.2f, 4.5f, 8f);
            this.platform.addInventoryItem(69 + 4.7f, 42 + 8.2f, 4.5f, 8f);
            this.platform.addInventoryItem(69 + 4.7f * 2, 42 + 8.2f, 4.5f, 8f);
            this.platform.addInventoryItem(69 + 4.7f * 3, 42 + 8.2f, 4.5f, 8f);
            this.platform.addInventoryItem(69 + 4.7f * 4, 42 + 8.2f, 4.5f, 8f);
            this.platform.addInventoryItem(69 + 4.7f * 5, 42 + 8.2f, 4.5f, 8f);

            // Third row
            this.platform.addInventoryItem(69, 42 + 8.2f * 2, 4.5f, 8f);
            this.platform.addInventoryItem(69 + 4.7f, 42 + 8.2f * 2, 4.5f, 8f);
            this.platform.addInventoryItem(69 + 4.7f * 2, 42 + 8.2f * 2, 4.5f, 8f);
            this.platform.addInventoryItem(69 + 4.7f * 3, 42 + 8.2f * 2, 4.5f, 8f);
            this.platform.addInventoryItem(69 + 4.7f * 4, 42 + 8.2f * 2, 4.5f, 8f);
            this.platform.addInventoryItem(69 + 4.7f * 5, 42 + 8.2f * 2, 4.5f, 8f);

            this.platform.addPlainImage(0, 100 - 100 * 0.189f * 1.777f + 1, 100, 100 * 0.189f * 1.777f, "HUD_small");
        }

        public void update()
        {

            this.platform.update();

            // Get Keyboard input to change overall GameState
            if (EmodiaQuest.Core.GUI.Controls_GUI.Instance.keyClicked(Keys.I))
                EmodiaQuest_Game.Gamestate_Game = GameStates_Overall.IngameScreen;
        }

        public void draw(SpriteBatch spritebatch)
        {
            this.platform.draw(spritebatch);
        }

    }
}
