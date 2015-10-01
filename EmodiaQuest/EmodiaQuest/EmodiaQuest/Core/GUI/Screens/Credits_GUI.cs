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

            platform.addDialogue(5, 30, 90, 50,"monoFont_small",
            " ;" +
            " ;" +
            " ;" +
            " ;" +
            " ;" +
            " ;" +
            " ;" +
            " ;" +
            " ;" +
            "Willkommen.;" +
            "Hier kommt der ganze Text fuer die Credits rein.;" +
            "Breite einfach ungefaehr per Augenmass ausrichten.;"+
            "This.....;"+
            "Shit.....;"+
            "Shit.....;" +
            "Shit.....;" +
            "Erdacht und entwickelt von:;" +
            "Janos Zimmermann;" +
            "Alex Mikulinski;" +
            "Claudius Grimm;" +
            "Kim Krietemeier;" +
            " ;" +
            " ;" +
            "Thanks for playing this game."+
            " ;" +
            "See you soon.;"
            
            
            , "creditsText");


            platform.updateDialogueScaleFactor("creditsText", 0.5f);
            
            platform.updateDialogueMoveSpeed("creditsText", 1.5f);
            platform.updateDialogueIsMoving("creditsText", true);
            platform.updateDialogueBoxIsShown("creditsText", false);


            //platform.updateDialogueBoxIsShown("creditsText", false);
            // isMoving, MoveSpeed, Scale, Text, BoxIsShown, BeiNeuemTextAllesNeu


            //EventHandler;
            platform.OnButtonValue += new GUI_Delegate_Button(this.ButtonEventValue);
        }

        public void update()
        {
            //if (EmodiaQuest.Core.GUI.Controls_GUI.Instance.keyClicked(Keys.Z))
            //{
            //    //platform.updateDialogueBoxIsDown("creditsText", false);
            //    //platform.updateDialogueScaleFactor("creditsText", 3.0f);
            //    //platform.updateDialogueText("creditsText", "NEUER TEXT!");
            //    platform.updateDialoguePosition("creditsText");
            //}
            if (EmodiaQuest.Core.GUI.Controls_GUI.Instance.keyClicked(Keys.U))
            {
                //platform.updateDialogueBoxIsDown("creditsText", false);
                //platform.updateDialogueScaleFactor("creditsText", 3.0f);
                //platform.updateDialogueText("creditsText", "NEUER TEXT!");
                platform.updateDialogueText("creditsText", "Das hier;Ist;Der;Neue...;Text!;Der;ist;Neu.;Echt;");
            }
            if (EmodiaQuest.Core.GUI.Controls_GUI.Instance.keyClicked(Keys.N))
            {
                platform.updateDialogueIsMoving("creditsText", true);
            }
            if (EmodiaQuest.Core.GUI.Controls_GUI.Instance.keyClicked(Keys.M))
            {
                platform.updateDialogueIsMoving("creditsText", false);
            }
            if (EmodiaQuest.Core.GUI.Controls_GUI.Instance.keyClicked(Keys.V))
            {
                platform.updateDialogueBoxIsShown("creditsText", false);
            }

            this.platform.update();

            if (EmodiaQuest.Core.GUI.Controls_GUI.Instance.keyClicked(Keys.Escape))
            {
                EmodiaQuest_Game.Gamestate_Game = GameStates_Overall.MenuScreen;
            }
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
