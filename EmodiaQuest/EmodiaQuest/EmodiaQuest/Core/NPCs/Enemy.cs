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
        public Vector3 Position;
        public Vector3 oldPosition;
        public float MaxEnemyHealth;
        public float Armor;
        public float MovementSpeed;
        public float TrackingRadius;

        // Constructor
        public Enemy(Vector3 position, EnvironmentController currentEnvironment)
        {
            this.currentEnvironment = currentEnvironment;
            this.Position = position;
            currentEnvironment.enemyArray[(int)Math.Round(Position.X / 10), (int)Math.Round(Position.Z / 10)].Add(this);
            this.TrackingRadius = 20f;
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
        }


        public void Update(GameTime gameTime)
        {
            oldPosition = Position;

            enemyAi.updateAi(Position);;
            Vector3 newPosition = Vector3.Add(enemyAi.TrackingDirection, Position);

            //all this should be tested
            //if next part of grid contains less then 5 enemies:
            //let Enymy walk
            //remove from old List
            //add to new List
            if (currentEnvironment.enemyArray[(int)Math.Round(newPosition.X / 10), (int)Math.Round(newPosition.Z / 10)].Count < 5)  //if next part of grid contains less then 5 Enemies
            {
                Position = Vector3.Add(enemyAi.TrackingDirection, Position);
                currentEnvironment.enemyArray[(int)Math.Round(oldPosition.X / 10), (int)Math.Round(oldPosition.Z / 10)].Remove(this);
                currentEnvironment.enemyArray[(int)Math.Round(Position.X / 10), (int)Math.Round(Position.Z / 10)].Add(this);      
            }

            //to test current position in array
            /*
            for (int i = 0; i < currentEnvironment.enemyArray.GetLength(0); i++)
            {
                for (int j = 0; j < currentEnvironment.enemyArray.GetLength(1); j++)
                {
                    if(currentEnvironment.enemyArray[i, j].Count == 1)
                        Console.Out.WriteLine(i + " " + j);
                }
            }
            */
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
