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
        public Renderer() { }

        public void drawBakcground()
        {

        }
        public void drawEnvironment()
        {

        }

        private void drawGround()
        {
            
        }

    }
}
