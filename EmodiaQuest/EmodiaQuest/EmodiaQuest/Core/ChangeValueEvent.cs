using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmodiaQuest.Core
{
        public class ChangeValueEvent : EventArgs
        {
            private float changeValue;
            public float ChangeValue { get { return changeValue; } }

            private string function;
            public string Function { get { return function; } }

            public ChangeValueEvent(float changeValue, string function)
            {
                this.changeValue = changeValue;
                this.function = function;
            }
        }
}
