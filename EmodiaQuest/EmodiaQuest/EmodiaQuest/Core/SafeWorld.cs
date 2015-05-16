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
using EmodiaQuest.Rendering;

namespace EmodiaQuest.Core
{
    /// <summary>
    /// Implement everything which is in the game + the special things of the SafeWorld, like the Caste and stuff
    /// </summary>
    public class SafeWorld : Game
    {
        public EnvironmentController controller;
        public Texture2D map;
        public ContentManager content;

        public SafeWorld(ContentManager content)
        {
            this.content = content;
            controller = new EnvironmentController();
        }

        /// <summary>
        /// Method for initialising Models and so on in SafeWorld
        /// </summary>
        public override void initialise()
        {
            controller.createMap(map);
        }

        /// <summary>
        /// Method for loading relevant content fpr SafeWorld
        /// </summary>
        public override void loadContent()
        {
            map = content.Load<Texture2D>("safeWorldMap");
        }
    }
}
