using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmodiaQuest.Core.GUI
{
    public class SliderEvent_GUI : EventArgs
    {
        private int sliderValue;        
        public int SliderValue { get { return sliderValue; } }

        private string function;
        public string Function { get { return function; } }

        public SliderEvent_GUI(int sliderValue, string function)
        {
            this.sliderValue = sliderValue;
            this.function = function;
        }
    }
}
