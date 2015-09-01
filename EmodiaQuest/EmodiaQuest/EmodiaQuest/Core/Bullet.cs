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
    public class Bullet
    {
        private Model bulletModel;
        public Vector2 bulletPosition;
        public Vector2 bulletMovement;
        public bool isActive;
        private float moveSpeed = 10f;

        public Bullet(Model bulletModel)
        {
            this.bulletPosition = Player.Instance.Position;
            this.bulletModel = bulletModel;
            isActive = false;
        }

        public void ShootBullet(Vector2 bulletMovement)
        {
            isActive = true;
            this.bulletMovement = bulletMovement;
            bulletMovement.Normalize();
        }

        public void Update(GameTime gameTime, CollisionHandler collisionHandler)
        {
            float elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            CollisionHandling(collisionHandler);
            bulletPosition += (bulletMovement * moveSpeed * elapsedTime);
        }

        public void CollisionHandling(CollisionHandler collisionHandler)
        {
            // Collision with walls

            // Collision with enemies
        }

        public void Kill()
        {
            isActive = false;
            bulletPosition = Player.Instance.Position;
        }

        public void Draw(Matrix world, Matrix view, Matrix projection)
        {
            foreach (ModelMesh mesh in bulletModel.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();
                    effect.World = world * Matrix.CreateScale(0.3f) * Matrix.CreateTranslation(new Vector3(bulletPosition.X, 1.3f, bulletPosition.Y));
                    effect.View = view;
                    effect.Projection = projection;
                }
                mesh.Draw();
            }
        }
    }
}
