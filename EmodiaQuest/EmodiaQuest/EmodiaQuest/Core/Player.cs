using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace EmodiaQuest.Core
{
    public class Player
    {
        private static Player _instance;

        // For movement and camera update
        private Vector2 movement; // future position
        private Vector2 lastPos; //position from last step (fixes false kamera focus)
        public float MovementOffset, ItemOffset;
        public float Angle;


        // The Model
        private Model playerModel, standingM, walkingM, jumpingM, swordfightingM, bowfightingM;
        // Skinning Data
        private SkinningData standingSD, walkingSD, jumpingSD, swordfightingSD, bowfightingSD;
        // The animation Player
        private AnimationPlayer standingAP, walkingAP, jumpingAP, swordfightingAP, bowfightingAP;
        // The animation Clips, which will be used by the model
        private AnimationClip standingC, walkingC, jumpingC, swordfightingC, bowfightingC;
        // The Bone Matrices for each animation
        private Matrix[] blendingBones, standingBones, walkingBones, jumpingBones, swordfightingBones, bowfightingBones;
        // The playerState, which will be needed to update the right animation
        public PlayerState PlayerState = PlayerState.Standing;
        public PlayerState LastPlayerState = PlayerState.Standing;
        private float standingDuration;
        private float walkingDuration;
        private float jumpingDuration;
        private float stateTime;

        // Playerstats
        public float Hp;
        public float Armor;
        public float PlayerSpeed;
        public float RotationSpeed;

        // Properties
        private Vector2 windowSize;
        public Vector2 WindowSize
        {
            set { windowSize = value; }
        }

        private ContentManager contentMngr;
        public ContentManager ContentMngr
        {
            set { contentMngr = value; }
        }

        private Vector2 position;
        public Vector2 Position
        {
            set { position = value; }
            get { return position; }
        }

        private CollisionHandler collisionHandler;
        public CollisionHandler CollisionHandler
        {
            set { collisionHandler = value; }
        }

        private Player()
        {
       
        }

        public static Player Instance
        {
            get { return _instance ?? (_instance = new Player()); }
        }


        public void LoadContent()
        {
            // Initialize player stuff
            PlayerSpeed = Settings.Instance.PlayerSpeed;
            RotationSpeed = Settings.Instance.PlayerRotationSpeed;

            // set defaults
            Hp = 100;
            Armor = 0;

            MovementOffset = 2.0f;
            ItemOffset = 0.0f;

            Angle = 0;

            //playerModel = contentMngr.Load<Model>("fbxContent/player/MainChar_run_34f");
            /*
            // Look up our custom skinning information.
            SkinningData skinningData = playerModel.Tag as SkinningData;
            // Create an animation player, and start decoding an animation clip.
            animationPlayer = new AnimationPlayer(skinningData);
            Console.WriteLine("Holla" + skinningData.AnimationClips.Count);
            AnimationClip clip = skinningData.AnimationClips["Jump"];
            animationPlayer.StartClip(clip);
            */

            //loading default mesh
            //playerModel = contentMngr.Load<Model>("fbxContent/player/MainChar_stand_34f");
            playerModel = contentMngr.Load<Model>("fbxContent/testPlayerv1");

            //loading Animation Models
            //standingM = contentMngr.Load<Model>("fbxContent/player/MainChar_stand_34f");
            standingM = contentMngr.Load<Model>("fbxContent/testPlayerv1_Stand");
            //walkingM = contentMngr.Load<Model>("fbxContent/player/MainChar_walk_34f");
            walkingM = contentMngr.Load<Model>("fbxContent/testPlayerv1_Run");
            //jumpingM = contentMngr.Load<Model>("fbxContent/player/MainChar_walk_34f");
            jumpingM = contentMngr.Load<Model>("fbxContent/testPlayerv1_Jump");

            //Loading Skinning Data
            standingSD = standingM.Tag as SkinningData;
            walkingSD = walkingM.Tag as SkinningData;
            jumpingSD = jumpingM.Tag as SkinningData;

            //Load an animation Player for each animation
            standingAP = new AnimationPlayer(standingSD);
            walkingAP = new AnimationPlayer(walkingSD);
            jumpingAP = new AnimationPlayer(jumpingSD);

            //loading Animation
            
            standingC = standingSD.AnimationClips["Stand"];
            walkingC = walkingSD.AnimationClips["Run"];
            jumpingC = jumpingSD.AnimationClips["Jump"];
            
            /*
            standingC = standingSD.AnimationClips["stand"];
            walkingC = walkingSD.AnimationClips["walk"];
            jumpingC = jumpingSD.AnimationClips["walk"];
            */
            //Safty Start Animations
            standingAP.StartClip(standingC);
            walkingAP.StartClip(walkingC);
            jumpingAP.StartClip(jumpingC);

            //assign the specific animationTimes
            standingDuration = standingC.Duration.Milliseconds/1f;
            walkingDuration = walkingC.Duration.Milliseconds/1f;
            jumpingDuration = jumpingC.Duration.Milliseconds/1f;
            stateTime = 0;

        }

        public void Update(GameTime gameTime, MouseState mouseState)
        {
            //scale position to 0.0 to 1.0 then center the +/- change
            Angle += (float) -(((mouseState.X/windowSize.X) - 0.5) * RotationSpeed);
            // reset mouse position to window center
            Mouse.SetPosition((int)(windowSize.X / 2), (int)(windowSize.Y / 2));

            lastPos = Position;
            movement = Position;
            
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                movement.Y += PlayerSpeed * (float)Math.Cos(Angle);
                movement.X += PlayerSpeed * (float)Math.Sin(Angle);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                movement.Y -= PlayerSpeed * (float)Math.Cos(Angle);
                movement.X -= PlayerSpeed * (float)Math.Sin(Angle);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                movement.Y -= PlayerSpeed * (float)Math.Cos(Angle - Math.PI / 2);
                movement.X -= PlayerSpeed * (float)Math.Sin(Angle - Math.PI / 2);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                movement.Y += PlayerSpeed * (float)Math.Cos(Angle + 3 * Math.PI / 2);
                movement.X += PlayerSpeed * (float)Math.Sin(Angle + 3 * Math.PI / 2);
            }

            //Collision with Walls
            if (Color.White == collisionHandler.GetCollisionColor(new Vector2(Position.X, movement.Y), collisionHandler.Controller.CollisionColors, MovementOffset))
            {
                position.Y = movement.Y;
            }
            if (Color.White == collisionHandler.GetCollisionColor(new Vector2(movement.X, Position.Y), collisionHandler.Controller.CollisionColors, MovementOffset))
            {
                position.X = movement.X;
            }

            //Collision with Items
            if (Color.White != collisionHandler.GetCollisionColor(new Vector2(Position.X, Position.Y), collisionHandler.Controller.ItemColors, ItemOffset))
            {
                for(var i = 0; i < collisionHandler.Controller.Items.Count; i++)
                {
                    var temp = new Vector2(collisionHandler.Controller.Items.ElementAt(i).position.X, collisionHandler.Controller.Items.ElementAt(i).position.Z);
                if (EuclideanDistance(temp, new Vector2(Position.X, Position.Y)) <= 12)
                {
                    collisionHandler.Controller.Items.RemoveAt(i);
                    //Console.Out.WriteLine("+1 Point");
                }
                }

            }
            //update playerState

            if ((lastPos.X != movement.X || lastPos.Y != movement.Y) && stateTime <= 10 && Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                PlayerState = PlayerState.WalkJumping;
                stateTime = jumpingDuration;
            }
            else if ((lastPos.X != movement.X || lastPos.Y != movement.Y) && stateTime <= 10)
            {
                PlayerState = PlayerState.Walking;
                stateTime = walkingDuration/3f;
            }
            else if(Keyboard.GetState().IsKeyDown(Keys.Space) && stateTime <= 10)
            {
                PlayerState = PlayerState.Jumping;
                stateTime = jumpingDuration;
            }
            else if(lastPos.X == movement.X && lastPos.Y == movement.Y && stateTime <= 0)
            {
                PlayerState = PlayerState.Standing;
                stateTime = standingDuration/4f;
            }
            stateTime -= gameTime.ElapsedGameTime.Milliseconds;

            //update only the animation which is required if the changed Playerstate      
            switch(PlayerState)
            {
                case PlayerState.Standing:
                    standingAP.Update(gameTime.ElapsedGameTime, true, Matrix.Identity);
                    break;
                case PlayerState.Walking:
                    walkingAP.Update(gameTime.ElapsedGameTime, true, Matrix.Identity);
                    break;
                case PlayerState.Jumping:
                    jumpingAP.Update(gameTime.ElapsedGameTime, true, Matrix.Identity);
                    break;
                case PlayerState.WalkJumping:
                    walkingAP.Update(gameTime.ElapsedGameTime, true, Matrix.Identity);
                    jumpingAP.Update(gameTime.ElapsedGameTime, true, Matrix.Identity);
                    break;
            }
            LastPlayerState = PlayerState;
        }

        public void Draw(Matrix world, Matrix view, Matrix projection)
        {
            // Bone updates for each required animation   
            
            switch (PlayerState)
            {
                case PlayerState.Standing:
                    standingBones = standingAP.GetSkinTransforms();
                    break;
                case PlayerState.Walking:
                    walkingBones = walkingAP.GetSkinTransforms();
                    break;
                case PlayerState.Jumping:
                    jumpingBones = jumpingAP.GetSkinTransforms();
                    break;
                case PlayerState.WalkJumping:
                    walkingBones = walkingAP.GetSkinTransforms();
                    jumpingBones = jumpingAP.GetSkinTransforms();
                    break;
            }
            
            foreach (var mesh in playerModel.Meshes)
            {
                foreach (SkinnedEffect effect in mesh.Effects)
                {
                    
                    //Draw the Bones which are required
                    
                    switch (PlayerState)
                    {
                        case PlayerState.Standing:
                            effect.SetBoneTransforms(standingBones);
                            break;
                        case PlayerState.Walking:
                            effect.SetBoneTransforms(walkingBones);
                            break;
                        case PlayerState.Jumping:
                            effect.SetBoneTransforms(jumpingBones);
                            break;
                        case PlayerState.WalkJumping:                          
                            blendingBones = walkingBones;
                            for (var i = 0; i < walkingBones.Length; i++)
                            {
                                blendingBones[i] = Matrix.Multiply(walkingBones[i], jumpingBones[i]);
                            }
                            effect.SetBoneTransforms(blendingBones);                           
                            break;
                    }
                    effect.EnableDefaultLighting();
                    effect.DiffuseColor = new Vector3(255, 0, 0);
                    effect.World = Matrix.CreateRotationY(Angle) * Matrix.CreateTranslation(new Vector3(lastPos.X, 0, lastPos.Y)) * world;
                    effect.View = view;
                    effect.Projection = projection;


                   effect.SpecularColor = new Vector3(0.25f);
                    effect.SpecularPower = 16;
                }
                mesh.Draw();
            }
        }

        private double EuclideanDistance(Vector2 p1, Vector2 p2)
        {
            return Math.Sqrt(Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2));
        }

    }
}
