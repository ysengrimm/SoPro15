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
        public Vector3 rotation;
        public float scale;
        public Model model;
        public Matrix projektion, view;

        /// <summary>
        /// Creates a new map from a pixelmap
        /// <param name="model">A model for the .</param>
        /// <param name="view">A view-matrix.</param>
        /// </summary>
        public GameObject(Model model, Vector3 position)
        {
            this.model = model;
            this.position = position;
        }

        /*
        /// <summary>
        /// Creates a new map from a pixelmap
        /// <param name="projektion">A projektion-matrix.</param>
        /// <param name="view">A view-matrix.</param>
        /// <param name="rotation">Rotation vector.</param>
        /// <param name="scale">Used for scaling the object.</param>
        /// </summary>
        public void Draw(Matrix projektion, Matrix view, Vector3 rotation, float scale)
        {
            this.projektion = projektion;
            this.view = view;
            this.rotation = rotation;
            this.scale = scale;

            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (BasicEffect effects in mesh.Effects)
                {
                    effects.View = view;
                    effects.Projection = projektion;
                    effects.EnableDefaultLighting();
                    effects.World = Matrix.CreateScale(scale) * Matrix.CreateFromYawPitchRoll(rotation.X, rotation.Y, rotation.Z) * Matrix.CreateTranslation(position);
                }
                mesh.Draw();
            }
        }
        */
    }
}
