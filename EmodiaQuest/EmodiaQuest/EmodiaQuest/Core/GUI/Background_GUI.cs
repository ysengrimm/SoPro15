using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace EmodiaQuest.Core.GUI
{
    class Background_GUI
    {
        private static Texture2D background;

        public static void loadContent(ContentManager Content)
        {
            background = Content.Load<Texture2D>("ff");
        }
    }
}
