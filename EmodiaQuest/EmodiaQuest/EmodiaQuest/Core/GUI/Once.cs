using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmodiaQuest.Core.GUI
{
    public class Once
    {
        private bool tryOnce = true;
        public bool TryOnce
        {
            get
            {
                if (tryOnce)
                {
                    tryOnce = false;
                    return true;
                }
                return tryOnce;
            }
        }
        public Once()
        {
        }
    }
}
