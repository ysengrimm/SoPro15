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

        //Constructor
        Matrix world, view, projection;

        public Renderer() { }
        /// <summary>
        /// constructor with the required Matrices given at the beginning
        /// </summary>
        /// <param name="world">the world matrix, which will be used for rendering</param>
        /// <param name="view">the view matrix, which will be used for rendering</param>
        /// <param name="projection">the projection matrix, which will be used for rendering</param>
        public Renderer(Matrix world, Matrix view, Matrix projection)
        {
            this.world = world;
            this.view = view;
            this.projection = projection;
        }

        public void drawSafeWorld(SafeWorld safeWorld)
        {
            safeWorld.drawGameScreen(world, view, projection);

            
        }

        /// <summary>
        /// Updates the world matrix, which will be used for rendering
        /// </summary>
        /// <param name="world"></param>
        public void updateWorld(Matrix world)
        {
            this.world = world;
        }
        /// <summary>
        /// Updates the view matrix, which will be used for rendering
        /// </summary>
        /// <param name="view"></param>
        public void updateView(Matrix view)
        {
            this.view = view;
        }
        /// <summary>
        /// Updates the projection matrix, which will be used for rendering
        /// </summary>
        /// <param name="projection"></param>
        public void updateProjection(Matrix projection)
        {
            this.projection = projection;
        }


    }
}
