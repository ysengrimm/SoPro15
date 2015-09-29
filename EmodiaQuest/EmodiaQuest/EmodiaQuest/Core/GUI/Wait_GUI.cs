using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace EmodiaQuest.Core.GUI
{
    public class Wait_GUI
    {
        private double currentTime = 0.0;
        public double CurrentTime
        {
            get { return currentTime; }
            set 
            { 
                this.currentTime = value;
                if (this.currentTime > closingTime)
                    Finished = true;
            }
        }

        private double closingTime = 0.0;
        public double ClosingTime
        {
            get { return closingTime; }
            set { this.closingTime = value; }
        }

        public bool Finished = false;
        public string Name = "";


        public Wait_GUI(double closingTime)
        {
            this.closingTime = closingTime;
        }

        public Wait_GUI(string Name, double closingTime)
        {
            this.Name = Name;
            this.closingTime = closingTime;
        }
    }
}
