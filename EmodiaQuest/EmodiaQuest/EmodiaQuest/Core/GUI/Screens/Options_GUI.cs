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
    public class Options_GUI
    {
        //EventHandler
        void SliderEventValue(object source, SliderEvent_GUI e)
        {
            switch (e.Function)
            {
                case "volumeChange":
                    EmodiaQuest.Core.Settings.Instance.MainVolume = (float)(e.SliderValue) / 100;
                    this.platform.updateLabel("volume", "Lautstaerke: " + e.SliderValue.ToString());
                    break;
                case "resolutionChange":
                    EmodiaQuest.Core.Settings.Instance.Resolution = EmodiaQuest.Core.Settings.PossibleResolutions[e.SliderValue];
                    int resX = EmodiaQuest.Core.Settings.Instance.Resolution.X;
                    int resY = EmodiaQuest.Core.Settings.Instance.Resolution.Y;
                    this.platform.updateLabel("resolution", resX + " x " + resY);
                    EmodiaQuest.Core.GUI.Platform_GUI.updateAllResolutions(resX, resY);
                    //Console.WriteLine(EmodiaQuest.Core.Settings.Instance.Resolution.X);
                    //Console.WriteLine(EmodiaQuest.Core.Settings.Instance.Resolution.Y);
                    //GraphicsCopy.PreferredBackBufferWidth = resX;
                    //GraphicsCopy.PreferredBackBufferHeight = resY;
                    //GraphicsCopy.ApplyChanges();
                    if (EmodiaQuest.Core.Settings.Instance.Fullscreen)
                        this.platform.updateLabel("fullscreenMode", "Aktiv");
                    else
                        this.platform.updateLabel("fullscreenMode", "Nicht aktiv");
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
                    if (EmodiaQuest.Core.Settings.Instance.Fullscreen)
                    {
                        EmodiaQuest.Core.Settings.Instance.Fullscreen = false;
                        this.platform.updateLabel("fullscreenMode", "Nicht aktiv");
                        this.platform.updateResolution(Settings.Instance.Resolution.X, Settings.Instance.Resolution.Y);
                    }
                    else
                    {
                        int monitorWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
                        int monitorHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
                        EmodiaQuest.Core.Settings.Instance.Resolution = new IntVector2(monitorWidth, monitorHeight);
                        //GraphicsCopy.PreferredBackBufferWidth = monitorWidth;
                        //GraphicsCopy.PreferredBackBufferHeight = monitorHeight;

                        EmodiaQuest.Core.Settings.Instance.Fullscreen = true;

                        this.platform.updateLabel("fullscreenMode", "Aktiv");
                        EmodiaQuest.Core.GUI.Platform_GUI.updateAllResolutions(monitorWidth, monitorHeight); 
                    }
                    break;
                case "hintsButton":
                    if (Settings.Instance.hints)
                    {
                        Settings.Instance.hints = false;
                        platform.updateLabel("hintsLabel", "Deaktiviert");
                    }
                    else
                    {
                        Settings.Instance.hints = true;
                        platform.updateLabel("hintsLabel", "Aktiviert");
                    }
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
            this.platform.addLabel(50, 10, 18, "dice_big", "Optionen", "Options", true);

            this.platform.addSlider(15, 30, 35, 8, 0, 2, 0, "resolutionChange");
            this.platform.addSlider(15, 40, 35, 8, 0, 100, 100, "volumeChange");

            this.platform.addLabel(55, 30, 35, 8, "monoFont_small", "854 x 480", "resolution");
            this.platform.addLabel(55, 40, 35, 8, "monoFont_small", "Lautstaerke: 100", "volume");
            //this.platform.addSlider(35, 60, 30, 8, 0, 100, "volumeChange2");

            //this.platform.addButton(20, 60, 30, 8, "switchFullscreen", "Screen Mode");
            this.platform.addButton(15, 50, 35, 8, "switchFullscreen", "Vollbild");
            //this.platform.addButton()
            //this.platform.updateButtonText("switchFullscreen", "Vollbild");
            this.platform.addLabel(55, 50, 35, 8, "monoFont_small", "Nicht aktiv", "fullscreenMode");

            this.platform.addButton(35, 75, 30, 8, "changeToMainMenu", "Hauptmenue");

            this.platform.addButton(15, 60, 35, 8, "hintsButton", "Hinweise");
            this.platform.addLabel(55, 60, 35, 8, "monoFont_small", "Aktiviert", "hintsLabel");

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

            if (EmodiaQuest.Core.GUI.Controls_GUI.Instance.keyClicked(Keys.Escape))
            {
                EmodiaQuest_Game.Gamestate_Game = GameStates_Overall.MenuScreen;
            }
                
            //if (EmodiaQuest.Core.GUI.Controls_GUI.Instance.keyClicked(Keys.V))
            //    Controls_GUI.Instance.WaitPoint_New("f", 10);
            //if (EmodiaQuest.Core.GUI.Controls_GUI.Instance.keyClicked(Keys.B))
            //    Controls_GUI.Instance.WaitPoint_New("g", 1);
            //if (EmodiaQuest.Core.GUI.Controls_GUI.Instance.keyClicked(Keys.C))
            //    Controls_GUI.Instance.WaitPoint_FactorLength("f", 0.5);
            
            
            //if (EmodiaQuest.Core.GUI.Controls_GUI.Instance.keyClicked(Keys.P))
            //{
            //    //GraphicsCopy.IsFullScreen = true;
            //    GraphicsCopy.PreferredBackBufferWidth = 1254;
            //    //GraphicsCopy.PreferredBackBufferHeight = 480;
            //    GraphicsCopy.ApplyChanges();
            //}

            //if (EmodiaQuest.Core.GUI.Controls_GUI.Instance.keyClicked(Keys.M))
            //{
            //    //GraphicsCopy.IsFullScreen = true;
            //    GraphicsCopy.PreferredBackBufferWidth = 1254;
            //    GraphicsCopy.PreferredBackBufferHeight = 480;
            //    GraphicsCopy.ApplyChanges();
            //}

            //if (EmodiaQuest.Core.GUI.Controls_GUI.Instance.keyClicked(Keys.L))
            //{
            //    GraphicsCopy.IsFullScreen = true;
            //    GraphicsCopy.ApplyChanges();
                
            //}
            //if (EmodiaQuest.Core.GUI.Controls_GUI.Instance.keyClicked(Keys.V))
            //{
            //    //int resX = EmodiaQuest.Core.Settings.Instance.Resolution.X;
            //    //int resY = EmodiaQuest.Core.Settings.Instance.Resolution.Y;
            //    //EmodiaQuest.Core.GUI.Platform_GUI.updateAllResolutions(resX, resY);


            //    int monitorWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            //    int monitorHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            //    GraphicsCopy.PreferredBackBufferWidth = 1280;
            //    GraphicsCopy.PreferredBackBufferHeight = 960;
            //    GraphicsCopy.ApplyChanges();
            //    EmodiaQuest.Core.GUI.Platform_GUI.updateAllResolutions(1280, 960);
            //    //Console.WriteLine(monitorWidth +", "+ monitorHeight);
            //}
        }



        public void draw(SpriteBatch spritebatch)
        {

            this.platform.draw(spritebatch);
        }
    }
}
