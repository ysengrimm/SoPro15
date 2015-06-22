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
using EmodiaQuest.Core;

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
        public NPCController NpcController;
        public CollisionHandler CollisionHandler;
        public HUD Hud;
        public Player Player;
        public Storycontroller StoryController;
        public EnvironmentController EnviromentController;

        /// <summary>
        /// Method for initialising Objects
        /// </summary>
        public abstract void Initialise();

        /// <summary>
        /// Method for loading Models and other stuff
        /// </summary>
        public abstract void LoadContent();

        /// <summary>
        /// Method for drawing the whole GameScreen
        /// </summary>
        public abstract void DrawGameScreen(Matrix world, Matrix view, Matrix projection);
    }
}
