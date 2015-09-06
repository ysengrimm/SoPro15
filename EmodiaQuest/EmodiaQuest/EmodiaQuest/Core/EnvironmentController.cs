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
        // TODO: Controll the whole environment, is used for getting easy access of everything wich is static, needs to be rendered or has collsion
        // will have a lot of lists with items
        public ContentManager Content;
        Random rnd = new Random();

        public Texture2D PlacementMap, CollisionMap, ItemMap;
        public Color[,] PlacementColors, CollisionColors, ItemColors;

        public List<GameObject> Ground, Wall, Items, Accessoires, Buildings, Teleporter, RandomStuff;
        public List<NPCs.Enemy>[,] enemyArray;
        public List<NPCs.NPC> NPCList;

        public WorldState CurrentWorld;

        private Model a;

        /// <summary>
        /// ONLY FOR DUNGEON
        /// Start point of Player
        /// </summary>
        public Vector2 StartPoint;

        /// <summary>
        /// This list only contains objects you can change world with
        /// </summary>
        public List<TeleObject> TeleporterObjList;

        /// <summary>
        /// This list only contains objects you can collide with
        /// </summary>
        public List<Object> CollisionObjList;

        public int MapWidth;
        public int MapHeight;

        public struct Object
        {
            public Model Model;
            public Color Color;
            public Vector2 Dimension;
            public String Name;
            public bool IsRandomStuff;

            public Object(Model model, Color color, Vector2 dimension, String name, bool isRandomStuff)
            {
                this.Model = model;
                this.Color = color;
                this.Dimension = dimension;
                this.Name = name;
                this.IsRandomStuff = isRandomStuff;
            }
        }

        public struct TeleObject
        {
            public Model Model;
            public Color Color;
            public Vector2 Dimension;
            public Vector2 TeleVector;
            public String Name;
            public bool IsRandomStuff;

            /// <summary>
            /// This struct supports the use of creating teleportfields in the collision map
            /// </summary>
            /// <param name="model"> The model of the teleporter object</param>
            /// <param name="color"> The color, which is used for the teleporter object </param>
            /// <param name="dimension"> The dimension, of the teleporter object</param>
            /// <param name="teleVector"> the Vecor, where the Teleport is, relative to the placement middle</param>
            public TeleObject(Model model, Color color, Vector2 dimension, Vector2 teleVector, String name, bool isRandomStuff)
            {
                this.Model = model;
                this.Color = color;
                this.Dimension = dimension;
                this.TeleVector = teleVector;
                this.Name = name;
                this.IsRandomStuff = isRandomStuff;
            }
        }
        
        public EnvironmentController(WorldState currentWorld, ContentManager content) 
        { 
            CurrentWorld = currentWorld;

            if (CurrentWorld == WorldState.Dungeon)
            {
                MapWidth = Settings.Instance.DungeonMapSize;
                MapHeight = Settings.Instance.DungeonMapSize;
            }
            else if (CurrentWorld == WorldState.Safeworld)
            {
                MapWidth = Settings.Instance.SafeWorldMapWidth;
                MapHeight = Settings.Instance.SafeWorldMapHeight;
            }

            Ground = new List<GameObject>();
            Wall = new List<GameObject>();
            Items = new List<GameObject>();
            Accessoires = new List<GameObject>();
            Buildings = new List<GameObject>();
            Teleporter = new List<GameObject>();
            RandomStuff = new List<GameObject>();

            CollisionObjList = new List<Object>();
            TeleporterObjList = new List<TeleObject>();
            
            NPCList = new List<NPCs.NPC>();

            PlacementColors = new Color[MapWidth, MapHeight];
            CollisionColors = new Color[MapWidth, MapHeight];
            ItemColors = new Color[MapWidth, MapHeight];

            CreateEnemyArray();
        }
        /// <summary>
        /// Initialises Array with Lists of Enemy
        /// </summary>
        public void CreateEnemyArray()
        {
            enemyArray = new List<NPCs.Enemy>[MapWidth, MapHeight];
            for (int i = 0; i < MapWidth; i++)
            {
                for (int j = 0; j < MapHeight; j++)
                {
                    enemyArray[i, j] = new List<NPCs.Enemy>();
                }
            }
        }

        /// <summary>
        /// Creates a new placement map from a pixelmap
        /// <param name="map">A Texture2D with loaded placement map-picture.</param>
        /// </summary>
        public void CreatePlacementMap(Texture2D map)
        {
            Color[] colors1D;

            this.PlacementMap = map;

            colors1D = new Color[map.Width * map.Height];
            map.GetData(colors1D);

            
            for (int x = 0; x < map.Width; x++)
            {
                for (int y = 0; y < map.Height; y++)
                {
                    PlacementColors[x, y] = colors1D[x + y * map.Width];
                }
            }
        }

        /// <summary>
        /// Creates a new item map from a pixelmap
        /// <param name="map">A Texture2D with loaded item map-picture.</param>
        /// </summary>
        public void CreateItemMap(Texture2D map)
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
        /// Method for fillig an object list with objects including their models and positions from placement map.
        /// Using B channel of .png to calculate rotation of selected object. 
        /// Using R and G channels to calculate wich object is selected.
        /// <param name="objList">List, that takes all the new objects.</param>
        /// <param name="model">What model of objects should be used?</param>
        /// <param name="color">On wich pixel-color is used to display the objects?</param>
        /// <param name="height">Integer for placing the object in Z-axis</param>
        /// <param name="name">Name of this Object</param>
        /// <param name="isRandomStuff">Decides, if this Object is type of Random Stuff</param>
        /// </summary>
        public void InsertObj(List<GameObject> objList, Model model, Color color, int height, String name, bool isRandomStuff)
        {
            for (int i = 0; i < MapWidth; i++)
            {
                for (int j = 0; j < MapHeight; j++)
                {
                    if (PlacementColors[i, j].R == color.R && PlacementColors[i, j].G == color.G)
                    {
                        if(PlacementColors[i, j].B > 3)
                        {
                            Console.WriteLine("GameObject has a wrong rotation: Blue Channel!" + PlacementColors[i,j]);
                        }
                        objList.Add(new GameObject(model, new Vector3(i * 10, height, j * 10), PlacementColors[i, j].B, name, isRandomStuff));
                        
                        if (name == "wall" && CurrentWorld == WorldState.Dungeon)
                        {
                            a = Player.Instance.ContentMngr.Load<Model>("fbxContent/gameobjects/Ground_10x10/Ground_10x10");
                            objList.Add(new GameObject(a, new Vector3(i * 10, height, j * 10), PlacementColors[i, j].B, "ground", false));
                        }
                        
                    }    
                }
            }
        }

        /// <summary>
        /// Inserting random Stuff to the environment
        /// </summary>
        /// <param name="objList"></param>
        /// <param name="model"></param>
        /// <param name="count">number of Stuff Obejects, added to the environment</param>
        /// <param name="name">bane of the random Stuff object, which will be added to the environment</param>
        /// <param name="isRandomStuff"></param>
        public void InsertRandomStuffObj(List<GameObject> objList, Model model, int count, String name, bool isRandomStuff)
        {
            int counter = count;
            while (counter > 0)
            {
                float X = Math.Abs((float)rnd.NextDouble() * (MapWidth * Settings.Instance.GridSize) - 6);
                float Y = Math.Abs((float)rnd.NextDouble() * (MapHeight * Settings.Instance.GridSize) - 6);
                if (Color.White == Player.Instance.CollisionHandler.GetCollisionColor(new Vector2(X, Y), Player.Instance.CollisionHandler.Controller.CollisionColors, 0f))
                {
                    objList.Add(new GameObject(model, new Vector3(X, 0, Y), 0, name, isRandomStuff));
                    counter--;
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
        /// <param name="name">Name of this Object</param>
        /// <param name="isRandomStuff">Decides, if this Object is type of Random Stuff</param>
        /// </summary>
        public void InsertItem(List<GameObject> itemList, Model model, Color color, int height, String name, bool isRandomStuff)
        {
            for (int i = 0; i < MapWidth; i++)
            {
                for (int j = 0; j < MapHeight; j++)
                {
                    if (ItemColors[i, j].R == color.R && ItemColors[i, j].G == color.G)
                    {
                        itemList.Add(new GameObject(model, new Vector3(i * 10, height, j * 10), ItemColors[i, j].B, name, isRandomStuff));
                    }
                }
            }
        }

        /// <summary>
        /// Method, wich generates a collision map from a placement map.
        /// Uses dimension and rotation of objects to generate black pixels on right positions
        /// </summary>
        public void GenerateCollisionMap(ContentManager content)
        {
            //gets >current< content path
            //at first gets path of debug directory and then replace the end to get path of content folder
            string contentPath = Path.GetDirectoryName(Environment.CurrentDirectory).Replace(@"EmodiaQuest\bin\x86", @"EmodiaQuestContent\");

            System.Drawing.Bitmap orgImage = new System.Drawing.Bitmap(MapWidth, MapHeight);

            // clears map
            for (int i = 0; i < orgImage.Width; i++)
            {
                for (int j = 0; j < orgImage.Height; j++)
                {
                    orgImage.SetPixel(i, j, System.Drawing.Color.White);
                    CollisionColors[i, j] = Color.White;
                }
            }

            foreach (Object obj in CollisionObjList)
            {
                for (int i = 0; i < orgImage.Width; i++)
                {
                    for (int j = 0; j < orgImage.Height; j++)
                    {
                        if (PlacementColors[i, j].R == obj.Color.R && PlacementColors[i, j].G == obj.Color.G)
                        {
                            // example: house with dimensions like XXX ... so it's 1x3

                            // with rotation like 0° or 180°
                            // this part will generate black pixels like XXX
                            if (PlacementColors[i, j].B == 0 || PlacementColors[i, j].B == 2)
                            {
                                for (int k = i - (int)obj.Dimension.Y / 2; k < i + 1 + (int)obj.Dimension.Y / 2; k++)
                                {
                                    for (int l = j - (int)obj.Dimension.X / 2; l < j + 1 + (int)obj.Dimension.X / 2; l++)
                                    {
                                        orgImage.SetPixel(k, l, System.Drawing.Color.Black);
                                        CollisionColors[k, l] = Color.Black;
                                    }
                                }
                            }

                            // with rotation like 90° or 270°
                            // this part will generate black pixels like X
                            //                                           X  <- actual position of house
                            //                                           X
                            else if (PlacementColors[i, j].B == 1 || PlacementColors[i, j].B == 3)
                            {
                                for (int k = i - (int)obj.Dimension.X / 2; k < i + 1 + (int)obj.Dimension.X / 2; k++)
                                {
                                    for (int l = j - (int)obj.Dimension.Y / 2; l < j + 1 + (int)obj.Dimension.Y / 2; l++)
                                    {
                                        orgImage.SetPixel(k, l, System.Drawing.Color.Black);
                                        CollisionColors[k, l] = Color.Black;
                                    }
                                }
                            }
                        }
                    }
                }

            }

            // The teleport objects will be escpecially added to the collision map
            // Function as above
            foreach (TeleObject telobj in TeleporterObjList)
            {
                for (int i = 0; i < orgImage.Width; i++)
                {
                    for (int j = 0; j < orgImage.Height; j++)
                    {
                        if (PlacementColors[i, j].R == telobj.Color.R && PlacementColors[i, j].G == telobj.Color.G)
                        {
                            if (PlacementColors[i, j].B == 0 || PlacementColors[i, j].B == 2)
                            {
                                for (int k = i - (int)telobj.Dimension.Y / 2; k < i + 1 + (int)telobj.Dimension.Y / 2; k++)
                                {
                                    for (int l = j - (int)telobj.Dimension.X / 2; l < j + 1 + (int)telobj.Dimension.X / 2; l++)
                                    {
                                        if(k == i && l == j)
                                        {
                                            orgImage.SetPixel(k, l, System.Drawing.Color.Violet);
                                            CollisionColors[k, l] = Color.Violet;
                                        }
                                        else
                                        {
                                            orgImage.SetPixel(k, l, System.Drawing.Color.Black);
                                            CollisionColors[k, l] = Color.Black;
                                        }
                                    }
                                }
                            }
                            else if (PlacementColors[i, j].B == 1 || PlacementColors[i, j].B == 3)
                            {
                                for (int k = i - (int)telobj.Dimension.X / 2; k < i + 1 + (int)telobj.Dimension.X / 2; k++)
                                {
                                    for (int l = j - (int)telobj.Dimension.Y / 2; l < j + 1 + (int)telobj.Dimension.Y / 2; l++)
                                    {
                                        if (k == i && l == j)
                                        {
                                            orgImage.SetPixel(k, l, System.Drawing.Color.Violet);
                                            CollisionColors[k, l] = Color.Violet;
                                        }
                                        else
                                        {
                                            orgImage.SetPixel(k, l, System.Drawing.Color.Black);
                                            CollisionColors[k, l] = Color.Black;
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
            orgImage.Save(contentPath + @"maps\" + CurrentWorld.ToString() + "_CollisionMap.png", ImageFormat.Png);

        }

        public double EuclideanDistance(Vector2 p1, Vector2 p2)
        {
            return Math.Sqrt(Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2));
        }


        public void LoadEnvironment(ContentManager content)
        {
            foreach (GameObject obj in Ground)
            {
                obj.loadContent(content);
            }
            foreach (GameObject obj in Wall)
            {
                obj.loadContent(content);
            }
            foreach (GameObject obj in Items)
            {
                obj.loadContent(content);
            }
            foreach (GameObject obj in Accessoires)
            {
                obj.loadContent(content);
            }
            foreach (GameObject obj in Buildings)
            {
                obj.loadContent(content);
            }
            foreach (GameObject obj in Teleporter)
            {
                obj.loadContent(content);
            }
            foreach (GameObject obj in RandomStuff)
            {
                obj.loadContent(content);
            }
        }

        public void UpdateEnvironment(GameTime gametime)
        {
            foreach (GameObject obj in Ground)
            {
                obj.update(gametime);
            }
            foreach (GameObject obj in Wall)
            {
                obj.update(gametime);
            }
            foreach (GameObject obj in Items)
            {
                obj.update(gametime);
            }
            foreach (GameObject obj in Accessoires)
            {
                obj.update(gametime);
            }
            foreach (GameObject obj in Buildings)
            {
                obj.update(gametime);
            }
            foreach (GameObject obj in Teleporter)
            {
                obj.update(gametime);
            }
            foreach (GameObject obj in RandomStuff)
            {
                obj.update(gametime);
            }
        }

        public void DrawEnvironment(Matrix world, Matrix view, Matrix projection)
        {
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
                obj.drawGameobject(world, view, projection);
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
            foreach (GameObject obj in RandomStuff)
            {
                obj.drawGameobject(world, view, projection);
            }
        }

    }
}
