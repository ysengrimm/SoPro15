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
         * 
         * WHEN COLLISION COLORS WILL BE SAME MAKE ONE MORE LIST WITH THOSE COLORS AND MEANINGS
        */

        public EnvironmentController Controller;

        /// <summary>
        /// Enables collision handling
        /// </summary>
        /// <param name="controller">Current enviroment controller</param>
        public void SetEnvironmentController(EnvironmentController controller)
        {
            this.Controller = controller;
        }

        /// <summary>
        /// Method for getting color of a position
        /// </summary>
        /// <param name="position">Position</param>
        /// <returns>Color of position</returns>
        public Color GetColor(Vector2 position, System.Drawing.Bitmap ColorMap)
        {
            // Current choosen color
            Color color = new Color();
            color.R = ColorMap.GetPixel((int)Math.Round(position.X / 10), (int)Math.Round(position.Y / 10)).R;
            color.G = ColorMap.GetPixel((int)Math.Round(position.X / 10), (int)Math.Round(position.Y / 10)).G;
            color.B = ColorMap.GetPixel((int)Math.Round(position.X / 10), (int)Math.Round(position.Y / 10)).B;
            color.A = ColorMap.GetPixel((int)Math.Round(position.X / 10), (int)Math.Round(position.Y / 10)).A;
            return color;
        }

        /// <summary>
        /// Procedure for getting color of next field the player will move to
        /// </summary>
        /// <param name="movement">Current movement</param>
        /// <param name="offset">offset which should be choosen for an extra distance between the collision object and the movement vector</param>
        /// <returns>Returns a Color</returns>
        public Color GetCollisionColor(Vector2 movement, System.Drawing.Bitmap ColorMap, float offset)
        {
            if (GetColor(new Vector2(movement.X, movement.Y + offset), ColorMap) != Color.White)
                return GetColor(new Vector2(movement.X, movement.Y + offset), ColorMap);
            else if (GetColor(new Vector2(movement.X + offset, movement.Y), ColorMap) != Color.White)
                return GetColor(new Vector2(movement.X + offset, movement.Y), ColorMap);
            else if (GetColor(new Vector2(movement.X, movement.Y - offset), ColorMap) != Color.White)
                return GetColor(new Vector2(movement.X, movement.Y - offset), ColorMap);
            else if (GetColor(new Vector2(movement.X - offset, movement.Y), ColorMap) != Color.White)
                return GetColor(new Vector2(movement.X - offset, movement.Y), ColorMap);
            else if (GetColor(new Vector2(movement.X - offset, movement.Y - offset), ColorMap) != Color.White)
                return GetColor(new Vector2(movement.X - offset, movement.Y - offset), ColorMap);
            else if (GetColor(new Vector2(movement.X + offset, movement.Y + offset), ColorMap) != Color.White)
                return GetColor(new Vector2(movement.X + offset, movement.Y + offset), ColorMap);
            else if (GetColor(new Vector2(movement.X - offset, movement.Y + offset), ColorMap) != Color.White)
                return GetColor(new Vector2(movement.X - offset, movement.Y + offset), ColorMap);
            else if (GetColor(new Vector2(movement.X + offset, movement.Y - offset), ColorMap) != Color.White)
                return GetColor(new Vector2(movement.X + offset, movement.Y - offset), ColorMap);
            else if (GetColor(new Vector2(movement.X + offset, movement.Y - offset), ColorMap) != Color.White)
                return GetColor(new Vector2(movement.X + offset, movement.Y - offset), ColorMap);
            else if (GetColor(new Vector2(movement.X - offset, movement.Y + offset), ColorMap) != Color.White)
                return GetColor(new Vector2(movement.X - offset, movement.Y + offset), ColorMap);
            else return Color.White;
        }

    }
}
