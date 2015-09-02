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
using EmodiaQuest.Core;
using EmodiaQuest.Core.NPCs;

namespace EmodiaQuest.Core.DungeonGeneration
{
    /*
     * Generates Placement map for dungeon randomly. 
     * Save in PlacementColors and also save as png for debugging (can be deleted later)
     */ 

    public class LevelGenerator
    {
        private string contentPath;
        public System.Drawing.Bitmap Map;
        public EnvironmentController Controller;

        // trigger for setting spawnroom
        bool set = true;

        // create list for room storage for easy access
        List<Room> rooms = new List<Room>();

        public List<Enemy> EnemyList = new List<Enemy>();

        /// <summary>
        /// This list contains all positions in the map wich are part of rooms (excpt. spawnroom) and do not contain an item
        /// </summary>
        private List<Point> enemyPoints = new List<Point>();
        
        private EnemyType[] enemies;

        Random rnd = new Random();

        public LevelGenerator(EnvironmentController controller, EnemyType[] enemies)
        {
            //gets >current< content path
            //at first gets path of debug directory and then replace the end to get path of content folder
            contentPath = Path.GetDirectoryName(Environment.CurrentDirectory).Replace(@"EmodiaQuest\bin\x86", @"EmodiaQuestContent\");

            this.Controller = controller;
            this.enemies = enemies;

            Map = new System.Drawing.Bitmap(Settings.Instance.DungeonMapSize, Settings.Instance.DungeonMapSize);

            placeRooms();
        }

        /// <summary>
        /// Places rooms and floors randomly
        /// </summary>
        private void placeRooms() {
            // initialise Map with black
            for (int i = 0; i < Settings.Instance.DungeonMapSize; i++)
            {
                for (int j = 0; j < Settings.Instance.DungeonMapSize; j++)
                {
                    Map.SetPixel(i, j, System.Drawing.Color.Black);
                    Controller.PlacementColors[i, j] = ColorListDungeon.Instance.Wall;
                } 
            }



            // Center of room
            Point newCenter = new Point();

		    // randomize values for each room
            for (int i = 0; i < Settings.Instance.MaxRooms; i++)
            {
                int w = Settings.Instance.MinRoomSize + rnd.Next(Settings.Instance.MaxRoomSize - Settings.Instance.MinRoomSize + 1);
                int h = Settings.Instance.MinRoomSize + rnd.Next(Settings.Instance.MaxRoomSize - Settings.Instance.MinRoomSize + 1);
                int x = rnd.Next(Settings.Instance.DungeonMapSize - w - 1) + 1;
                int y = rnd.Next(Settings.Instance.DungeonMapSize - h - 1) + 1;
			        
                // create room with randomized values
			    Room newRoom = new Room(x, y, w, h);

			    Boolean failed = false;
			    foreach (Room otherRoom in rooms) {
				    if (newRoom.Intersects(otherRoom)) {
					    failed = true;
					    break;
				    }
			    }

			    if (!failed) {
			        // local function to carve out new room
			        createRoom(newRoom);

			        // store center for new room
			        newCenter = newRoom.Center;

			        if(rooms.Count != 0){
				        // store center of previous room
				        Point prevCenter = rooms[rooms.Count - 1].Center;

				        // carve out corridors between rooms based on centers
				        // randomly start with horizontal or vertical corridors
				        if (rnd.Next(2) == 1) {
					        HCorridor((int)prevCenter.X, (int)newCenter.X, (int)prevCenter.Y);
					        VCorridor((int)prevCenter.Y, (int)newCenter.Y, (int)newCenter.X);
				        } else {
					        VCorridor((int)prevCenter.Y, (int)newCenter.Y, (int)prevCenter.X);
					        HCorridor((int)prevCenter.X, (int)newCenter.X, (int)newCenter.Y);
					    }
				    }

                    // selecting spawnpoint of player
                    if (set)
                    {
                        Map.SetPixel((int)newRoom.X, (int)newRoom.Y, System.Drawing.Color.Red);
                        Controller.StartPoint = new Vector2(newRoom.X * Settings.Instance.GridSize, newRoom.Y * Settings.Instance.GridSize);
                        set = false;
                    }
			    }
		        if(!failed) rooms.Add(newRoom);
		    }

            DeleteWalls();

            // this method would place items in rooms and floors
            // if choosen delete same method from "createRoom", wich places items only in rooms
            //insertItems();

            if(enemies.Length != 0 && Settings.Instance.NumEnemies != 0) insertEnemies();

            saveMap();
	    }

        /// <summary>
        /// Saves the map
        /// </summary>
        private void saveMap()
        {
            //draw things in new image
            System.Drawing.Graphics gimage = System.Drawing.Graphics.FromImage(Map);
            gimage.DrawImage(Map, 0, 0);

            //save new image
            Map.Save(contentPath + @"maps\" + "Dungeon_PlacementMapDebug_forreasons_.png", ImageFormat.Png);
        }

        /// <summary>
        /// Draws a room in the map
        /// </summary>
        /// <param name="room"> Current room </param>
        private void createRoom(Room room)
        {
            for (int i = (int)room.X; i < room.X + room.Width; i++)
            {
                for (int j = (int)room.Y; j < room.Y + room.Height; j++)
                {
                    Map.SetPixel(i, j, System.Drawing.Color.White);
                    Controller.PlacementColors[i, j] = ColorListDungeon.Instance.Ground;

                    // randomly setting items in a room
                    // does not set an item in spawn room
                    if (rnd.Next(10) == 0 && !set)     // 10% chance for setting item
                    {
                        Controller.ItemColors[i, j] = ColorListDungeon.Instance.Item;
                        Map.SetPixel(i, j, System.Drawing.Color.Gray);
                    }
                    else if(!set)
                    {
                        // items and enemies not on same field
                        enemyPoints.Add(new Point(i, j));
                    }

                    // placing teleporter
                    if (set)
                    {
                        Map.SetPixel((int)room.X2-2, (int)room.Y2-2, System.Drawing.Color.Violet);
                        Controller.PlacementColors[(int)room.X2 - 2, (int)room.Y2 - 2] = ColorListDungeon.Instance.Teleporter;
                    }
                }
            }
        }

        /// <summary>
        /// create horizontal corridor to connect rooms
        /// </summary>
        /// <param name="x1"> Start point</param>
        /// <param name="x2"> End point</param>
        /// <param name="y"> Choosen y-axis</param>
        private void HCorridor(int x1, int x2, int y) {
            for (int i = (int)Math.Min(x1, x2); i < (int)Math.Max(x1, x2)+1; i++){
			    // does not set ground again if ground is allready there
                // is important to prevent overvriting objects in rooms
                if (Controller.PlacementColors[i, y] == ColorListDungeon.Instance.Wall || Controller.PlacementColors[i, y] == ColorListDungeon.Instance.Nothing)
                {
                    // destory the tiles to "carve" out corridor
                    Map.SetPixel(i, y, System.Drawing.Color.White);
                    Controller.PlacementColors[i, y] = ColorListDungeon.Instance.Ground;
                }
		    }
	    }

	    /// <summary>
        /// create vertical corridor to connect rooms
	    /// </summary>
        /// <param name="y1"> Start point</param>
        /// <param name="y2"> End point</param>
        /// <param name="x"> Choosen x-axis</param>
	    private void VCorridor(int y1, int y2, int x) {
            for (int i = (int)Math.Min(y1, y2); i < (int)Math.Max(y1, y2) + 1; i++) {
                // does not set ground again if ground is allready there
                // is important to prevent overvriting objects in rooms
                if (Controller.PlacementColors[x, i] == ColorListDungeon.Instance.Wall || Controller.PlacementColors[x, i] == ColorListDungeon.Instance.Nothing)
                {
                    // destroy the tiles to "carve" out corridor
                    Map.SetPixel(x, i, System.Drawing.Color.White);
                    Controller.PlacementColors[x, i] = ColorListDungeon.Instance.Ground;
                }
		    }
	    }

        /// <summary>
        /// Deletes all not reachable walls. 
        /// Now not drawing all walls -> better performance (ca. 60% less models to draw for 100x100 map)
        /// </summary>
        private void DeleteWalls()
        {
            List<Point> walls = new List<Point>();

            // searching for all walls wich have walls around them like the O
            // XXX
            // XOX
            // XXX
            // then add the O to the List
            for (int i = 1; i < Math.Sqrt(Controller.PlacementColors.Length) - 1; i++)
            {
                for (int j = 1; j < Math.Sqrt(Controller.PlacementColors.Length) - 1; j++)
                {
                    if (Controller.PlacementColors[i, j] == ColorListDungeon.Instance.Wall &&
                        Controller.PlacementColors[i + 1, j] == ColorListDungeon.Instance.Wall &&
                        Controller.PlacementColors[i - 1, j] == ColorListDungeon.Instance.Wall &&
                        Controller.PlacementColors[i, j + 1] == ColorListDungeon.Instance.Wall &&
                        Controller.PlacementColors[i, j - 1] == ColorListDungeon.Instance.Wall &&
                        Controller.PlacementColors[i + 1, j + 1] == ColorListDungeon.Instance.Wall &&
                        Controller.PlacementColors[i - 1, j + 1] == ColorListDungeon.Instance.Wall &&
                        Controller.PlacementColors[i + 1, j - 1] == ColorListDungeon.Instance.Wall &&
                        Controller.PlacementColors[i - 1, j - 1] == ColorListDungeon.Instance.Wall)
                    {
                        walls.Add(new Point(i, j));
                    }
                }
            }

            // set all pixels in the List to "nothing"
            foreach (Point item in walls)
            {
                Controller.PlacementColors[item.X, item.Y] = ColorListDungeon.Instance.Nothing;
                Map.SetPixel(item.X, item.Y, System.Drawing.Color.White);
            }
        }

        /// <summary>
        /// Inserts randomly items in rooms and floors
        /// </summary>
        private void insertItems()
        {
            for (int i = 1; i < Math.Sqrt(Controller.PlacementColors.Length) - 1; i++)
            {
                for (int j = 1; j < Math.Sqrt(Controller.PlacementColors.Length) - 1; j++)
                {
                    if (Controller.PlacementColors[i, j] == ColorListDungeon.Instance.Ground && rnd.Next(100) == 0)     // 1% chance for setting item
                    {
                        Controller.ItemColors[i, j] = ColorListDungeon.Instance.Item;
                        Map.SetPixel(i, j, System.Drawing.Color.Green);
                    }
                }
            }
        }

        private void insertEnemies()
        {
            int count = Settings.Instance.NumEnemies;

            while (count != 0)
            {
                Point point = enemyPoints[rnd.Next(enemyPoints.Count)];

                float distX = (float)rnd.NextDouble() * 3;
                float distY = (float)rnd.NextDouble() * 3;
                if (rnd.Next(10) > 4) distX *= -1;
                if (rnd.Next(10) > 4) distY *= -1;

                if (Controller.enemyArray[point.X, point.Y].Count > 0)        //das feld ist nicht leer
                {
                    bool samePosition = false;
                    foreach (Enemy enemy in Controller.enemyArray[point.X, point.Y])    //gehe die liste in diesem feld durch
                    {
                        if (Controller.EuclideanDistance(new Vector2(point.X + distX, point.Y + distY), enemy.Position) < 3) //wenn die random position nah an der eines enemys ist
                        {
                            samePosition = true;    // an die position kommt kein monster hin
                            break;
                        }
                    }

                    if (!samePosition)      // wenn man eine position im feld gefunden hat wo kein monster steht
                    {
                        Enemy enemy = new Enemy(new Vector2(point.X * Settings.Instance.GridSize + distX, point.Y * Settings.Instance.GridSize + distY), Controller, enemies[rnd.Next(enemies.Length)]);
                        EnemyList.Add(enemy);
                        count--;

                        Map.SetPixel(point.X, point.Y, System.Drawing.Color.LightSkyBlue);
                    }
                }
                else
                {
                    Enemy enemy = new Enemy(new Vector2(point.X * Settings.Instance.GridSize + distX, point.Y * Settings.Instance.GridSize + distY), Controller, enemies[rnd.Next(enemies.Length)]);
                    EnemyList.Add(enemy);
                    count--;

                    Map.SetPixel(point.X, point.Y, System.Drawing.Color.LightSkyBlue);
                }
            }
        }
    }
}
