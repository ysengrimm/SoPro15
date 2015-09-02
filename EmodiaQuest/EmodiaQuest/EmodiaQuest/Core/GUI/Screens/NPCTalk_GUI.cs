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
    public class NPCTalk_GUI
    {
        private static NPCTalk_GUI instance;

        private NPCTalk_GUI() { }

        public static NPCTalk_GUI Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new NPCTalk_GUI();
                }
                return instance;
            }
        }

        private Platform_GUI platform = new Platform_GUI();

        void ButtonEventValue(object source, ButtonEvent_GUI e)
        {
            switch (e.ButtonFunction)
            {
                case "quest1":
                    this.platform.updateLabel("label1", "Angenommen");
                    break;
                case "quest2":
                    this.platform.updateLabel("label2", "Angenommen");
                    //EmodiaQuest_Game.Gamestate_Game = GameStates_Overall.MenuScreen;
                    break;
                default:
                    //Console.WriteLine(e.ButtonFunction);
                    Console.WriteLine("Function name does not exist");
                    break;
            }
        }



        private string nPCName = "Konstantin";
        public string NPCName
        {
            get { return nPCName; }
            set
            {
                this.nPCName = value;
                this.platform.updateLabel("npcname", value);
            }
        }

        public void loadContent(ContentManager Content)
        {
            this.platform.loadContent(Content);
            //this.platform.backgroundOff();

            // headline
            this.platform.addLabel(50, 10, 8, "monoFont_big", this.nPCName, "npcname", true);

            // labels for buttons
            this.platform.addLabel(70, 25, 8, "monoFont_small", "Hau 3 Monster um.", "label1", true);
            this.platform.addLabel(70, 35, 8, "monoFont_small", "Sammel das Buch ein.", "label2", true);

            // buttons
            this.platform.addButton(10, 25, 20, 8, "quest1", "Quest 1");
            this.platform.addButton(10, 35, 20, 8, "quest2", "Quest 2");



            //var questsByOwner = from quest in Quests where quest.Owner == "$OwnerNameHere" select quest;
            //QuestController.Instance.Quests




            platform.OnButtonValue += new GUI_Delegate_Button(this.ButtonEventValue);
        }

        public void update()
        {
            this.platform.update();

            // Get Keyboard input to change overall GameState
            if (EmodiaQuest.Core.GUI.Controls_GUI.Instance.keyClicked(Keys.Z))
                EmodiaQuest_Game.Gamestate_Game = GameStates_Overall.IngameScreen;
        }

        public void draw(SpriteBatch spritebatch)
        {
            this.platform.draw(spritebatch);
        }
    }
}
