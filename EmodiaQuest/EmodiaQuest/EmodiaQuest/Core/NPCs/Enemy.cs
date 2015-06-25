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
    abstract class Enemy
    {
        // The Model
        private Model enemyModel, standingM, walkingM, jumpingM, swordfightingM, bowfightingM, runningM, idleM;
        // Skinning Data
        private SkinningData standingSD, walkingSD, jumpingSD, swordfightingSD, bowfightingSD, runningSD, idleSD;
        // The animation Player
        private AnimationPlayer standingAP, walkingAP, jumpingAP, swordfightingAP, bowfightingAP, runningAP, idleAP;
        // The animation Clips, which will be used by the model
        private AnimationClip standingC, walkingC, jumpingC, swordfightingC, bowfightingC, runningC, idleC;
        // The Bone Matrices for each animation
        private Matrix[] blendingBones, standingBones, walkingBones, jumpingBones, swordfightingBones, bowfightingBones, runningBones, idleBones;
        // The playerState, which will be needed to update the right animation
        public EnemyState EnemyState = EnemyState.Standing;
        public EnemyState LastEnemyState = EnemyState.Standing;

        // Enemystats
        public Vector2 Position;
        public float Hp;
        public float Armor;
        public float MovementSpeed;


        // Methods
        public void LoadContent(ContentManager content) { }
        public void Draw(Matrix world, Matrix view, Matrix projection) { }
        public void Update(GameTime gameTime) { }
    }


}
