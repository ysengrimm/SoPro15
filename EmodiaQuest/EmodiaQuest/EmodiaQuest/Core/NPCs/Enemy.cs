using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EmodiaQuest.Core.Items;
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
        // Variables for Ai
        private EnvironmentController currentEnvironment;
        private Ai enemyAi;

        private bool isAttacking;

        // Enemystats
        public Vector2 Position;
        public float ViewAngle;

        private Vector2 oldPosition;
        public float MaxEnemyHealth = 0;
        public float CurrentHealth;
        public float Armor;
        public float MovementSpeed;
        public float TrackingRadius;
        public float CircleCollision;
        public float AttackRange;
        public float Damage;
        public int Level;

        private float attackTimer;
        public float AttackThreshold;

        // healthbar
        public int HpForHealthbar = 10;
        //public string healthbar = "h10";

        public EnemyType EnemyType;

        private float gridSize = Settings.Instance.GridSize;

        /// <summary>
        /// Trigger to spawn only one ball
        /// </summary>
        private bool spawnHealthBall = true;

        public bool IsAlive { get; set; }

        private CollisionHandler collHandler;

        public ContentManager Content;

        /**
         * Animation and Model
        **/
        // The Model
        private Model enemyModel, idleM, runM, fightM;
        // Skinning Data
        private SkinningData idleSD, runSD, fightSD;
        // The animation Player
        private AnimationPlayer idleAP, runAP, fightAP;
        // The animation Clips, which will be used by the model
        private AnimationClip idleC, runC, fightC;
        // The Bone Matrices for each animation
        private Matrix[] blendingBones, idleBones, runBones, fightBones;
        // The playerState, which will be needed to update the right animation
        public EnemyState CurrentEnemyState = EnemyState.Idle;
        public EnemyState TempEnemyState = EnemyState.Idle;
        public EnemyState LastEnemyState = EnemyState.Idle;

        public Weapon activeWeapon = Weapon.Hammer;

        // duration of the animations
        private float idleDuration;
        private float runDuration;
        private float fightDuration;

        private float stateTime;
        private float fixedBlendDuration;

        // variables for animation
        public float activeBlendTime;
        public bool isBlending;

        // Healthbar Content
        private Model health0;
        private Model health1;
        private Model health2;
        private Model health3;
        private Model health4;
        private Model health5;
        private Model health6;
        private Model health7;
        private Model health8;
        private Model health9;
        private Model health10;

        private bool isHit = false;
        private bool breathingOn = false;
        private bool overlayBool = false;
        private Vector4 ambientColor = new Vector4(1, 0, 0, 1);
        private float ambientIntensity = 0.0f;
        private float distanceToPlayer = 0.0f;


        public Enemy(Vector2 position, EnvironmentController currentEnvironment, EnemyType enemyType, int Level)
        {
            this.Level = Level;
            this.currentEnvironment = currentEnvironment;
            this.Position = position;
            this.EnemyType = enemyType;
            currentEnvironment.enemyArray[(int)Math.Round(Position.X / 10), (int)Math.Round(Position.Y / 10)].Add(this);
            this.isAttacking = false;
        }


        // Methods
        public void LoadContent(ContentManager content)
        {
            this.Content = content;

            // Normal variables for each different enemy Type
            switch (EnemyType)
            {
                case EnemyType.Monster1:
                    MovementSpeed = Settings.Instance.HumanEnemySpeed;
                    MaxEnemyHealth = Settings.Instance.MaxHumanEnemyHealth*1.5f * Level;
                    AttackRange = 7;
                    Damage = 5 * Level;

                    // movement
                    TrackingRadius = 90f;
                    MovementSpeed = 0.25f;
                    enemyAi = new Ai(Position, CurrentEnemyState, LastEnemyState, TrackingRadius, MovementSpeed, currentEnvironment);
                    ViewAngle = -enemyAi.TrackingAngle;

                    attackTimer = 0;

                    // collision
                    CircleCollision = 1.0f;
                    break;
                case EnemyType.Monster2:
                    MovementSpeed = Settings.Instance.HumanEnemySpeed;
                    MaxEnemyHealth = Settings.Instance.MaxHumanEnemyHealth * Level;
                    AttackRange = 7;
                    Damage = 5 * Level;

                    // movement
                    TrackingRadius = 90f;
                    MovementSpeed = 0.25f;
                    enemyAi = new Ai(Position, CurrentEnemyState, LastEnemyState, TrackingRadius, MovementSpeed, currentEnvironment);
                    ViewAngle = -enemyAi.TrackingAngle;

                    attackTimer = 0;

                    // collision
                    CircleCollision = 1.0f;
                    break;
                case EnemyType.Monster3:
                    MovementSpeed = Settings.Instance.HumanEnemySpeed;
                    MaxEnemyHealth = Settings.Instance.MaxHumanEnemyHealth * 3 * Level;
                    AttackRange = 7;
                    Damage = 5 * Level;

                    // movement
                    TrackingRadius = 90f;
                    MovementSpeed = 0.25f;
                    enemyAi = new Ai(Position, CurrentEnemyState, LastEnemyState, TrackingRadius, MovementSpeed, currentEnvironment);
                    ViewAngle = -enemyAi.TrackingAngle;

                    attackTimer = 0;

                    // collision
                    CircleCollision = 1.0f;
                    break;
                case EnemyType.Monster4:
                    MovementSpeed = Settings.Instance.HumanEnemySpeed;
                    MaxEnemyHealth = Settings.Instance.MaxHumanEnemyHealth * 1.5f * Level;
                    AttackRange = 7;
                    Damage = 5 * Level;

                    // movement
                    TrackingRadius = 90f;
                    MovementSpeed = 0.25f;
                    enemyAi = new Ai(Position, CurrentEnemyState, LastEnemyState, TrackingRadius, MovementSpeed, currentEnvironment);
                    ViewAngle = -enemyAi.TrackingAngle;

                    attackTimer = 0;

                    // collision
                    CircleCollision = 1.0f;
                    break;
                case EnemyType.Monster5:
                    MovementSpeed = Settings.Instance.HumanEnemySpeed;
                    MaxEnemyHealth = Settings.Instance.MaxHumanEnemyHealth * 2.5f * Level;
                    AttackRange = 7;
                    Damage = 5 * Level;

                    // movement
                    TrackingRadius = 90f;
                    MovementSpeed = 0.25f;
                    enemyAi = new Ai(Position, CurrentEnemyState, LastEnemyState, TrackingRadius, MovementSpeed, currentEnvironment);
                    ViewAngle = -enemyAi.TrackingAngle;

                    attackTimer = 0;

                    // collision
                    CircleCollision = 1.0f;
                    break;
                case EnemyType.Monster6:
                    MovementSpeed = Settings.Instance.HumanEnemySpeed;
                    MaxEnemyHealth = Settings.Instance.MaxHumanEnemyHealth * 2 * Level;
                    AttackRange = 7;
                    Damage = 5 * Level;

                    // movement
                    TrackingRadius = 90f;
                    MovementSpeed = 0.25f;
                    enemyAi = new Ai(Position, CurrentEnemyState, LastEnemyState, TrackingRadius, MovementSpeed, currentEnvironment);
                    ViewAngle = -enemyAi.TrackingAngle;

                    attackTimer = 0;

                    // collision
                    CircleCollision = 1.0f;
                    break;
                case EnemyType.Monster7:
                    MovementSpeed = Settings.Instance.HumanEnemySpeed;
                    MaxEnemyHealth = Settings.Instance.MaxHumanEnemyHealth * 2 * Level;
                    AttackRange = 7;
                    Damage = 5 * Level;

                    // movement
                    TrackingRadius = 90f;
                    MovementSpeed = 0.25f;
                    enemyAi = new Ai(Position, CurrentEnemyState, LastEnemyState, TrackingRadius, MovementSpeed, currentEnvironment);
                    ViewAngle = -enemyAi.TrackingAngle;

                    attackTimer = 0;

                    // collision
                    CircleCollision = 1.0f;
                    break;
                case EnemyType.Monster8:
                    MovementSpeed = Settings.Instance.HumanEnemySpeed;
                    MaxEnemyHealth = Settings.Instance.MaxHumanEnemyHealth * 3.5f * Level;
                    AttackRange = 7;
                    Damage = 5 * Level;

                    // movement
                    TrackingRadius = 90f;
                    MovementSpeed = 0.25f;
                    enemyAi = new Ai(Position, CurrentEnemyState, LastEnemyState, TrackingRadius, MovementSpeed, currentEnvironment);
                    ViewAngle = -enemyAi.TrackingAngle;

                    attackTimer = 0;

                    // collision
                    CircleCollision = 1.0f;
                    break;
            }


            // Setting the health to the MaxHealth at startup
            CurrentHealth = MaxEnemyHealth;

            collHandler = CollisionHandler.Instance;

            IsAlive = true;

            // Loading the different meshes for the enemies
            switch (EnemyType)
            {
                case EnemyType.Monster1:
                    // loading default mesh
                    enemyModel = Content.Load<Model>("fbxContent/enemies/Monster1/Monster1"); // <--------------- Insert your Mesh here, need at least 2 keyframes

                    // loading Animation Models
                    idleM = Content.Load<Model>("fbxContent/enemies/Monster1/Monster1idle"); // <--------------------- The animation Meshes here
                    runM = Content.Load<Model>("fbxContent/enemies/Monster1/Monster1run");
                    fightM = Content.Load<Model>("fbxContent/enemies/Monster1/Monster1fight");
                    break;
                case EnemyType.Monster2:
                    // loading default mesh
                    enemyModel = Content.Load<Model>("fbxContent/enemies/Monster2/Monster2"); // <--------------- Insert your Mesh here, need at least 2 keyframes

                    // loading Animation Models
                    idleM = Content.Load<Model>("fbxContent/enemies/Monster2/Monster2idle"); // <--------------------- The animation Meshes here
                    runM = Content.Load<Model>("fbxContent/enemies/Monster2/Monster2run");
                    fightM = Content.Load<Model>("fbxContent/enemies/Monster2/Monster2fight");
                    break;
                case EnemyType.Monster3:
                    // loading default mesh
                    enemyModel = Content.Load<Model>("fbxContent/enemies/Monster3/Monster3"); // <--------------- Insert your Mesh here, need at least 2 keyframes

                    // loading Animation Models
                    idleM = Content.Load<Model>("fbxContent/enemies/Monster3/Monster3idle"); // <--------------------- The animation Meshes here
                    runM = Content.Load<Model>("fbxContent/enemies/Monster3/Monster3run");
                    fightM = Content.Load<Model>("fbxContent/enemies/Monster3/Monster3fight");
                    break;
                case EnemyType.Monster4:
                    // loading default mesh
                    enemyModel = Content.Load<Model>("fbxContent/enemies/Monster4/Monster4"); // <--------------- Insert your Mesh here, need at least 2 keyframes

                    // loading Animation Models
                    idleM = Content.Load<Model>("fbxContent/enemies/Monster4/Monster4idle"); // <--------------------- The animation Meshes here
                    runM = Content.Load<Model>("fbxContent/enemies/Monster4/Monster4run");
                    fightM = Content.Load<Model>("fbxContent/enemies/Monster4/Monster4fight");
                    break;
                case EnemyType.Monster5:
                    // loading default mesh
                    enemyModel = Content.Load<Model>("fbxContent/enemies/Monster5/Monster5"); // <--------------- Insert your Mesh here, need at least 2 keyframes

                    // loading Animation Models
                    idleM = Content.Load<Model>("fbxContent/enemies/Monster5/Monster5idle"); // <--------------------- The animation Meshes here
                    runM = Content.Load<Model>("fbxContent/enemies/Monster5/Monster5run");
                    fightM = Content.Load<Model>("fbxContent/enemies/Monster5/Monster5fight");
                    break;
                case EnemyType.Monster6:
                    // loading default mesh
                    enemyModel = Content.Load<Model>("fbxContent/enemies/Monster6/Monster6"); // <--------------- Insert your Mesh here, need at least 2 keyframes

                    // loading Animation Models
                    idleM = Content.Load<Model>("fbxContent/enemies/Monster6/Monster6idle"); // <--------------------- The animation Meshes here
                    runM = Content.Load<Model>("fbxContent/enemies/Monster6/Monster6run");
                    fightM = Content.Load<Model>("fbxContent/enemies/Monster6/Monster6fight");
                    break;
                case EnemyType.Monster7:
                    // loading default mesh
                    enemyModel = Content.Load<Model>("fbxContent/enemies/Monster7/Monster7"); // <--------------- Insert your Mesh here, need at least 2 keyframes

                    // loading Animation Models
                    idleM = Content.Load<Model>("fbxContent/enemies/Monster7/Monster7idle"); // <--------------------- The animation Meshes here
                    runM = Content.Load<Model>("fbxContent/enemies/Monster7/Monster7run");
                    fightM = Content.Load<Model>("fbxContent/enemies/Monster7/Monster7fight");
                    break;
                case EnemyType.Monster8:
                    // loading default mesh
                    enemyModel = Content.Load<Model>("fbxContent/enemies/Monster8/Monster8"); // <--------------- Insert your Mesh here, need at least 2 keyframes

                    // loading Animation Models
                    idleM = Content.Load<Model>("fbxContent/enemies/Monster8/Monster8idle"); // <--------------------- The animation Meshes here
                    runM = Content.Load<Model>("fbxContent/enemies/Monster8/Monster8run");
                    fightM = Content.Load<Model>("fbxContent/enemies/Monster8/Monster8fight");
                    break;
            }


            // Loading Skinning Data
            idleSD = idleM.Tag as SkinningData;
            runSD = runM.Tag as SkinningData;
            fightSD = fightM.Tag as SkinningData;

            // Load an animation Player for each animation
            idleAP = new AnimationPlayer(idleSD);
            runAP = new AnimationPlayer(runSD);
            fightAP = new AnimationPlayer(fightSD);


            // loading different animations for the different enemies
            switch (EnemyType)
            {
                case EnemyType.Monster1:
                    // loading the animation clips
                    idleC = idleSD.AnimationClips["Idle"]; // <------------------------------------ The name of the animation in blender
                    runC = runSD.AnimationClips["Run"];
                    fightC = fightSD.AnimationClips["Fight"];
                    break;
                case EnemyType.Monster2:
                    // loading the animation clips
                    idleC = idleSD.AnimationClips["Idle"]; // <------------------------------------ The name of the animation in blender
                    runC = runSD.AnimationClips["Run"];
                    fightC = fightSD.AnimationClips["Fight"];
                    break;
                case EnemyType.Monster3:
                    // loading the animation clips
                    idleC = idleSD.AnimationClips["Idle"]; // <------------------------------------ The name of the animation in blender
                    runC = runSD.AnimationClips["Run"];
                    fightC = fightSD.AnimationClips["Fight"];
                    break;
                case EnemyType.Monster4:
                    // loading the animation clips
                    idleC = idleSD.AnimationClips["Idle"]; // <------------------------------------ The name of the animation in blender
                    runC = runSD.AnimationClips["Run"];
                    fightC = fightSD.AnimationClips["Fight"];
                    break;
                case EnemyType.Monster5:
                    // loading the animation clips
                    idleC = idleSD.AnimationClips["Idle"]; // <------------------------------------ The name of the animation in blender
                    runC = runSD.AnimationClips["Run"];
                    fightC = fightSD.AnimationClips["Fight"];
                    break;
                case EnemyType.Monster6:
                    // loading the animation clips
                    idleC = idleSD.AnimationClips["Idle"]; // <------------------------------------ The name of the animation in blender
                    runC = runSD.AnimationClips["Run"];
                    fightC = fightSD.AnimationClips["Fight"];
                    break;
                case EnemyType.Monster7:
                    // loading the animation clips
                    idleC = idleSD.AnimationClips["Idle"]; // <------------------------------------ The name of the animation in blender
                    runC = runSD.AnimationClips["Run"];
                    fightC = fightSD.AnimationClips["Fight"];
                    break;
                case EnemyType.Monster8:
                    // loading the animation clips
                    idleC = idleSD.AnimationClips["Idle"]; // <------------------------------------ The name of the animation in blender
                    runC = runSD.AnimationClips["Run"];
                    fightC = fightSD.AnimationClips["Fight"];
                    break;
            }

            // Safty Start Animations
            idleAP.StartClip(idleC);
            runAP.StartClip(runC);
            fightAP.StartClip(fightC);

            //assign the specific animationTimes
            idleDuration = (float)idleC.Duration.TotalMilliseconds / 1f;
            runDuration = (float)runC.Duration.TotalMilliseconds / 1f;
            fightDuration = (float)fightC.Duration.TotalMilliseconds / 1f;


            stateTime = 0;
            // Duration of Blending Animations in milliseconds
            fixedBlendDuration = 500;
            AttackThreshold = fightDuration;

            //Console.WriteLine(EnemyType + " hat idleZeit:" + idleDuration + ". hat runDuration:" + runDuration + ", hat fightDuration:" + fightDuration);


            // Healthbar Content
            health0 = Content.Load<Model>("fbxContent/healthbars/h0/healthbar");
            health1 = Content.Load<Model>("fbxContent/healthbars/h1/healthbar");
            health2 = Content.Load<Model>("fbxContent/healthbars/h2/healthbar");
            health3 = Content.Load<Model>("fbxContent/healthbars/h3/healthbar");
            health4 = Content.Load<Model>("fbxContent/healthbars/h4/healthbar");
            health5 = Content.Load<Model>("fbxContent/healthbars/h5/healthbar");
            health6 = Content.Load<Model>("fbxContent/healthbars/h6/healthbar");
            health7 = Content.Load<Model>("fbxContent/healthbars/h7/healthbar");
            health8 = Content.Load<Model>("fbxContent/healthbars/h8/healthbar");
            health9 = Content.Load<Model>("fbxContent/healthbars/h9/healthbar");
            health10 = Content.Load<Model>("fbxContent/healthbars/h10/healthbar");

            // The custom effect
            Effect customEffect = Content.Load<Effect>("shaders/CustomSkinnedEffect");

            // Apply it to the model
            foreach (ModelMesh mesh in enemyModel.Meshes)
            {
                foreach (ModelMeshPart part in mesh.MeshParts)
                {
                    SkinnedEffect skinnedEffect = part.Effect as SkinnedEffect;
                    if (skinnedEffect != null)
                    {
                        // Create new custom skinned effect from our base effect
                        CustomSkinnedEffect custom = new CustomSkinnedEffect(customEffect);
                        custom.CopyFromSkinnedEffect(skinnedEffect);

                        part.Effect = custom;
                    }
                }
            }

        }


        public void Update(GameTime gameTime)
        {

            // Computing the distance to the player
            distanceToPlayer = (float)EuclideanDistance(this.Position, Player.Instance.Position);

            if (distanceToPlayer < 60)
            {
                //Update Temp EnemyState
                TempEnemyState = CurrentEnemyState;

                oldPosition = Position;

                attackTimer += gameTime.ElapsedGameTime.Milliseconds;

                enemyAi.UpdateAi(Position);
                Vector2 newPosition = Vector2.Add(enemyAi.TrackingDirection, Position);
                ViewAngle = -enemyAi.TrackingAngle;

                // object collision
                if (IsAlive)
                {
                    if (currentEnvironment.enemyArray[(int)Math.Round(newPosition.X / 10), (int)Math.Round(newPosition.Y / 10)].Count < 15)
                    {
                        if (Color.White == collHandler.GetCollisionColor(new Vector2(Position.X, newPosition.Y), collHandler.Controller.CollisionColors, 2.0f))
                        {
                            Position.Y = newPosition.Y;
                        }
                        if (Color.White == collHandler.GetCollisionColor(new Vector2(newPosition.X, Position.Y), collHandler.Controller.CollisionColors, 2.0f))
                        {
                            Position.X = newPosition.X;
                        }

                        Vector2 currentGridPos = new Vector2((float)Math.Round(newPosition.X / gridSize), (float)Math.Round(newPosition.Y / gridSize));
                        for (int i = -1; i < 2; i++)
                        {
                            for (int j = -1; j < 2; j++)
                            {
                                List<Enemy> currentBlockEnemyList = currentEnvironment.enemyArray[(int)currentGridPos.X + i, (int)currentGridPos.Y + j];
                                if (currentBlockEnemyList.Count <= 0) continue;

                                // enemy - player collision
                                var pdx = (Position.X - Player.Instance.Position.X) * (Position.X - Player.Instance.Position.X);
                                var pdy = (Position.Y - Player.Instance.Position.Y) * (Position.Y - Player.Instance.Position.Y);
                                if (Math.Sqrt(pdx + pdy) < (CircleCollision + Player.Instance.CollisionRadius))
                                {
                                    Position.X = oldPosition.X;
                                    Position.Y = oldPosition.Y;
                                }

                                // enemy - enemy collision
                                foreach (var enemy in currentBlockEnemyList)
                                {
                                    if (this != enemy)
                                    {
                                        var dx1 = (Position.X - enemy.Position.X) * (Position.X - enemy.Position.X);
                                        var dy1 = (oldPosition.Y - enemy.Position.Y) * (oldPosition.Y - enemy.Position.Y);
                                        if (Math.Sqrt(dx1 + dy1) < (CircleCollision + enemy.CircleCollision / 2))
                                        {
                                            Position.X = oldPosition.X;
                                        }
                                        var dx2 = (oldPosition.X - enemy.Position.X) * (oldPosition.X - enemy.Position.X);
                                        var dy2 = (Position.Y - enemy.Position.Y) * (Position.Y - enemy.Position.Y);
                                        if (Math.Sqrt(dx2 + dy2) < (CircleCollision + enemy.CircleCollision / 2))
                                        {
                                            Position.Y = oldPosition.Y;
                                        }
                                    }
                                }
                            }
                        }

                        currentEnvironment.enemyArray[(int)Math.Round(oldPosition.X / 10), (int)Math.Round(oldPosition.Y / 10)].Remove(this);
                        currentEnvironment.enemyArray[(int)Math.Round(Position.X / 10), (int)Math.Round(Position.Y / 10)].Add(this);
                    }
                }

                // interaction
                if (IsAlive)
                {
                    isAttacking = false;
                    
                    if (EuclideanDistance(Position, Player.Instance.Position) <= AttackRange)
                    {
                        isAttacking = true;
                    }
                    if (attackTimer >= AttackThreshold && EuclideanDistance(Position, Player.Instance.Position) <= AttackRange)
                    {
                        Player.Instance.Attack(Damage);
                        attackTimer = 0;
                    }
                    // The breathing, which means if the creature is hit, it turns red for a short while
                    if (isHit)
                    {
                        isHit = false;
                        breathingOn = true;
                        overlayBool = true;
                    }
                    if (breathingOn)
                        breathing();
                }




                // setting the right EnemyStates

                if (oldPosition.X != Position.X || oldPosition.Y != Position.Y && stateTime == 0)
                {
                    CurrentEnemyState = EnemyState.Run;
                    stateTime = runDuration;
                    fixedBlendDuration = 150;
                }
                else if (isAttacking == true && stateTime == 0)
                {
                    CurrentEnemyState = EnemyState.Fight;
                    stateTime = fightDuration;
                    fixedBlendDuration = 250;
                }
                else if (stateTime == 0)
                {
                    CurrentEnemyState = EnemyState.Idle;
                    stateTime = idleDuration;
                    fixedBlendDuration = 100;
                }

                if (stateTime > 0)
                {
                    stateTime -= gameTime.ElapsedGameTime.Milliseconds;
                }
                else stateTime = 0;

                //Console.WriteLine(CurrentEnemyState + ", " + LastEnemyState);


                //update only the animation which is required if the npcstate changed
                //Update the active animation

                switch (CurrentEnemyState)
                {
                    case EnemyState.Idle:
                        idleAP.Update(gameTime.ElapsedGameTime, true, Matrix.Identity);
                        break;
                    case EnemyState.Run:
                        runAP.Update(gameTime.ElapsedGameTime, true, Matrix.Identity);
                        break;
                    case EnemyState.Fight:
                        fightAP.Update(gameTime.ElapsedGameTime, true, Matrix.Identity);
                        break;
                }

                // Secure, that the last NPCstate is always a other NPCstate, than the acutal
                if (CurrentEnemyState != TempEnemyState)
                {
                    LastEnemyState = TempEnemyState;
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
                    switch (LastEnemyState)
                    {
                        case EnemyState.Idle:
                            idleAP.Update(gameTime.ElapsedGameTime, true, Matrix.Identity);
                            break;
                        case EnemyState.Run:
                            runAP.Update(gameTime.ElapsedGameTime, true, Matrix.Identity);
                            break;
                        case EnemyState.Fight:
                            fightAP.Update(gameTime.ElapsedGameTime, true, Matrix.Identity);
                            break;
                    }
                }
            }

            if (!IsAlive && spawnHealthBall)
            {
                currentEnvironment.Items.Add(new GameObject(Content.Load<Model>("fbxContent/miscellaneous/healthOrb/healthOrb"), new Vector3(Position.X, 0, Position.Y), 0, "healthOrb", false));
                spawnHealthBall = false;
            }
        }

        bool onSameGridElement(Vector2 a, Vector2 b)
        {
            Vector2 aOnGrid = new Vector2((int)Math.Round(a.X / Settings.Instance.GridSize), (int)Math.Round(a.Y / Settings.Instance.GridSize));
            Vector2 bOnGrid = new Vector2((int)Math.Round(b.X / Settings.Instance.GridSize), (int)Math.Round(b.Y / Settings.Instance.GridSize));

            return aOnGrid.X == bOnGrid.X && aOnGrid.Y == bOnGrid.Y;
        }

        public void Attack(float damage)
        {
            CurrentHealth -= damage;

            // Factor to get the scale to 100
            float hpFactor = 100 / MaxEnemyHealth;
            // Puts the health in a value between 0 and 10
            HpForHealthbar = (int)(CurrentHealth * hpFactor + 10) / 10;
            // Sets the healthbar to h0 to h10
            //healthbar = "h"+hpForHealthbar.ToString();
            //Console.WriteLine(healthbar);
            isHit = true;


            if (!(CurrentHealth <= 0)) return;
            if (currentEnvironment.enemyArray[(int)Math.Round(Position.X / 10), (int)Math.Round(Position.Y / 10)].Remove(this))
            {
                IsAlive = false;

                // Add killed monsters
                String killOut = "";
                foreach (Quest activeQuest in QuestController.Instance.ActiveQuests)
                {
                    activeQuest.Tasks.TryGetValue("kill", out killOut);    
                }
                if (killOut != null)
                {
                    string[] enemyAndCount = killOut.Split(',');
                    if (EnemyType.ToString() == enemyAndCount[0])
                    {
                        if (QuestController.Instance.KilledEnemies.ContainsKey(enemyAndCount[0]))
                        {
                            QuestController.Instance.KilledEnemies[enemyAndCount[0]] += 1;
                        }
                        else
                        {
                            QuestController.Instance.KilledEnemies.Add(enemyAndCount[0], 1);
                        }
                    }
                }
            }

            Player.Instance.Experience += (int)Math.Round(50 * Math.Pow(Math.Sqrt(Player.Instance.Level), 2));

            List<Item> droppedItems = ItemTestClass.Instance.ItemGeneratorMonster(Player.Instance.Level, EnemyType);
            foreach (Item item in droppedItems)
            {
                TextMessage.Instance.NewMessage("Item " + item.Name + " Lvl." + item.Lvl + " gefunden!", Color.Blue);
                if (Player.Instance.PlayerInventory.Count < 18)
                {
                    Player.Instance.PlayerInventory.Add(item);    
                }
                else
                {
                    Player.Instance.ItemsDropped.Add(item);
                }
            }
        }

        public void SetAsAlive()
        {
            IsAlive = true;
            currentEnvironment.enemyArray[(int)Math.Round(Position.X / 10), (int)Math.Round(Position.Y / 10)].Add(this);
        }

        private double EuclideanDistance(Vector2 p1, Vector2 p2)
        {
            return Math.Sqrt(Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2));
        }

        public void breathing()
        {
            if (this.overlayBool)
                this.ambientIntensity += 0.04f;
            else
                this.ambientIntensity -= 0.04f;
            if (this.ambientIntensity > 0.98)
                this.overlayBool = false;
            if (this.ambientIntensity < 0.05f && !this.overlayBool)
            {
                this.ambientIntensity = 0.0f;
                breathingOn = false;
                //Console.WriteLine(distanceToPlayer);
            }
        }

        public void Draw(Matrix world, Matrix view, Matrix projection)
        {
            if (distanceToPlayer < 60)
            {
                if (IsAlive)
                {
                    // Bone updates for each required animation   
                    switch (CurrentEnemyState)
                    {
                        case EnemyState.Idle:
                            idleBones = idleAP.GetSkinTransforms();
                            break;
                        case EnemyState.Run:
                            runBones = runAP.GetSkinTransforms();
                            break;
                        case EnemyState.Fight:
                            fightBones = fightAP.GetSkinTransforms();
                            break;
                    }

                    if (isBlending)
                    {
                        // Bone updates for each required animation for blending 
                        switch (LastEnemyState)
                        {
                            case EnemyState.Idle:
                                blendingBones = idleAP.GetSkinTransforms();
                                break;
                            case EnemyState.Run:
                                blendingBones = runAP.GetSkinTransforms();
                                break;
                            case EnemyState.Fight:
                                blendingBones = fightAP.GetSkinTransforms();
                                break;
                        }
                    }

                    Model HPModel;
                    switch (HpForHealthbar)
                    {
                        case 0:
                            HPModel = health0;
                            break;
                        case 1:
                            HPModel = health1;
                            break;
                        case 2:
                            HPModel = health2;
                            break;
                        case 3:
                            HPModel = health3;
                            break;
                        case 4:
                            HPModel = health4;
                            break;
                        case 5:
                            HPModel = health5;
                            break;
                        case 6:
                            HPModel = health6;
                            break;
                        case 7:
                            HPModel = health7;
                            break;
                        case 8:
                            HPModel = health8;
                            break;
                        case 9:
                            HPModel = health9;
                            break;
                        case 10:
                            HPModel = health10;
                            break;
                        default:
                            HPModel = health10;
                            break;
                    }

                    // Drawing the healthbar
                    if (distanceToPlayer < 20)
                    {
                        foreach (ModelMesh mesh in HPModel.Meshes)
                        {
                            foreach (BasicEffect effect in mesh.Effects)
                            {
                                effect.EnableDefaultLighting();
                                effect.World = Matrix.CreateRotationX((float)(-0.5 * Math.PI)) * Matrix.CreateRotationY(ViewAngle + (float)(0.5 * Math.PI)) * Matrix.CreateTranslation(new Vector3(Position.X, 3, Position.Y)) * world;
                                effect.View = view;
                                effect.Projection = projection;
                                effect.SpecularColor = new Vector3(0.25f);
                                effect.SpecularPower = 16;
                                effect.PreferPerPixelLighting = true;
                            }
                            mesh.Draw();
                        }
                    }

                    foreach (ModelMesh mesh in enemyModel.Meshes)
                    {

                        //Console.WriteLine(mesh.Name);
                        // Only renders the Right assigned Weapon

                        //foreach (SkinnedEffect effect in mesh.Effects)
                        foreach (CustomSkinnedEffect effect in mesh.Effects)
                        {
                            //Draw the Bones which are required
                            if (isBlending)
                            {
                                float percentageOfBlending = activeBlendTime / fixedBlendDuration;
                                switch (CurrentEnemyState)
                                {
                                    case EnemyState.Idle:
                                        for (int i = 0; i < blendingBones.Length; i++)
                                        {
                                            blendingBones[i] = Matrix.Lerp(blendingBones[i], idleBones[i], 1 - percentageOfBlending);
                                        }
                                        effect.SetBoneTransforms(blendingBones);
                                        break;
                                    case EnemyState.Run:
                                        for (int i = 0; i < blendingBones.Length; i++)
                                        {
                                            blendingBones[i] = Matrix.Lerp(blendingBones[i], runBones[i], 1 - percentageOfBlending);
                                        }
                                        effect.SetBoneTransforms(blendingBones);
                                        break;
                                    case EnemyState.Fight:
                                        for (int i = 0; i < blendingBones.Length; i++)
                                        {
                                            blendingBones[i] = Matrix.Lerp(blendingBones[i], fightBones[i], 1 - percentageOfBlending);
                                        }
                                        effect.SetBoneTransforms(blendingBones);
                                        break;
                                }
                            }
                            else
                            {
                                switch (CurrentEnemyState)
                                {
                                    case EnemyState.Idle:
                                        effect.SetBoneTransforms(idleBones);
                                        break;
                                    case EnemyState.Run:
                                        effect.SetBoneTransforms(runBones);
                                        break;
                                    case EnemyState.Fight:
                                        effect.SetBoneTransforms(fightBones);
                                        break;
                                }
                            }


                            effect.EnableDefaultLighting();
                            effect.World = Matrix.CreateRotationX((float)(-0.5 * Math.PI)) * Matrix.CreateRotationY(ViewAngle + (float)(0.5 * Math.PI)) * Matrix.CreateTranslation(new Vector3(Position.X, 0, Position.Y)) * world;
                            effect.View = view;
                            effect.Projection = projection;
                            effect.SpecularColor = new Vector3(0.25f);
                            effect.SpecularPower = 16;
                            effect.PreferPerPixelLighting = true;
                            effect.Parameters["AmbientIntensity"].SetValue(ambientIntensity);
                            effect.Parameters["DistanceToPlayer"].SetValue(distanceToPlayer);


                            // Textures

                            // effect.tectures = ....  
                        }

                        mesh.Draw();
                    }

                }
            }
        }

    }
}
