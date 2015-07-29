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

        //Some variables for the Load and stuff
        public ContentManager contentMngr;
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
        public NPCState CurrentNPCState = NPCState.Standing;
        public NPCState LastNPCState = NPCState.Standing;
        public NPCState TempNPCState = NPCState.Standing;
        public Weapon activeWeapon = Weapon.Hammer;
        private WorldState activeWorld = WorldState.Safeworld;
        // duration of the animations
        private float standingDuration;
        private float walkingDuration;
        private float jumpingDuration;
        private float swordFightingDuration;

        private float stateTime;
        private float fixedBlendDuration;

        // variables for animation
        public float activeBlendTime;
        public bool isBlending;

        // Textures Body
        private Texture2D defaultBodyTex;
        private Texture2D defaultHairTex;
        private Texture2D defaultEyeTex;

        // Textures Cloth
        private Texture2D defaultShirtTex;
        private Texture2D defaultTrousersTex;

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
            // assign CollisionHandler
            collHandler = CollisionHandler.Instance;
            // assign contentManager
            contentMngr = content;
            // loading Body Textures
            defaultBodyTex = contentMngr.Load<Texture2D>("Texturen/Playertexturen/young_lightskinned_male_diffuse");
            defaultHairTex = contentMngr.Load<Texture2D>("Texturen/Playertexturen/male02_diffuse_black");
            defaultEyeTex = contentMngr.Load<Texture2D>("Texturen/NPCTexturen/Body/male/brown_eye");
            // loading Cloth Textures
            defaultShirtTex = contentMngr.Load<Texture2D>("Texturen/NPCTexturen/Cloth/Hemd_blue");
            defaultTrousersTex = contentMngr.Load<Texture2D>("Texturen/NPCTexturen/Cloth/Trousers_green");

            // loading default mesh
            npcModel = content.Load<Model>("fbxContent/NPC/NPC_male_idle");

            // loading Animation Models
            standingM = contentMngr.Load<Model>("fbxContent/NPC/NPC_male_idle");
            walkingM = contentMngr.Load<Model>("fbxContent/NPC/NPC_male_idle");
            jumpingM = contentMngr.Load<Model>("fbxContent/NPC/NPC_male_idle");
            swordfightingM = contentMngr.Load<Model>("fbxContent/NPC/NPC_male_idle");

            // Loading Skinning Data
            standingSD = standingM.Tag as SkinningData;
            walkingSD = walkingM.Tag as SkinningData;
            jumpingSD = jumpingM.Tag as SkinningData;
            swordfightingSD = swordfightingM.Tag as SkinningData;

            // Load an animation Player for each animation
            standingAP = new AnimationPlayer(standingSD);
            walkingAP = new AnimationPlayer(walkingSD);
            jumpingAP = new AnimationPlayer(jumpingSD);
            swordfightingAP = new AnimationPlayer(swordfightingSD);

            // loading the animation clips
            standingC = standingSD.AnimationClips["idle"];
            walkingC = walkingSD.AnimationClips["idle"];
            jumpingC = jumpingSD.AnimationClips["idle"];
            swordfightingC = swordfightingSD.AnimationClips["idle"];

            // Safty Start Animations
            standingAP.StartClip(standingC);
            walkingAP.StartClip(walkingC);
            jumpingAP.StartClip(jumpingC);
            swordfightingAP.StartClip(swordfightingC);

            //assign the specific animationTimes
            standingDuration = standingC.Duration.Milliseconds / 1f;
            walkingDuration = walkingC.Duration.Milliseconds / 1f;
            jumpingDuration = jumpingC.Duration.Milliseconds / 1f;
            swordFightingDuration = swordfightingC.Duration.Milliseconds / 1f;

            stateTime = 0;
            // Duration of Blending Animations in milliseconds
            fixedBlendDuration = 500;

        }


        public void Update(GameTime gameTime)
        {
            oldPosition = Position;

            //npcAi.updateAi(Position);
            //Vector2 newPosition = Vector2.Add(npcAi.TrackingDirection, Position);

            //update only the animation which is required if the changed Playerstate
            //Update the active animation
            switch (CurrentNPCState)
            {
                case NPCState.Standing:
                    standingAP.Update(gameTime.ElapsedGameTime, true, Matrix.Identity);
                    break;
                case NPCState.Walking:
                    walkingAP.Update(gameTime.ElapsedGameTime, true, Matrix.Identity);
                    break;
                case NPCState.Jumping:
                    jumpingAP.Update(gameTime.ElapsedGameTime, true, Matrix.Identity);
                    break;
                case NPCState.Swordfighting:
                    swordfightingAP.Update(gameTime.ElapsedGameTime, true, Matrix.Identity);
                    break;
                case NPCState.Talking:
                    walkingAP.Update(gameTime.ElapsedGameTime, true, Matrix.Identity);
                    jumpingAP.Update(gameTime.ElapsedGameTime, true, Matrix.Identity);
                    break;
            }

            // Secure, that the last NPCstate is always a other NPCstate, than the acutal
            if (CurrentNPCState != TempNPCState)
            {
                LastNPCState = TempNPCState;
                // When the playerState changes, we need to blend
                isBlending = true;
                activeBlendTime = fixedBlendDuration;
            }


            // if the Time for blending is over, set it on false;
            if (activeBlendTime <= 0)
            {
                isBlending = false;
                if (activeBlendTime < 0)
                {
                    activeBlendTime = 0;
                }
            }
            else
            {
                // update the blendDuration
                activeBlendTime -= gameTime.ElapsedGameTime.Milliseconds;
            }

            // Update the last animation (only 500 milliseconds after the last changing state required)
            if (isBlending)
            {
                switch (LastNPCState)
                {
                    case NPCState.Standing:
                        standingAP.Update(gameTime.ElapsedGameTime, true, Matrix.Identity);
                        break;
                    case NPCState.Walking:
                        walkingAP.Update(gameTime.ElapsedGameTime, true, Matrix.Identity);
                        break;
                    case NPCState.Jumping:
                        jumpingAP.Update(gameTime.ElapsedGameTime, true, Matrix.Identity);
                        break;
                    case NPCState.Swordfighting:
                        swordfightingAP.Update(gameTime.ElapsedGameTime, true, Matrix.Identity);
                        break;
                    case NPCState.Talking:
                        walkingAP.Update(gameTime.ElapsedGameTime, true, Matrix.Identity);
                        jumpingAP.Update(gameTime.ElapsedGameTime, true, Matrix.Identity);
                        break;
                }
            }


            //Update Temp PNPCState
            TempNPCState = CurrentNPCState;

        }

        public void Draw(Matrix world, Matrix view, Matrix projection)
        {
            // Bone updates for each required animation   
            switch (CurrentNPCState)
            {
                case NPCState.Standing:
                    standingBones = standingAP.GetSkinTransforms();
                    break;
                case NPCState.Walking:
                    walkingBones = walkingAP.GetSkinTransforms();
                    break;
                case NPCState.Swordfighting:
                    swordfightingBones = swordfightingAP.GetSkinTransforms();
                    break;
                case NPCState.Jumping:
                    jumpingBones = jumpingAP.GetSkinTransforms();
                    break;
                case NPCState.Talking:
                    walkingBones = walkingAP.GetSkinTransforms();
                    break;
            }

            if (isBlending)
            {
                // Bone updates for each required animation for blending 
                switch (LastNPCState)
                {
                    case NPCState.Standing:
                        blendingBones = standingAP.GetSkinTransforms();
                        break;
                    case NPCState.Walking:
                        blendingBones = walkingAP.GetSkinTransforms();
                        break;
                    case NPCState.Swordfighting:
                        blendingBones = swordfightingAP.GetSkinTransforms();
                        break;
                    case NPCState.Jumping:
                        blendingBones = jumpingAP.GetSkinTransforms();
                        break;
                    case NPCState.Talking:
                        blendingBones = walkingAP.GetSkinTransforms();
                        break;
                }
            }

            foreach (ModelMesh mesh in npcModel.Meshes)
            {

                //Console.WriteLine(mesh.Name);
                    // Only renders the Right assigned Weapon
                if (mesh.Name == "Stock" && activeWeapon != Weapon.Stock)
                {

                }
                else if (mesh.Name == "Hammer" && activeWeapon != Weapon.Hammer)
                {

                }
                    // Only renders the Meshes with the right resolution
                else if(mesh.Name == "NPC_body" && Settings.Instance.NPCMeshQuality != Settings.MeshQuality.High)
                {

                }
                else if(mesh.Name == "NPC_body_lowPoly" && Settings.Instance.NPCMeshQuality != Settings.MeshQuality.Low)
                {

                }
                    // renders everything that should be
                else
                {
                    foreach (SkinnedEffect effect in mesh.Effects)
                    {
                        //Draw the Bones which are required
                        if (isBlending)
                        {
                            float percentageOfBlending = activeBlendTime / fixedBlendDuration;
                            switch (CurrentNPCState)
                            {
                                case NPCState.Standing:
                                    for (int i = 0; i < blendingBones.Length; i++)
                                    {
                                        blendingBones[i] = Matrix.Lerp(blendingBones[i], standingBones[i], 1 - percentageOfBlending);
                                    }
                                    effect.SetBoneTransforms(blendingBones);
                                    break;
                                case NPCState.Walking:
                                    for (int i = 0; i < blendingBones.Length; i++)
                                    {
                                        blendingBones[i] = Matrix.Lerp(blendingBones[i], walkingBones[i], 1 - percentageOfBlending);
                                    }
                                    effect.SetBoneTransforms(blendingBones);
                                    break;
                                case NPCState.Swordfighting:
                                    for (int i = 0; i < blendingBones.Length; i++)
                                    {
                                        blendingBones[i] = Matrix.Lerp(blendingBones[i], swordfightingBones[i], 1 - percentageOfBlending);
                                    }
                                    effect.SetBoneTransforms(blendingBones);
                                    break;
                                case NPCState.Jumping:
                                    for (int i = 0; i < blendingBones.Length; i++)
                                    {
                                        blendingBones[i] = Matrix.Lerp(blendingBones[i], jumpingBones[i], 1 - percentageOfBlending);
                                    }
                                    effect.SetBoneTransforms(blendingBones);
                                    break;
                                case NPCState.Talking:
                                    for (int i = 0; i < blendingBones.Length; i++)
                                    {
                                        blendingBones[i] = Matrix.Lerp(blendingBones[i], walkingBones[i], 1 - percentageOfBlending);
                                    }
                                    effect.SetBoneTransforms(blendingBones);
                                    break;
                            }
                        }
                        else
                        {
                            switch (CurrentNPCState)
                            {
                                case NPCState.Standing:
                                    effect.SetBoneTransforms(standingBones);
                                    break;
                                case NPCState.Walking:
                                    effect.SetBoneTransforms(walkingBones);
                                    break;
                                case NPCState.Swordfighting:
                                    effect.SetBoneTransforms(swordfightingBones);
                                    break;
                                case NPCState.Jumping:
                                    effect.SetBoneTransforms(jumpingBones);
                                    break;
                                case NPCState.Talking:
                                    effect.SetBoneTransforms(walkingBones);
                                    break;
                            }
                        }


                        effect.EnableDefaultLighting();
                        effect.World = Matrix.CreateRotationX((float)(-0.5 * Math.PI)) * Matrix.CreateTranslation(new Vector3(Position.X, 0, Position.Y)) * world;
                        effect.View = view;
                        effect.Projection = projection;
                        effect.SpecularColor = new Vector3(0.25f);
                        effect.SpecularPower = 16;
                        effect.PreferPerPixelLighting = true;

                        // Textures

                        // Body
                        if (mesh.Name == "NPC_body")
                        {
                            effect.Texture = defaultBodyTex;
                        }
                        else if (mesh.Name == "NPC_hair")
                        {
                            effect.Texture = defaultHairTex;
                        }
                        else if (mesh.Name == "NPC_eyes")
                        {
                            effect.Texture = defaultEyeTex;
                        }

                        // Cloth
                        else if (mesh.Name == "NPC_shirt")
                        {
                            effect.Texture = defaultShirtTex;
                        }
                        else if (mesh.Name == "NPC_hose")
                        {
                            effect.Texture = defaultTrousersTex;
                        }
                        else
                        {
                            effect.Texture = defaultBodyTex;
                        }

                    }
                    mesh.Draw();
                }              
            }
        }
    }
}
