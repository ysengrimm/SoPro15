using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace EmodiaQuest.Core.GUI
{
    class Controls_GUI
    {
        private static Controls_GUI instance;
        private Controls_GUI() { }
        public static Controls_GUI Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Controls_GUI();
                }
                return instance;
            }
        }

        // Declare of the MouseState for the GUI
        // Should be set to the middle of the screen when opening menu TODO
        public MouseState Mouse_GUI { get; set; }


        public void loadContent()
        {

        }
        public void update()
        {
            this.Mouse_GUI = Mouse.GetState();
        }
    }
}
