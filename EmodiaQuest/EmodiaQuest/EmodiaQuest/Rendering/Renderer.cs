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

namespace EmodiaQuest.Rendering
{
    public class Renderer : Microsoft.Xna.Framework.Game
    {
        private static Renderer instance;

        private Renderer() { }

        public static Renderer Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Renderer();
                }
                return instance;
            }
        }
        //TODO: renders everthing of the game -> calling the draw methods of:
        /* 1. Backgroundtexture
         * 2. Static Environment
         * 2.1 Shadows
         * 2.2 Particles (Smoke, Fire etc)
         * 3. The Enemies
         * 3.1 Shadows
         * 3.2 Particles /Attacks
         * 4. The Player
         * 4.1 Shadow
         * 4.2 Particles / Attacks
         * 5. Hud
         * 6. Buttons and other UI Elements in CameraSpace
         */

        /// <summary>
        /// Stores the world matrix for the model, which transforms the 
        /// model to be in the correct position, scale, and rotation
        /// in the game world.
        /// </summary>

        private Matrix world;
        public Matrix World
        {
            get { return world; }
            set { world = value; }
        }

        /// <summary>
        /// Stores the view matrix for the model, which gets the model
        /// in the right place, relative to the camera.
        /// </summary>
        
        private Matrix view;
        public Matrix View
        {
            get { return view; }
            set { view = value; }
        }

        /// <summary>
        /// Stores the projection matrix, which gets the model projected
        /// onto the screen in the correct way.  Essentially, this defines the
        /// properties of the camera you are using.
        /// </summary>

        private Matrix projection;
        public Matrix Projection
        {
            get { return projection; }
            set { projection = value; }
        }

        /// <summary>
        /// draws ever object, which is listed in the Safeworld
        /// </summary>
        /// <param name="safeWorld"></param>
        public void DrawSafeWorld(SafeWorld safeWorld)
        {
            safeWorld.DrawGameScreen(world, view, projection);          
        }

        /// <summary>
        /// draws the player object
        /// </summary>
        /// <param name="player"></param>
        public void DrawPlayer(Player player)
        {
            player.Draw(world, view, projection);
        }
    }
}
