using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace EmodiaQuest.Core
{
    class Settings
    {
        private static Settings instance;

        private Settings() { }

        public static Settings Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Settings();
                }
                return instance;
            }
        }
        /// <summary>
        /// Debug output
        /// </summary>
        public bool DebugMode = false;

        /// <summary>
        /// Resolution for the game
        /// </summary>
        public Vector2 Resolution = new Vector2(800, 600);

        /// <summary>
        /// Volume of the sound
        /// </summary>
        public float Volume = 1.0f;
        
        

    }
}
