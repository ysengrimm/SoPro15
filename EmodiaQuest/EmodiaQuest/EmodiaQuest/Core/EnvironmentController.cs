using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Drawing.Imaging;


namespace EmodiaQuest.Core
{
    public class EnvironmentController
    {
        public System.Drawing.Bitmap Placementmap, Collisionmap, Itemmap;

        public List<GameObject> Ground, Wall, Items, Accessoires, Buildings, Teleporter;
        public List<NPCs.Enemy>[,] enemyArray;
        public List<NPCs.NPC> NPCList;

        /// <summary>
        /// This list only contains objects you can change world with
        /// </summary>
        public List<TeleObject> TeleporterObjList;


        /// <summary>
        /// This list only contains objects you can collide with
        /// </summary>
        public List<Object> CollisionObjList;

        public struct Object
        {
            public Model Model;
            public Color Color;
            public Vector2 Dimension;

            public Object(Model model, Color color, Vector2 dimension)
            {
                this.Model = model;
                this.Color = color;
                this.Dimension = dimension;
            }
        }

        public struct TeleObject
        {
            public Model Model;
            public Color Color;
            public Vector2 Dimension;
            public Vector2 TeleVector;

            /// <summary>
            /// This struct supports the use of creating teleportfields in the collision map
            /// </summary>
            /// <param name="model"> The model of the teleporter object</param>
            /// <param name="color"> The color, which is used for the teleporter object </param>
            /// <param name="dimension"> The dimension, of the teleporter object</param>
            /// <param name="teleVector"> the Vecor, where the Teleport is, relative to the placement middle</param>
            public TeleObject(Model model, Color color, Vector2 dimension, Vector2 teleVector)
            {
                this.Model = model;
                this.Color = color;
                this.Dimension = dimension;
                this.TeleVector = teleVector;
            }
        }

        //lets items jump :D
        float jump = 0;

        public EnvironmentController() 
        { 
            Ground = new List<GameObject>();
            Wall = new List<GameObject>();
            Items = new List<GameObject>();
            Accessoires = new List<GameObject>();
            Buildings = new List<GameObject>();
            Teleporter = new List<GameObject>();

            CollisionObjList = new List<Object>();
            TeleporterObjList = new List<TeleObject>();

            NPCList = new List<NPCs.NPC>();

        }

        /// <summary>
        /// Initialises Array with Lists of Enemy
        /// </summary>
        public void CreateEnemyArray()
        {
            enemyArray = new List<NPCs.Enemy>[Placementmap.Width, Placementmap.Height];
            for (int i = 0; i < Placementmap.Width; i++)
            {
                for (int j = 0; j < Placementmap.Height; j++)
                {
                    enemyArray[i, j] = new List<NPCs.Enemy>();
                }
            }
        }
        
        /// <summary>
        /// Method for fillig an object list with objects including their models and positions from placement map.
        /// Using B channel of .png to calculate rotation of selected object. 
        /// Using R and G channels to calculate wich object is selected.
        /// <param name="objList">List, that takes all the new objects.</param>
        /// <param name="model">What model of objects should be used?</param>
        /// <param name="color">On wich pixel-color is used to display the objects?</param>
        /// <param name="height">Integer for placing the object in Z-axis</param>
        /// </summary>
        public void InsertObj(List<GameObject> objList, Model model, Color color, int height)
        {
            for (int i = 0; i < Placementmap.Width; i++)
            {
                for (int j = 0; j < Placementmap.Height; j++)
                {
                    if (Placementmap.GetPixel(i, j).R == color.R && Placementmap.GetPixel(i, j).G == color.G)
                    {
                        if (Placementmap.GetPixel(i, j).B > 3)
                        {
                            Console.WriteLine("GameObject has a wrong rotation: Blue Channel!" + Placementmap.GetPixel(i, j));
                        }
                        objList.Add(new GameObject(model, new Vector3(i * 10, height, j * 10), Placementmap.GetPixel(i, j).B));
                    }    
                }
            }
        }

        /// <summary>
        /// Method for fillig an object list with items including their models and positions from item map.
        /// Using B channel of .png to calculate rotation of selected item. 
        /// Using R and G channels to calculate wich item is selected.
        /// <param name="itemList">List, that takes all the new items.</param>
        /// <param name="model">What model of item should be used?</param>
        /// <param name="color">Wich pixel-color is used to display the items?</param>
        /// <param name="height">Integer for placing the item in Z-axis</param>
        /// </summary>
        public void InsertItem(List<GameObject> itemList, Model model, Color color, int height)
        {
            for (int i = 0; i < Itemmap.Width; i++)
            {
                for (int j = 0; j < Itemmap.Height; j++)
                {
                    if (Itemmap.GetPixel(i, j).R == color.R && Itemmap.GetPixel(i, j).G == color.G)
                    {
                        itemList.Add(new GameObject(model, new Vector3(i * 10, height, j * 10), Itemmap.GetPixel(i, j).B));
                    }
                }
            }
        }

        /// <summary>
        /// Method, wich generates a collision map from a placement map.
        /// Uses dimension and rotation of objects to generate black pixels on right positions
        /// </summary>
        public void GenerateCollisionMap(ContentManager content, WorldState collisionWorld)
        {
            //gets >current< content path
            //at first gets path of debug directory and then replace the end to get path of content folder
            string contentPath = Path.GetDirectoryName(Environment.CurrentDirectory).Replace(@"EmodiaQuest\bin\x86", @"EmodiaQuestContent\");

            System.Drawing.Bitmap orgImage = new System.Drawing.Bitmap(Placementmap.Width, Placementmap.Height);

            // clears map
            for (int i = 0; i < orgImage.Width; i++)
            {
                for (int j = 0; j < orgImage.Height; j++)
                {
                    orgImage.SetPixel(i, j, System.Drawing.Color.White);
                }
            }

            foreach (Object obj in CollisionObjList)
            {
                for (int i = 0; i < orgImage.Width; i++)
                {
                    for (int j = 0; j < orgImage.Height; j++)
                    {
                        if (Placementmap.GetPixel(i, j).R == obj.Color.R && Placementmap.GetPixel(i, j).G == obj.Color.G)
                        {
                            // example: house with dimensions like XXX ... so it's 1x3

                            // with rotation like 0° or 180°
                            // this part will generate black pixels like XXX
                            if (Placementmap.GetPixel(i, j).B == 0 || Placementmap.GetPixel(i, j).B == 2)
                            {
                                for (int k = i - (int)obj.Dimension.Y / 2; k < i + 1 + (int)obj.Dimension.Y / 2; k++)
                                {
                                    for (int l = j - (int)obj.Dimension.X / 2; l < j + 1 + (int)obj.Dimension.X / 2; l++)
                                    {
                                        orgImage.SetPixel(k, l, System.Drawing.Color.Black);
                                    }
                                }
                            }

                            // with rotation like 90° or 270°
                            // this part will generate black pixels like X
                            //                                           X  <- actual position of house
                            //                                           X
                            else if (Placementmap.GetPixel(i, j).B == 1 || Placementmap.GetPixel(i, j).B == 3)
                            {
                                for (int k = i - (int)obj.Dimension.X / 2; k < i + 1 + (int)obj.Dimension.X / 2; k++)
                                {
                                    for (int l = j - (int)obj.Dimension.Y / 2; l < j + 1 + (int)obj.Dimension.Y / 2; l++)
                                    {
                                        orgImage.SetPixel(k, l, System.Drawing.Color.Black);
                                    }
                                }
                            }
                        }
                    }
                }
            }

            // The teleport objects will be escpecially added to the collision map
            // Function like foreach above
            foreach (TeleObject telobj in TeleporterObjList)
            {
                for (int i = 0; i < orgImage.Width; i++)
                {
                    for (int j = 0; j < orgImage.Height; j++)
                    {
                        if (Placementmap.GetPixel(i, j).R == telobj.Color.R && Placementmap.GetPixel(i, j).G == telobj.Color.G)
                        {
                            if (Placementmap.GetPixel(i, j).B == 0 || Placementmap.GetPixel(i, j).B == 2)
                            {
                                for (int k = i - (int)telobj.Dimension.Y / 2; k < i + 1 + (int)telobj.Dimension.Y / 2; k++)
                                {
                                    for (int l = j - (int)telobj.Dimension.X / 2; l < j + 1 + (int)telobj.Dimension.X / 2; l++)
                                    {
                                        if(k == i && l == j)
                                        {
                                            orgImage.SetPixel(k, l, System.Drawing.Color.Violet);
                                        }
                                        else
                                        {
                                            orgImage.SetPixel(k, l, System.Drawing.Color.Black);
                                        }
                                    }
                                }
                            }
                            else if (Placementmap.GetPixel(i, j).B == 1 || Placementmap.GetPixel(i, j).B == 3)
                            {
                                for (int k = i - (int)telobj.Dimension.X / 2; k < i + 1 + (int)telobj.Dimension.X / 2; k++)
                                {
                                    for (int l = j - (int)telobj.Dimension.Y / 2; l < j + 1 + (int)telobj.Dimension.Y / 2; l++)
                                    {
                                        if (k == i && l == j)
                                        {
                                            orgImage.SetPixel(k, l, System.Drawing.Color.Violet);
                                        }
                                        else
                                        {
                                            orgImage.SetPixel(k, l, System.Drawing.Color.Black);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            //draw things in new image
            System.Drawing.Graphics gimage = System.Drawing.Graphics.FromImage(orgImage);
            gimage.DrawImage(orgImage, 0, 0);
            
            //save new image
            orgImage.Save(contentPath + @"maps\" + collisionWorld.ToString() + "_CollisionMap.png", ImageFormat.Png);
            Collisionmap = new System.Drawing.Bitmap(orgImage);
        }

        public void DrawEnvironment(Matrix world, Matrix view, Matrix projection)
        {
            jump += 0.11f;

            foreach(GameObject obj in Ground)
            {
                obj.drawGameobject(world, view, projection);
            }
            foreach (GameObject obj in Wall)
            {
                obj.drawGameobject(world, view, projection);
            }
            foreach (GameObject obj in Items)
            {
                obj.drawGameobject(world * Matrix.CreateTranslation(0, (float)Math.Sin(jump)+1, 0), view, projection);
            }
            foreach (GameObject obj in Accessoires)
            {
                obj.drawGameobject(world, view, projection);
            }
            foreach (GameObject obj in Buildings)
            {
                obj.drawGameobject(world, view, projection);
            }
            foreach (GameObject obj in Teleporter)
            {
                obj.drawGameobject(world, view, projection);
            }
        }

    }
}
