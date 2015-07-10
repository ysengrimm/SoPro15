using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmodiaQuest.Core.GUI
{
    public class ButtonEvent_GUI : EventArgs
    {
        private string buttonFunction;
        public string ButtonFunction { get { return buttonFunction; } }

        public ButtonEvent_GUI(string buttonFunction)
        {
            this.buttonFunction = buttonFunction;
        }
    }

}
