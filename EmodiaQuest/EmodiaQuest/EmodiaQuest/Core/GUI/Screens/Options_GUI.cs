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
        static void SliderEventValue(object source, SliderEvent_GUI e)
        {
            switch (e.Function)
            {
                case "volumeChange":
                    EmodiaQuest.Core.Settings.Instance.Volume = e.SliderValue/100;
                    break;
                case "resolutionChange":
                    EmodiaQuest.Core.Settings.Instance.Resolution = EmodiaQuest.Core.Settings.PossibleResolutions[e.SliderValue];
                    break;
                default:
                    Console.WriteLine("Function name does not exist");
                    break;
            }
        }

        static void ButtonEventValue(object source, ButtonEvent_GUI e)
        {
            switch (e.ButtonFunction)
            {
                case "changeToMainMenu":
                    EmodiaQuest_Game.Gamestate_Game = GameStates_Overall.MenuScreen;
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

        public void loadContent(ContentManager Content)
        {
            this.platform.loadContent(Content);

            //Resolution
            //Fullscreen
            //Volume
            this.platform.setBackground(Content, "Content_GUI/menu_background");

            //this.platform.addPlainText(50.0f, 10.0f, "monoFont_big", "OPTIONS MENU", true);
            this.platform.addLabel(50, 10, 18, "dice_big", "Options", true);

            this.platform.addSlider(35, 50, 30, 8, 0, 100, "volumeChange");
            this.platform.addSlider(35, 40, 30, 8, 0, 2, "resolutionChange");
            //this.platform.addSlider(35, 60, 30, 8, 0, 100, "volumeChange2");

            this.platform.addButton(35, 75, 30, 8, "changeToMainMenu", "Main Menu");
           // this.platform.addPlainImage

            //this.platform.addPlainImage(10, 10, 30, 30, "HUD_small");

            //EventHandler;
            platform.OnSliderValue += new GUI_Delegate_Slider(SliderEventValue);
            platform.OnButtonValue += new GUI_Delegate_Button(ButtonEventValue);
        }


        public void update()
        {
            this.platform.update();
        }

        public void draw(SpriteBatch spritebatch)
        {
            this.platform.draw(spritebatch);
        }
    }
}
