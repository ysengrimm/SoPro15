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
    public class Ai
    {
        // Implements behavior for NPCs ( normal NPCs and the enemies:  A*!?)
        private Vector2 playerPosition;
        private Vector2 ownPosition;
        private EnemyState currentState;
        private EnemyState lastState;
        private float trackingRadius;
        private float ownMovementSpeed;
        private EnvironmentController currentEnvironment;
        private bool hasTracked = false;


        private Vector2 trackingDirection;
        public Vector2 TrackingDirection
        {
            get { return trackingDirection; }
        }


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="ownPosition"></param>
        /// <param name="currentState"></param>
        /// <param name="lastState"></param>
        /// <param name="trackingRadius"></param>
        /// <param name="ownMovementSpeed"></param>
        /// <param name="currentEnvironment"></param>
        public Ai(Vector2 ownPosition, EnemyState currentState, EnemyState lastState, float trackingRadius, float ownMovementSpeed, EnvironmentController currentEnvironment)
        {
            this.playerPosition = new Vector2(Player.Instance.Position.X, Player.Instance.Position.Y);
            this.ownPosition = ownPosition;
            this.currentState = currentState;
            this.lastState = lastState;
            this.trackingRadius = trackingRadius;
            this.ownMovementSpeed = ownMovementSpeed;
            this.currentEnvironment = currentEnvironment;
        }

        /// <summary>
        /// calculate the boolean "hasTracked" which indicates, if the enemy is near the Player and wan´t to follow the player
        /// </summary>
        public void CalculateTracking()
        {
            if (EuclideanDistance(new Vector2(playerPosition.X, playerPosition.Y), new Vector2(ownPosition.X, ownPosition.Y)) < trackingRadius)
            {
                hasTracked = true;
            }
            else hasTracked = false;
        }

        /// <summary>
        /// calculate the "Tracking Direction" where the Player is, in relation to the actual enemy position
        /// </summary>
        public void CalculateTrackingDirection()
        {
            if (hasTracked)
            {
                trackingDirection = Vector2.Multiply(Vector2.Normalize(Vector2.Subtract(playerPosition, ownPosition)),ownMovementSpeed);
            }
            else
            {
                trackingDirection = new Vector2(0);
            }
        }

        /// <summary>
        /// updates whether the player is in tracking range
        /// updates the direction if the player is in tracking range
        /// if the player isn´t in the tracking range, the tracking direction will be set to 0
        /// </summary>
        /// <param name="ownPosition"></param>
        public void updateAi(Vector2 ownPosition)
        {
            this.playerPosition = new Vector2(Player.Instance.Position.X, Player.Instance.Position.Y);
            this.ownPosition = ownPosition;
            CalculateTracking();
            CalculateTrackingDirection();
        }

        /// <summary>
        /// Calculates the euclidean distance between 2 Points (Vector2)
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        private double EuclideanDistance(Vector2 p1, Vector2 p2)
        {
            return Math.Sqrt(Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2));
        }

    }
}
