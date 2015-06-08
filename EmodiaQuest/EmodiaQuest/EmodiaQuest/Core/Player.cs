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

        public Vector2 position;

        private Model playerModel;

        private CollisionHandler collisionHandler;

        public Model Model
        {
            set { playerModel = value; }
        }

        /// <summary>
        /// Creates new instance of player.
        /// </summary>
        /// <param name="position">Initial player position.</param>
        /// <param name="collisionHandler">Current collision handler</param>
        public Player(Vector2 position, CollisionHandler collisionHandler)
        {
            this.position = position;
            this.collisionHandler = collisionHandler;
            // set defaults
            Hp = 100;
            Armor = 0;
        }

        public void LoadContent()
        {
            
        }

        public void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                if (!collisionHandler.getWallCollision(new Vector2(position.X, position.Y - PlayerSpeed)))
                position.Y -= PlayerSpeed;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                if (!collisionHandler.getWallCollision(new Vector2(position.X, position.Y + PlayerSpeed)))
                position.Y += PlayerSpeed;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                if (!collisionHandler.getWallCollision(new Vector2(position.X - PlayerSpeed, position.Y)))
                position.X -= PlayerSpeed;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                if (!collisionHandler.getWallCollision(new Vector2(position.X + PlayerSpeed, position.Y)))
                position.X += PlayerSpeed;
            }
            
            //Console.WriteLine(position);
        }

        public void Draw(Matrix world, Matrix view, Matrix projection)
        {
            foreach (ModelMesh mesh in playerModel.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();
                    effect.World = Matrix.CreateTranslation(new Vector3(position.X, 0, position.Y)) * world;
                    effect.View = view;
                    effect.Projection = projection;
                }
                mesh.Draw();
            }
        }

    }
}
