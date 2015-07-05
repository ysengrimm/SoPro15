using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace EmodiaQuest.Core.GUI
{
    public class SpriteFonts_GUI
    {
        public string FontName { get; set; }
        public SpriteFont SFont { get; set; }
        public float fontHeight { get; set; }
        public SpriteFonts_GUI(string FontName)
        {
            this.FontName = FontName;
        }
    }
}
