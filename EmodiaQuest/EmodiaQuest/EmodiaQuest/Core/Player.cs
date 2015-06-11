using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    public class Player
    {

        public float Hp;
        public float Armor;

        public float PlayerSpeed = 1f;
        public float RotationSpeed = 0.2f;

        public Vector2 Position;
        private Vector2 movement;
        private float offset = 4;
        public float Angle;

        private Model playerModel;

        private CollisionHandler collisionHandler;
        private float collOf;

        private Vector2 windowSize;

        public Model Model
        {
            set { playerModel = value; }
        }



        /// <summary>
        /// Creates new instance of player.
        /// </summary>
        /// <param name="position">Initial player position.</param>
        /// <param name="collisionHandler">Current collision handler</param>
        public Player(Vector2 position, CollisionHandler collisionHandler, Vector2 winSize)
        {
            Position = position;
            this.collisionHandler = collisionHandler;
            windowSize = winSize;
            
            // set defaults
            Hp = 100;
            Armor = 0;

            collOf = 1f;

            Angle = 0;

        }

        public void LoadContent()
        {
            
        }

        public void Update(GameTime gameTime, MouseState mouseState)
        {
            //scale position to 0.0 to 1.0 then center the +/- change
            Angle = (float) -(((mouseState.X/windowSize.X))*2*Math.PI);// * RotationSpeed);
            
            movement = Position;

            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                movement.Y += PlayerSpeed * (float)Math.Cos(Angle);
                movement.X += PlayerSpeed * (float)Math.Sin(Angle);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                movement.Y -= PlayerSpeed * (float)Math.Cos(Angle);
                movement.X -= PlayerSpeed * (float)Math.Sin(Angle);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                movement.Y -= PlayerSpeed * (float)Math.Cos(Angle - Math.PI / 2);
                movement.X -= PlayerSpeed * (float)Math.Sin(Angle - Math.PI / 2);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                movement.Y += PlayerSpeed * (float)Math.Cos(Angle + 3 * Math.PI / 2);
                movement.X += PlayerSpeed * (float)Math.Sin(Angle + 3 * Math.PI / 2);
            }

            if (!collisionHandler.getWallCollision(new Vector2(Position.X, movement.Y), offset))
            {
                Position.Y = movement.Y;
            }
            if (!collisionHandler.getWallCollision(new Vector2(movement.X, Position.Y), offset))
            {
                Position.X = movement.X;
            }

            // not really necessary beacuse only vertical rotation
            //float mousePosYNorm = mouseState.Y / windowSize.Y;

            //Console.Out.WriteLine(Position);
        }

        public void Draw(Matrix world, Matrix view, Matrix projection)
        {
            foreach (ModelMesh mesh in playerModel.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();
                    effect.World = Matrix.CreateRotationY(Angle) * Matrix.CreateTranslation(new Vector3(Position.X, 0, Position.Y)) * world;
                    effect.View = view;
                    effect.Projection = projection;
                }
                mesh.Draw();
            }
        }

    }
}
