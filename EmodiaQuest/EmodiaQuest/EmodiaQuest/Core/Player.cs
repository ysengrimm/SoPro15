﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using EmodiaQuest.Core.Items;
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

        private Random rnd = new Random();

        // time for attacking
        private float attackTimer;
        private float attackThreshold;

        // shooting type
        public enum Shootingtype { Normal, Blast, Lightning };
        private Shootingtype shootingtype = Shootingtype.Normal;
        // time for shooting
        private float shootingTimer = 0;
        private float shootingThreshold;

        // For sound handling
        private bool HitEnemyWithSword;
        private bool HitAir;

        // Effects
        public Effect copiedEffect;

        // For movement and camera update
        private Vector2 movement; // future position
        private Vector2 lastPos; //position from last step (fixes false kamera focus)
        public float MovementOffset, ItemOffset;
        public float Angle;

        // Eventhandler
        public event Delegates_CORE.ChangeValueDelegate OnChangeValue;

        // Collision
        public float CollisionRadius;
        private float gridSize = Settings.Instance.GridSize;

        // mouse click handling
        private MouseState lastMouseState;
        private MouseState currentMouseState;

        // Playerstats
        private int hp = 100;
        public int Hp
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

        private int maxHp;
        public int MaxHp
        {
            get { return maxHp; }
            set
            {
                maxHp = value;
                if (OnChangeValue != null)
                {
                    OnChangeValue(this, new ChangeValueEvent(maxHp, "max_hp"));
                }
            }
        }

        private int hpRegen;
        public int HpRegen
        {
            get { return hpRegen; }
            set
            {
                hpRegen = value;
                if (OnChangeValue != null)
                {
                    OnChangeValue(this, new ChangeValueEvent(hpRegen, "hp_regen"));
                }
            }
        } 

        private int focus;
        public int Focus
        {
            get { return focus; }
            set
            {
                focus = value;
                if (OnChangeValue != null)
                {
                    OnChangeValue(this, new ChangeValueEvent(focus, "focus"));
                }
            }
        }

        private int maxFocus;
        public int MaxFocus
        {
            get { return maxFocus; }
            set
            {
                maxFocus = value;
                if (OnChangeValue != null)
                {
                    OnChangeValue(this, new ChangeValueEvent(maxFocus, "max_focus"));
                }
            }
        }

        private int focusRegen;
        public int FocusRegen
        {
            get { return focusRegen; }
            set
            {
                focusRegen = value;
                if (OnChangeValue != null)
                {
                    OnChangeValue(this, new ChangeValueEvent(focusRegen, "focus_regen"));
                }
            }
        } 

        public float PlayerSpeed;
        public float RotationSpeed;

        // Player Stats
        private int armor;
        public int Armor
        {
            get { return armor; }
            set
            {
                armor = value;
                if (OnChangeValue != null)
                {
                    OnChangeValue(this, new ChangeValueEvent(armor, "armor"));
                }
            }
        }

        private int minDamage;
        public int MinDamage
        {
            get { return minDamage; }
            set
            {
                minDamage = value;
                if (OnChangeValue != null)
                {
                    OnChangeValue(this, new ChangeValueEvent(minDamage, "dmg"));
                }
            }
        }

        private int maxDamage;
        public int MaxDamage
        {
            get { return maxDamage; }
            set
            {
                maxDamage = value;
                if (OnChangeValue != null)
                {
                    OnChangeValue(this, new ChangeValueEvent(maxDamage, "dmg"));
                }
            }
        }

        private int strength;
        public int Strength
        {
            get { return strength; }
            set
            {
                strength = value;
                if (OnChangeValue != null)
                {
                    OnChangeValue(this, new ChangeValueEvent(strength, "strength"));
                }
            }
        }

        private int skill;
        public int Skill
        {
            get { return skill; }
            set
            {
                skill = value;
                if (OnChangeValue != null)
                {
                    OnChangeValue(this, new ChangeValueEvent(skill, "skill"));
                }
            }
        }

        private int intelligence;
        public int Intelligence
        {
            get { return intelligence; }
            set
            {
                intelligence = value;
                if (OnChangeValue != null)
                {
                    OnChangeValue(this, new ChangeValueEvent(intelligence, "intelligence"));
                }
            }
        }

        private int strengthGaidedThrougLvls;
        private int skillGainedThroughLvls;
        private int intellGainedThroughLvls;

        private int gold;
        public int Gold
        {
            get { return gold; }
            set
            {
                gold = value;
                if (OnChangeValue != null)
                {
                    OnChangeValue(this, new ChangeValueEvent(gold, "gold"));
                }
            }
        }

        private int level;
        public int Level
        {
            get { return level; }
            set
            {
                level = value;
                if (OnChangeValue != null)
                {
                    OnChangeValue(this, new ChangeValueEvent(level, "level"));
                }
            }
        }

        private int experience;
        public int Experience
        {
            get { return experience; }
            set
            {
                experience = value;
                if (OnChangeValue != null)
                {
                    OnChangeValue(this, new ChangeValueEvent(experience, "xp"));
                }
            }
        }

        private int xpToNextLevel;
        public int XPToNextLevel
        {
            get { return xpToNextLevel; }
            set
            {
                xpToNextLevel = value;
                if (OnChangeValue != null)
                {
                    OnChangeValue(this, new ChangeValueEvent(xpToNextLevel, "xp_next_lvl"));
                }
            }
        }
        private int xpScaleFactor = 100;

        // inventory
        public List<Item> PlayerInventory { get; set; }

        public Item CurrentEquippedHelmet;
        public Item CurrentEquippedArmor;
        public Item CurrentEquippedBoots;
        public Item CurrentEquippedWeapon;

        private Item oldEquippedHelmet;
        private Item oldEquippedArmor;
        private Item oldEquippedBoots;
        private Item oldEquippedWeapon;

        private Quest activeQuest = new Quest() {Name = "", Description = ""};
        public Quest ActiveQuest
        {
            get { return activeQuest; }
            set
            {
                activeQuest = value;
                if (OnChangeValue != null)
                {
                    OnChangeValue(this, new ChangeValueEvent(0, "quest"));
                }
            }
        }
        private Quest oldQuest;

        /**
         * Animation and Model
        **/
        //Textures for the Model
        private Texture2D defaultBodyTex;
        private Texture2D defaultHairTex;

        // bulletModel
        private Model bulletModel;

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
        public List<Bullet> BulletList = new List<Bullet>();

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
            get { return contentMngr; }
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
            get { return collisionHandler; }
        }

        private bool gameIsInFocus;
        public bool GameIsInFocus
        {
            set { gameIsInFocus = value; }
            get { return gameIsInFocus; }
        }

        private EnvironmentController gameEnv;
        public EnvironmentController GameEnv
        {
            get { return gameEnv; }
            set { gameEnv = value; }
        }

        private Player() { }

        public static Player Instance
        {
            get { return _instance ?? (_instance = new Player()); }
        }

        public void LoadContent()
        {
            // Effects
            copiedEffect = contentMngr.Load<Effect>("shaders/simples/Copied");

            HitEnemyWithSword = false;
            HitAir = false;
            // Initialize mouseState
            currentMouseState = Mouse.GetState();

            // Initialize player stuff
            PlayerSpeed = Settings.Instance.PlayerSpeed;
            RotationSpeed = Settings.Instance.PlayerRotationSpeed;

            // set defaults
            // Max HP is XP for next lvl + 10% strength with base 100
            MaxHp = (int)((100 * Math.Sqrt(Level)) < 100 ? 100 : (100 * Math.Sqrt(Level)) + Math.Round(Strength / 10.0));
            Hp = MaxHp;
            HpRegen = 10;

            // Max Focus is 100 + 10% intel
            MaxFocus = (int)(100 + Math.Round(Intelligence / 10.0));
            Focus = MaxFocus;
            FocusRegen = (int) Math.Round(Intelligence/50.0);

            Armor = 0;
            MinDamage = 0;
            MaxDamage = 0;
            Gold = 0;

            Level = 1;
            Experience = 0;
            XPToNextLevel = 100;

            PlayerInventory = new List<Item>();

            CurrentEquippedHelmet = new Item(ItemClass.Helmet, "InitHelmet");
            CurrentEquippedArmor = new Item(0, ItemClass.Armor,0,0,0,10,10);
            CurrentEquippedBoots = new Item(ItemClass.Boots, "InitBoots");
            CurrentEquippedWeapon = new Item(0, ItemClass.Weapon, 0, 5, 7, 0, 0);

            ActiveQuest = new Quest {Name = "", Description = ""};

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
            standingDuration = standingC.Duration.Milliseconds / 1f;
            walkingDuration = walkingC.Duration.Milliseconds / 1f;
            jumpingDuration = jumpingC.Duration.Milliseconds / 1f;
            swordFightingDuration = swordfightingC.Duration.Milliseconds / 1f;
            /*
            Console.WriteLine("StandingDuration: " + standingC.Duration.TotalMilliseconds);
            Console.WriteLine("StandingKeyframes: " + standingC.Keyframes.Count);
            Console.WriteLine("WalkingDuration: " + walkingC.Duration.TotalMilliseconds);
            Console.WriteLine("WalkingKeyframes: " + walkingC.Keyframes.Count);
            */
            stateTime = 0;
            attackThreshold = swordFightingDuration;
            shootingThreshold = swordFightingDuration;
            // Duration of Blending Animations in milliseconds
            fixedBlendDuration = 200;

            // Load Bullet
            bulletModel = contentMngr.Load<Model>("fbxContent/items/Point");
        }

        //mystuff
        private double DegreeToRadian(double angle)
        {
            return Math.Abs(Angle) / Math.PI * 180;
        }

        private double Kosinussatz (Vector2 npcPos, Vector2 viewPos)
        {
            double a = Math.Sqrt(Math.Pow(Position.X - viewPos.X, 2) + Math.Pow(Position.Y - viewPos.Y, 2));
            double b = Math.Sqrt(Math.Pow(viewPos.X - npcPos.X, 2) + Math.Pow(viewPos.Y - npcPos.Y, 2));
            double c = Math.Sqrt(Math.Pow(Position.X - npcPos.X, 2) + Math.Pow(Position.Y - npcPos.Y, 2));
            double e = (Math.Pow(a, 2) + Math.Pow(c, 2) - Math.Pow(b, 2))/(2*a*c);
            return Math.Acos(e); ;
        }

        public void Update(GameTime gameTime)
        {
            HitEnemyWithSword = false;
            HitAir = false;
            // update weapon
            if (Keyboard.GetState().IsKeyDown(Keys.D1))
            {
                ActiveWeapon = Weapon.Stock;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D2))
            {
                ActiveWeapon = Weapon.Hammer;
            }

            //Update interaction with NPCs
            if(activeWorld == WorldState.Safeworld)
            {
                foreach (NPC npc in SafeWorld.Instance.NPCList)
                {
                    if (gameEnv.EuclideanDistance(npc.Position, Position) < 15f)
                    {
                        //mystuff <-- why, just why?
                        //double a = Kosinussatz(npc.Position, v);
                        //Console.WriteLine(DegreeToRadian(a) - DegreeToRadian(Angle));

                        //Vector2 view = new Vector2((float)Math.Sin(Angle), (float)Math.Cos(Angle));
                        //view.Normalize();

                        //Vector2 circlePos = Position + view*3;
                        //if (gameEnv.EuclideanDistance(circlePos, npc.Position) < 1f)
                        //    Console.WriteLine("Kreis");
                        //else
                        //    Console.WriteLine("Keinkreis");

                        // solve quests when near the quest provider
                        foreach (Quest sQuest in QuestController.Instance.GetAllAvailableSolvedQuests(npc))
                        {
                            if (QuestController.Instance.SolveQuest(sQuest.Name))
                            {
                                Console.WriteLine("You solved the Quest " + sQuest.Name);
                            }
                            else
                            {
                                //Console.WriteLine("There is a unsolved quest to be solved, something is wrong!");
                            }
                        }
                        
                        if (GUI.Controls_GUI.Instance.keyClicked(Keys.E))
                        {

                            GUI.Screens.NPCTalk_GUI.Instance.NPCName = npc;
                            //Console.WriteLine(EmodiaQuest.Core.GUI.Screens.NPCTalk_GUI.Instance.NPCName);
                            EmodiaQuest_Game.Gamestate_Game = GameStates_Overall.NPCScreen;
                        }
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
                if (Angle > Math.PI * 2)
                    Angle -= (float)Math.PI * 2;
                if (Angle < -Math.PI * 2)
                    Angle += (float)Math.PI * 2;
                // reset mouse position to window center
                Mouse.SetPosition((int)(windowSize.X / 2), (int)(windowSize.Y / 2));
            }

            lastPos = Position;
            movement = Position;

            // running ;)
            if (ActivePlayerState == PlayerState.Swordfighting && !IsBlending)
            {
                PlayerSpeed = Settings.Instance.PlayerSpeed / 2f;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.LeftShift))
            {
                if (PlayerSpeed < 0.6)
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
                PlayerSpeed = Settings.Instance.PlayerSpeed / 1.5f;
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

            //Console.WriteLine(shootingTimer);
            // Shooting bullet
            if (activeWorld == WorldState.Dungeon)
            {
                //if (currentMouseState.RightButton == ButtonState.Released && lastMouseState.RightButton == ButtonState.Pressed)
                if (currentMouseState.RightButton == ButtonState.Released && lastMouseState.RightButton == ButtonState.Pressed && shootingTimer > shootingThreshold)
                {
                    BulletList.Add(new Bullet(bulletModel, new Vector2((float)Math.Sin(Angle), (float)Math.Cos(Angle)), Position));                   
                    shootingTimer = 0;
                }
                for (int i = 0; i < BulletList.Count; i++)
                {
                    if (BulletList[i].isActive)
                        BulletList[i].Update(gameTime, collisionHandler, shootingtype);
                    else BulletList.RemoveAt(i);
                }
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
            if (collisionHandler.Controller.ItemColors != null) // because in a Dungeon we might not have an Itemmap
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
            if (currentMouseState.LeftButton == ButtonState.Pressed && stateTime <= 500 && attackThreshold == swordFightingDuration)
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


            if (stateTime > 0)
            {
                stateTime -= gameTime.ElapsedGameTime.Milliseconds;
            }


            //update only the animation which is required if the changed Playerstate
            //Update the active animation
            switch (ActivePlayerState)
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
            if (ActivePlayerState != TempPlayerState)
            {
                LastPlayerState = TempPlayerState;
                // When the playerState changes, we need to blend
                IsBlending = true;
                ActiveBlendTime = fixedBlendDuration;
            }


            // if the Time for blending is over, set it on false;
            if (ActiveBlendTime <= 0)
            {
                IsBlending = false;
                if (ActiveBlendTime < 0)
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
            Vector2 frontDirection = new Vector2((float)Math.Round(Math.Sin(Angle)), (float)Math.Round(Math.Cos(Angle)));
            Vector2 gridPosInView = new Vector2((float)(Math.Round(Position.X / gridSize) + frontDirection.X), (float)(Math.Round(Position.Y / gridSize) + frontDirection.Y));

            //mystuff
            //Console.WriteLine(DegreeToRadian(Angle));

            // interaction checks happen only if interactable object is in view (eg no killing behind back anymore)
            // only == 2 in edges, else normal 3 in a row

            if (Ingame.Instance.ActiveWorld == WorldState.Dungeon)
            {
                attackTimer += gameTime.ElapsedGameTime.Milliseconds;
                shootingTimer += gameTime.ElapsedGameTime.Milliseconds;
                // collision with enemies
                Vector2 currentGridPos = new Vector2((float)Math.Round(position.X / gridSize), (float)Math.Round(position.Y / gridSize));

                //mystuff
                List<Enemy> currentBlockEnemyListtest = new List<Enemy>();

                for (int i = -1; i < 2; i++)
                {
                    for (int j = -1; j < 2; j++)
                    {
                        List<Enemy> currentBlockEnemyList = gameEnv.enemyArray[(int)currentGridPos.X + i, (int)currentGridPos.Y + j];

                        //mystuff
                        foreach (var item in currentBlockEnemyList)
                        {
                            currentBlockEnemyListtest.Add(item);
                        }

                        if (currentBlockEnemyList.Count <= 0) continue;

                        foreach (var enemy in currentBlockEnemyList)
                        {
                            var dx1 = (Position.X - enemy.Position.X) * (Position.X - enemy.Position.X);
                            var dy1 = (lastPos.Y - enemy.Position.Y) * (lastPos.Y - enemy.Position.Y);
                            if (Math.Sqrt(dx1 + dy1) < (CollisionRadius + enemy.CircleCollision / 2))
                            {
                                position.X = lastPos.X;
                            }
                            var dx2 = (lastPos.X - enemy.Position.X) * (lastPos.X - enemy.Position.X);
                            var dy2 = (Position.Y - enemy.Position.Y) * (Position.Y - enemy.Position.Y);
                            if (Math.Sqrt(dx2 + dy2) < (CollisionRadius + enemy.CircleCollision/2))
                            {
                                position.Y = lastPos.Y;
                            }
                        }
                    }
                }
                //Vector2 p = new Vector2(2, 2);
                //p.Normalize();
                //Console.WriteLine(p);
                //mystuff
                //Console.WriteLine(currentBlockEnemyListtest.Count);
                //Console.WriteLine(Math.Sin(Angle) + ", " + Math.Cos(Angle));

                //Console.WriteLine((float)Math.Asin(Math.Sin(Angle)) / Math.PI * 180 + 90 + (float)Math.Acos(Math.Cos(Angle)) / Math.PI * 180 + 90);
                
                if (ActivePlayerState == PlayerState.Swordfighting)
                {
                    //mystuff
                    bool at = false;
                    foreach (var nmy in currentBlockEnemyListtest)
                    {
                        Vector2 view = new Vector2((float)Math.Sin(Angle), (float)Math.Cos(Angle));
                        view.Normalize();

                        Vector2 circlePos = Position + view * 2.5f;
                        if (gameEnv.EuclideanDistance(circlePos, nmy.Position) < 1.2f)
                        {
                            if (attackTimer >= attackThreshold && lastMouseState.LeftButton == ButtonState.Released && currentMouseState.LeftButton == ButtonState.Pressed)
                            { 
                                nmy.Attack(rnd.Next(MinDamage, MaxDamage + 1));
                                //attackTimer = 0;
                                at = true;
                                HitEnemyWithSword = true;
                                HitAir = false;
                            }
                        }
                    }
                    if (at)
                        attackTimer = 0;
                }
                currentBlockEnemyListtest.Clear();
            }


            // Leveling
            XpToLevel();
            // Calculate stats on equipment change
            if (CurrentEquippedArmor != oldEquippedArmor || CurrentEquippedBoots != oldEquippedBoots || CurrentEquippedHelmet != oldEquippedHelmet || CurrentEquippedWeapon != oldEquippedWeapon)
            {
                Console.WriteLine("Equipped items changed, computed new stats.");
                GrandStats();
                oldEquippedArmor = CurrentEquippedArmor;
                oldEquippedBoots = CurrentEquippedBoots;
                oldEquippedHelmet = CurrentEquippedHelmet;
                oldEquippedWeapon = CurrentEquippedWeapon;
            }

            // Regen HP and Focus 
            // TODO: Timer or something?!
            Hp += HpRegen;
            if (Hp > MaxHp)
            {
                Hp = MaxHp;
            }

            Focus += FocusRegen;
            if (Focus > MaxFocus)
            {
                Focus = MaxFocus;
            }

            if (ActiveQuest != oldQuest)
            {
                UpdateQuest();
                oldQuest = activeQuest;
            }

            

            // Call Sounds
            if (HitEnemyWithSword)
            {
                Jukebox.Instance.PlaySwordFightSound();
            }
            if (!HitEnemyWithSword && HitAir)
            {
                Jukebox.Instance.PlayAudioMouseFeedback();
            }

            // Get Keyboard input to change overall GameState
            if (EmodiaQuest.Core.GUI.Controls_GUI.Instance.keyClicked(Keys.I))
                EmodiaQuest_Game.Gamestate_Game = GameStates_Overall.InventoryScreen;
            if (EmodiaQuest.Core.GUI.Controls_GUI.Instance.keyClicked(Keys.O))
                EmodiaQuest_Game.Gamestate_Game = GameStates_Overall.OptionsScreen;
        }

        private void XpToLevel()
        {
            if (Experience >= XPToNextLevel)
            {
                Level++;
                Console.WriteLine("Level Up! You are now lvl " + Level);

                // Grant lvlup boni
                strengthGaidedThrougLvls++;
                skillGainedThroughLvls++;
                intellGainedThroughLvls++;
                GrandStats();

                Experience = Experience - XPToNextLevel;
                XPToNextLevel = (int)Math.Round(xpScaleFactor * Math.Sqrt(Level));
            }
        }

        private void GrandStats()
        {
            Strength = CurrentEquippedArmor.StrengthPlus + CurrentEquippedBoots.StrengthPlus + CurrentEquippedHelmet.StrengthPlus +
                    CurrentEquippedWeapon.StrengthPlus + strengthGaidedThrougLvls;

            Skill = CurrentEquippedArmor.SkillPlus + CurrentEquippedBoots.SkillPlus + CurrentEquippedHelmet.SkillPlus +
                    CurrentEquippedWeapon.SkillPlus + skillGainedThroughLvls;

            Intelligence = CurrentEquippedArmor.IntelligencePlus + CurrentEquippedBoots.IntelligencePlus + CurrentEquippedHelmet.IntelligencePlus +
                    CurrentEquippedWeapon.IntelligencePlus + intellGainedThroughLvls;

            Armor = CurrentEquippedArmor.Armor + CurrentEquippedBoots.Armor + CurrentEquippedHelmet.Armor +
                    CurrentEquippedWeapon.Armor;

            MinDamage = CurrentEquippedArmor.MinDamage + CurrentEquippedBoots.MinDamage + CurrentEquippedHelmet.MinDamage +
                    CurrentEquippedWeapon.MinDamage;
            // max dmg + 10% skill 
            MaxDamage = CurrentEquippedArmor.MaxDamage + CurrentEquippedBoots.MaxDamage + CurrentEquippedHelmet.MaxDamage +
                    CurrentEquippedWeapon.MaxDamage + (int)Math.Round(Skill/10.0);

            // Max HP is XP for next lvl + 10% strength with base 100
            MaxHp = (int)((100 * Math.Sqrt(Level)) < 100 ? 100 : (100 * Math.Sqrt(Level)) + Math.Round(Strength / 10.0));

            // Max Focus is 100 + 10% intel
            MaxFocus = (int) (100 + Math.Round(Intelligence/10.0));

            FocusRegen = (int) Math.Round(Intelligence/2.0);
        }

        private void UpdateQuest()
        {
            if (QuestController.Instance.ActiveQuests.Any())
            {
                ActiveQuest = QuestController.Instance.ActiveQuests[0];
            }
        }

        private Enemy getClosestMonster(List<Enemy> enemyList)
        {
            float currentCosest = float.MaxValue;
            Enemy closestEnemy = null;

            foreach (var enemy in enemyList)
            {
                float dist = (float)gameEnv.EuclideanDistance(Position, enemy.Position);
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
            // you geht 10% damage reduction on your armor
            Hp -= (int)(damage - Math.Round(Armor/10.0));
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
                                    blendingBones[i] = Matrix.Lerp(blendingBones[i], standingBones[i], 1 - percentageOfBlending);
                                }
                                effect.SetBoneTransforms(blendingBones);
                                break;
                            case PlayerState.Walking:
                                for (int i = 0; i < blendingBones.Length; i++)
                                {
                                    blendingBones[i] = Matrix.Lerp(blendingBones[i], walkingBones[i], 1 - percentageOfBlending);
                                }
                                effect.SetBoneTransforms(blendingBones);
                                break;
                            case PlayerState.Swordfighting:
                                for (int i = 0; i < blendingBones.Length; i++)
                                {
                                    blendingBones[i] = Matrix.Lerp(blendingBones[i], swordfightingBones[i], 1 - percentageOfBlending);
                                }
                                effect.SetBoneTransforms(blendingBones);
                                break;
                            case PlayerState.Jumping:
                                for (int i = 0; i < blendingBones.Length; i++)
                                {
                                    blendingBones[i] = Matrix.Lerp(blendingBones[i], jumpingBones[i], 1 - percentageOfBlending);
                                }
                                effect.SetBoneTransforms(blendingBones);
                                break;
                            case PlayerState.WalkJumping:
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
                    effect.World = Matrix.CreateRotationX((float)(-0.5 * Math.PI)) * Matrix.CreateRotationY(Angle) * Matrix.CreateTranslation(new Vector3(lastPos.X, 0, lastPos.Y)) * world;
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
                    else if (mesh.Name == "MainChar_hair")
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


            for (int i = 0; i < BulletList.Count; i++)
            {
                BulletList[i].Draw(world, view, projection);
            }
        }
    }
}
