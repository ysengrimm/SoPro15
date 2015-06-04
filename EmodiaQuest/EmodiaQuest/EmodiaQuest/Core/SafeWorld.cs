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
        public Model testCube;

        public SafeWorld(ContentManager content)
        {
            this.content = content;
            controller = new EnvironmentController();
        }

        /// <summary>
        /// Method for initialising Models and so on in SafeWorld
        /// </summary>
        public override void initialise(){}

        /// <summary>
        /// Method for loading relevant content fpr SafeWorld
        /// </summary>
        public override void loadContent()
        {
            map = content.Load<Texture2D>("maps/safeWorldMap");
            controller.createMap(map);

            testCube = content.Load<Model>("testCube");
            controller.insertObj(controller.wall, testCube, Color.Black, 0);
        }

        /// <summary>
        /// Method for drawing the whole GameScreen
        /// Especially the following:
        /// Environment, NPCs, HUD, Player
        /// </summary>
        /// <param name="world">the world Matrix, which will be used for rendering</param>
        /// <param name="view">the view Matrix, which will be used for the rendering</param>
        /// <param name="projection">the projection Matrix, which will be used for the rendering</param>
        public override void drawGameScreen(Matrix world, Matrix view, Matrix projection)
        {
            drawEnvironment(world, view, projection);
            //drawNPCs();
            //drawHUD();
            //drawPlayer(); <--- nope is in EmodiaQuest.cs
            
        }

        private void drawEnvironment(Matrix world, Matrix view, Matrix projection)
        {
            controller.drawEnvironment(world, view, projection);
        }
    }
}
