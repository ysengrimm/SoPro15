using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace EmodiaQuest.Core
{
    /// <summary>
    /// Contains all colors of objects in dungeon
    /// </summary>
    public class ColorListDungeon
    {
        private static ColorListDungeon _instance;
        public static ColorListDungeon Instance
        {
            get { return _instance ?? (_instance = new ColorListDungeon()); }
        }

        public Color Wall = new Color(101, 101, 0);
        public Color Ground = new Color(101, 102, 0);
        public Color Item = new Color(255, 0, 0);
        public Color Teleporter = new Color(101, 103, 0);
        /// <summary>
        /// Draws nothing here. Improves performance.
        /// </summary>
        public Color Nothing = new Color(0, 0, 0);
    }
}
