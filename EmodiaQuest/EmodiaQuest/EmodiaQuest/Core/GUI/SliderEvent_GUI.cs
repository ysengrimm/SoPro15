using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmodiaQuest.Core.GUI
{
    public class SliderEvent_GUI : EventArgs
    {
        private int sliderValue;
        public int SliderValue{get{return sliderValue;}}

        public SliderEvent_GUI(int sliderValue)
        {
            this.sliderValue = sliderValue;
        }
    }
}
