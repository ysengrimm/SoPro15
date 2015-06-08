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
        public float RotationSpeed = 1.3f;

        public Vector2 Position;
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
            Angle = ((mouseState.X / windowSize.X) - 0.5f) * RotationSpeed;
            
            
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                if (!collisionHandler.getWallCollision(new Vector2(Position.X, Position.Y - collOf - PlayerSpeed)))
                    Position.Y += (PlayerSpeed + (float) Math.Cos(Angle));
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                if (!collisionHandler.getWallCollision(new Vector2(Position.X, Position.Y + collOf + PlayerSpeed)))
                    Position.Y -= (PlayerSpeed + (float) Math.Cos(Angle));
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                if (!collisionHandler.getWallCollision(new Vector2(Position.X - collOf - PlayerSpeed, Position.Y)))
                    Position.X += (PlayerSpeed + (float) Math.Sin(Angle));
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                if (!collisionHandler.getWallCollision(new Vector2(Position.X + collOf + PlayerSpeed, Position.Y)))
                    Position.X -= (PlayerSpeed + (float) Math.Sin(Angle));
            }

            // not really necessary beacuse only vertical rotation
            //float mousePosYNorm = mouseState.Y / windowSize.Y;

            
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
