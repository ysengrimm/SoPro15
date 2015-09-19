using System;
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

        // speedHeck for multiple movement keys pressed
        float KeysPressing;

        // shooting type
        public enum Shootingtype { Normal, Blast, Lightning };
        private Shootingtype shootingtype = Shootingtype.Normal;
        // time for shooting
        private float shootingTimer;
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
        public Vector2 PlayerViewDirection = new Vector2(0, 0);

        // Eventhandler
        public event Delegates_CORE.ChangeValueDelegate OnChangeValue;

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

        private float maxHp;
        public float MaxHp
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

        private float hpRegen;
        public float HpRegen
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

        public List<Item> ItemsDropped { get; set; } 

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
        private Model bulletModel1;
        private Model bulletModel2;
        private Model bulletModel3;
        private Model bullet2animation;

        // The Model
        private Model playerModel, standingM, walkingM, runningM, swordfighting1M, swordfighting2M, fightingStandM, gunfightingM;
        // Skinning Data
        private SkinningData standingSD, walkingSD, runningSD, swordfighting1SD, swordfighting2SD, fightingStandSD, gunfightingSD;
        // The animation Player
        private AnimationPlayer standingAP, walkingAP, runningAP, swordfighting1AP, swordfighting2AP, fightingStandAP, gunfightingAP;
        // The animation Clips, which will be used by the model
        private AnimationClip standingC, walkingC, runningC, swordfighting1C, swordfighting2C, fightingStandC, gunfightingC;
        // The Bone Matrices for each animation
        private Matrix[] blendingBones, standingBones, walkingBones, runningBones, swordfighting1Bones, swordfighting2Bones, fightingStandBones, gunfightingBones;
        // The playerState, which will be needed to update the right animation
        public PlayerState ActivePlayerState = PlayerState.Standing;
        public PlayerState LastPlayerState = PlayerState.Standing;
        public PlayerState TempPlayerState = PlayerState.Standing;
        private WorldState activeWorld = WorldState.Safeworld;
        public float standingDuration;
        public float walkingDuration;
        public float runningDuration;
        public float swordFighting1Duration;
        public float swordFighting2Duration;
        public float fightingStandDuration;
        public float gunFightingDuration;

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
            MaxHp = (float)((100 * Math.Sqrt(Level)) < 100 ? 100 : (100 * Math.Sqrt(Level)) + Math.Round(Strength / 10.0));
            Hp = MaxHp;
            HpRegen = 0.02f;

            // Max Focus is 100 + 10% intel
            MaxFocus = (int)(100 + Math.Round(Intelligence / 10.0));
            Focus = MaxFocus;
            FocusRegen = (int) Math.Round(Intelligence/50.0);

            Armor = 0;
            MinDamage = 50;
            MaxDamage = 80;
            Gold = 0;

            Level = 1;
            Experience = 0;
            XPToNextLevel = 100;

            PlayerInventory = new List<Item>();
            ItemsDropped = new List<Item>();

            CurrentEquippedHelmet = new Item(1, ItemClass.Helmet, 0, 20, 0, 0, "rostigerSchrottHelm");
            CurrentEquippedArmor = new Item(0, ItemClass.Armor, 0, 0, 10, 10, "leichteModerneRuestung");
            CurrentEquippedBoots = new Item(1, ItemClass.Boots, 0, 10, 0, 0, "Schuhe");
            CurrentEquippedWeapon = new Item(0, ItemClass.Weapon, 0, 50, 70, 0, 0, true, "SpeziellesGewehr");

            ActiveQuest = new Quest {Name = "", Description = ""};

            MovementOffset = 2.0f;
            ItemOffset = 0.0f;
            Angle = 0;
            CollisionRadius = 1.5f;
            attackTimer = 0;
            shootingTimer = 0;

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
            defaultBodyTex = contentMngr.Load<Texture2D>("fbxContent/player/young_lightskinned_male_diffuse");
            defaultHairTex = contentMngr.Load<Texture2D>("fbxContent/player/male02_diffuse_black");
            //loading default mesh
            playerModel = contentMngr.Load<Model>("fbxContent/player/Main_Char_t_pose");

            //loading Animation Models
            standingM = contentMngr.Load<Model>("fbxContent/player/Main_Char_idle_stand");
            walkingM = contentMngr.Load<Model>("fbxContent/player/Main_Char_walk");
            runningM = contentMngr.Load<Model>("fbxContent/player/Main_Char_run");
            swordfighting1M = contentMngr.Load<Model>("fbxContent/player/Main_Char_swordFighting");
            swordfighting2M = contentMngr.Load<Model>("fbxContent/player/Main_Char_swordFighting2");
            fightingStandM = contentMngr.Load<Model>("fbxContent/player/Main_Char_fighting_stand");
            gunfightingM = contentMngr.Load<Model>("fbxContent/player/Main_Char_gunFighting");

            //Loading Skinning Data
            standingSD = standingM.Tag as SkinningData;
            walkingSD = walkingM.Tag as SkinningData;
            runningSD = runningM.Tag as SkinningData;
            swordfighting1SD = swordfighting1M.Tag as SkinningData;
            swordfighting2SD = swordfighting2M.Tag as SkinningData;
            fightingStandSD = fightingStandM.Tag as SkinningData;
            gunfightingSD = gunfightingM.Tag as SkinningData;

            //Load an animation Player for each animation
            standingAP = new AnimationPlayer(standingSD);
            walkingAP = new AnimationPlayer(walkingSD);
            runningAP = new AnimationPlayer(runningSD);
            swordfighting1AP = new AnimationPlayer(swordfighting1SD);
            swordfighting2AP = new AnimationPlayer(swordfighting2SD);
            fightingStandAP = new AnimationPlayer(fightingStandSD);
            gunfightingAP = new AnimationPlayer(gunfightingSD);

            //loading Animation
            /*
            standingC = standingSD.AnimationClips["Stand"];
            walkingC = walkingSD.AnimationClips["Run"];
            jumpingC = jumpingSD.AnimationClips["Jump"];
            */

            standingC = standingSD.AnimationClips["idle_stand"];
            walkingC = walkingSD.AnimationClips["walk"];
            runningC = runningSD.AnimationClips["run"];
            swordfighting1C = swordfighting1SD.AnimationClips["swordFighting"];
            swordfighting2C = swordfighting2SD.AnimationClips["swordFighting2"];
            fightingStandC = fightingStandSD.AnimationClips["fighting_stand"];
            gunfightingC = gunfightingSD.AnimationClips["gunFighting"];
            

            //Safty Start Animations
            standingAP.StartClip(standingC);
            walkingAP.StartClip(walkingC);
            runningAP.StartClip(runningC);
            swordfighting1AP.StartClip(swordfighting1C);
            swordfighting2AP.StartClip(swordfighting2C);
            fightingStandAP.StartClip(fightingStandC);
            gunfightingAP.StartClip(gunfightingC);

            //assign the specific animationTimes
            standingDuration = 200;
            walkingDuration = (float)walkingC.Duration.TotalMilliseconds / 1f;
            runningDuration = (float)runningC.Duration.TotalMilliseconds / 1f;
            swordFighting1Duration = (float)swordfighting1C.Duration.TotalMilliseconds / 1f;
            swordFighting2Duration = (float)swordfighting2C.Duration.TotalMilliseconds / 1f;
            fightingStandDuration = (float)fightingStandC.Duration.TotalMilliseconds / 1f;
            gunFightingDuration = (float)gunfightingC.Duration.TotalMilliseconds / 1f;
            /*
            Console.WriteLine("StandingDuration: " + standingC.Duration.TotalMilliseconds);
            Console.WriteLine("StandingKeyframes: " + standingC.Keyframes.Count);
            Console.WriteLine("WalkingDuration: " + walkingC.Duration.TotalMilliseconds);
            Console.WriteLine("WalkingKeyframes: " + walkingC.Keyframes.Count);
            */
            stateTime = 0;
            attackThreshold = swordFighting1Duration;
            shootingThreshold = gunFightingDuration;
            // Duration of Blending Animations in milliseconds
            fixedBlendDuration = 400;

            // Load Bullets
            bulletModel1 = contentMngr.Load<Model>("fbxContent/bullets/bullet1/bullet1");
            bulletModel2 = contentMngr.Load<Model>("fbxContent/bullets/bullet2/bullet2");
            bulletModel3 = contentMngr.Load<Model>("fbxContent/bullets/bullet3/bullet3");
            bullet2animation = contentMngr.Load<Model>("fbxContent/bullets/bullet2/bullet2animation/bullet2animation");
            Bullet.blastAnimation = bullet2animation;
        }

        //mystuff
        private double DegreeToRadian(double angle)
        {
            return Math.Abs(Angle) / Math.PI * 180;
        }

        private double Kosinussatz(Vector2 npcPos, Vector2 viewPos)
        {
            double a = Math.Sqrt(Math.Pow(Position.X - viewPos.X, 2) + Math.Pow(Position.Y - viewPos.Y, 2));
            double b = Math.Sqrt(Math.Pow(viewPos.X - npcPos.X, 2) + Math.Pow(viewPos.Y - npcPos.Y, 2));
            double c = Math.Sqrt(Math.Pow(Position.X - npcPos.X, 2) + Math.Pow(Position.Y - npcPos.Y, 2));
            double e = (Math.Pow(a, 2) + Math.Pow(c, 2) - Math.Pow(b, 2)) / (2 * a * c);
            return Math.Acos(e); ;
        }

        public void Update(GameTime gameTime)
        {
            //Console.WriteLine(CurrentEquippedWeapon.Name);

            HitEnemyWithSword = false;
            HitAir = false;
            KeysPressing = 1;

            // Computer view direction
            PlayerViewDirection = new Vector2((float)Math.Sin(Angle), (float)Math.Cos(Angle));
            //PlayerViewDirection.Normalize();


            //Update interaction with NPCs
            if (activeWorld == WorldState.Safeworld)
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

            // running 
            if ((ActivePlayerState == PlayerState.Swordfighting1 || ActivePlayerState == PlayerState.Swordfighting2 || ActivePlayerState == PlayerState.Gunfighting || ActivePlayerState == PlayerState.Standfighting) && !IsBlending)
            {
                PlayerSpeed = Settings.Instance.PlayerSpeed / 2f;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.LeftShift))
            {
                if (PlayerSpeed < 0.65)
                {
                    PlayerSpeed += 0.1f;
                }
            }
            else
            {
                PlayerSpeed = Settings.Instance.PlayerSpeed;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                KeysPressing += 0.2f;
                movement.Y += PlayerSpeed * (float)Math.Cos(Angle);
                movement.X += PlayerSpeed * (float)Math.Sin(Angle);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                KeysPressing += 0.2f;
                PlayerSpeed /= KeysPressing;
                PlayerSpeed = Settings.Instance.PlayerSpeed / 1.5f;
                movement.Y -= PlayerSpeed * (float)Math.Cos(Angle);
                movement.X -= PlayerSpeed * (float)Math.Sin(Angle);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                KeysPressing += 0.2f;
                PlayerSpeed /= KeysPressing;
                movement.Y -= PlayerSpeed * (float)Math.Cos(Angle - Math.PI / 2);
                movement.X -= PlayerSpeed * (float)Math.Sin(Angle - Math.PI / 2);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                KeysPressing += 0.2f;
                PlayerSpeed /= KeysPressing;
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
            if (GUI.Controls_GUI.Instance.mousePressedLeft() && stateTime <= 120 )
            {
                // Gunshooting!
                if (CurrentEquippedWeapon.IsRangedWeapon == true && shootingTimer > shootingThreshold && Focus >= 10)
                {
                    ActivePlayerState = PlayerState.Gunfighting; Model bulletModelToGive = bulletModel1;
                    // Choose the right bullet!
                    switch (shootingtype)
                    {
                        case Shootingtype.Normal:
                            bulletModelToGive = bulletModel1;
                            Focus -= 10;
                            break;
                        case Shootingtype.Blast:
                            bulletModelToGive = bulletModel2;
                            Focus -= 10;
                            break;
                        case Shootingtype.Lightning:
                            bulletModelToGive = bulletModel3;
                            Focus -= 10;
                            break;
                        default:
                            break;
                    }
                    shootingTimer = 0;
                    BulletList.Add(new Bullet(bulletModelToGive, PlayerViewDirection, Position, Angle, shootingtype));
                    stateTime = gunFightingDuration;
                }
                    // First swordfight animation
                else if (rnd.Next(2) == 0 && CurrentEquippedWeapon.IsRangedWeapon == false)
                {
                    ActivePlayerState = PlayerState.Swordfighting1;
                    stateTime = swordFighting1Duration;
                    HitAir = true;
                    attackTimer = 0;
                }
                    // Second swordfight animation
                else if (CurrentEquippedWeapon.IsRangedWeapon == false)
                {
                    ActivePlayerState = PlayerState.Swordfighting2;
                    stateTime = swordFighting2Duration;
                    HitAir = true;
                    attackTimer = 0;
                }
                fixedBlendDuration = 200;
            }
                // Running!
            else if ((lastPos.X != movement.X || lastPos.Y != movement.Y) && PlayerSpeed > Settings.Instance.PlayerSpeed && stateTime <= 100)
            {
                ActivePlayerState = PlayerState.Running;
                fixedBlendDuration = 300;
            }
                // Walking
            else if ((lastPos.X != movement.X || lastPos.Y != movement.Y) && stateTime <= 100)
            {
                ActivePlayerState = PlayerState.Walking;
                stateTime = walkingDuration / 2;
                fixedBlendDuration = 300;
            }
                // Standing
            else if (lastPos.X == movement.X && lastPos.Y == movement.Y && stateTime <= 100)
            {
                ActivePlayerState = PlayerState.Standing;
                stateTime = standingDuration / 2;
                fixedBlendDuration = 300;
            }
            // update stateTime
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
                case PlayerState.Running:
                    runningAP.Update(gameTime.ElapsedGameTime, true, Matrix.Identity);
                    break;
                case PlayerState.Swordfighting1:
                    swordfighting1AP.Update(gameTime.ElapsedGameTime, true, Matrix.Identity);
                    break;
                case PlayerState.Swordfighting2:
                    swordfighting2AP.Update(gameTime.ElapsedGameTime, true, Matrix.Identity);
                    break;
                case PlayerState.Standfighting:
                    fightingStandAP.Update(gameTime.ElapsedGameTime, true, Matrix.Identity);
                    break;
                case PlayerState.Gunfighting:
                    gunfightingAP.Update(gameTime.ElapsedGameTime, true, Matrix.Identity);
                    break;
            }

            // Secure, that the last Playerstate is always a other Playerstate, than the acutal
            if (ActivePlayerState != TempPlayerState)
            {
                LastPlayerState = TempPlayerState;
                // When the playerState changes, we need to blend
                IsBlending = true;
                ActiveBlendTime = fixedBlendDuration;
                
                switch(ActivePlayerState)
                {
                    case PlayerState.Gunfighting:
                        gunfightingAP.StartClip(gunfightingC);
                        break;
                    case PlayerState.Running:
                        runningAP.StartClip(runningC);
                        break;
                    case PlayerState.Standing:
                        standingAP.StartClip(standingC);
                        break;
                    case PlayerState.Swordfighting1:
                        swordfighting1AP.StartClip(swordfighting1C);
                        break;
                    case PlayerState.Swordfighting2:
                        swordfighting2AP.StartClip(swordfighting2C);
                        break;
                    case PlayerState.Walking:
                        walkingAP.StartClip(walkingC);
                        break;
                }
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

            // Update the last animation (only as long as the fixed blendduration is)
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
                    case PlayerState.Running:
                        runningAP.Update(gameTime.ElapsedGameTime, true, Matrix.Identity);
                        break;
                    case PlayerState.Swordfighting1:
                        swordfighting1AP.Update(gameTime.ElapsedGameTime, true, Matrix.Identity);
                        break;
                    case PlayerState.Swordfighting2:
                        swordfighting2AP.Update(gameTime.ElapsedGameTime, true, Matrix.Identity);
                        break;
                    case PlayerState.Standfighting:
                        fightingStandAP.Update(gameTime.ElapsedGameTime, true, Matrix.Identity);
                        break;
                    case PlayerState.Gunfighting:
                        gunfightingAP.Update(gameTime.ElapsedGameTime, true, Matrix.Identity);
                        break;
                }
            }


            //Update Temp PlayerState
            TempPlayerState = ActivePlayerState;

            // interaction
            Vector2 frontDirection = new Vector2((float)Math.Round(Math.Sin(Angle)), (float)Math.Round(Math.Cos(Angle)));
            Vector2 gridPosInView = new Vector2((float)(Math.Round(Position.X / gridSize) + frontDirection.X), (float)(Math.Round(Position.Y / gridSize) + frontDirection.Y));

            // interaction in the dungeon (only walking)
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
                            if (Math.Sqrt(dx2 + dy2) < (CollisionRadius + enemy.CircleCollision / 2))
                            {
                                position.Y = lastPos.Y;
                            }
                        }
                    }
                }

                if (ActivePlayerState == PlayerState.Swordfighting1 || ActivePlayerState == PlayerState.Swordfighting2 || ActivePlayerState == PlayerState.Standfighting)
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
                            if (attackTimer >= attackThreshold && GUI.Controls_GUI.Instance.mouseClickAndHoldLeft())
                            {
                                nmy.Attack(rnd.Next(MinDamage, MaxDamage + 1));
                                //attackTimer = 0;
                                at = true;
                                HitEnemyWithSword = true;
                                HitAir = false;
                            }
                        }
                    }
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
                if (CurrentEquippedWeapon.Name == "EinfachesGewehr")
                {
                    shootingtype = Shootingtype.Normal;
                }
                else if(CurrentEquippedWeapon.Name == "Gewehr")
                {
                    shootingtype = Shootingtype.Blast;
                }
                else
                {
                    shootingtype = Shootingtype.Lightning;
                }
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

            for (int i = 0; i < BulletList.Count; i++)
            {
                if (BulletList[i].isActive)
                    BulletList[i].Update(gameTime, collisionHandler);
                else BulletList.RemoveAt(i);
            }

            

            // Call Sounds
            if (HitEnemyWithSword)
            {
                Jukebox.Instance.PlaySwordFightSound();
            }
            if (!HitEnemyWithSword && HitAir)
            {
                Console.WriteLine(HitEnemyWithSword + " omg: " + HitAir);
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

        public void GrandStats()
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
                //Hp = 100;
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
                case PlayerState.Swordfighting1:
                    swordfighting1Bones = swordfighting1AP.GetSkinTransforms();
                    break;
                case PlayerState.Running:
                    runningBones = runningAP.GetSkinTransforms();
                    break;
                case PlayerState.Swordfighting2:
                    swordfighting2Bones = swordfighting2AP.GetSkinTransforms();
                    break;
                case PlayerState.Standfighting:
                    fightingStandBones = fightingStandAP.GetSkinTransforms();
                    break;
                case PlayerState.Gunfighting:
                    gunfightingBones = gunfightingAP.GetSkinTransforms();
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
                    case PlayerState.Swordfighting1:
                        blendingBones = swordfighting1AP.GetSkinTransforms();
                        break;
                    case PlayerState.Running:
                        blendingBones = runningAP.GetSkinTransforms();
                        break;
                    case PlayerState.Swordfighting2:
                        swordfighting2Bones = swordfighting2AP.GetSkinTransforms();
                        break;
                    case PlayerState.Standfighting:
                        fightingStandBones = fightingStandAP.GetSkinTransforms();
                        break;
                    case PlayerState.Gunfighting:
                        gunfightingBones = gunfightingAP.GetSkinTransforms();
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
                            case PlayerState.Swordfighting1:
                                for (int i = 0; i < blendingBones.Length; i++)
                                {
                                    blendingBones[i] = Matrix.Lerp(blendingBones[i], swordfighting1Bones[i], 1 - percentageOfBlending);
                                }
                                effect.SetBoneTransforms(blendingBones);
                                break;
                            case PlayerState.Running:
                                for (int i = 0; i < blendingBones.Length; i++)
                                {
                                    blendingBones[i] = Matrix.Lerp(blendingBones[i], runningBones[i], 1 - percentageOfBlending);
                                }
                                effect.SetBoneTransforms(blendingBones);
                                break;
                            case PlayerState.Swordfighting2:
                                for (int i = 0; i < blendingBones.Length; i++)
                                {
                                    blendingBones[i] = Matrix.Lerp(blendingBones[i], swordfighting2Bones[i], 1 - percentageOfBlending);
                                }
                                effect.SetBoneTransforms(blendingBones);
                                break;
                            case PlayerState.Standfighting:
                                for (int i = 0; i < blendingBones.Length; i++)
                                {
                                    blendingBones[i] = Matrix.Lerp(blendingBones[i], fightingStandBones[i], 1 - percentageOfBlending);
                                }
                                effect.SetBoneTransforms(blendingBones);
                                break;
                            case PlayerState.Gunfighting:
                                for (int i = 0; i < blendingBones.Length; i++)
                                {
                                    blendingBones[i] = Matrix.Lerp(blendingBones[i], gunfightingBones[i], 1 - percentageOfBlending);
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
                            case PlayerState.Swordfighting1:
                                effect.SetBoneTransforms(swordfighting1Bones);
                                break;
                            case PlayerState.Running:
                                effect.SetBoneTransforms(runningBones);
                                break;
                            case PlayerState.Swordfighting2:
                                effect.SetBoneTransforms(swordfighting2Bones);
                                break;
                            case PlayerState.Standfighting:
                                effect.SetBoneTransforms(fightingStandBones);
                                break;
                            case PlayerState.Gunfighting:
                                effect.SetBoneTransforms(gunfightingBones);
                                break;
                        }
                    }


                    effect.EnableDefaultLighting();
                    effect.World = Matrix.CreateRotationX((float)(-0.5 * Math.PI)) * Matrix.CreateRotationY(Angle) * Matrix.CreateTranslation(new Vector3(lastPos.X, 0, lastPos.Y)) * world;
                    effect.View = view;
                    effect.Projection = projection;
                    effect.SpecularColor = new Vector3(0.15f);
                    effect.SpecularPower = 16;

                    // Textures
                    // Body Textures
                    
                    if (mesh.Name == "MainChar_body")
                    {
                        effect.Texture = defaultBodyTex;
                    }
                    else if (mesh.Name == "MainChar_hair")
                    {
                        effect.Texture = defaultHairTex;
                    }
                    else if (mesh.Name == "MainChar_body_lowPoly")
                    {
                        effect.Texture = defaultBodyTex;
                    }
                    

                }
                // Always render the Player
                if (mesh.Name == "MainChar_Body" || mesh.Name == "MainChar_eye" || mesh.Name == "MainChar_Body_lowPoly" || mesh.Name == "Einfache_Hose" || mesh.Name == "Einfache_Schuhe")
                {
                    mesh.Draw();
                }
                //Only render the equipped Weapon
                else if (mesh.Name == "Stock" && CurrentEquippedWeapon.Name == "Stock")
                {
                    mesh.Draw();
                }
                else if (mesh.Name == "Hammer" && CurrentEquippedWeapon.Name == "Hammer")
                {
                    mesh.Draw();
                }
                else if (mesh.Name == "Schwert" && CurrentEquippedWeapon.Name == "Schwert")
                {
                    mesh.Draw();
                }
                else if (mesh.Name == "Einfaches_Gewehr" && CurrentEquippedWeapon.Name == "EinfachesGewehr")
                {
                    mesh.Draw();
                }
                else if (mesh.Name == "Normales_Gewehr" && CurrentEquippedWeapon.Name == "Gewehr")
                {
                    mesh.Draw();
                }
                else if (mesh.Name == "Spezielles_Gewehr" && CurrentEquippedWeapon.Name == "SpeziellesGewehr")
                {
                    mesh.Draw();
                }
                // Only render the equipped Helmet
                else if (mesh.Name == "Einfache_Kappe" && (CurrentEquippedHelmet.Name == "LederKappe" || CurrentEquippedHelmet.Name == "StoffKappe" || CurrentEquippedHelmet.Name == "MetallKappe"))
                {
                    mesh.Draw();
                }
                else if (mesh.Name == "Schrott_Helm" && CurrentEquippedHelmet.Name == "rostigerSchrottHelm")
                {
                    mesh.Draw();
                }
                else if (mesh.Name == "Spezieller_Helm" && (CurrentEquippedHelmet.Name == "speziellerHelm" || CurrentEquippedHelmet.Name == "speziellerRostigerHelm" || CurrentEquippedHelmet.Name == "speziellerSignierterHelm"))
                {
                    mesh.Draw();
                }
                // Only render the equipped Amor
                else if (mesh.Name == "Einfache_R_stung" && (CurrentEquippedArmor.Name == "leichteRuestung" || CurrentEquippedArmor.Name == "leichteModerneRuestung"))
                {
                    mesh.Draw();
                }
                else if (mesh.Name == "Einfaches_Shirt" && (CurrentEquippedArmor.Name == "StoffHemd" || CurrentEquippedArmor.Name == "neuesStoffhemd" || CurrentEquippedArmor.Name == "altesStoffhemd"))
                {
                    mesh.Draw();
                }
                else if (mesh.Name == "Schwere_R_stung" && (CurrentEquippedArmor.Name == "alteSchwereRuestung" || CurrentEquippedArmor.Name == "moderneSchwereRuestung" || CurrentEquippedArmor.Name == "schwereRuestung"))
                {
                    mesh.Draw();
                }


                //else mesh.Draw();
            }


            for (int i = 0; i < BulletList.Count; i++)
            {
                BulletList[i].Draw(world, view, projection);
            }
        }
    }
}
