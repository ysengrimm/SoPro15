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
        // implements damage, range, mesh, texture etc (Are Questing Npcs also part of this class!?) NO!

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

        // Variables for Ai
        private EnvironmentController currentEnvironment;
        private Ai enemyAi;

        // Enemystats
        public Vector2 Position;
        public Vector2 oldPosition;
        public float MaxEnemyHealth;
        public float Armor;
        public float MovementSpeed;
        public float TrackingRadius;

        public bool IsAlive { get; set; }

        private CollisionHandler collHandler;

        // Constructor
        public Enemy(Vector2 position, EnvironmentController currentEnvironment)
        {
            this.currentEnvironment = currentEnvironment;
            this.Position = position;
            currentEnvironment.enemyArray[(int)Math.Round(Position.X / 10), (int)Math.Round(Position.Y / 10)].Add(this);
            this.TrackingRadius = 30f;
            MovementSpeed = 0.25f;
            this.enemyAi = new Ai(position, CurrentEnemyState, LastEnemyState, TrackingRadius, MovementSpeed, currentEnvironment);
        }


        // Methods
        public void LoadContent(ContentManager content)
        {
            MovementSpeed = Settings.Instance.HumanEnemySpeed;
            MaxEnemyHealth = Settings.Instance.MaxHumanEnemyHealth;
            //enemyModel = content.Load<Model>("fbxContent/enemies/human/temp_enemy_v1");
            enemyModel = content.Load<Model>("fbxContent/enemies/human/temp_enemy_v1");

            IsAlive = true;
            collHandler = CollisionHandler.Instance;
        }


        public void Update(GameTime gameTime)
        {
            oldPosition = Position;

            enemyAi.updateAi(Position);;
            Vector2 newPosition = Vector2.Add(enemyAi.TrackingDirection, Position);

            //all this should be tested
            //if next part of grid contains less then 5 enemies:
            //let Enymy walk
            //remove from old List
            //add to new List
            // TODO: /10 shouldn't be a magic number
            
            // object collision
            if (Color.White == collHandler.GetCollisionColor(new Vector2(Position.X, newPosition.Y), collHandler.Controller.Collisionmap, 2.0f))
            {
                if (IsAlive && currentEnvironment.enemyArray[(int)Math.Round(newPosition.X / 10), (int)Math.Round(newPosition.Y / 10)].Count < 5 && !onSameGridElement(newPosition, Player.Instance.Position))  //if next part of grid contains less then 5 Enemies
                {
                    Position.Y = newPosition.Y;
                    currentEnvironment.enemyArray[(int)Math.Round(oldPosition.X / 10), (int)Math.Round(oldPosition.Y / 10)].Remove(this);
                    currentEnvironment.enemyArray[(int)Math.Round(Position.X / 10), (int)Math.Round(Position.Y / 10)].Add(this);
                }
                
            }
            if (Color.White == collHandler.GetCollisionColor(new Vector2(newPosition.X, Position.Y), collHandler.Controller.Collisionmap, 2.0f))
            {
                if (IsAlive && currentEnvironment.enemyArray[(int)Math.Round(newPosition.X / 10), (int)Math.Round(newPosition.Y / 10)].Count < 5 && !onSameGridElement(newPosition, Player.Instance.Position))  //if next part of grid contains less then 5 Enemies
                {
                    Position.X = newPosition.X;
                    currentEnvironment.enemyArray[(int)Math.Round(oldPosition.X / 10), (int)Math.Round(oldPosition.Y / 10)].Remove(this);
                    currentEnvironment.enemyArray[(int)Math.Round(Position.X / 10), (int)Math.Round(Position.Y / 10)].Add(this);
                }
            }
        }

        bool onSameGridElement(Vector2 a, Vector2 b)
        {
            Vector2 aOnGrid = new Vector2((int)Math.Round(a.X / Settings.Instance.GridSize), (int)Math.Round(a.Y / Settings.Instance.GridSize));
            Vector2 bOnGrid = new Vector2((int)Math.Round(b.X / Settings.Instance.GridSize), (int)Math.Round(b.Y / Settings.Instance.GridSize));

            return aOnGrid.X == bOnGrid.X && aOnGrid.Y == bOnGrid.Y;
        }

        public void SetAsDead()
        {
            if (currentEnvironment.enemyArray[(int) Math.Round(Position.X/10), (int) Math.Round(Position.Y/10)].Remove(this))
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
                        effect.World = Matrix.CreateTranslation(new Vector3(Position.X, 0, Position.Y))*world;
                        effect.View = view;
                        effect.Projection = projection;
                    }
                    mesh.Draw();
                }
            }
        }


        
    }
}
