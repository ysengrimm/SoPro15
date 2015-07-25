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
    public class NPC
    {
        // implements damage, range, mesh, texture etc (Are Questing Npcs also part of this class!?) NO!

        // The Model
        private Model npcModel, standingM, walkingM, jumpingM, swordfightingM, bowfightingM, runningM, idleM, talkingM;
        // Skinning Data
        private SkinningData standingSD, walkingSD, jumpingSD, swordfightingSD, bowfightingSD, runningSD, idleSD, talkingSD;
        // The animation Player
        private AnimationPlayer standingAP, walkingAP, jumpingAP, swordfightingAP, bowfightingAP, runningAP, idleAP, talkingAP;
        // The animation Clips, which will be used by the model
        private AnimationClip standingC, walkingC, jumpingC, swordfightingC, bowfightingC, runningC, idleC, talkingC;
        // The Bone Matrices for each animation
        private Matrix[] blendingBones, standingBones, walkingBones, jumpingBones, swordfightingBones, bowfightingBones, runningBones, idleBones, talkingBones;
        // The playerState, which will be needed to update the right animation
        public NPCState CurrentEnemyState = NPCState.Standing;
        public NPCState LastEnemyState = NPCState.Standing;

        // Variables for Ai
        private EnvironmentController currentEnvironment;
        private Ai npcAi;

        // Enemystats
        public Vector2 Position;
        public Vector2 oldPosition;
        public float MaxNPCHealth;
        public float Armor;
        public float MovementSpeed;
        public float TrackingRadius;
        public enum NPCName { Jack, Yorlgon, Konstantin, Namensloser}

        public NPCName Name = NPCName.Namensloser;
        public NPCProfession Profession = NPCProfession.Arbeitslos;

        private CollisionHandler collHandler;

        // Constructor
        public NPC(Vector2 position, EnvironmentController currentEnvironment, NPCName name, NPCProfession profession)
        {
            this.Name = name;
            this.Profession = profession;
            this.currentEnvironment = currentEnvironment;
            this.Position = position;
            this.TrackingRadius = 30f;
            MovementSpeed = 0.25f;
        }


        // Methods
        public void LoadContent(ContentManager content)
        {

            npcModel = content.Load<Model>("fbxContent/enemies/human/temp_enemy_v1");
            collHandler = CollisionHandler.Instance;
        }


        public void Update(GameTime gameTime)
        {
            oldPosition = Position;

            npcAi.updateAi(Position);
            Vector2 newPosition = Vector2.Add(npcAi.TrackingDirection, Position);

            //all this should be tested
            //if next part of grid contains less then 5 enemies:
            //let Enymy walk
            //remove from old List
            //add to new List
            // TODO: /10 shouldn't be a magic number <-- we have a GridSize in the Settings now!

        }

        public void Draw(Matrix world, Matrix view, Matrix projection)
        {
            foreach (ModelMesh mesh in npcModel.Meshes)
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
