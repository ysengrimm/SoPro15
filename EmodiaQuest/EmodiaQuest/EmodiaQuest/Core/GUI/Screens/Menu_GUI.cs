﻿using System;
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
        // EventHandler
        void ButtonEventValue(object source, ButtonEvent_GUI e)
        {
            switch (e.ButtonFunction)
            {
                case "playGame":
                    this.platform.updateButtonText("playGame", "Fortsetzen");
                    this.platform.updateResolution(Settings.Instance.Resolution.X, Settings.Instance.Resolution.Y);
                    if (showIntro)
                        EmodiaQuest_Game.Gamestate_Game = GameStates_Overall.IntroScreen;
                    else
                        EmodiaQuest_Game.Gamestate_Game = EmodiaQuest_Game.Gamestate_Game_Continue;
                    break;
                case "options":
                    EmodiaQuest_Game.Gamestate_Game = GameStates_Overall.OptionsScreen;
                    break;
                case "bindings":
                    EmodiaQuest_Game.Gamestate_Game = GameStates_Overall.KeyBindingsScreen;
                    break;
                case "test":
                    platform.updateButtonPicture("test", "armor");
                    break;
                case "credits":
                    EmodiaQuest_Game.Gamestate_Game = GameStates_Overall.CreditsScreen;
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

        public bool showIntro = true;

        //private string functionCalled = null;

        public void loadContent(ContentManager Content)
        {
            this.platform.loadContent(Content);

            //this.platform.setBackground(Content, "Content_GUI/menu_background");
            this.platform.setBackground(Content, "Content_GUI/menu_full_small");

            //platform.addButton(0, 0, 50, 50, "test");

            this.platform.addButton(38, 40, 24, 8, "playGame", "Spiel starten");
            this.platform.addButton(38, 55, 24, 8, "options", "Optionen");
            this.platform.addButton(38, 70, 24, 8, "bindings", "Tastenbelegung");
            this.platform.addButton(38, 85, 24, 8, "credits", "Credits");

            this.platform.addLabel(50, 10, 20, "dice_big", "Emodia Quest", "Menu", true);
            //this.platform.addLabel(50, 30, 20, "monoFont_big", "Menu2", "Menu2", true);
            //this.platform.addLabel(30, 50, 40, 20, "monoFont_big", "labelText", "label1");
            //this.platform.updateLabel("label1", "newText");

            //this.platform.addPlainText(50.0f, 10.0f, "monoFont_big", "MAIN MENU", true);

            //EventHandler;
            platform.OnButtonValue += new GUI_Delegate_Button(this.ButtonEventValue);
        }



        public void update()
        {
            this.platform.update();
            //if ((this.functionCalled = this.platform.update()) != null)
            //    this.functionCall();
            if (EmodiaQuest.Core.GUI.Controls_GUI.Instance.keyClicked(Keys.U))
            {
                List<EmodiaQuest.Core.Items.Item> testList = new List<EmodiaQuest.Core.Items.Item>();
                //testList = EmodiaQuest.Core.Items.ItemTestClass.Instance.ItemGeneratorMerchant(15, "Konstantin");
                testList = EmodiaQuest.Core.Items.ItemTestClass.Instance.ItemGeneratorMonster(15, NPCs.EnemyType.Monster1);
                foreach (var item in testList)
                {
                    Console.WriteLine(item.Class + ", " + item.Lvl + ", " + item.StrengthPlus + ", " + item.SkillPlus + ", " + item.IntelligencePlus);


                }
                //Console.WriteLine(testList.Count);
            }
            if (EmodiaQuest.Core.GUI.Controls_GUI.Instance.keyClicked(Keys.W))
            {
                
            }
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
