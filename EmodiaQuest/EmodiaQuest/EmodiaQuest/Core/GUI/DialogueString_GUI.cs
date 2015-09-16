using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmodiaQuest.Core.GUI
{
    public class DialogueString_GUI
    {
        public int YPos = 0;
        public string Text = "A";
        public bool IsDrawn = true;
        public DialogueString_GUI(int yPos, string text, bool isDrawn)
        {
            this.YPos = yPos;
            this.Text = text;
            this.IsDrawn = isDrawn;
        }
    }
}
