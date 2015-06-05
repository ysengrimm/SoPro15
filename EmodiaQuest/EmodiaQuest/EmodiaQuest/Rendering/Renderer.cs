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

        public Matrix world, view, projection; // TODO: better solution for player rendering

        /// <summary>
        /// draws ever object, which is listed in the Safeworld
        /// </summary>
        /// <param name="safeWorld"></param>
        public void DrawSafeWorld(SafeWorld safeWorld)
        {
            safeWorld.drawGameScreen(world, view, projection);          
        }

        /// <summary>
        /// Updates the world matrix, which will be used for rendering
        /// </summary>
        /// <param name="world"></param>
        public void UpdateWorld(Matrix world)
        {
            this.world = world;
        }
        /// <summary>
        /// Updates the view matrix, which will be used for rendering
        /// </summary>
        /// <param name="view"></param>
        public void UpdateView(Matrix view)
        {
            this.view = view;
        }
        /// <summary>
        /// Updates the projection matrix, which will be used for rendering
        /// </summary>
        /// <param name="projection"></param>
        public void UpdateProjection(Matrix projection)
        {
            this.projection = projection;
        }


    }
}
