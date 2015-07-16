using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using EmodiaQuest.Core.NPCs;
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

        //Textures for the Model
        private Texture2D defaultBodyTex;
        private Texture2D defaultHairTex;

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
        private WorldState activeWorld = WorldState.Safeworld;
        private float standingDuration;
        private float walkingDuration;
        private float jumpingDuration;
        private float swordFightingDuration;
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

        private bool gameIsInFocus = false;
        public bool GameIsInFocus
        {
            set { gameIsInFocus= value; }
        }

        private EnvironmentController gameEnv;
        public EnvironmentController GameEnv
        {
            set { gameEnv = value; }
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

            //loading Textures
            defaultBodyTex = contentMngr.Load<Texture2D>("Texturen/Playertexturen/young_lightskinned_male_diffuse");
            defaultHairTex = contentMngr.Load<Texture2D>("Texturen/Playertexturen/male02_diffuse_black");
            //loading default mesh
            playerModel = contentMngr.Load<Model>("fbxContent/player/Main_Char_t_pose");
            //playerModel = contentMngr.Load<Model>("fbxContent/testPlayerv1");

            //loading Animation Models
            standingM = contentMngr.Load<Model>("fbxContent/player/Main_Char_idle_stand");
            //standingM = contentMngr.Load<Model>("fbxContent/testPlayerv1_Stand");
            walkingM = contentMngr.Load<Model>("fbxContent/player/Main_Char_walk");
            //walkingM = contentMngr.Load<Model>("fbxContent/testPlayerv1_Run");
            jumpingM = contentMngr.Load<Model>("fbxContent/player/Main_Char_walk");
            //jumpingM = contentMngr.Load<Model>("fbxContent/testPlayerv1_Jump");
            swordfightingM = contentMngr.Load<Model>("fbxContent/player/Main_Char_swordFighting");

            //Loading Skinning Data
            standingSD = standingM.Tag as SkinningData;
            walkingSD = walkingM.Tag as SkinningData;
            jumpingSD = jumpingM.Tag as SkinningData;
            swordfightingSD = swordfightingM.Tag as SkinningData;

            //Load an animation Player for each animation
            standingAP = new AnimationPlayer(standingSD);
            walkingAP = new AnimationPlayer(walkingSD);
            jumpingAP = new AnimationPlayer(jumpingSD);
            swordfightingAP = new AnimationPlayer(swordfightingSD);

            //loading Animation
            /*
            standingC = standingSD.AnimationClips["Stand"];
            walkingC = walkingSD.AnimationClips["Run"];
            jumpingC = jumpingSD.AnimationClips["Jump"];
            */
            
            standingC = standingSD.AnimationClips["idle_stand"];
            walkingC = walkingSD.AnimationClips["walk"];
            jumpingC = jumpingSD.AnimationClips["walk"];
            swordfightingC = swordfightingSD.AnimationClips["swordFighting"];
            
            //Safty Start Animations
            standingAP.StartClip(standingC);
            walkingAP.StartClip(walkingC);
            jumpingAP.StartClip(jumpingC);
            swordfightingAP.StartClip(swordfightingC);

            //assign the specific animationTimes
            standingDuration = standingC.Duration.Milliseconds/1f;
            walkingDuration = walkingC.Duration.Milliseconds/1f;
            jumpingDuration = jumpingC.Duration.Milliseconds/1f;
            swordFightingDuration = swordfightingC.Duration.Milliseconds/1f;
            /*
            Console.WriteLine("StandingDuration: " + standingC.Duration.TotalMilliseconds);
            Console.WriteLine("StandingKeyframes: " + standingC.Keyframes.Count);
            Console.WriteLine("WalkingDuration: " + walkingC.Duration.TotalMilliseconds);
            Console.WriteLine("WalkingKeyframes: " + walkingC.Keyframes.Count);
            */
            stateTime = 0;

        }

        public void Update(GameTime gameTime, MouseState mouseState)
        {
            // only set fixed mouse pos if game is in focus
            if (gameIsInFocus)
            {
                //scale position to 0.0 to 1.0 then center the +/- change
                Angle += (float)-(((mouseState.X / windowSize.X) - 0.5) * RotationSpeed);
                // reset mouse position to window center
                Mouse.SetPosition((int)(windowSize.X / 2), (int)(windowSize.Y / 2));    
            }

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
            
            // Collision with Walls
            if (Color.White == collisionHandler.GetCollisionColor(new Vector2(Position.X, movement.Y), collisionHandler.Controller.Collisionmap, MovementOffset))
            {
                position.Y = movement.Y;
            }
            if (Color.White == collisionHandler.GetCollisionColor(new Vector2(movement.X, Position.Y), collisionHandler.Controller.Collisionmap, MovementOffset))
            {
                position.X = movement.X;
            }

            // Running towards teleporters
            if (Color.Violet == collisionHandler.GetCollisionColor(new Vector2(Position.X, movement.Y), collisionHandler.Controller.Collisionmap, MovementOffset))
            {
                position.Y = movement.Y;
            }
            if (Color.Violet == collisionHandler.GetCollisionColor(new Vector2(movement.X, Position.Y), collisionHandler.Controller.Collisionmap, MovementOffset))
            {
                position.X = movement.X;
            }

            // Collsiion with Teleporters
            if (Color.Violet == collisionHandler.GetCollisionColor(new Vector2(Position.X, Position.Y), collisionHandler.Controller.Collisionmap, 0))
            {
                
                if (activeWorld == WorldState.Safeworld)
                {
                    Console.WriteLine("You get teleported to a Dungeon!");
                    Ingame.Instance.ChangeToDungeon();
                    activeWorld = WorldState.Dungeon;
                }
                else if (activeWorld == WorldState.Dungeon)
                {
                    Console.WriteLine("You get teleported to the SafeWorld!");
                    Ingame.Instance.ChangeToSafeworld();
                    activeWorld = WorldState.Safeworld;
                }
            }
            
            //Collision with Items
            if(collisionHandler.Controller.Itemmap != null) // this is, because in a Dungeon we might don´t have a Itemmap
            {
                if (Color.White != collisionHandler.GetCollisionColor(new Vector2(Position.X, Position.Y), collisionHandler.Controller.Itemmap, ItemOffset))
                {
                    for (var i = 0; i < collisionHandler.Controller.Items.Count; i++)
                    {
                        var temp = new Vector2(collisionHandler.Controller.Items.ElementAt(i).position.X, collisionHandler.Controller.Items.ElementAt(i).position.Z);
                        if (EuclideanDistance(temp, new Vector2(Position.X, Position.Y)) <= 12)
                        {
                            collisionHandler.Controller.Items.RemoveAt(i);
                            //Console.Out.WriteLine("+1 Point");
                        }
                    }

                }
            }
            
            
            //update playerState
            if ( mouseState.LeftButton == ButtonState.Pressed)
            {
                PlayerState = PlayerState.Swordfighting;
                stateTime = swordFightingDuration;
            }
            else if ((lastPos.X != movement.X || lastPos.Y != movement.Y) && stateTime <= 10 && Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                PlayerState = PlayerState.WalkJumping;
                stateTime = standingDuration;
            }
            else if ((lastPos.X != movement.X || lastPos.Y != movement.Y) && stateTime <= 10)
            {
                PlayerState = PlayerState.Walking;
                stateTime = walkingDuration/2f;
            }
            else if(Keyboard.GetState().IsKeyDown(Keys.Space) && stateTime <= 10)
            {
                PlayerState = PlayerState.Jumping;
                stateTime = jumpingDuration;
            }
            else if(lastPos.X == movement.X && lastPos.Y == movement.Y && stateTime <= 0)
            {
                PlayerState = PlayerState.Standing;
                stateTime = standingDuration;
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
                case PlayerState.Swordfighting:
                    swordfightingAP.Update(gameTime.ElapsedGameTime, true, Matrix.Identity);
                    break;
                case PlayerState.WalkJumping:
                    walkingAP.Update(gameTime.ElapsedGameTime, true, Matrix.Identity);
                    jumpingAP.Update(gameTime.ElapsedGameTime, true, Matrix.Identity);
                    break;
            }

            LastPlayerState = PlayerState;

            // interaction
            int gridBlockSize = 10;
            Vector2 frontDirection = new Vector2((float) Math.Round(Math.Sin(Angle)), (float) Math.Round(Math.Cos(Angle)));
            Vector2 gridPosInView = new Vector2((float)(Math.Round(Position.X / gridBlockSize) + frontDirection.X), (float)(Math.Round(Position.Y / gridBlockSize) + frontDirection.Y));

            // interaction checks happen only if interactable object is in view (eg no killing behind back anymore)
            // only == 2 in edges, else normal 3 in a row
            if(Ingame.Instance.ActiveWorld == WorldState.Dungeon)
            {
                if (Math.Abs(frontDirection.X) + Math.Abs(frontDirection.Y) >= 2)
                {
                    // top left 
                    if ((int)frontDirection.X == -1 && (int)frontDirection.Y == -1)
                    {
                        for (int i = 0; i < 2; i++)
                        {
                            for (int j = 0; j < 2; j++)
                            {
                                List<Enemy> currentBlockEnemyList = gameEnv.enemyArray[(int)gridPosInView.X + i, (int)gridPosInView.Y + j];
                                if (currentBlockEnemyList.Count > 0)
                                {
                                    Enemy currentClosestEnemy = getClosestMonster(currentBlockEnemyList);
                                    if (mouseState.LeftButton == ButtonState.Pressed)
                                    {
                                        currentClosestEnemy.SetAsDead();
                                    }
                                }
                            }
                        }
                    } // top right
                    else if ((int)frontDirection.X == 1 && (int)frontDirection.Y == -1)
                    {
                        for (int i = -1; i < 1; i++)
                        {
                            for (int j = 0; j < 2; j++)
                            {
                                List<Enemy> currentBlockEnemyList = gameEnv.enemyArray[(int)gridPosInView.X + i, (int)gridPosInView.Y + j];
                                if (currentBlockEnemyList.Count > 0)
                                {
                                    Enemy currentClosestEnemy = getClosestMonster(currentBlockEnemyList);
                                    if (mouseState.LeftButton == ButtonState.Pressed)
                                    {
                                        currentClosestEnemy.SetAsDead();
                                    }
                                }
                            }
                        }
                    } // bot left
                    else if ((int)frontDirection.X == -1 && (int)frontDirection.Y == 1)
                    {
                        for (int i = 0; i < 2; i++)
                        {
                            for (int j = -1; j < 1; j++)
                            {
                                List<Enemy> currentBlockEnemyList = gameEnv.enemyArray[(int)gridPosInView.X + i, (int)gridPosInView.Y + j];
                                if (currentBlockEnemyList.Count > 0)
                                {
                                    Enemy currentClosestEnemy = getClosestMonster(currentBlockEnemyList);
                                    if (mouseState.LeftButton == ButtonState.Pressed)
                                    {
                                        currentClosestEnemy.SetAsDead();
                                    }
                                }
                            }
                        }
                    } // bot right (X = 1, Y = 1)
                    else
                    {
                        for (int i = -1; i < 1; i++)
                        {
                            for (int j = -1; j < 1; j++)
                            {
                                List<Enemy> currentBlockEnemyList = gameEnv.enemyArray[(int)gridPosInView.X + i, (int)gridPosInView.Y + j];
                                if (currentBlockEnemyList.Count > 0)
                                {
                                    Enemy currentClosestEnemy = getClosestMonster(currentBlockEnemyList);
                                    if (mouseState.LeftButton == ButtonState.Pressed)
                                    {
                                        currentClosestEnemy.SetAsDead();
                                    }
                                }
                            }
                        }
                    }

                }
                else
                {
                    // left or right
                    if ((int)Math.Abs(frontDirection.X) == 1 && (int)frontDirection.Y == 0)
                    {
                        for (int j = -1; j < 2; j++)
                        {
                            List<Enemy> currentBlockEnemyList = gameEnv.enemyArray[(int)gridPosInView.X, (int)gridPosInView.Y + j];
                            if (currentBlockEnemyList.Count > 0)
                            {
                                Enemy currentClosestEnemy = getClosestMonster(currentBlockEnemyList);
                                if (mouseState.LeftButton == ButtonState.Pressed)
                                {
                                    currentClosestEnemy.SetAsDead();
                                }
                            }
                        }
                    }
                    else // top or bottom
                    {
                        for (int j = -1; j < 2; j++)
                        {
                            List<Enemy> currentBlockEnemyList = gameEnv.enemyArray[(int)gridPosInView.X + j, (int)gridPosInView.Y];
                            if (currentBlockEnemyList.Count > 0)
                            {
                                Enemy currentClosestEnemy = getClosestMonster(currentBlockEnemyList);
                                if (mouseState.LeftButton == ButtonState.Pressed)
                                {
                                    currentClosestEnemy.SetAsDead();
                                }
                            }
                        }
                    }

                }
            }
            
        }

        private Enemy getClosestMonster(List<Enemy> enemyList)
        {
            float currentCosest = float.MaxValue;
            Enemy closestEnemy = null;
            foreach (var enemy in enemyList)
            {
                float dist = (float) EuclideanDistance(Position, enemy.Position);
                if (dist < currentCosest)
                {
                    currentCosest = dist;
                    closestEnemy = enemy;
                }
            }
            return closestEnemy;
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
                case PlayerState.Swordfighting:
                    swordfightingBones = swordfightingAP.GetSkinTransforms();
                    break;
                case PlayerState.Jumping:
                    jumpingBones = jumpingAP.GetSkinTransforms();
                    break;
                case PlayerState.WalkJumping:
                    walkingBones = walkingAP.GetSkinTransforms();
                    jumpingBones = jumpingAP.GetSkinTransforms();
                    break;
            }

            foreach (ModelMesh mesh in playerModel.Meshes)
            {
                //Console.WriteLine("Meshes:" + mesh.MeshParts.Count);
                foreach (SkinnedEffect effect in mesh.Effects)
                {
                    //Console.WriteLine("Effects:" + mesh.Effects.Count);
                    //Draw the Bones which are required
                    
                    switch (PlayerState)
                    {
                        case PlayerState.Standing:
                            effect.SetBoneTransforms(standingBones);
                            break;
                        case PlayerState.Walking:
                            effect.SetBoneTransforms(walkingBones);
                            break;
                        case PlayerState.Swordfighting:
                            effect.SetBoneTransforms(swordfightingBones);
                            break;
                        case PlayerState.Jumping:
                            effect.SetBoneTransforms(jumpingBones);
                            break;
                        case PlayerState.WalkJumping:                          
                            blendingBones = walkingBones;
                            for (int i = 0; i < walkingBones.Length; i++)
                            {
                                blendingBones[i] = Matrix.Multiply(walkingBones[i], jumpingBones[i]);
                            }
                            effect.SetBoneTransforms(blendingBones);                           
                            break;
                    }

                    effect.EnableDefaultLighting();
                    effect.World = Matrix.CreateRotationX((float)(-0.5*Math.PI)) * Matrix.CreateRotationY(Angle)  * Matrix.CreateTranslation(new Vector3(lastPos.X, 0, lastPos.Y)) * world;
                    //effect.World = Matrix.CreateRotationX((float)(-0.5 * Math.PI)) * Matrix.CreateRotationY(0) * Matrix.CreateTranslation(new Vector3(lastPos.X, 0, lastPos.Y)) * world;
                    effect.View = view;
                    effect.Projection = projection;

                   effect.SpecularColor = new Vector3(0.25f);
                   effect.SpecularPower = 16;
                   effect.PreferPerPixelLighting = true;

                    // Textures
                   if (mesh.Name == "MainChar_body")
                   {
                       effect.Texture = defaultBodyTex;
                   }
                   else if(mesh.Name == "MainChar_hair")
                   {
                       effect.Texture = defaultHairTex;
                   }
                   else
                   {
                       effect.Texture = defaultBodyTex;
                   }

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
