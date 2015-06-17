using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using EmodiaQuest.Core.GUI;

namespace EmodiaQuest.Core
{
    class NetGraph
    {
        private static NetGraph instance;
        private NetGraph() { }
        public static NetGraph Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new NetGraph();
                }
                return instance;
            }
        }

        private Platform_GUI platform = new Platform_GUI();

        public void loadContent(ContentManager Content)
        { 
            
        }

    }
}
