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
    class Start_GUI
    {
        private static Start_GUI instance;

        private Start_GUI() { }

        public static Start_GUI Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Start_GUI();
                }
                return instance;
            }
        }

        private Platform_GUI platform = new Platform_GUI();

        private string functionCalled = null;

        

        public void loadContent(ContentManager Content)
        {
            this.platform.loadContent(Content);

            int h = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            int w = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            this.platform.addButton(0, 0, h, w, "clickToPlay", false);

            this.platform.addPlainText(80, 95, "dice_big", "EMODIA \n\n  QUEST ");
        }

        public void update()
        {
            if ((this.functionCalled = this.platform.update()) != null)
                this.functionCall();

            this.platform.breathing();
        }

        public void draw(SpriteBatch spritebatch)
        {
            this.platform.draw(spritebatch);
        }

        private void functionCall()
        {
            switch (this.functionCalled)
            {
                case "clickToPlay":
                    EmodiaQuest_Game.Gamestate_Game = GameStates_Overall.MenuScreen;
                    break;
                default:
                    Console.WriteLine("Function name does not exist");
                    break;
            }
        }
    }
}