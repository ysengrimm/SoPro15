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
    public abstract class Game : Screen
    {
        // Implement everything, the Overworld and the Dungeon needs :
        /*
         * NPCController
         * Collisionandler
         * HUD
         * PlayerController
         * StoryController
         * EnvironmentController
         */
        public Core.EnvironmentController enviromentController;

        /// <summary>
        /// Method for initialising Objects
        /// </summary>
        public abstract void initialise();

        /// <summary>
        /// Method for loading Models and other stuff
        /// </summary>
        public abstract void loadContent();

    }
}
