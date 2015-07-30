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
using SkinnedModel;

namespace EmodiaQuest.Core.NPCs
{
    public class Enemy
    {
        // Variables for Ai
        private EnvironmentController currentEnvironment;
        private Ai enemyAi;

        // Enemystats
        public Vector2 Position;
        public float ViewAngle;

        private Vector2 oldPosition;
        public float MaxEnemyHealth;
        public float Armor;
        public float MovementSpeed;
        public float TrackingRadius;
        public float CircleCollision;
        public float AttackRange;
        public float Damage;

        private float attackTimer;
        public float AttackThreshold;
        public float AttackSpeed;


        private float gridSize = Settings.Instance.GridSize;

        public bool IsAlive { get; set; }

        private CollisionHandler collHandler;


        /**
         * Animation and Model
        **/
        // The Model
        private Model enemyModel, standingM, walkingM, jumpingM, swordfightingM, bowfightingM, runningM, idleM;
        // Skinning Data
        private SkinningData standingSD, walkingSD, jumpingSD, swordfightingSD, bowfightingSD, runningSD, idleSD;
        // The animation Player
        private AnimationPlayer standingAP, walkingAP, jumpingAP, swordfightingAP, bowfightingAP, runningAP, idleAP;
        // The animation Clips, which will be used by the model
        private AnimationClip standingC, walkingC, jumpingC, swordfightingC, bowfightingC, runningC, idleC;
        // The Bone Matrices for each animation
        private Matrix[] blendingBones, standingBones, walkingBones, jumpingBones, swordfightingBones, bowfightingBones, runningBones, idleBones;
        // The playerState, which will be needed to update the right animation
        public EnemyState CurrentEnemyState = EnemyState.Standing;
        public EnemyState LastEnemyState = EnemyState.Standing;


        public Enemy(Vector2 position, EnvironmentController currentEnvironment)
        {
            this.currentEnvironment = currentEnvironment;
            Position = position;

            currentEnvironment.enemyArray[(int)Math.Round(Position.X / 10), (int)Math.Round(Position.Y / 10)].Add(this);
        }


        // Methods
        public void LoadContent(ContentManager content)
        {
            MovementSpeed = Settings.Instance.HumanEnemySpeed;
            MaxEnemyHealth = Settings.Instance.MaxHumanEnemyHealth;
            AttackRange = 5;
            Damage = 5;

            // movement
            TrackingRadius = 30f;
            MovementSpeed = 0.25f;
            enemyAi = new Ai(Position, CurrentEnemyState, LastEnemyState, TrackingRadius, MovementSpeed, currentEnvironment);
            ViewAngle = -enemyAi.TrackingAngle;

            attackTimer = 0;
            AttackThreshold = 20;
            AttackSpeed = 0.5f;

            // collision
            CircleCollision = 1.5f;
            collHandler = CollisionHandler.Instance;

            IsAlive = true;

            enemyModel = content.Load<Model>("fbxContent/enemies/human/temp_enemy_v1");
        }


        public void Update(GameTime gameTime)
        {
            oldPosition = Position;

            attackTimer += AttackSpeed;

            enemyAi.UpdateAi(Position);
            Vector2 newPosition = Vector2.Add(enemyAi.TrackingDirection, Position);
            ViewAngle = -enemyAi.TrackingAngle;

            // object collision
            if (IsAlive && Color.White == collHandler.GetCollisionColor(new Vector2(Position.X, newPosition.Y), collHandler.Controller.CollisionColors, 2.0f))
            {
                if (currentEnvironment.enemyArray[(int)Math.Round(newPosition.X / 10), (int)Math.Round(newPosition.Y / 10)].Count < 5 && !onSameGridElement(newPosition, Player.Instance.Position))
                {
                    Position.Y = newPosition.Y;
                    Position.X = newPosition.X;

                    Vector2 currentGridPos = new Vector2((float)Math.Round(newPosition.X / gridSize), (float)Math.Round(newPosition.Y / gridSize));
                    for (int i = -1; i < 2; i++)
                    {
                        for (int j = -1; j < 2; j++)
                        {
                            List<Enemy> currentBlockEnemyList = currentEnvironment.enemyArray[(int)currentGridPos.X + i, (int)currentGridPos.Y + j];
                            if (currentBlockEnemyList.Count <= 0) continue;

                            // enemy - player collision
                            var pdx = (Position.X - Player.Instance.Position.X) * (Position.X - Player.Instance.Position.X);
                            var pdy = (Position.Y - Player.Instance.Position.Y) * (Position.Y - Player.Instance.Position.Y);
                            if (Math.Sqrt(pdx + pdy) < (CircleCollision + Player.Instance.CollisionRadius))
                            {
                                Position.X = oldPosition.X;
                                Position.Y = oldPosition.Y;
                            }

                            // enemy - enemy collision
                            foreach (var enemy in currentBlockEnemyList)
                            {
                                if (this != enemy)
                                {
                                    var dx = (Position.X - enemy.Position.X) * (Position.X - enemy.Position.X);
                                    var dy = (Position.Y - enemy.Position.Y) * (Position.Y - enemy.Position.Y);
                                    if (Math.Sqrt(dx + dy) < (CircleCollision + enemy.CircleCollision))
                                    {
                                        Position.X = oldPosition.X;
                                        Position.Y = oldPosition.Y;
                                    }
                                }
                            }
                        }
                    }
                    
                    currentEnvironment.enemyArray[(int)Math.Round(oldPosition.X / 10), (int)Math.Round(oldPosition.Y / 10)].Remove(this);
                    currentEnvironment.enemyArray[(int)Math.Round(Position.X / 10), (int)Math.Round(Position.Y / 10)].Add(this);
                }
            }

            // interaction
            if (IsAlive)
            {
                for (int i = -1; i < 2; i++)
                {
                    for (int j = -1; j < 2; j++)
                    {
                        if (attackTimer >= AttackThreshold && onSameGridElement(new Vector2(Position.X + i, Position.Y + j), Player.Instance.Position) && EuclideanDistance(Position, Player.Instance.Position) <= AttackRange)
                        {
                            Player.Instance.Attack(Damage);
                            attackTimer = 0;
                        }
                    }
                }
            }
        }

        bool onSameGridElement(Vector2 a, Vector2 b)
        {
            Vector2 aOnGrid = new Vector2((int)Math.Round(a.X / Settings.Instance.GridSize), (int)Math.Round(a.Y / Settings.Instance.GridSize));
            Vector2 bOnGrid = new Vector2((int)Math.Round(b.X / Settings.Instance.GridSize), (int)Math.Round(b.Y / Settings.Instance.GridSize));

            return aOnGrid.X == bOnGrid.X && aOnGrid.Y == bOnGrid.Y;
        }

        public void Attack(float damage)
        {
            MaxEnemyHealth -= damage;

            if (!(MaxEnemyHealth <= 0)) return;
            if (currentEnvironment.enemyArray[(int)Math.Round(Position.X / 10), (int)Math.Round(Position.Y / 10)].Remove(this))
            {
                IsAlive = false;
            }
            Console.WriteLine("Enemy at " + Position + " died");
        }

        public void SetAsAlive()
        {
            IsAlive = true;
            currentEnvironment.enemyArray[(int) Math.Round(Position.X/10), (int) Math.Round(Position.Y/10)].Add(this);
        }

        public void Draw(Matrix world, Matrix view, Matrix projection)
        {
            if (IsAlive)
            {
                foreach (ModelMesh mesh in enemyModel.Meshes)
                {
                    foreach (BasicEffect effect in mesh.Effects)
                    {
                        effect.EnableDefaultLighting();
                        effect.World = Matrix.CreateRotationY(ViewAngle) * Matrix.CreateTranslation(new Vector3(Position.X, 0, Position.Y))*world;
                        effect.View = view;
                        effect.Projection = projection;
                    }
                    mesh.Draw();
                }
            }
        }

        private double EuclideanDistance(Vector2 p1, Vector2 p2)
        {
            return Math.Sqrt(Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2));
        }
        
    }
}
