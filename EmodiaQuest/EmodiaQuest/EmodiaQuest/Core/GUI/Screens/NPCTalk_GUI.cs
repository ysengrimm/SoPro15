using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EmodiaQuest.Core.NPCs;
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

        private Quest currentActiveQuest;

        void ButtonEventValue(object source, ButtonEvent_GUI e)
        {
            switch (e.ButtonFunction)
            {
                case "quest1":
                    //this.platform.updateLabel("label1", "Angenommen");
                    if (unfold)
                    {
                        setVisibleClickable();
                        this.platform.updateButtonVisibility("accept", false);
                        this.platform.updateButtonClickability("accept", false);

                        this.platform.updateButtonPosition(buttonInMovement, buttonPosSave.X, buttonPosSave.Y);
                        this.platform.updateLabelPosition(labelInMovement, labelPosSave.X, labelPosSave.Y);
                        this.platform.updateButtonText(buttonInMovement, "Ansehen");
                        this.platform.updateResolution(Settings.Instance.Resolution.X, Settings.Instance.Resolution.Y);
                        unfold = false;
                    }
                    else
                    {
                        buttonInMovement = e.ButtonFunction;
                        labelInMovement = "label1";
                        inMovement = true;
                        currentActiveQuest = QuestController.Instance.PossibleActiveQuests[0];
                    }

                    break;
                case "quest2":
                    //this.platform.updateLabel("label1", "Angenommen");
                    if (unfold)
                    {
                        setVisibleClickable();
                        this.platform.updateButtonVisibility("accept", false);
                        this.platform.updateButtonClickability("accept", false);

                        this.platform.updateButtonPosition(buttonInMovement, buttonPosSave.X, buttonPosSave.Y);
                        this.platform.updateLabelPosition(labelInMovement, labelPosSave.X, labelPosSave.Y);
                        this.platform.updateButtonText(buttonInMovement, "Ansehen");
                        this.platform.updateResolution(Settings.Instance.Resolution.X, Settings.Instance.Resolution.Y);
                        unfold = false;
                    }
                    else
                    {
                        buttonInMovement = e.ButtonFunction;
                        labelInMovement = "label2";
                        inMovement = true;
                        currentActiveQuest = QuestController.Instance.PossibleActiveQuests[1];
                    }
                    break;
                case "quest3":
                    //this.platform.updateLabel("label1", "Angenommen");
                    if (unfold)
                    {
                        setVisibleClickable();
                        this.platform.updateButtonVisibility("accept", false);
                        this.platform.updateButtonClickability("accept", false);

                        this.platform.updateButtonPosition(buttonInMovement, buttonPosSave.X, buttonPosSave.Y);
                        this.platform.updateLabelPosition(labelInMovement, labelPosSave.X, labelPosSave.Y);
                        this.platform.updateButtonText(buttonInMovement, "Ansehen");
                        this.platform.updateResolution(Settings.Instance.Resolution.X, Settings.Instance.Resolution.Y);
                        unfold = false;
                    }
                    else
                    {
                        buttonInMovement = e.ButtonFunction;
                        labelInMovement = "label3";
                        inMovement = true;
                        currentActiveQuest = QuestController.Instance.PossibleActiveQuests[2];
                    }
                    break;
                case "quest4":
                    //this.platform.updateLabel("label1", "Angenommen");
                    if (unfold)
                    {
                        setVisibleClickable();
                        this.platform.updateButtonVisibility("accept", false);
                        this.platform.updateButtonClickability("accept", false);

                        this.platform.updateButtonPosition(buttonInMovement, buttonPosSave.X, buttonPosSave.Y);
                        this.platform.updateLabelPosition(labelInMovement, labelPosSave.X, labelPosSave.Y);
                        this.platform.updateButtonText(buttonInMovement, "Ansehen");
                        this.platform.updateResolution(Settings.Instance.Resolution.X, Settings.Instance.Resolution.Y);
                        unfold = false;
                    }
                    else
                    {
                        buttonInMovement = e.ButtonFunction;
                        labelInMovement = "label4";
                        inMovement = true;
                        currentActiveQuest = QuestController.Instance.PossibleActiveQuests[3];
                    }
                    break;
                case "quest5":
                    //this.platform.updateLabel("label1", "Angenommen");
                    if (unfold)
                    {
                        setVisibleClickable();
                        this.platform.updateButtonVisibility("accept", false);
                        this.platform.updateButtonClickability("accept", false);

                        this.platform.updateButtonPosition(buttonInMovement, buttonPosSave.X, buttonPosSave.Y);
                        this.platform.updateLabelPosition(labelInMovement, labelPosSave.X, labelPosSave.Y);
                        this.platform.updateButtonText(buttonInMovement, "Ansehen");
                        this.platform.updateResolution(Settings.Instance.Resolution.X, Settings.Instance.Resolution.Y);
                        unfold = false;
                    }
                    else
                    {
                        buttonInMovement = e.ButtonFunction;
                        labelInMovement = "label5";
                        inMovement = true;
                        currentActiveQuest = QuestController.Instance.PossibleActiveQuests[4];
                    }
                    break;
                case "accept":
                    // Quest annehmen
                    QuestController.Instance.AcceptQuest(currentActiveQuest.Name);
                    Console.WriteLine("Quest " + currentActiveQuest.Name + " angenommen");
                    break;
                default:
                    //Console.WriteLine(e.ButtonFunction);
                    Console.WriteLine("Function name does not exist");
                    break;
            }
        }

        // this is super random but without it there is a nullpointer exception somwhere down there, just ignore it :)
        private NPC nPCName = new NPC(Vector2.Zero, new EnvironmentController(WorldState.Safeworld, Player.Instance.ContentMngr),NPC.NPCName.Jack, NPCProfession.Arbeitslos);
        public NPC NPCName
        {
            get { return nPCName; }
            set
            {
                isOpened = true;

                nPCName = value;
                qTest = QuestController.Instance.GetAllAvailableQuests(nPCName);
                questCount = 0;
                foreach (var q in qTest)
                {
                    //Console.WriteLine(q.Name);
                    questCount++;
                    this.platform.updateButtonVisibility("quest" + questCount, true);
                    this.platform.updateButtonClickability("quest" + questCount, true);
                    this.platform.updateLabel("label" + questCount, q.Name);
                }
                while (questCount < 5)
                {
                    questCount++;
                    this.platform.updateButtonVisibility("quest" + questCount, false);
                    this.platform.updateButtonClickability("quest" + questCount, false);
                    this.platform.updateLabel("label" + questCount, "");
                }
                platform.updateLabel("npcname", nPCName.ToString());
            }
        }

        // bool for the case, that this dialogue is just opened
        public bool isOpened = false;

        // Questlist
        private List<Quest> qTest;

        // QuestCounter
        private int questCount = 0;

        // MovementForButtonsAndLabels
        bool inMovement = false;
        string buttonInMovement = "b1";
        string labelInMovement = "l1";
        bool unfold = false;
        Vector2 buttonPosSave = new Vector2(0, 0);
        Vector2 labelPosSave = new Vector2(0, 0);

        public void loadContent(ContentManager Content)
        {
            this.platform.loadContent(Content);
            //this.platform.backgroundOff();

            // headline
            this.platform.addLabel(50, 10, 8, "monoFont_big", nPCName.ToString(), "npcname", true);

            // labels for buttons
            this.platform.addLabel(70, 30, 8, "monoFont_small", "Hau 3 Monster um.", "label1", true);
            this.platform.addLabel(70, 40, 8, "monoFont_small", "Sammel das Buch ein.", "label2", true);
            this.platform.addLabel(70, 50, 8, "monoFont_small", "Hau 3 Monster um.", "label3", true);
            this.platform.addLabel(70, 60, 8, "monoFont_small", "Sammel das Buch ein.", "label4", true);
            this.platform.addLabel(70, 70, 8, "monoFont_small", "Hau 3 Monster um.", "label5", true);

            // buttons
            this.platform.addButton(10, 30, 20, 8, "quest1", "Ansehen");
            this.platform.addButton(10, 40, 20, 8, "quest2", "Ansehen");
            this.platform.addButton(10, 50, 20, 8, "quest3", "Ansehen");
            this.platform.addButton(10, 60, 20, 8, "quest4", "Ansehen");
            this.platform.addButton(10, 70, 20, 8, "quest5", "Ansehen");

            // buttonToAcceptQuest
            this.platform.addButton(60, 80, 20, 8, "accept", "Annehmen");
            this.platform.updateButtonVisibility("accept", false);
            this.platform.updateButtonClickability("accept", false);

            qTest = QuestController.Instance.GetAllAvailableQuests(nPCName);
            
            platform.OnButtonValue += new GUI_Delegate_Button(this.ButtonEventValue);
        }

        public void update()
        {

            //QuestController.Instance.Quests


            this.platform.update();

            // Check if the dialogue was just opened
            if (isOpened)
            {
                setVisibleClickable();
                this.platform.updateButtonVisibility("accept", false);
                this.platform.updateButtonClickability("accept", false);

                this.platform.updateButtonPosition(buttonInMovement, buttonPosSave.X, buttonPosSave.Y);
                this.platform.updateLabelPosition(labelInMovement, labelPosSave.X, labelPosSave.Y);
                this.platform.updateButtonText(buttonInMovement, "Ansehen");
                this.platform.updateResolution(Settings.Instance.Resolution.X, Settings.Instance.Resolution.Y);
                unfold = false;
                isOpened = false;
            }

            // Get Keyboard input to change overall GameState
            if (EmodiaQuest.Core.GUI.Controls_GUI.Instance.keyClicked(Keys.E))
            {
                setVisibleClickable();
                this.platform.updateButtonVisibility("accept", false);
                this.platform.updateButtonClickability("accept", false);

                this.platform.updateButtonPosition(buttonInMovement, buttonPosSave.X, buttonPosSave.Y);
                this.platform.updateLabelPosition(labelInMovement, labelPosSave.X, labelPosSave.Y);
                this.platform.updateButtonText(buttonInMovement, "Ansehen");
                this.platform.updateResolution(Settings.Instance.Resolution.X, Settings.Instance.Resolution.Y);
                unfold = false;
                EmodiaQuest_Game.Gamestate_Game = GameStates_Overall.IngameScreen;
            }

            if (EmodiaQuest.Core.GUI.Controls_GUI.Instance.keyClicked(Keys.U))
            {
                int counter = 0;
                Console.WriteLine(counter);
                //this.platform.updateButtonPosition("quest1", 60, 60);
                //this.platform.updateLabelPosition("label1", 80, 60);
                //this.platform.updateResolution(Settings.Instance.Resolution.X, Settings.Instance.Resolution.Y);
                //this.platform.updateLabelVisibility("label1", false);
                //Console.WriteLine(platform.getLabelPosition("label1"));
                //platform.updateButtonText("quest1", "fuu");
                //this.platform.updateResolution(Settings.Instance.Resolution.X, Settings.Instance.Resolution.Y);
            }
            //    //this.platform.updateButtonVisibility("quest1", false);
            //    //this.platform.updateButtonClickability("quest1", false);
            //    this.platform.addButton(10, 35, 30, 8, "quest3", "Quest 3");
            if (inMovement)
            {
                buttonPosSave = platform.getButtonPosition(buttonInMovement);
                labelPosSave = platform.getLabelPosition(labelInMovement);

                setInvisibleInclickable();

                this.platform.updateButtonVisibility(buttonInMovement, true);
                this.platform.updateButtonPosition(buttonInMovement, 10, 30);
                this.platform.updateLabelVisibility(labelInMovement, true);
                this.platform.updateLabelPosition(labelInMovement, 70, 30);
                this.platform.updateButtonText(buttonInMovement, "Schliessen");
                this.platform.updateResolution(Settings.Instance.Resolution.X, Settings.Instance.Resolution.Y);

                this.platform.updateButtonClickability(buttonInMovement, true);


                this.platform.updateButtonVisibility("accept", true);
                this.platform.updateButtonClickability("accept", true);
                inMovement = false;
                unfold = true;
            }

        }

        public void draw(SpriteBatch spritebatch)
        {
            this.platform.draw(spritebatch);
        }

        private void setInvisibleInclickable()
        {
            this.platform.updateButtonVisibility("quest1", false);
            this.platform.updateButtonClickability("quest1", false);
            this.platform.updateLabelVisibility("label1", false);
            this.platform.updateButtonVisibility("quest2", false);
            this.platform.updateButtonClickability("quest2", false);
            this.platform.updateLabelVisibility("label2", false);
            this.platform.updateButtonVisibility("quest3", false);
            this.platform.updateButtonClickability("quest3", false);
            this.platform.updateLabelVisibility("label3", false);
            this.platform.updateButtonVisibility("quest4", false);
            this.platform.updateButtonClickability("quest4", false);
            this.platform.updateLabelVisibility("label4", false);
            this.platform.updateButtonVisibility("quest5", false);
            this.platform.updateButtonClickability("quest5", false);
            this.platform.updateLabelVisibility("label5", false);
        }
        private void setVisibleClickable()
        {
            //QuestController.Instance.
            this.qTest = QuestController.Instance.GetAllAvailableQuests(nPCName);
            //this.qTest = QuestController.Instance.GetAllAvailableQuests()
            questCount = 0;
            foreach (var q in qTest)
            {
                //Console.WriteLine(q.Name);
                questCount++;
                this.platform.updateButtonVisibility("quest" + questCount, true);
                this.platform.updateButtonClickability("quest" + questCount, true);
                this.platform.updateLabelVisibility("label" + questCount, true);
            }
            while (questCount < 5)
            {
                questCount++;
                this.platform.updateButtonVisibility("quest" + questCount, false);
                this.platform.updateButtonClickability("quest" + questCount, false);
                this.platform.updateLabelVisibility("label" + questCount, false);
            }
        }
    }
}
