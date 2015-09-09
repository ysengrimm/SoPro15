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
        public Vector2 Position;
        private Vector2 movement;
        private Vector2 bulletDirection;
        private float speed = 30f;
        private Vector2 playerPosition;
        private float range = 30;
        // private float gridSize = Settings.Instance.GridSize; // LOL this is deadly!
        private float gridSize;
        private float Damage = 50;
        public bool isActive = true;
        private float movementOffset = 0.4f;

        private float normalAngle = 0.0f;

        private float playerAngle;
        private bool blastHit = false;
        private bool oneMoreBlastComputation = false;
        public static Model blastAnimation;
        private GUI.Once once = new GUI.Once();

        public Player.Shootingtype Shootingtype = Player.Shootingtype.Normal;

        // Shading values
        private bool breathingOn = false;
        private float ambientIntensity = 1.0f;

        public Bullet(Model bulletModel, Vector2 bulletMovement, Vector2 playerPosition, float playerAngle, Player.Shootingtype shootingtype)
        {
            this.playerPosition = playerPosition;
            this.bulletModel = bulletModel;
            this.movement = bulletMovement;
            bulletMovement.Normalize();
            bulletDirection = bulletMovement;
            this.Position = playerPosition + bulletDirection * 1.5f;
            this.Shootingtype = shootingtype;
            this.playerAngle = playerAngle;
            gridSize = Settings.Instance.GridSize;
            switch (shootingtype)
            {
                case Player.Shootingtype.Normal:
                    this.speed = 30;
                    break;
                case Player.Shootingtype.Blast:
                    this.speed = 10;
                    break;
                case Player.Shootingtype.Lightning:
                    break;
                default:
                    break;
            }
        }

        public void Update(GameTime gameTime, CollisionHandler collisionHandler)
        {
            float elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if(blastHit)
            {
                if(once.TryOnce)
                    bulletModel = Bullet.blastAnimation;
            }

            if (EuclideanDistance(playerPosition, Position) > range)
            {
                if (Shootingtype == Player.Shootingtype.Blast)
                    oneMoreBlastComputation = true;
                else
                    Kill();
            }

            // Collision with walls

            if (Color.White == collisionHandler.GetCollisionColor(new Vector2(Position.X, Position.Y + (movement.Y * speed * elapsedTime)), collisionHandler.Controller.CollisionColors, movementOffset))
            {
                if (Shootingtype != Player.Shootingtype.Lightning)
                    Position.Y += (movement.Y * speed * elapsedTime);
            }
            else
            {
                if (Shootingtype == Player.Shootingtype.Blast)
                    oneMoreBlastComputation = true;
                else
                    Kill();
            }
            if (Color.White == collisionHandler.GetCollisionColor(new Vector2(Position.X + (movement.X * speed * elapsedTime), Position.Y), collisionHandler.Controller.CollisionColors, movementOffset))
            {
                if (Shootingtype != Player.Shootingtype.Lightning)
                    Position.X += (movement.X * speed * elapsedTime);
            }
            else
            {
                if (Shootingtype == Player.Shootingtype.Blast)
                    oneMoreBlastComputation = true;
                else
                    Kill();
            }

            // Collision with enemies
            if (Ingame.Instance.ActiveWorld == WorldState.Dungeon && !blastHit)
            {
                // collision with enemies
                Vector2 currentGridPos = new Vector2((float)Math.Round(Position.X / gridSize), (float)Math.Round(Position.Y / gridSize));

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
                switch (Shootingtype)
                {
                    case Player.Shootingtype.Normal:
                        foreach (var nmy in currentBlockEnemyListtest)
                        {
                            Vector2 circlePos = Position + movement;
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
                            Vector2 circlePos = Position + movement;
                            if (EuclideanDistance(circlePos, nmy.Position) < 1.8f)
                            {
                                currentEnemyPosition = nmy.Position;

                                blastAttack = true;
                            }
                        }
                        if (blastAttack || oneMoreBlastComputation)
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
                        if (oneMoreBlastComputation)
                            currentEnemyPosition = this.Position;
                        if (blastAttack || oneMoreBlastComputation)
                        {
                            foreach (var nmy in currentBlockEnemyListtest)
                            {
                                Vector2 circlePos = Position + movement;
                                if (EuclideanDistance(currentEnemyPosition, nmy.Position) < 6f)
                                {
                                    nmy.Attack(Damage);
                                }
                            }

                            //Kill();
                            blastHit = true;
                        }
                        break;
                    case Player.Shootingtype.Lightning:

                        // QUICK AND DIRTY

                        //shading
                        //if (fired)
                        //{
                        //    fired = false;
                        breathingOn = true;
                        //overlayBool = true;
                        //}
                        if (breathingOn)
                            breathing();

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
                                // This if is the computation, if the enemy is behind the player or not
                                if (pointSide(turnedBulletDirection, playerPosition, nmy.Position) > 0)
                                    nmy.Attack(Damage);
                            }
                        }


                        //Kill();
                        break;
                    default:
                        Console.WriteLine("Wrong shootingtype chosen");
                        break;
                }

                currentBlockEnemyListtest.Clear();
            }
            if (blastHit)
            {
                normalAngle += 0.02f;
                if (normalAngle > 1)
                    Kill();
            }

        }

        public void CollisionHandling(CollisionHandler collisionHandler, Player.Shootingtype shootingtype)
        {

        }

        public void breathing()
        {
            if (this.breathingOn)
                this.ambientIntensity -= 0.04f;
            if (this.ambientIntensity < 0.01f)
            {
                this.ambientIntensity = 0.0f;
                breathingOn = false;
                //Console.WriteLine(distanceToPlayer);
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
            switch (Shootingtype)
            {
                case Player.Shootingtype.Normal:
                    foreach (ModelMesh mesh in bulletModel.Meshes)
                    {
                        foreach (BasicEffect effect in mesh.Effects)
                        {
                            effect.EnableDefaultLighting();
                            //effect.World = world * Matrix.CreateScale(1.3f) * Matrix.CreateTranslation(new Vector3(Position.X, 1.3f, Position.Y));
                            //effect.World = Matrix.CreateRotationY(normalAngle*100) * Matrix.CreateRotationX((float)(Math.PI)) * Matrix.CreateRotationX((float)(-0.5 * Math.PI)) * Matrix.CreateRotationY(playerAngle) * Matrix.CreateRotationY((float)(Math.PI)) * Matrix.CreateTranslation(new Vector3(Position.X, 2.3f, Position.Y)) * world;
                            effect.World = Matrix.CreateScale(3) * Matrix.CreateRotationX((float)(-0.5 * Math.PI)) * Matrix.CreateRotationY(playerAngle) * Matrix.CreateRotationY((float)(Math.PI)) * Matrix.CreateTranslation(new Vector3(Position.X, 3.3f, Position.Y)) * world;
                            effect.View = view;
                            effect.Projection = projection;
                        }
                        mesh.Draw();
                    }
                    break;
                case Player.Shootingtype.Blast:
                    foreach (ModelMesh mesh in bulletModel.Meshes)
                    {
                        foreach (BasicEffect effect in mesh.Effects)
                        {
                            effect.EnableDefaultLighting();
                            if(blastHit)
                                effect.World = Matrix.CreateScale(10 * normalAngle) * Matrix.CreateRotationX((float)(-0.5 * Math.PI)) * Matrix.CreateRotationY(playerAngle + 5 * normalAngle) * Matrix.CreateRotationY((float)(Math.PI)) * Matrix.CreateTranslation(new Vector3(Position.X, 3.3f, Position.Y)) * world;
                                //effect.World = world * Matrix.CreateRotationY(normalAngle*3) * Matrix.CreateScale(10 * normalAngle) * Matrix.CreateRotationX((float)(-0.5 * Math.PI)) * Matrix.CreateTranslation(new Vector3(Position.X, 1.3f, Position.Y));
                            else
                                effect.World = world * Matrix.CreateScale(5) * Matrix.CreateTranslation(new Vector3(Position.X, 3.3f, Position.Y));
                            effect.View = view;
                            effect.Projection = projection;
                            effect.Alpha = 1 - normalAngle;
                        }
                        mesh.Draw();
                    }
                    break;
                case Player.Shootingtype.Lightning:
                    foreach (ModelMesh mesh in bulletModel.Meshes)
                    {
                        foreach (BasicEffect effect in mesh.Effects)
                        {
                            effect.EnableDefaultLighting();
                            //normal
                            //effect.World = world * Matrix.CreateScale(0.3f) * Matrix.CreateTranslation(new Vector3(Position.X, 1.3f, Position.Y));
                            //player
                            effect.World = Matrix.CreateRotationX((float)(-0.5 * Math.PI)) * Matrix.CreateRotationY(playerAngle) * Matrix.CreateRotationY((float)(Math.PI)) * Matrix.CreateTranslation(new Vector3(Position.X, 3.3f, Position.Y)) * world;
                            //question
                            //effect.World = Matrix.CreateRotationX((float)(-0.5 * Math.PI)) * Matrix.CreateTranslation(new Vector3(Position.X, 1.3f, Position.Y)) * world;
                            effect.View = view;
                            effect.Projection = projection;
                            effect.Alpha = ambientIntensity;
                        }
                        mesh.Draw();
                    }
                    break;
                default:
                    break;
            }

        }
    }
}
