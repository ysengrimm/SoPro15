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
        //EventHandler
        void SliderEventValue(object source, SliderEvent_GUI e)
        {
            switch (e.Function)
            {
                case "changeinventory":
                    switch (e.SliderValue)
                    {
                        case 0:
                            this.platform.updateLabel("changeside", "Questside");
                            break;
                        case 1:
                            this.platform.updateLabel("changeside", "StorySide");
                            break;
                        case 2:
                            this.platform.updateLabel("changeside", "Questside");
                            break;
                        default:
                            break;
                    }
                    break;
                default:
                    Console.WriteLine("Function name does not exist");
                    break;
            }
        }

        //EventHandler
        void ChangeValueEventValue(object source, ChangeValueEvent e)
        {
            switch (e.Function)
            {
                case "hp":
                    break;
                case "xp_next_lvl":
                    platform.updateLabel("xp_text", Player.Instance.Experience + "/" + Player.Instance.XPToNextLevel);
                    break;
                case "level":
                    platform.updateLabel("lvl", e.ChangeValue.ToString());
                    break;
                case "gold":
                    platform.updateLabel("gold", e.ChangeValue.ToString());
                    break;
                case "armor":
                    platform.updateLabel("armor", "Ruestung: " + e.ChangeValue.ToString());
                    break;
                case "strength":
                    platform.updateLabel("str", "Kraft: " + e.ChangeValue.ToString());
                    break;
                case "skill":
                    platform.updateLabel("skill", "Fertigkeit: " + e.ChangeValue.ToString());
                    break;
                case "intelligence":
                    platform.updateLabel("intel", "Intelligenz: " + e.ChangeValue.ToString());
                    break;
                case "dmg":
                    platform.updateLabel("damage", "Schaden: " + Player.Instance.MinDamage + " - " + Player.Instance.MaxDamage);
                    break;
            }
        }

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

            //mystuff
            //this.platform.addPlainImage(0, 0, 50, 50, "testtest1", "test1");
            //this.platform.addPlainImage(50, 50, 50, 50, "testtest2", "test2");

            this.platform.addSlider(5, 5, 24, 8, 0, 2, 0, "changeinventory");

            //this.platform.addLabel(15, 0, 10, "monoFont_big", "QuestLog", "questlog", true);
            //this.platform.addLabel(15, 40, 10, "monoFont_big", "QuestText", "questtext", true);
            this.platform.addLabel(15, 20, 10, "monoFont_big", "QuestSide", "changeside", true);

            this.platform.addLabel(50, 0, 10, "monoFont_big", "Character Stats", "story", true);

            this.platform.addLabel(50, 40, 10, "monoFont_big", "Item Stats", "stats", true);
            this.platform.addLabel(85, 0, 10, "monoFont_big", "Mini-Map", "inventory", true);

            // ItemSockets (Look out to only use 16:9 or it will not be a square!)
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

            this.platform.addPlainImage(0, 100 - 100 * 0.189f * 1.777f + 1, 100, 100 * 0.189f * 1.777f, "HUD", "HUD_small");
            //this.platform.addPlainImage(100, 100, -100, -100 * 0.189f * 1.777f, "HUD", "HUD_small");

            platform.OnSliderValue += new GUI_Delegate_Slider(this.SliderEventValue);

            // XP Number
            platform.addLabel(50, 50, 5, "monoFont_big", Player.Instance.Experience + "/" + Player.Instance.XPToNextLevel, "xp_text", true);

            // Level Number
            platform.addLabel(50, 40, 5, "monoFont_big", Player.Instance.Level.ToString(), "lvl", true);

            // Gold
            platform.addLabel(50, 60, 5, "monoFont_big", Player.Instance.Gold.ToString(), "gold", true);

            // Player stats
            platform.addLabel(50, 10, 5, "monoFont_big", "Ruestung: " + Player.Instance.Armor, "armor", true);
            platform.addLabel(50, 15, 5, "monoFont_big", "Schaden: " + Player.Instance.MinDamage + " - " + Player.Instance.MaxDamage, "damage", true);
            platform.addLabel(50, 20, 5, "monoFont_big", "Kraft: " + Player.Instance.Strength, "str", true);
            platform.addLabel(50, 25, 5, "monoFont_big", "Fertigkeit: " + Player.Instance.Skill, "skill", true);
            platform.addLabel(50, 30, 5, "monoFont_big", "Intelligenz: " + Player.Instance.Intelligence, "intel", true);

            Player.Instance.OnChangeValue += new Delegates_CORE.ChangeValueDelegate(ChangeValueEventValue);

            // Items
            // Hab grad nur First genommen, weil ich erst ein Item drin hab. Sonst natuerlich ElementAt
            this.platform.CharItems.Add(Items.ItemTestClass.Instance.Quests.First());
            this.platform.CharItems.Add(Items.ItemTestClass.Instance.Helmets.First());
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

        public void addCharItem(Items.Item item)
        {
            this.platform.CharItems.Add(item);
            Console.WriteLine("New Item with ID: " + item.ID + " has been added.");

            // Ein Aufruf sieht so aus: Man muss halt ein Item aus den Listen aus ItemTestclass nehmen.
            // EmodiaQuest.Core.GUI.Screens.Inventory_GUI.Instance.addCharItem(Items.ItemTestClass.Instance.Armors.First());
        }

    }
}
