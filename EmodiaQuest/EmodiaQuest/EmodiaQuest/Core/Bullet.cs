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
using EmodiaQuest.Core.NPCs;

namespace EmodiaQuest.Core
{
    public class Bullet
    {
        private Model bulletModel;
        public Vector2 bulletPosition;
        public Vector2 bulletMovement;
        private float moveSpeed = 10f;
        private Vector2 playerPosition;
        private float range = 20;
        private float gridSize = Settings.Instance.GridSize;
        private float Damage = 50;
        public bool isActive = true;

        public Bullet(Model bulletModel, Vector2 bulletMovement, Vector2 playerPosition)
        {
            this.bulletPosition = playerPosition;
            this.playerPosition = playerPosition;
            this.bulletModel = bulletModel;
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
            if (EuclideanDistance(playerPosition, bulletPosition) > range) Kill();

            // Collision with walls

            // Collision with enemies
            if (Ingame.Instance.ActiveWorld == WorldState.Dungeon)
            {
                // collision with enemies
                Vector2 currentGridPos = new Vector2((float)Math.Round(bulletPosition.X / gridSize), (float)Math.Round(bulletPosition.Y / gridSize));

                //mystuff
                List<Enemy> currentBlockEnemyListtest = new List<Enemy>();

                for (int i = -1; i < 2; i++)
                {
                    for (int j = -1; j < 2; j++)
                    {
                        List<Enemy> currentBlockEnemyList = Player.Instance.GameEnv.enemyArray[(int)currentGridPos.X + i, (int)currentGridPos.Y + j];

                        //mystuff

                        if (currentBlockEnemyList.Count <= 0) continue;
                        
                        foreach (var item in currentBlockEnemyList)
                        {
                            currentBlockEnemyListtest.Add(item);
                        }

                        Console.WriteLine(currentBlockEnemyListtest.Count);
                    }
                }

                foreach (var nmy in currentBlockEnemyListtest)
                {
                    Vector2 circlePos = bulletPosition + bulletMovement;
                    if (EuclideanDistance(circlePos, nmy.Position) < 5f)
                    {
                        nmy.Attack(Damage);
                        Kill();
                    }
                }

                currentBlockEnemyListtest.Clear();
            }
        }

        public void Kill()
        {
            isActive = false;
        }

        public double EuclideanDistance(Vector2 p1, Vector2 p2)
        {
            return Math.Sqrt(Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2));
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
