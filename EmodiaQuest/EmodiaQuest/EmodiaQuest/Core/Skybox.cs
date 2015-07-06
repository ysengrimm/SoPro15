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

    public class Skybox
    {
        public Vector3 Position = new Vector3(250, 150, 250);
        public float Scale = 1000;
        public Model Model;

        /// <summary>
        /// Creates a new map from a pixelmap
        /// <param name="model">A model for the .</param>
        /// </summary>
        public Skybox(Model model)
        {
            this.Model = model;
        }

        /// <summary>
        /// Creates a new map from a pixelmap
        /// <param name="world">World matrix for multiplying other functions.</param>
        /// <param name="projektion">A projektion-matrix.</param>
        /// <param name="view">A view-matrix.</param>
        /// </summary>
        public void Draw(Matrix world, Matrix view, Matrix projection)
        {
            foreach (ModelMesh mesh in Model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();
                    effect.World = world * Matrix.CreateScale(Scale) * Matrix.CreateFromYawPitchRoll(0, -(float)Math.PI/2, (float)Math.PI / 2) * Matrix.CreateTranslation(Position);
                    effect.View = view;
                    effect.Projection = projection;
                }
                mesh.Draw();
            }
        }

    }
}