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
    /// <summary>
    /// Class for handling collisions with any object in game
    /// </summary>
    public class CollisionHandler
    {
        private static CollisionHandler instance;

        private CollisionHandler() { }

        public static CollisionHandler Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CollisionHandler();
                }
                return instance;
            }
        }
        // TODO: Implement collision with
        /* Walls and stuff (movement restriction)
         * Enemy NPCs ( Damage and Kill)
         * Friedly NPCs ( Quest Dialogues and Trading)
         * Items for collecting
         * Collsiion with a DoorToAnotherWorld = go to a dungeon or return to Safeworld
         */

        /*List of colors, RGB and use
         * Black: 0, 0, 0           Wall
         * White: 255, 255, 255     Floor
         * Blue: 0, 0, 255
         * Grey: 128, 128, 128
         * Green: 0, 128, 0
         * Lime: 0, 255, 0
         * Red: 255, 0, 0
        */

        EnvironmentController controller;

        // Current choosen color
        Color color;

        /// <summary>
        /// Enables collision handling
        /// </summary>
        /// <param name="controller">Current enviroment controller</param>
        /// <param name="player">Current player</param>
        public void SetEnvironmentController(EnvironmentController controller)
        {
            this.controller = controller;
        }

        /// <summary>
        /// Method for getting color of a position
        /// </summary>
        /// <param name="movement">Position</param>
        /// <returns>Color of position</returns>
        public Color getColor(Vector2 position)
        {
            color = controller.colors2D[(int)Math.Round(position.X/10)+1, (int)Math.Round(position.Y/10)+1];
            return color;
        }

        /// <summary>
        /// Procedure for handling a collision with wall
        /// </summary>
        /// <param name="movement">Current movement</param>
        /// <returns>True if collide with wall</returns>
        public bool getWallCollision(Vector2 movement)
        {
            if (getColor(movement) == Color.Black)
                return true;
            return false;
        }

    }
}
