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

        public Texture2D PlacementMap, CollisionMap, ItemMap;
        public Color[,] PlacementColors, CollisionColors, ItemColors;

        public List<GameObject> ground, wall, items, accessoires, buildings;

        //lets items jump :D
        float jump = 0;

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
        /// Creates a new item map from a pixelmap
        /// <param name="map">A Texture2D with loaded item map-picture.</param>
        /// </summary>
        public void createItemMap(Texture2D map)
        {
            Color[] colors1D;

            this.ItemMap = map;

            colors1D = new Color[map.Width * map.Height];
            map.GetData(colors1D);

            ItemColors = new Color[map.Width, map.Height];
            for (int x = 0; x < map.Width; x++)
            {
                for (int y = 0; y < map.Height; y++)
                {
                    ItemColors[x, y] = colors1D[x + y * map.Width];
                }
            }
        }
        
        /// <summary>
        /// Method for fillig an object list with objects including their models and positions from placement map
        /// <param name="objList">List, that takes all the new objects.</param>
        /// <param name="model">What model of objects should be used?</param>
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

        /// <summary>
        /// Method for fillig an object list with items including their models and positions from item map
        /// <param name="itemList">List, that takes all the new items.</param>
        /// <param name="model">What model of item should be used?</param>
        /// <param name="color">Wich pixel-color is used to display the items?</param>
        /// <param name="height">Integer for placing the item in Z-axis</param>
        /// </summary>
        public void insertItem(List<GameObject> itemList, Model model, Color color, int height)
        {
            for (int i = 0; i < ItemMap.Width; i++)
            {
                for (int j = 0; j < ItemMap.Height; j++)
                {
                    if (ItemColors[i, j] == color)
                    {
                        itemList.Add(new GameObject(model, new Vector3(i * 10, height, j * 10)));
                    }
                }
            }
        }

        public void drawEnvironment(Matrix world, Matrix view, Matrix projection)
        {
            jump += 0.11f;

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
                obj.drawGameobject(world * Matrix.CreateTranslation(0, (float)Math.Sin(jump)+1, 0), view, projection);
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
