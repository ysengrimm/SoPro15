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
        private Vector2 bulletDirection;
        private float moveSpeed = 20f;
        private Vector2 playerPosition;
        private float range = 30;
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
            bulletDirection = bulletMovement;
        }

        public void Update(GameTime gameTime, CollisionHandler collisionHandler, Player.Shootingtype shootingtype)
        {
            float elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            CollisionHandling(collisionHandler, shootingtype);
            bulletPosition += (bulletMovement * moveSpeed * elapsedTime);
        }

        public void CollisionHandling(CollisionHandler collisionHandler, Player.Shootingtype shootingtype)
        {
            if (EuclideanDistance(playerPosition, bulletPosition) > range) Kill();

            // Collision with walls

            // Collision with enemies
            if (Ingame.Instance.ActiveWorld == WorldState.Dungeon)
            {
                // collision with enemies
                Vector2 currentGridPos = new Vector2((float)Math.Round(bulletPosition.X / gridSize), (float)Math.Round(bulletPosition.Y / gridSize));

                List<Enemy> currentBlockEnemyListtest = new List<Enemy>();

                for (int i = -1; i < 2; i++)
                {
                    for (int j = -1; j < 2; j++)
                    {
                        List<Enemy> currentBlockEnemyList = Player.Instance.GameEnv.enemyArray[(int)currentGridPos.X + i, (int)currentGridPos.Y + j];

                        if (currentBlockEnemyList.Count < 1) continue;

                        foreach (var item in currentBlockEnemyList)
                        {
                            currentBlockEnemyListtest.Add(item);
                        }
                    }
                }
                switch (shootingtype)
                {
                    case Player.Shootingtype.Normal:
                        foreach (var nmy in currentBlockEnemyListtest)
                        {
                            Vector2 circlePos = bulletPosition + bulletMovement;
                            if (EuclideanDistance(circlePos, nmy.Position) < 1.8f)
                            {
                                nmy.Attack(Damage);
                                Kill();
                            }
                        }
                        break;
                    case Player.Shootingtype.Blast:

                        Vector2 currentEnemyPosition = new Vector2(0, 0);

                        bool blastAttack = false;
                        foreach (var nmy in currentBlockEnemyListtest)
                        {
                            Vector2 circlePos = bulletPosition + bulletMovement;
                            if (EuclideanDistance(circlePos, nmy.Position) < 1.8f)
                            {
                                currentEnemyPosition = nmy.Position;

                                blastAttack = true;
                            }
                        }
                        if (blastAttack)
                            currentBlockEnemyListtest.Clear();

                        for (int i = -1; i < 2; i++)
                        {
                            for (int j = -1; j < 2; j++)
                            {
                                List<Enemy> currentBlockEnemyList = Player.Instance.GameEnv.enemyArray[(int)currentGridPos.X + i, (int)currentGridPos.Y + j];

                                if (currentBlockEnemyList.Count < 1) continue;
                                foreach (var item in currentBlockEnemyList)
                                {
                                    currentBlockEnemyListtest.Add(item);
                                }
                            }
                        }
                        if (blastAttack)
                        {
                            foreach (var nmy in currentBlockEnemyListtest)
                            {
                                Vector2 circlePos = bulletPosition + bulletMovement;
                                if (EuclideanDistance(currentEnemyPosition, nmy.Position) < 6f)
                                {
                                    nmy.Attack(Damage);
                                }
                            }

                            Kill();
                        }
                        break;
                    case Player.Shootingtype.Lightning:

                        // QUICK AND DIRTY

                        currentBlockEnemyListtest.Clear();
                        //Console.WriteLine(currentGridPos.X + ", " + currentGridPos.Y);
                        for (int i = -4; i < 4; i++)
                        {
                            for (int j = -4; j < 4; j++)
                            {
                                List<Enemy> currentBlockEnemyList = Player.Instance.GameEnv.enemyArray[(int)currentGridPos.X + i, (int)currentGridPos.Y + j];

                                if (currentBlockEnemyList.Count < 1) continue;

                                foreach (var item in currentBlockEnemyList)
                                {
                                    currentBlockEnemyListtest.Add(item);
                                }
                            }
                        }


                        //bulletDirection = Vector2.Transform(bulletDirection, Matrix.CreateTranslation(new Vector3(2, 0, 0)));
                        Vector2 turnedBulletDirection = Vector2.Transform(bulletDirection, Matrix.CreateRotationZ((float)(0.5 * Math.PI)));
                        foreach (var nmy in currentBlockEnemyListtest)
                        {
                            if (pointSide(bulletDirection, playerPosition, nmy.Position) < 1.5 && pointSide(bulletDirection, playerPosition, nmy.Position) > -1.5)
                            {
                                if (pointSide(turnedBulletDirection, playerPosition, nmy.Position) > 0)
                                    nmy.Attack(Damage);
                            }
                        }


                        Kill();
                        break;
                    default:
                        Console.WriteLine("Wrong shootingtype chosen");
                        break;
                }

                currentBlockEnemyListtest.Clear();
            }
        }

        public void Kill()
        {
            isActive = false;
        }

        private double EuclideanDistance(Vector2 p1, Vector2 p2)
        {
            return Math.Sqrt(Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2));
        }

        private float pointSide(Vector2 g, Vector2 s, Vector2 p)
        {
            float result;
            result = g.Y * (p.X - s.X) - (g.X * (p.Y - s.Y));
            return result;
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
