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
        public Model model;
        public Boolean drawable = true;

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

        /// <summary>
        /// Creates a new map from a pixelmap
        /// <param name="world">World matrix for multiplying other functions.</param>
        /// <param name="projektion">A projektion-matrix.</param>
        /// <param name="view">A view-matrix.</param>
        /// </summary>
        public void drawGameobject(Matrix world, Matrix view, Matrix projection)
        {
            //for items
            if (drawable)
            {
                foreach (ModelMesh mesh in model.Meshes)
                {
                    foreach (BasicEffect effect in mesh.Effects)
                    {
                        effect.EnableDefaultLighting();
                        effect.World = Matrix.CreateTranslation(position) * world;
                        effect.View = view;
                        effect.Projection = projection;
                    }
                    mesh.Draw();
                }
            }
        }

    }
}
