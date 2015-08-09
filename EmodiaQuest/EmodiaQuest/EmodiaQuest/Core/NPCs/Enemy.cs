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
        // Variables for Ai
        private EnvironmentController currentEnvironment;
        private Ai enemyAi;

        private bool isAttacking;

        // Enemystats
        public Vector2 Position;
        public float ViewAngle;

        private Vector2 oldPosition;
        public float MaxEnemyHealth;
        public float Armor;
        public float MovementSpeed;
        public float TrackingRadius;
        public float CircleCollision;
        public float AttackRange;
        public float Damage;

        private float attackTimer;
        public float AttackThreshold;
        public float AttackSpeed;

        public EnemyType EnemyTyp;

        private float gridSize = Settings.Instance.GridSize;

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


        public Enemy(Vector2 position, EnvironmentController currentEnvironment, EnemyType enemyTyp)
        {
            this.currentEnvironment = currentEnvironment;
            this.Position = position;
            this.EnemyTyp = enemyTyp;
            currentEnvironment.enemyArray[(int)Math.Round(Position.X / 10), (int)Math.Round(Position.Y / 10)].Add(this);
            this.isAttacking = false;
        }


        // Methods
        public void LoadContent(ContentManager content)
        {
            this.Content = content;
            // Normal variables for each different enemy Type
            switch (EnemyTyp)
            {
                case EnemyType.NPCTest:
                    MovementSpeed = Settings.Instance.HumanEnemySpeed;
                    MaxEnemyHealth = Settings.Instance.MaxHumanEnemyHealth;
                    AttackRange = 7;
                    Damage = 5;

                    // movement
                    TrackingRadius = 50f;
                    MovementSpeed = 0.25f;
                    enemyAi = new Ai(Position, CurrentEnemyState, LastEnemyState, TrackingRadius, MovementSpeed, currentEnvironment);
                    ViewAngle = -enemyAi.TrackingAngle;

                    attackTimer = 0;
                    AttackThreshold = 20;
                    AttackSpeed = 0.5f;

                    // collision
                    CircleCollision = 1.0f;
                    break;
                case EnemyType.Monster1:
                    MovementSpeed = Settings.Instance.HumanEnemySpeed;
                    MaxEnemyHealth = Settings.Instance.MaxHumanEnemyHealth;
                    AttackRange = 7;
                    Damage = 5;

                    // movement
                    TrackingRadius = 50f;
                    MovementSpeed = 0.25f;
                    enemyAi = new Ai(Position, CurrentEnemyState, LastEnemyState, TrackingRadius, MovementSpeed, currentEnvironment);
                    ViewAngle = -enemyAi.TrackingAngle;

                    attackTimer = 0;
                    AttackThreshold = 20;
                    AttackSpeed = 0.5f;

                    // collision
                    CircleCollision = 1.0f;
                    break;
                case EnemyType.Monster2:
                    MovementSpeed = Settings.Instance.HumanEnemySpeed;
                    MaxEnemyHealth = Settings.Instance.MaxHumanEnemyHealth;
                    AttackRange = 7;
                    Damage = 5;

                    // movement
                    TrackingRadius = 50f;
                    MovementSpeed = 0.25f;
                    enemyAi = new Ai(Position, CurrentEnemyState, LastEnemyState, TrackingRadius, MovementSpeed, currentEnvironment);
                    ViewAngle = -enemyAi.TrackingAngle;

                    attackTimer = 0;
                    AttackThreshold = 20;
                    AttackSpeed = 0.5f;

                    // collision
                    CircleCollision = 1.0f;
                    break;
                case EnemyType.Monster3:
                    MovementSpeed = Settings.Instance.HumanEnemySpeed;
                    MaxEnemyHealth = Settings.Instance.MaxHumanEnemyHealth;
                    AttackRange = 7;
                    Damage = 5;

                    // movement
                    TrackingRadius = 50f;
                    MovementSpeed = 0.25f;
                    enemyAi = new Ai(Position, CurrentEnemyState, LastEnemyState, TrackingRadius, MovementSpeed, currentEnvironment);
                    ViewAngle = -enemyAi.TrackingAngle;

                    attackTimer = 0;
                    AttackThreshold = 20;
                    AttackSpeed = 0.5f;

                    // collision
                    CircleCollision = 1.0f;
                    break;
            }

            collHandler = CollisionHandler.Instance;

            IsAlive = true;

            // Loading the different meshes for the enemies
            switch (EnemyTyp)
            {
                case EnemyType.NPCTest:
                    // loading default mesh
                    enemyModel = content.Load<Model>("fbxContent/NPC/NPC_male_idle"); // <--------------- Insert your Mesh here, need at least 2 keyframes

                    // loading Animation Models
                    idleM = Content.Load<Model>("fbxContent/NPC/NPC_male_idle"); // <--------------------- The animation Meshes here
                    runM = Content.Load<Model>("fbxContent/NPC/NPC_male_idle");
                    fightM = Content.Load<Model>("fbxContent/NPC/NPC_male_idle");
                    break;

                case EnemyType.Monster1:
                    // loading default mesh
                    enemyModel = content.Load<Model>("fbxContent/enemies/Monster1/Monster1"); // <--------------- Insert your Mesh here, need at least 2 keyframes

                    // loading Animation Models
                    idleM = Content.Load<Model>("fbxContent/enemies/Monster1/Monster1idle"); // <--------------------- The animation Meshes here
                    runM = Content.Load<Model>("fbxContent/enemies/Monster1/Monster1run");
                    fightM = Content.Load<Model>("fbxContent/enemies/Monster1/Monster1fight");
                    break;
                case EnemyType.Monster2:
                    // loading default mesh
                    enemyModel = content.Load<Model>("fbxContent/enemies/Monster2/Monster2"); // <--------------- Insert your Mesh here, need at least 2 keyframes

                    // loading Animation Models
                    idleM = Content.Load<Model>("fbxContent/enemies/Monster2/Monster2idle"); // <--------------------- The animation Meshes here
                    runM = Content.Load<Model>("fbxContent/enemies/Monster2/Monster2run");
                    fightM = Content.Load<Model>("fbxContent/enemies/Monster2/Monster2fight");
                    break;
                case EnemyType.Monster3:
                    // loading default mesh
                    enemyModel = content.Load<Model>("fbxContent/enemies/Monster3/Monster3"); // <--------------- Insert your Mesh here, need at least 2 keyframes

                    // loading Animation Models
                    idleM = Content.Load<Model>("fbxContent/enemies/Monster3/Monster3idle"); // <--------------------- The animation Meshes here
                    runM = Content.Load<Model>("fbxContent/enemies/Monster3/Monster3run");
                    fightM = Content.Load<Model>("fbxContent/enemies/Monster3/Monster3fight");
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
            switch (EnemyTyp)
            {
                case EnemyType.NPCTest:
                    // loading the animation clips
                    idleC = idleSD.AnimationClips["idle"]; // <------------------------------------ The name of the animation in blender
                    runC = runSD.AnimationClips["idle"];
                    fightC = fightSD.AnimationClips["idle"];
                    break;
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
            }

            // Safty Start Animations
            idleAP.StartClip(idleC);
            runAP.StartClip(runC);
            fightAP.StartClip(fightC);

            //assign the specific animationTimes
            idleDuration = (float) idleC.Duration.TotalMilliseconds / 1f;
            runDuration = (float) runC.Duration.TotalMilliseconds / 1f;
            fightDuration = (float) fightC.Duration.TotalMilliseconds / 1f;


            stateTime = 0;
            // Duration of Blending Animations in milliseconds
            fixedBlendDuration = 500;
            AttackSpeed = fightDuration / 1000;
            //Console.WriteLine(EnemyTyp + " hat idleZeit:" + idleDuration + ". hat runDuration:" + runDuration + ", hat fightDuration:" + fightDuration);
        }


        public void Update(GameTime gameTime)
        {
            


            //Update Temp EnemyState
            TempEnemyState = CurrentEnemyState;

            oldPosition = Position;

            attackTimer += AttackSpeed;

            enemyAi.UpdateAi(Position);
            Vector2 newPosition = Vector2.Add(enemyAi.TrackingDirection, Position);
            ViewAngle = -enemyAi.TrackingAngle;

            // object collision
            if (IsAlive && Color.White == collHandler.GetCollisionColor(new Vector2(Position.X, newPosition.Y), collHandler.Controller.CollisionColors, 2.0f))
            {
                if (currentEnvironment.enemyArray[(int)Math.Round(newPosition.X / 10), (int)Math.Round(newPosition.Y / 10)].Count < 5 && !onSameGridElement(newPosition, Player.Instance.Position))
                {
                    Position.Y = newPosition.Y;
                    Position.X = newPosition.X;

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
                                    var dx = (Position.X - enemy.Position.X) * (Position.X - enemy.Position.X);
                                    var dy = (Position.Y - enemy.Position.Y) * (Position.Y - enemy.Position.Y);
                                    if (Math.Sqrt(dx + dy) < (CircleCollision + enemy.CircleCollision))
                                    {
                                        Position.X = oldPosition.X;
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
                for (int i = -1; i < 2; i++)
                {
                    for (int j = -1; j < 2; j++)
                    {
                        if(onSameGridElement(new Vector2(Position.X + i, Position.Y + j), Player.Instance.Position) && EuclideanDistance(Position, Player.Instance.Position) <= AttackRange)
                        {
                            isAttacking = true;
                        }
                        if (attackTimer >= AttackThreshold && onSameGridElement(new Vector2(Position.X + i, Position.Y + j), Player.Instance.Position) && EuclideanDistance(Position, Player.Instance.Position) <= AttackRange)
                        {
                            Player.Instance.Attack(Damage);
                            attackTimer = 0;
                        }
                    }
                }
            }

            // setting the right EnemyStates

            if(oldPosition.X != Position.X || oldPosition.Y != Position.Y)
            {
                CurrentEnemyState = EnemyState.Run;
                stateTime = runDuration;
                fixedBlendDuration = 150;
            }
            else if (isAttacking == true)
            {
                CurrentEnemyState = EnemyState.Fight;
                stateTime = fightDuration;
                fixedBlendDuration = 250;
            }
            else
            {
                CurrentEnemyState = EnemyState.Idle;
                stateTime = idleDuration;
                fixedBlendDuration = 100;
            }

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

        bool onSameGridElement(Vector2 a, Vector2 b)
        {
            Vector2 aOnGrid = new Vector2((int)Math.Round(a.X / Settings.Instance.GridSize), (int)Math.Round(a.Y / Settings.Instance.GridSize));
            Vector2 bOnGrid = new Vector2((int)Math.Round(b.X / Settings.Instance.GridSize), (int)Math.Round(b.Y / Settings.Instance.GridSize));

            return aOnGrid.X == bOnGrid.X && aOnGrid.Y == bOnGrid.Y;
        }

        public void Attack(float damage)
        {
            MaxEnemyHealth -= damage;

            if (!(MaxEnemyHealth <= 0)) return;
            if (currentEnvironment.enemyArray[(int)Math.Round(Position.X / 10), (int)Math.Round(Position.Y / 10)].Remove(this))
            {
                IsAlive = false;
            }
            Console.WriteLine("Enemy at " + Position + " died");
        }

        public void SetAsAlive()
        {
            IsAlive = true;
            currentEnvironment.enemyArray[(int) Math.Round(Position.X/10), (int) Math.Round(Position.Y/10)].Add(this);
        }

        private double EuclideanDistance(Vector2 p1, Vector2 p2)
        {
            return Math.Sqrt(Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2));
        }

        public void Draw(Matrix world, Matrix view, Matrix projection)
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

            foreach (ModelMesh mesh in enemyModel.Meshes)
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
                        effect.World = Matrix.CreateRotationX((float)(-0.5 * Math.PI)) * Matrix.CreateRotationY(ViewAngle + (float) (0.5 * Math.PI)) * Matrix.CreateTranslation(new Vector3(Position.X, 0, Position.Y)) * world;
                        effect.View = view;
                        effect.Projection = projection;
                        effect.SpecularColor = new Vector3(0.25f);
                        effect.SpecularPower = 16;
                        effect.PreferPerPixelLighting = true;
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
