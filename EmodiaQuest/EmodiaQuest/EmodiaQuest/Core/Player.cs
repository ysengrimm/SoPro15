﻿using System;
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

        public float Hp;
        public float Armor;

        public float PlayerSpeed = 0.5f;
        public float RotationSpeed = 1.5f;

        public Vector2 Position;
        private Vector2 movement; // future position
        private Vector2 lastPos; //position from last step (fixes false kamera focus)
        public float MovementOffset, ItemOffset;
        public float Angle;

        private Model playerModel;
        private AnimationPlayer animationPlayer;

        private CollisionHandler collisionHandler;

        private Vector2 windowSize;

        public ContentManager content;

        public Model Model
        {
            set { playerModel = value; }
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

            // Look up our custom skinning information.
            SkinningData skinningData = playerModel.Tag as SkinningData;

            if (skinningData == null)
                throw new InvalidOperationException
                    ("This model does not contain a SkinningData tag.");

            // Create an animation player, and start decoding an animation clip.
            animationPlayer = new AnimationPlayer(skinningData);
            Console.WriteLine("Holla");
            Console.WriteLine("Test:" + skinningData.AnimationClips.ElementAt(0));

            AnimationClip clip = skinningData.AnimationClips["Default Take"];

            animationPlayer.StartClip(clip);

        }

        public void LoadContent()
        {
            
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

            // not really necessary beacuse only vertical rotation
            //float mousePosYNorm = mouseState.Y / windowSize.Y;


            animationPlayer.Update(gameTime.ElapsedGameTime, true, Matrix.Identity);

            //Console.Out.WriteLine(Position);
        }

        public void Draw(Matrix world, Matrix view, Matrix projection)
        {
            // Bone update
            Matrix[] bones = animationPlayer.GetSkinTransforms();

            foreach (ModelMesh mesh in playerModel.Meshes)
            {
                foreach (SkinnedEffect effect in mesh.Effects)
                {
                    effect.SetBoneTransforms(bones);
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
