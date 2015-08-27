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

namespace EmodiaQuest.Core
{

    public class GameObject
    {
        public Vector3 position;
        public int rotation;
        public Model model;
        private Vector3 ambi = new Vector3(0.05333332f, 0.09882354f, 0.1819608f) * 3;
        private Vector3 diff = new Vector3(0.1178511f, 0.05631156f, 0.03418359f) * 2;
        private Vector3 spec = new Vector3(0.25f, 0.25f, 0.25f);
        private Vector3 emis = new Vector3(0.0f, 0.0f, 0.0f);

        /// <summary>
        /// Creates a new map from a pixelmap
        /// <param name="model">A model for the .</param>
        /// <param name="position">Position of object.</param>
        /// <param name="rotation">Creates rotation of object: 0 -> 0°, 1 -> 90° etc.</param>
        /// </summary>
        public GameObject(Model model, Vector3 position, int rotation)
        {
            this.model = model;
            this.position = position;
            if (rotation == 0 || rotation == 1 || rotation == 2 || rotation == 3)
                this.rotation = rotation;
        }
        /*
  
        public void drawGameobject(Matrix world, Matrix view, Matrix projection)
        {
            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();
                    effect.World = world * Matrix.CreateRotationY(rotation * (float)Math.PI / 2) * Matrix.CreateTranslation(position);
                    effect.View = view;
                    effect.Projection = projection;
                }
                mesh.Draw();
            }
        }
        */
        public double EuclideanDistance(Vector2 p1, Vector2 p2)
        {
            return Math.Sqrt(Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2));
        }

        /// <summary>
        /// Creates a new map from a pixelmap
        /// <param name="world">World matrix for multiplying other functions.</param>
        /// <param name="projektion">A projektion-matrix.</param>
        /// <param name="view">A view-matrix.</param>
        /// </summary>      
        public void drawGameobject(Matrix world, Matrix view, Matrix projection)
        {
            if (EmodiaQuest.Core.Ingame.Instance.ActiveWorld == WorldState.Safeworld)
            {
                foreach (ModelMesh mesh in model.Meshes)
                {
                    foreach (BasicEffect effect in mesh.Effects)
                    {
                        effect.EnableDefaultLighting();
                        effect.World = world * Matrix.CreateRotationY(rotation * (float)Math.PI / 2) * Matrix.CreateTranslation(position);
                        effect.View = view;
                        effect.Projection = projection;

                        //Ambient: {X:0,05333332 Y:0,09882354 Z:0,1819608}         
                        //Diffuse: {X:0,1178511 Y:0,05631156 Z:0,03418359}
                        //Specular: {X:0,25 Y:0,25 Z:0,25}
                        //Emissive: {X:0 Y:0 Z:0}
                    }
                    mesh.Draw();
                }
            }
            else
            {
                float distanceToPlayer = (float)EuclideanDistance(new Vector2(this.position.X, this.position.Z), Player.Instance.Position);
                foreach (ModelMesh mesh in model.Meshes)
                {
                    foreach (BasicEffect effect in mesh.Effects)
                    {
                        //effect.EnableDefaultLighting();
                        effect.World = world * Matrix.CreateRotationY(rotation * (float)Math.PI / 2) * Matrix.CreateTranslation(position);
                        effect.View = view;
                        effect.Projection = projection;
                        if (distanceToPlayer > 100)
                        {
                            effect.AmbientLightColor = new Vector3(0, 0, 0);
                            effect.DiffuseColor = new Vector3(0, 0, 0);
                            effect.SpecularColor = new Vector3(0, 0, 0);
                            effect.EmissiveColor = new Vector3(0, 0, 0);
                        }
                        else
                        {
                            effect.AmbientLightColor = ambi * (1.0f - distanceToPlayer * 0.01f);
                            effect.DiffuseColor = diff * (1.0f - distanceToPlayer * 0.01f);
                            effect.SpecularColor = spec * (1.0f - distanceToPlayer * 0.01f);
                            //effect.EmissiveColor = emis * (1.0f - distanceToPlayer * 0.005f);
                        }

                    }
                    mesh.Draw();
                }
            }

        }



        //foreach (ModelMesh mesh in model.Meshes)
        //{
        //    foreach (ModelMeshPart part in mesh.MeshParts)
        //    {
        //        part.Effect = EmodiaQuest.Core.Ingame.Instance.DiffuseEffect;
        //        EmodiaQuest.Core.Ingame.Instance.DiffuseEffect.Parameters["World"].SetValue(world * mesh.ParentBone.Transform);
        //        EmodiaQuest.Core.Ingame.Instance.DiffuseEffect.Parameters["View"].SetValue(view);
        //        EmodiaQuest.Core.Ingame.Instance.DiffuseEffect.Parameters["Projection"].SetValue(projection);


        //        //ambientEffect.Parameters["AmbientColor"].SetValue(Color.Green.ToVector4());
        //        //ambientEffect.Parameters["AmbientIntensity"].SetValue(0.9f);

        //        Matrix worldInverseTransposeMatrix = Matrix.Transpose(Matrix.Invert(mesh.ParentBone.Transform * world));
        //        EmodiaQuest.Core.Ingame.Instance.DiffuseEffect.Parameters["WorldInverseTranspose"].SetValue(worldInverseTransposeMatrix);
        //    }
        //    mesh.Draw();
        //}


    }
}
