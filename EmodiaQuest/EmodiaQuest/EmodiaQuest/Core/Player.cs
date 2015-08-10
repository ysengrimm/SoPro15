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

        // FASTFIX
        private float attackTimer;
        private float attackThreshold;

        // For sound handling
        private bool HitEnemyWithSword;
        private bool HitAir;

        // For movement and camera update
        private Vector2 movement; // future position
        private Vector2 lastPos; //position from last step (fixes false kamera focus)
        public float MovementOffset, ItemOffset;
        public float Angle;

        // Eventhandler
        public event EmodiaQuest.Core.Delegates_CORE.ChangeValueDelegate OnChangeValue;

        // Collision
        public float CollisionRadius;
        private float gridSize = Settings.Instance.GridSize;

        // mouse click handling
        private MouseState lastMouseState;
        private MouseState currentMouseState;

        // Playerstats
        private float hp = 100;
        public float Hp
        {
            get { return hp; }
            set
            {
                hp = value;
                if (OnChangeValue != null)
                {
                    OnChangeValue(this, new ChangeValueEvent(hp, "hp"));
                }
            }
        }

        public int Level;
        public float Armor;
        public float Damage;
        public float PlayerSpeed;
        public float RotationSpeed;

        public int Gold;

        /**
         * Animation and Model
        **/
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
        public PlayerState ActivePlayerState = PlayerState.Standing;
        public PlayerState LastPlayerState = PlayerState.Standing;
        public PlayerState TempPlayerState = PlayerState.Standing;
        public Weapon ActiveWeapon = Weapon.Hammer;
        private WorldState activeWorld = WorldState.Safeworld;
        public float standingDuration;
        public float walkingDuration;
        public float jumpingDuration;
        public float swordFightingDuration;
        private float stateTime;
        private float fixedBlendDuration;
        // public for Netstat
        public float ActiveBlendTime;
        public bool IsBlending;

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

        private bool gameIsInFocus;
        public bool GameIsInFocus
        {
            set { gameIsInFocus= value; }
            get { return gameIsInFocus; }
        }

        private EnvironmentController gameEnv;
        public EnvironmentController GameEnv
        {
            set { gameEnv = value; }
        }

        private Player() { }

        public static Player Instance
        {
            get { return _instance ?? (_instance = new Player()); }
        }

        public void LoadContent()
        {
            HitEnemyWithSword = false;
            HitAir = false;
            // Initialize mouseState
            currentMouseState = Mouse.GetState();

            // Initialize player stuff
            PlayerSpeed = Settings.Instance.PlayerSpeed;
            RotationSpeed = Settings.Instance.PlayerRotationSpeed;

            // set defaults
            Hp = 100; // Settings.Instance.MaxPlayerHealth ?
            Armor = 0;
            Damage = 50;
            Level = 1;
            Gold = 100;

            MovementOffset = 2.0f;
            ItemOffset = 0.0f;
            Angle = 0;
            CollisionRadius = 1.5f;
            attackTimer = 0;

            /**
            * Animation and Model
            **/

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
            jumpingM = contentMngr.Load<Model>("fbxContent/player/Main_Char_swordFighting");
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
            jumpingC = jumpingSD.AnimationClips["swordFighting"];
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
            attackThreshold = swordFightingDuration;
            // Duration of Blending Animations in milliseconds
            fixedBlendDuration = 200;
        }

        public void Update(GameTime gameTime)
        {
            HitEnemyWithSword = false;
            HitAir = false;
            // update weapon
            if(Keyboard.GetState().IsKeyDown(Keys.D1))
            {
                ActiveWeapon = Weapon.Stock;
            }
            if(Keyboard.GetState().IsKeyDown(Keys.D2))
            {
                ActiveWeapon = Weapon.Hammer;
            }

            //Update interaction with NPCs
            foreach (NPC npc in SafeWorld.Instance.NPCList)
            {
                if (gameEnv.EuclideanDistance(npc.Position, Position) < 9f)
                {
                    //Console.WriteLine("You can interact with: " + npc.Name);
                    if (Keyboard.GetState().IsKeyDown(Keys.E))
                    {
                        QuestController.Instance.QuestUpdate(npc);
                    }
                }
            }

            lastMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();

            // only set fixed mouse pos if game is in focus
            if (gameIsInFocus)
            {
                //scale position to 0.0 to 1.0 then center the +/- change
                Angle += (float)-(((currentMouseState.X / windowSize.X) - 0.5) * RotationSpeed);
                // reset mouse position to window center
                Mouse.SetPosition((int)(windowSize.X / 2), (int)(windowSize.Y / 2));    
            }

            lastPos = Position;
            movement = Position;

            // running ;)
            if (Keyboard.GetState().IsKeyDown(Keys.LeftShift))
            {         
                if(PlayerSpeed < 0.6)
                {
                    PlayerSpeed += 0.025f;
                }
            }
            else
            {
                PlayerSpeed = Settings.Instance.PlayerSpeed;
            }
            
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
            if (Color.White == collisionHandler.GetCollisionColor(new Vector2(Position.X, movement.Y), collisionHandler.Controller.CollisionColors, MovementOffset))
            {
                position.Y = movement.Y;
            }
            if (Color.White == collisionHandler.GetCollisionColor(new Vector2(movement.X, Position.Y), collisionHandler.Controller.CollisionColors, MovementOffset))
            {
                position.X = movement.X;
            }

            // Running towards teleporters
            if (Color.Violet == collisionHandler.GetCollisionColor(new Vector2(Position.X, movement.Y), collisionHandler.Controller.CollisionColors, MovementOffset))
            {
                position.Y = movement.Y;
            }
            if (Color.Violet == collisionHandler.GetCollisionColor(new Vector2(movement.X, Position.Y), collisionHandler.Controller.CollisionColors, MovementOffset))
            {
                position.X = movement.X;
            }

            // Collsiion with Teleporters
            if (Color.Violet == collisionHandler.GetCollisionColor(new Vector2(Position.X, Position.Y), collisionHandler.Controller.CollisionColors, 0))
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
            if(collisionHandler.Controller.ItemColors != null) // because in a Dungeon we might not have an Itemmap
            {
                if (Color.White != collisionHandler.GetCollisionColor(new Vector2(Position.X, Position.Y), collisionHandler.Controller.ItemColors, ItemOffset))
                {
                    for (var i = 0; i < collisionHandler.Controller.Items.Count; i++)
                    {
                        var temp = new Vector2(collisionHandler.Controller.Items.ElementAt(i).position.X, collisionHandler.Controller.Items.ElementAt(i).position.Z);
                        if (gameEnv.EuclideanDistance(temp, new Vector2(Position.X, Position.Y)) <= 12)
                        {
                            collisionHandler.Controller.Items.RemoveAt(i);
                            //Console.Out.WriteLine("+1 Point");
                        }
                    }

                }
            }
            
            
            //update playerState
            if ( currentMouseState.LeftButton == ButtonState.Pressed && stateTime <= 500 && attackThreshold == swordFightingDuration)
            {
                ActivePlayerState = PlayerState.Swordfighting;
                stateTime = swordFightingDuration;
                fixedBlendDuration = 100;
                HitAir = true;
            }
            else if ((lastPos.X != movement.X || lastPos.Y != movement.Y) && Keyboard.GetState().IsKeyDown(Keys.Space) && stateTime <= 0)
            {
                ActivePlayerState = PlayerState.WalkJumping;
                fixedBlendDuration = 200;
            }
            else if ((lastPos.X != movement.X || lastPos.Y != movement.Y) && stateTime <= 200)
            {
                ActivePlayerState = PlayerState.Walking;
                stateTime = walkingDuration / 2;
                fixedBlendDuration = 200;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Space) && stateTime <= 200)
            {
                ActivePlayerState = PlayerState.Jumping;
                stateTime = jumpingDuration / 2;
                fixedBlendDuration = 200;
            }
            else if (lastPos.X == movement.X && lastPos.Y == movement.Y && stateTime <= 200)
            {
                ActivePlayerState = PlayerState.Standing;
                stateTime = standingDuration / 2;
                fixedBlendDuration = 200;
            }


            if(stateTime >0)
            {
                stateTime -= gameTime.ElapsedGameTime.Milliseconds;
            }


            //update only the animation which is required if the changed Playerstate
            //Update the active animation
            switch(ActivePlayerState)
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

            // Secure, that the last Playerstate is always a other Playerstate, than the acutal
            if(ActivePlayerState != TempPlayerState)
            {
                LastPlayerState = TempPlayerState;
                // When the playerState changes, we need to blend
                IsBlending = true;
                ActiveBlendTime = fixedBlendDuration;
            }


            // if the Time for blending is over, set it on false;
            if(ActiveBlendTime <= 0)
            {
                IsBlending = false;
                if(ActiveBlendTime < 0)
                {
                    ActiveBlendTime = 0;
                }
            }
            else
            {
                // update the blendDuration
                ActiveBlendTime -= gameTime.ElapsedGameTime.Milliseconds;
            }

            // Update the last animation (only 500 milliseconds after the last changing state required)
            if (IsBlending)
            {
                switch (LastPlayerState)
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
            }


            //Update Temp PlayerState
            TempPlayerState = ActivePlayerState;

            // interaction
            Vector2 frontDirection = new Vector2((float) Math.Round(Math.Sin(Angle)), (float) Math.Round(Math.Cos(Angle)));
            Vector2 gridPosInView = new Vector2((float)(Math.Round(Position.X / gridSize) + frontDirection.X), (float)(Math.Round(Position.Y / gridSize) + frontDirection.Y));

            // interaction checks happen only if interactable object is in view (eg no killing behind back anymore)
            // only == 2 in edges, else normal 3 in a row
            if( ActivePlayerState == PlayerState.Swordfighting && !IsBlending)
            {
                position.X = lastPos.X;
                position.Y = lastPos.Y;
            }
            if(Ingame.Instance.ActiveWorld == WorldState.Dungeon)
            {
                attackTimer += gameTime.ElapsedGameTime.Milliseconds;
                // collision with enemies
                Vector2 currentGridPos = new Vector2((float)Math.Round(position.X / gridSize), (float)Math.Round(position.Y / gridSize));
                for (int i = -1; i < 2; i++)
                {
                    for (int j = -1; j < 2; j++)
                    {
                        List<Enemy> currentBlockEnemyList = gameEnv.enemyArray[(int)currentGridPos.X + i, (int)currentGridPos.Y + j];
                        if (currentBlockEnemyList.Count <= 0) continue;
                        foreach (var enemy in currentBlockEnemyList)
                        {
                            var dx = (Position.X - enemy.Position.X) * (Position.X - enemy.Position.X);
                            var dy = (Position.Y - enemy.Position.Y) * (Position.Y - enemy.Position.Y);
                            if (Math.Sqrt(dx + dy) < (CollisionRadius + enemy.CircleCollision))
                            {
                                position.X = lastPos.X;
                                position.Y = lastPos.Y;
                            }
                        }
                    }
                }
                if (ActivePlayerState == PlayerState.Swordfighting)
                {
                    // interaction checks
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
                                        if (attackTimer >= attackThreshold && lastMouseState.LeftButton == ButtonState.Released && currentMouseState.LeftButton == ButtonState.Pressed)
                                        {
                                            currentClosestEnemy.Attack(Damage);
                                            attackTimer = 0;
                                            HitEnemyWithSword = true;
                                            HitAir = false;
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
                                        if (attackTimer >= attackThreshold && lastMouseState.LeftButton == ButtonState.Released && currentMouseState.LeftButton == ButtonState.Pressed)
                                        {
                                            currentClosestEnemy.Attack(Damage);
                                            attackTimer = 0;
                                            HitEnemyWithSword = true;
                                            HitAir = false;
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
                                        if (attackTimer >= attackThreshold && lastMouseState.LeftButton == ButtonState.Released && currentMouseState.LeftButton == ButtonState.Pressed)
                                        {
                                            currentClosestEnemy.Attack(Damage);
                                            attackTimer = 0;
                                            HitEnemyWithSword = true;
                                            HitAir = false;
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
                                        if (attackTimer >= attackThreshold && lastMouseState.LeftButton == ButtonState.Released && currentMouseState.LeftButton == ButtonState.Pressed)
                                        {
                                            currentClosestEnemy.Attack(Damage);
                                            attackTimer = 0;
                                            HitEnemyWithSword = true;
                                            HitAir = false;
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
                                    if (attackTimer >= attackThreshold && lastMouseState.LeftButton == ButtonState.Released && currentMouseState.LeftButton == ButtonState.Pressed)
                                    {
                                        currentClosestEnemy.Attack(Damage);
                                        attackTimer = 0;
                                        HitEnemyWithSword = true;
                                        HitAir = false;
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
                                    if (attackTimer >= attackThreshold && lastMouseState.LeftButton == ButtonState.Released && currentMouseState.LeftButton == ButtonState.Pressed)
                                    {
                                        currentClosestEnemy.Attack(Damage);
                                        attackTimer = 0;
                                        HitEnemyWithSword = true;
                                        HitAir = false;
                                    }
                                }
                            }
                        }

                    }
                }
                }
                

            // Call Sounds

            if(HitEnemyWithSword)
            {
                Jukebox.Instance.PlaySwordFightSound();
            }
            if(!HitEnemyWithSword && HitAir)
            {
                Jukebox.Instance.PlayAudioMouseFeedback();
            }

            // Get Keyboard input to change overall GameState
            if(EmodiaQuest.Core.GUI.Controls_GUI.Instance.keyClicked(Keys.I))
                EmodiaQuest_Game.Gamestate_Game = GameStates_Overall.InventoryScreen;
            if (EmodiaQuest.Core.GUI.Controls_GUI.Instance.keyClicked(Keys.O))
                EmodiaQuest_Game.Gamestate_Game = GameStates_Overall.OptionsScreen;
        }

        private Enemy getClosestMonster(List<Enemy> enemyList)
        {
            float currentCosest = float.MaxValue;
            Enemy closestEnemy = null;
            foreach (var enemy in enemyList)
            {
                float dist = (float) gameEnv.EuclideanDistance(Position, enemy.Position);
                if (dist < currentCosest)
                {
                    currentCosest = dist;
                    closestEnemy = enemy;
                }
            }
            return closestEnemy;
        }

        public void Attack(float damage)
        {
            Hp -= damage;
            if (Hp <= 0)
            {
                Hp = 100;
            }
        }

        public void Draw(Matrix world, Matrix view, Matrix projection)
        {
            // Bone updates for each required animation   
            switch (ActivePlayerState)
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
                    break;
            }

            if (IsBlending)
            {
                // Bone updates for each required animation for blending 
                switch (LastPlayerState)
                {
                    case PlayerState.Standing:
                        blendingBones = standingAP.GetSkinTransforms();
                        break;
                    case PlayerState.Walking:
                        blendingBones = walkingAP.GetSkinTransforms();
                        break;
                    case PlayerState.Swordfighting:
                        blendingBones = swordfightingAP.GetSkinTransforms();
                        break;
                    case PlayerState.Jumping:
                        blendingBones = jumpingAP.GetSkinTransforms();
                        break;
                    case PlayerState.WalkJumping:
                        blendingBones = walkingAP.GetSkinTransforms();
                        break;
                }
            }

            foreach (ModelMesh mesh in playerModel.Meshes)
            {
                //Console.WriteLine("Meshes:" + mesh.MeshParts.Count);
                foreach (SkinnedEffect effect in mesh.Effects)
                {
                    //Console.WriteLine("Effects:" + mesh.Effects.Count);
                    //Draw the Bones which are required
                    
                    if (IsBlending)
                    {
                        float percentageOfBlending = ActiveBlendTime / fixedBlendDuration;
                        switch (ActivePlayerState)
                        {
                            case PlayerState.Standing:
                                for (int i = 0; i < blendingBones.Length; i++)
                                {
                                    blendingBones[i] = Matrix.Lerp(blendingBones[i], standingBones[i], 1-percentageOfBlending);
                                }
                                effect.SetBoneTransforms(blendingBones);                             
                                break;
                            case PlayerState.Walking:
                                for (int i = 0; i < blendingBones.Length; i++)
                                {
                                    blendingBones[i] = Matrix.Lerp(blendingBones[i], walkingBones[i], 1-percentageOfBlending);
                                }
                                effect.SetBoneTransforms(blendingBones);
                                break;
                            case PlayerState.Swordfighting:
                                for (int i = 0; i < blendingBones.Length; i++)
                                {
                                    blendingBones[i] = Matrix.Lerp(blendingBones[i], swordfightingBones[i], 1-percentageOfBlending);
                                }
                                effect.SetBoneTransforms(blendingBones);
                                break;
                            case PlayerState.Jumping:
                                for (int i = 0; i < blendingBones.Length; i++)
                                {
                                    blendingBones[i] = Matrix.Lerp(blendingBones[i], jumpingBones[i], 1-percentageOfBlending);
                                }
                                effect.SetBoneTransforms(blendingBones);
                                break;
                            case PlayerState.WalkJumping:
                                for (int i = 0; i < blendingBones.Length; i++)
                                {
                                    blendingBones[i] = Matrix.Lerp(blendingBones[i], walkingBones[i], 1-percentageOfBlending);
                                }
                                effect.SetBoneTransforms(blendingBones);
                                break;
                        }
                    }
                    else
                    {
                        switch (ActivePlayerState)
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
                                effect.SetBoneTransforms(walkingBones);
                                break;
                        }
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
                //Console.WriteLine(mesh.Name);
                if (mesh.Name == "Stock" && ActiveWeapon != Weapon.Stock)
                {

                }
                else if (mesh.Name == "Hammer" && ActiveWeapon != Weapon.Hammer)
                {

                }
                else mesh.Draw();
            }
        }
    }
}
