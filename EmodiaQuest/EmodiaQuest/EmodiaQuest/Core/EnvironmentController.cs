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
    public class EnvironmentController
    {
        // TODO: Controll the whole environment, is used for getting easy access of everything wich is static, needs to be rendered or has collsion
        // will have a lot of lists with items

        public Texture2D CollisionMap, PlacementMap;
        public Color[,] PlacementColors;
        public Color[,] CollisionColors;

        public List<GameObject> ground, wall, items, accessoires, buildings;

        public EnvironmentController() 
        { 
            ground = new List<GameObject>();
            wall = new List<GameObject>();
            items = new List<GameObject>();
            accessoires = new List<GameObject>();
            buildings = new List<GameObject>();
        }

        /// <summary>
        /// Creates a new placement map from a pixelmap
        /// <param name="map">A Texture2D with loaded placement map-picture.</param>
        /// </summary>
        public void createPlacementMap(Texture2D map)
        {
            Color[] colors1D;

            this.PlacementMap = map;

            colors1D = new Color[map.Width * map.Height];
            map.GetData(colors1D);
            
            PlacementColors = new Color[map.Width, map.Height];
            for (int x = 0; x < map.Width; x++)
            {
                for (int y = 0; y < map.Height; y++)
                {
                    PlacementColors[x, y] = colors1D[x + y * map.Width];
                }
            }
        }

        /// <summary>
        /// Creates a new collision map from a pixelmap
        /// <param name="map">A Texture2D with loaded collision map-picture.</param>
        /// </summary>
        public void createCollisionMap(Texture2D map)
        {
            Color[] colors1D;

            this.CollisionMap = map;

            colors1D = new Color[map.Width * map.Height];
            map.GetData(colors1D);

            CollisionColors = new Color[map.Width, map.Height];
            for (int x = 0; x < map.Width; x++)
            {
                for (int y = 0; y < map.Height; y++)
                {
                    CollisionColors[x, y] = colors1D[x + y * map.Width];
                }
            }
        }
        
        /// <summary>
        /// Method for fillig an object list with objects including their models and positions
        /// <param name="objList">List, that takes all the new objects.</param>
        /// <param name="obj">What type of objects should be used?</param>
        /// <param name="color">On wich pixel-color is used to display the objects?</param>
        /// <param name="height">Integer for placing the object in Z-axis</param>
        /// </summary>
        public void insertObj (List<GameObject> objList, Model model, Color color, int height)
        {
            for (int i = 0; i < PlacementMap.Width; i++)
            {
                for (int j = 0; j < PlacementMap.Height; j++)
                {
                    if (PlacementColors[i, j] == color)
                    {
                        objList.Add(new GameObject(model, new Vector3(i * 10, height, j * 10)));
                    }    
                }
            }
        }

        public void drawEnvironment(Matrix world, Matrix view, Matrix projection)
        {
            foreach(GameObject obj in ground)
            {
                obj.drawGameobject(world, view, projection);
            }
            foreach (GameObject obj in wall)
            {
                obj.drawGameobject(world, view, projection);
            }
            foreach (GameObject obj in items)
            {
                obj.drawGameobject(world, view, projection);
            }
            foreach (GameObject obj in accessoires)
            {
                obj.drawGameobject(world, view, projection);
            }
            foreach (GameObject obj in buildings)
            {
                obj.drawGameobject(world, view, projection);
            }
        }

    }
}
