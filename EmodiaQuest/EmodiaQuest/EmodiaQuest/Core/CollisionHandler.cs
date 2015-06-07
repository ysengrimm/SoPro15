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
        // TODO: Implement collision with
        /* Walls and stuff (movement restriction)
         * Enemy NPCs ( Damage and Kill)
         * Friedly NPCs ( Quest Dialogues and Trading)
         * Items for collecting
         * Collsiion with a DoorToAnotherWorld = go to a dungeon or return to Safeworld
         */

        EnvironmentController controller;

        // Current player movement color
        Color movementColor;

        /// <summary>
        /// Enables collision handling
        /// </summary>
        /// <param name="controller">Current enviroment controller</param>
        /// <param name="player">Current player</param>
        public CollisionHandler(EnvironmentController controller)
        {
            this.controller = controller;
        }

        /// <summary>
        /// Method for getting color of position
        /// </summary>
        /// <param name="movement">Position</param>
        /// <returns>Color of position</returns>
        public Color getMovementColor(Vector2 position)
        {
            movementColor = controller.colors2D[(int)position.X, (int)position.Y];
            return movementColor;
        }

        /// <summary>
        /// Procedure for handling a collision with wall
        /// </summary>
        /// <param name="movement">Current movement</param>
        /// <returns>True if collide with wall</returns>
        public bool getWallCollision(Vector2 movement)
        {
            if (getMovementColor(movement) == Color.Black)
                return true;
            return false;
        }


    }
}
