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

        public float PlayerSpeed = 1;

        public Vector2 Position;

        private Model playerModel;

        public Model Model
        {
            set { playerModel = value; }
        }

        /// <summary>
        /// The Player, nothing new.
        /// </summary>
        /// <param name="position">Initial player position.</param>
        public Player(Vector2 position)
        {
            Position = position;

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
                Position.Y -= PlayerSpeed;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                Position.Y += PlayerSpeed;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                Position.X -= PlayerSpeed;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                Position.X += PlayerSpeed;
            }

            //Console.WriteLine(Position);
        }


        public void Draw(Matrix world, Matrix view, Matrix projection)
        {
            foreach (ModelMesh mesh in playerModel.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();
                    effect.World = Matrix.CreateTranslation(new Vector3(Position.X, 0, Position.Y)) * world;
                    effect.View = view;
                    effect.Projection = projection;
                }
                mesh.Draw();
            }
        }


    }
}
