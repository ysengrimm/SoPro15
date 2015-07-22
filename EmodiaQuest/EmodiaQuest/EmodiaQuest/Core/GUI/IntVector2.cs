using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmodiaQuest.Core.GUI
{
    public class IntVector2
    {
        private int x = 0;
        public int X { 
            get { return this.x; }
            set
            {
                this.x = value;
            }
        }
        private int y = 0;
        public int Y
        {
            get { return this.y; }
            set
            {
                this.y = value;
            }
        }

        public IntVector2(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        
    }
}
