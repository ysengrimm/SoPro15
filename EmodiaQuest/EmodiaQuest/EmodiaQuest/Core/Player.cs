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
        // Playerstats
        public float Hp;
        public float Armor;
        public float PlayerSpeed = 0.5f;
        public float RotationSpeed = 1.5f;

        public Vector2 Position;
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
        private Matrix[] standingBones, walkingBones, jumpingBones, swordfightingBones, bowfightingBones;
        // The playerState, which will be needed to update the right animation
        public PlayerState playerState = PlayerState.Standing;
        public PlayerState lastPlayerState = PlayerState.Standing;
        private float standingDuration;
        private float walkingDuration;
        private float jumpingDuration;
        private float stateTime;

        private CollisionHandler collisionHandler;

        private Vector2 windowSize;

        public ContentManager content;

        public Model Model
        {
            set { standingM = value; }
        }



        /// <summary>
        /// Creates new instance of player.
        /// </summary>
        /// <param name="position">Initial player position.</param>
        /// <param name="collisionHandler">Current collision handler</param>
        public Player(Vector2 position, CollisionHandler collisionHandler, Vector2 winSize, ContentManager content)
        {
            Position = position;
            this.collisionHandler = collisionHandler;
            this.content = content;
            windowSize = winSize;
            
            // set defaults
            Hp = 100;
            Armor = 0;

            MovementOffset = 2.0f;
            ItemOffset = 0.0f;

            Angle = 0;
            
            playerModel = content.Load<Model>("fbxContent/testPlayerv1");
            /*
            // Look up our custom skinning information.
            SkinningData skinningData = playerModel.Tag as SkinningData;
            // Create an animation player, and start decoding an animation clip.
            animationPlayer = new AnimationPlayer(skinningData);
            Console.WriteLine("Holla" + skinningData.AnimationClips.Count);
            AnimationClip clip = skinningData.AnimationClips["Jump"];

            animationPlayer.StartClip(clip);
            */
        }

        public void LoadContent()
        {
            //loading default mesh
            playerModel = content.Load<Model>("fbxContent/testPlayerv1");
            //loading Animation Models
            standingM = content.Load<Model>("fbxContent/testPlayerv1_Stand");
            walkingM = content.Load<Model>("fbxContent/testPlayerv1_Run");
            jumpingM = content.Load<Model>("fbxContent/testPlayerv1_Jump");
            //Loading Skinning Data
            standingSD = standingM.Tag as SkinningData;
            walkingSD = walkingM.Tag as SkinningData;
            jumpingSD = jumpingM.Tag as SkinningData;
            //Load an animation Player for each animation
            standingAP = new AnimationPlayer(standingSD);
            walkingAP = new AnimationPlayer(walkingSD);
            jumpingAP = new AnimationPlayer(jumpingSD);
            //loading Animation Clips
            AnimationClip standingC = standingSD.AnimationClips["Stand"];
            AnimationClip walkingC = walkingSD.AnimationClips["Run"];
            AnimationClip jumpingC = jumpingSD.AnimationClips["Jump"];
            //Safty Start Animations
            standingAP.StartClip(standingC);
            walkingAP.StartClip(walkingC);
            jumpingAP.StartClip(jumpingC);
            //assign the specific animationTimes
            standingDuration = standingC.Duration.Milliseconds/1.5f;
            walkingDuration = walkingC.Duration.Milliseconds/1.5f;
            jumpingDuration = jumpingC.Duration.Milliseconds/1.5f;
            stateTime = 0;

        }

        public void Update(GameTime gameTime, MouseState mouseState)
        {
            //scale position to 0.0 to 1.0 then center the +/- change
            Angle = (float) -(((mouseState.X/windowSize.X))*2*Math.PI * RotationSpeed);
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
            if (Color.White == collisionHandler.getCollisionColor(new Vector2(Position.X, movement.Y), collisionHandler.Controller.CollisionColors, MovementOffset))
            {
                Position.Y = movement.Y;
            }
            if (Color.White == collisionHandler.getCollisionColor(new Vector2(movement.X, Position.Y), collisionHandler.Controller.CollisionColors, MovementOffset))
            {
                Position.X = movement.X;
            }

            //Collision with Items
            if (Color.White != collisionHandler.getCollisionColor(new Vector2(Position.X, Position.Y), collisionHandler.Controller.ItemColors, ItemOffset))
            {
                for(int i = 0; i < collisionHandler.Controller.items.Count; i++)
                {
                    Vector2 temp = new Vector2(collisionHandler.Controller.items.ElementAt(i).position.X, collisionHandler.Controller.items.ElementAt(i).position.Z);
                if (euclideanDistance(temp, new Vector2(Position.X, Position.Y)) <= 15)
                {
                    collisionHandler.Controller.items.RemoveAt(i);
                    Console.Out.WriteLine("+1 Point");
                }
                }

            }
            //update playerState
            if ((lastPos.X != movement.X || lastPos.Y != movement.Y) && stateTime <= 10)
            {
                playerState = PlayerState.Walking;
                stateTime = walkingDuration;
            }
            else if(Keyboard.GetState().IsKeyDown(Keys.Space) && stateTime <= 10)
            {
                playerState = PlayerState.Jumping;
                stateTime = jumpingDuration;
            }
            else if(lastPos.X == movement.X && lastPos.Y == movement.Y && stateTime <= 0)
            {
                playerState = PlayerState.Standing;
                stateTime = standingDuration;
            }
            stateTime -= gameTime.ElapsedGameTime.Milliseconds;

            Console.WriteLine("stateTime " + stateTime);
            
            //update only the animation which is required if the Playerstate changed        
            switch(playerState)
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
            }
            lastPlayerState = playerState;
        }

        public void Draw(Matrix world, Matrix view, Matrix projection)
        {
            // Bone updates for each required animation        
            switch (playerState)
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
            }

            foreach (ModelMesh mesh in playerModel.Meshes)
            {
                foreach (SkinnedEffect effect in mesh.Effects)
                {
                    
                    //Draw the Bones which are required
                    switch (playerState)
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
                    }
                    effect.DiffuseColor = new Vector3(255, 0, 0);

                    effect.World = Matrix.CreateRotationY(Angle) * Matrix.CreateTranslation(new Vector3(lastPos.X, 0, lastPos.Y)) * world;
                    effect.View = view;
                    effect.Projection = projection;

                    effect.EnableDefaultLighting();

                    effect.SpecularColor = new Vector3(0.25f);
                    effect.SpecularPower = 16;
                }
                mesh.Draw();
            }
        }


        private double euclideanDistance(Vector2 p1, Vector2 p2)
        {
            return Math.Sqrt(Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2));
        }

    }
}
