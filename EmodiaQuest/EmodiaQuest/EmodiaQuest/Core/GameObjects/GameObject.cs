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
    
    abstract class GameObject
    {
        private Vector3 _position;
        private Vector3 _rotation;
        private float _scale;
        private Model _model;


        public Vector3 getPosition()
        {
            return this._position;
        }
        public void setPosition(Vector3 position)
        {
            this._position = position;
        }
        

    
    }
}
