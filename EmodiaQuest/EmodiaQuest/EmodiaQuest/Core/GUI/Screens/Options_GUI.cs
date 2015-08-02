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
    class Options_GUI
    {
        //EventHandler
        void SliderEventValue(object source, SliderEvent_GUI e)
        {
            switch (e.Function)
            {
                case "volumeChange":
                    EmodiaQuest.Core.Settings.Instance.Volume = (float)(e.SliderValue)/100;
                    this.platform.updateLabel("volume", e.SliderValue.ToString());
                    GraphicsCopy.ApplyChanges();
                    break;
                case "resolutionChange":
                    EmodiaQuest.Core.Settings.Instance.Resolution = EmodiaQuest.Core.Settings.PossibleResolutions[e.SliderValue];
                    int resX = EmodiaQuest.Core.Settings.Instance.Resolution.X;
                    int resY = EmodiaQuest.Core.Settings.Instance.Resolution.Y;
                    this.platform.updateLabel("resolution", resX + " x " + resY);
                    EmodiaQuest.Core.GUI.Platform_GUI.updateAllResolutions(resX, resY);
                    //GraphicsCopy.PreferredBackBufferWidth = resX;
                    //GraphicsCopy.PreferredBackBufferHeight = resY;
                    //GraphicsCopy.ApplyChanges();
                    if (EmodiaQuest.Core.Settings.Instance.Fullscreen)
                        this.platform.updateLabel("fullscreenMode", "Fullscreen");
                    else
                        this.platform.updateLabel("fullscreenMode", "Windowed");
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
                case "changeToMainMenu":
                    EmodiaQuest_Game.Gamestate_Game = GameStates_Overall.MenuScreen;
                    break;
                case "switchFullscreen":                    
                    EmodiaQuest.Core.Settings.Instance.Fullscreen = !EmodiaQuest.Core.Settings.Instance.Fullscreen;
                    if (EmodiaQuest.Core.Settings.Instance.Fullscreen)
                        this.platform.updateLabel("fullscreenMode", "Fullscreen");
                    else
                        this.platform.updateLabel("fullscreenMode", "Windowed");
                    break;
                default:
                    Console.WriteLine("Function name does not exist");
                    break;
            }
        }

        private static Options_GUI instance;

        private Options_GUI() { }

        public static Options_GUI Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Options_GUI();
                }
                return instance;
            }
        }


        private Platform_GUI platform = new Platform_GUI();
        public GraphicsDeviceManager GraphicsCopy { get; set; }

        public void loadContent(ContentManager Content)
        {
            this.platform.loadContent(Content);

            //Resolution
            //Fullscreen
            //Volume
            this.platform.setBackground(Content, "Content_GUI/menu_background");

            //this.platform.addPlainText(50.0f, 10.0f, "monoFont_big", "OPTIONS MENU", true);
            this.platform.addLabel(50, 10, 18, "dice_big", "Options", "Options", true);

            this.platform.addSlider(20, 40, 30, 8, 0, 2, 0, "resolutionChange");
            this.platform.addSlider(20, 50, 30, 8, 0, 100, 100, "volumeChange");            

            this.platform.addLabel(60, 40, 30, 8, "monoFont_small", "854 x 480", "resolution");
            this.platform.addLabel(60, 50, 30, 8, "monoFont_small", "100", "volume");
            //this.platform.addSlider(35, 60, 30, 8, 0, 100, "volumeChange2");

            this.platform.addButton(20, 60, 30, 8, "switchFullscreen", "Screen Mode");
            this.platform.addLabel(60, 60, 30, 8, "monoFont_small", "Windowed", "fullscreenMode");

            this.platform.addButton(35, 75, 30, 8, "changeToMainMenu", "Main Menu");

            //this.platform.addButton(35, 60, 30, 8, "nextState", "Start Game");
           // this.platform.addPlainImage

            //this.platform.addPlainImage(10, 10, 30, 30, "HUD_small");

            //EventHandler;
            platform.OnSliderValue += new GUI_Delegate_Slider(this.SliderEventValue);
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
    }
}
