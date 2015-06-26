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
        private Vector3 playerPosition;
        private Vector3 ownPosition;
        private EnemyState currentState;
        private EnemyState lastState;
        private float trackingRadius;
        private float ownMovementSpeed;
        private EnvironmentController currentEnvironment;
        private bool hasTracked = false;


        private Vector3 calculatedMovingDirection;
        public Vector3 CalculatedMovingDirection
        {
            get { return calculatedMovingDirection; }
        }



        public Ai(Vector3 ownPosition, EnemyState currentState, EnemyState lastState, float trackingRadius, float ownMovementSpeed, EnvironmentController currentEnvironment)
        {
            this.playerPosition = new Vector3(Player.Instance.Position.X, 0, Player.Instance.Position.Y);
            this.ownPosition = ownPosition;
            this.currentState = currentState;
            this.lastState = lastState;
            this.trackingRadius = trackingRadius;
            this.ownMovementSpeed = ownMovementSpeed;
            this.currentEnvironment = currentEnvironment;
        }


        public void calculateTracking()
        {
            if (EuclideanDistance(new Vector2(playerPosition.X, playerPosition.Z), new Vector2(ownPosition.X, ownPosition.Z)) < trackingRadius)
            {
                hasTracked = true;
            }
            else hasTracked = false;
            //Console.WriteLine("PlayerPosition: " + playerPosition + " EnemyPosition: " + ownPosition);
            //Console.WriteLine("Distance: " + EuclideanDistance(new Vector2(playerPosition.X, playerPosition.Z), new Vector2(ownPosition.X, ownPosition.Z)));
        }

        public void calculateMovementDirection()
        {
            if (hasTracked)
            {
                calculatedMovingDirection = Vector3.Multiply(Vector3.Normalize(Vector3.Subtract(playerPosition, ownPosition)),ownMovementSpeed);
            }
            else
            {
                calculatedMovingDirection = new Vector3(0);
            }
      }

        public void updateAi(Vector3 ownPosition)
        {
            this.playerPosition = new Vector3(Player.Instance.Position.X, 0, Player.Instance.Position.Y);
            this.ownPosition = ownPosition;
            calculateTracking();
            calculateMovementDirection();
        }

        private double EuclideanDistance(Vector2 p1, Vector2 p2)
        {
            return Math.Sqrt(Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2));
        }

    }
}
