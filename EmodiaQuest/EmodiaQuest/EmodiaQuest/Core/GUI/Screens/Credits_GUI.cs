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
    public class Credits_GUI
    {
        private static Credits_GUI instance;

        private Credits_GUI() { }

        public static Credits_GUI Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Credits_GUI();
                }
                return instance;
            }
        }

        private Platform_GUI platform = new Platform_GUI();

        public void loadContent(ContentManager Content)
        {
            this.platform.loadContent(Content);

            this.platform.setBackground(Content, "Content_GUI/menu_full_small");

            // headline
            this.platform.addLabel(50, 10, 20, "monoFont_big", "Credits", "credits", true);

            this.platform.addButton(38, 85, 24, 8, "menu", "Menue");

            //this.platform.addLabel(50, 35, 6, "monoFont_small", "Janos Zimmermann", "Menu", true);
            //this.platform.addLabel(50, 40, 6, "monoFont_small", "Alex Mikulinski", "Menu", true);
            //this.platform.addLabel(50, 45, 6, "monoFont_small", "Claudius Grimm", "Menu", true);
            //this.platform.addLabel(50, 50, 6, "monoFont_small", "Kim Krietemeier", "Menu", true);

            

            platform.addPlainImage(5, 5, 20, 20, "acagamics", "pixel_red");
            platform.updatePlainImagePicture("acagamics", "other/acagamics");

            // textBox-test
            platform.addDialogue(20, 50, 30, 30, "monoFont_small", "Let's.......;Test....;This.....;Shit.....;Out.....", "testding");
            //platform.updateDialogueScaleFactor("testding", 0.2f);
            
            platform.updateDialogueMoveSpeed("testding", 2);
            //platform.updateDialogueBoxIsShown("testding", false);
            // isMoving, MoveSpeed, Scale, Text, BoxIsShown, BeiNeuemTextAllesNeu


            //EventHandler;
            platform.OnButtonValue += new GUI_Delegate_Button(this.ButtonEventValue);
        }

        public void update()
        {
            //if (EmodiaQuest.Core.GUI.Controls_GUI.Instance.keyClicked(Keys.Z))
            //{
            //    //platform.updateDialogueBoxIsDown("testding", false);
            //    //platform.updateDialogueScaleFactor("testding", 3.0f);
            //    //platform.updateDialogueText("testding", "NEUER TEXT!");
            //    platform.updateDialoguePosition("testding");
            //}
            if (EmodiaQuest.Core.GUI.Controls_GUI.Instance.keyClicked(Keys.U))
            {
                //platform.updateDialogueBoxIsDown("testding", false);
                //platform.updateDialogueScaleFactor("testding", 3.0f);
                //platform.updateDialogueText("testding", "NEUER TEXT!");
                platform.updateDialogueText("testding", "Das hier;Ist;Der;Neue...;Text!;Der;ist;Neu.;Echt;");
            }
            if (EmodiaQuest.Core.GUI.Controls_GUI.Instance.keyClicked(Keys.N))
            {
                platform.updateDialogueIsMoving("testding", true);
            }
            if (EmodiaQuest.Core.GUI.Controls_GUI.Instance.keyClicked(Keys.M))
            {
                platform.updateDialogueIsMoving("testding", false);
            }
            if (EmodiaQuest.Core.GUI.Controls_GUI.Instance.keyClicked(Keys.V))
            {
                platform.updateDialogueBoxIsShown("testding", false);
            }

            this.platform.update();
        }

        public void draw(SpriteBatch spritebatch)
        {
            this.platform.draw(spritebatch);
        }

        // EventHandler
        void ButtonEventValue(object source, ButtonEvent_GUI e)
        {
            switch (e.ButtonFunction)
            {
                case "menu":
                    EmodiaQuest_Game.Gamestate_Game = GameStates_Overall.MenuScreen;
                    break;
                default:
                    Console.WriteLine("No such Function.");
                    break;
            }
        }

    }
}
