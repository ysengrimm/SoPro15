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

        bool questMenuIsActive = true;

        //EventHandler
        //public event GUI_Delegate_Slider TrickySliderDelegate;
        void SliderEventValue(object source, SliderEvent_GUI e)
        {
            switch (e.Function)
            {
                case "menuChange":
                    switch (e.SliderValue)
                    {
                        case 0:
                            questMenuIsActive = true;
                            // Here the stuff for non-questing is made invisible
                            break;
                        case 1:
                            questMenuIsActive = false;
                            // Set all possible quests to invisible
                            setInvisibleInclickable();

                            // Set the accept-button for the quests to invisible
                            platform.updateButtonVisibility("accept", false);
                            platform.updateButtonClickability("accept", false);

                            platform.updateDialogueIsVisible("questDescription", false);
                            platform.updateDialogueIsVisible("questStory", false);

                            break;
                        default:
                            Console.WriteLine("This SliderValue musn't be possible.");
                            break;
                    }
                    break;
                default:
                    Console.WriteLine("Function name does not exist");
                    break;
            }
        }

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

                        platform.updateDialogueIsVisible("questDescription", false);
                        platform.updateDialogueIsVisible("questStory", false);
                    }
                    else
                    {
                        buttonInMovement = e.ButtonFunction;
                        labelInMovement = "label1";
                        inMovement = true;
                        currentActiveQuest = QuestController.Instance.PossibleActiveQuests[0];

                        platform.updateButtonText("accept", "Annehmen");
                        platform.updateResolution(Settings.Instance.Resolution.X, Settings.Instance.Resolution.Y);

                        platform.updateDialogueText("questDescription", currentActiveQuest.Description);
                        platform.updateDialogueIsVisible("questDescription", true);
                        platform.updateDialogueText("questStory", currentActiveQuest.Story);
                        platform.updateDialogueIsVisible("questStory", true);
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

                        platform.updateDialogueIsVisible("questDescription", false);
                        platform.updateDialogueIsVisible("questStory", false);
                    }
                    else
                    {
                        buttonInMovement = e.ButtonFunction;
                        labelInMovement = "label2";
                        inMovement = true;
                        currentActiveQuest = QuestController.Instance.PossibleActiveQuests[1];

                        platform.updateButtonText("accept", "Annehmen");
                        platform.updateResolution(Settings.Instance.Resolution.X, Settings.Instance.Resolution.Y);

                        platform.updateDialogueText("questDescription", currentActiveQuest.Description);
                        platform.updateDialogueIsVisible("questDescription", true);
                        platform.updateDialogueText("questStory", currentActiveQuest.Story);
                        platform.updateDialogueIsVisible("questStory", true);
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

                        platform.updateDialogueIsVisible("questDescription", false);
                        platform.updateDialogueIsVisible("questStory", false);
                    }
                    else
                    {
                        buttonInMovement = e.ButtonFunction;
                        labelInMovement = "label3";
                        inMovement = true;
                        currentActiveQuest = QuestController.Instance.PossibleActiveQuests[2];

                        platform.updateButtonText("accept", "Annehmen");
                        platform.updateResolution(Settings.Instance.Resolution.X, Settings.Instance.Resolution.Y);

                        platform.updateDialogueText("questDescription", currentActiveQuest.Description);
                        platform.updateDialogueIsVisible("questDescription", true);
                        platform.updateDialogueText("questStory", currentActiveQuest.Story);
                        platform.updateDialogueIsVisible("questStory", true);
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

                        platform.updateDialogueIsVisible("questDescription", false);
                        platform.updateDialogueIsVisible("questStory", false);
                    }
                    else
                    {
                        buttonInMovement = e.ButtonFunction;
                        labelInMovement = "label4";
                        inMovement = true;
                        currentActiveQuest = QuestController.Instance.PossibleActiveQuests[3];

                        platform.updateButtonText("accept", "Annehmen");
                        platform.updateResolution(Settings.Instance.Resolution.X, Settings.Instance.Resolution.Y);

                        platform.updateDialogueText("questDescription", currentActiveQuest.Description);
                        platform.updateDialogueIsVisible("questDescription", true);
                        platform.updateDialogueText("questStory", currentActiveQuest.Story);
                        platform.updateDialogueIsVisible("questStory", true);
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

                        platform.updateDialogueIsVisible("questDescription", false);
                        platform.updateDialogueIsVisible("questStory", false);
                    }
                    else
                    {
                        buttonInMovement = e.ButtonFunction;
                        labelInMovement = "label5";
                        inMovement = true;
                        currentActiveQuest = QuestController.Instance.PossibleActiveQuests[4];

                        platform.updateButtonText("accept", "Annehmen");
                        platform.updateResolution(Settings.Instance.Resolution.X, Settings.Instance.Resolution.Y);

                        platform.updateDialogueText("questDescription", currentActiveQuest.Description);
                        platform.updateDialogueIsVisible("questDescription", true);
                        platform.updateDialogueText("questStory", currentActiveQuest.Story);
                        platform.updateDialogueIsVisible("questStory", true);
                    }
                    break;
                case "accept":
                    // Quest annehmen
                    QuestController.Instance.AcceptQuest(currentActiveQuest.Name);
                    Console.WriteLine("Quest " + currentActiveQuest.Name + " angenommen");

                    platform.updateButtonText("accept", "Quest angenommen!");
                    platform.updateResolution(Settings.Instance.Resolution.X, Settings.Instance.Resolution.Y);
                    break;
                default:
                    //Console.WriteLine(e.ButtonFunction);
                    Console.WriteLine("Function name does not exist");
                    break;
            }
        }

        // this is super random but without it there is a nullpointer exception somwhere down there, just ignore it :)
        private NPC nPCName = new NPC(Vector2.Zero, 0, new EnvironmentController(WorldState.Safeworld, Player.Instance.ContentMngr), NPC.NPCName.Jack, NPCProfession.Arbeitslos);
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
            this.platform.addLabel(50, 7, 10, "monoFont_big", nPCName.ToString(), "npcname", true);

            // labels for buttons
            this.platform.addLabel(70, 30, 5, "monoFont_small", "Hau 3 Monster um.", "label1", true);
            this.platform.addLabel(70, 40, 5, "monoFont_small", "Sammel das Buch ein.", "label2", true);
            this.platform.addLabel(70, 50, 5, "monoFont_small", "Hau 3 Monster um.", "label3", true);
            this.platform.addLabel(70, 60, 5, "monoFont_small", "Sammel das Buch ein.", "label4", true);
            this.platform.addLabel(70, 70, 5, "monoFont_small", "Hau 3 Monster um.", "label5", true);

            // buttons
            this.platform.addButton(10, 30, 20, 8, "quest1", "Ansehen");
            this.platform.addButton(10, 40, 20, 8, "quest2", "Ansehen");
            this.platform.addButton(10, 50, 20, 8, "quest3", "Ansehen");
            this.platform.addButton(10, 60, 20, 8, "quest4", "Ansehen");
            this.platform.addButton(10, 70, 20, 8, "quest5", "Ansehen");

            // buttonToAcceptQuest
            this.platform.addButton(66, 78, 25, 8, "accept", "Annehmen");
            this.platform.updateButtonVisibility("accept", false);
            this.platform.updateButtonClickability("accept", false);

            // Talk Dialogue
            platform.addDialogue(65, 40, 33, 30, "monoFont_small", "...", "questDescription");
            platform.updateDialogueScaleFactor("questDescription", 0.4f);
            platform.updateDialogueIsVisible("questDescription", false);

            platform.addDialogue(3, 40, 60, 50, "monoFont_small", "...", "questStory");
            platform.updateDialogueScaleFactor("questStory", 0.4f);
            platform.updateDialogueIsVisible("questStory", false);

            qTest = QuestController.Instance.GetAllAvailableQuests(nPCName);

            //platform.addLabel(21, 10, 8, "monoFont_small", "Quests", "labelForMenuChange", false);
            //platform.addSlider(10, 10, 10, 8, 0, 1, 0, "menuChange");

            //EventHandler;
            platform.OnButtonValue += new GUI_Delegate_Button(this.ButtonEventValue);
            platform.OnSliderValue += new GUI_Delegate_Slider(this.SliderEventValue);

        }

        public void update()
        {

            //if (EmodiaQuest.Core.GUI.Controls_GUI.Instance.keyClicked(Keys.R))
            //{
            //    platform.updateSliderPosition("menuChange", 1);
            //    platform.updateResolution(Settings.Instance.Resolution.X, Settings.Instance.Resolution.Y);
            //}
            //Console.WriteLine(questMenuIsActive);

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

                // new stuff to set the slider value
                platform.updateSliderPosition("menuChange", 0);
                platform.updateLabel("labelForMenuChange", "Quests");
                questMenuIsActive = true;
                platform.updateResolution(Settings.Instance.Resolution.X, Settings.Instance.Resolution.Y);

                EmodiaQuest_Game.Gamestate_Game = GameStates_Overall.IngameScreen;
            }

            //if (EmodiaQuest.Core.GUI.Controls_GUI.Instance.keyClicked(Keys.U))
            //{
            //    int counter = 0;
            //    Console.WriteLine(counter);
            //    //this.platform.updateButtonPosition("quest1", 60, 60);
            //    //this.platform.updateLabelPosition("label1", 80, 60);
            //    //this.platform.updateResolution(Settings.Instance.Resolution.X, Settings.Instance.Resolution.Y);
            //    //this.platform.updateLabelVisibility("label1", false);
            //    //Console.WriteLine(platform.getLabelPosition("label1"));
            //    //platform.updateButtonText("quest1", "fuu");
            //    //this.platform.updateResolution(Settings.Instance.Resolution.X, Settings.Instance.Resolution.Y);
            //}
            //    //this.platform.updateButtonVisibility("quest1", false);
            //    //this.platform.updateButtonClickability("quest1", false);
            //    this.platform.addButton(10, 35, 30, 8, "quest3", "Quest 3");
            if (inMovement)
            {
                buttonPosSave = platform.getButtonPosition(buttonInMovement);
                labelPosSave = platform.getLabelPosition(labelInMovement);

                setInvisibleInclickable();

                this.platform.updateButtonVisibility(buttonInMovement, true);
                this.platform.updateButtonPosition(buttonInMovement, 68.5f, 88.5f);
                this.platform.updateLabelVisibility(labelInMovement, true);
                this.platform.updateLabelPosition(labelInMovement, 50, 25);
                this.platform.updateButtonText(buttonInMovement, "Zurueck");
                this.platform.updateResolution(Settings.Instance.Resolution.X, Settings.Instance.Resolution.Y);

                this.platform.updateButtonClickability(buttonInMovement, true);


                this.platform.updateButtonVisibility("accept", true);
                this.platform.updateButtonClickability("accept", true);
                inMovement = false;
                unfold = true;
            }

            if (EmodiaQuest.Core.GUI.Controls_GUI.Instance.keyClicked(Keys.Escape))
            {
                EmodiaQuest_Game.Gamestate_Game_Continue = GameStates_Overall.NPCScreen;
                EmodiaQuest_Game.Gamestate_Game = GameStates_Overall.MenuScreen;
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
