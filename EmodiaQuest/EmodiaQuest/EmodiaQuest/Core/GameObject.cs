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
        //public float scale; Don´t let us scale GameObject in the game
        public Model model;

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
        /// <param name="projektion">A projektion-matrix.</param>
        /// <param name="view">A view-matrix.</param>
        /// <param name="rotation">Rotation vector.</param>
        /// <param name="scale">Used for scaling the object.</param>
        /// </summary>
        public void drawGameobject(Matrix world, Matrix view, Matrix projection)
        {
            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();
                    effect.World = Matrix.CreateTranslation(position);
                    effect.View = view;
                    effect.Projection = projection;
                }
                mesh.Draw();
            }
        }
        
    }
}
