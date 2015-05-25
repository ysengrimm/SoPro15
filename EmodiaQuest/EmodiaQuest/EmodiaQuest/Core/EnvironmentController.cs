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

        public Texture2D map;
        public Color[] colors1D;
        public Color[,] colors2D;

        public List<GameObject> ground, wall, items, accessoires, buildings;

        public EnvironmentController() { }

        /// <summary>
        /// Creates a new map from a pixelmap
        /// <param name="map">A Texture2D with loaded map-picture.</param>
        /// </summary>
        ///
        public void createMap(Texture2D map)
        {
            this.map = map;

            colors1D = new Color[map.Width * map.Height];
            map.GetData(colors1D);
            
            colors2D = new Color[map.Width, map.Height];
            for (int x = 0; x < map.Width; x++)
            {
                for (int y = 0; y < map.Height; y++)
                {
                    colors2D[x, y] = colors1D[x + y * map.Width];
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
            for (int i = 0; i < map.Width; i++)
            {
                for (int j = 0; j < map.Height; j++)
                {
                    if (colors2D[i, j] == color)
                        objList.Add(new GameObject(model, new Vector3(i, height, j)));
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
