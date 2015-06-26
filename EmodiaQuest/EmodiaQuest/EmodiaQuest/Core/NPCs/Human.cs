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
    public class Human
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
        private Ai humanAi;

        // Enemystats
        public Vector3 Position;
        public float MaxHumanEnemyHealth;
        public float Armor;
        public float MovementSpeed;
        public float TrackingRadius;

        // Constructor
        public Human(Vector3 position, EnvironmentController currentEnvironment)
        {
            this.currentEnvironment = currentEnvironment;
            this.Position = position;
            this.TrackingRadius = 20f;
            MovementSpeed = 0.25f;
            this.humanAi = new Ai(position, CurrentEnemyState, LastEnemyState, TrackingRadius, MovementSpeed, currentEnvironment);
        }


        // Methods
        public void LoadContent(ContentManager content)
        {
            MovementSpeed = Settings.Instance.HumanEnemySpeed;
            MaxHumanEnemyHealth = Settings.Instance.MaxHumanEnemyHealth;
            enemyModel = content.Load<Model>("fbxContent/enemies/human/temp_enemy_v1");
        }


        public void Update(GameTime gameTime)
        {
            humanAi.updateAi(Position);;
            Position = Vector3.Add(humanAi.TrackingDirection, Position);
        }



        public void Draw(Matrix world, Matrix view, Matrix projection)
        {
            foreach (ModelMesh mesh in enemyModel.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();
                    effect.World = Matrix.CreateTranslation(Position) * world;
                    effect.View = view;
                    effect.Projection = projection;
                }
                mesh.Draw();
            }
        }


        
    }
}
