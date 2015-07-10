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
            if(rotation  == 0 || rotation  == 1 || rotation  == 2 || rotation  == 3)
                this.rotation = rotation;
        }

        /// <summary>
        /// Creates a new map from a pixelmap
        /// <param name="world">World matrix for multiplying other functions.</param>
        /// <param name="projektion">A projektion-matrix.</param>
        /// <param name="view">A view-matrix.</param>
        /// </summary>
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

    }
}
