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

namespace EmodiaQuest.Core.DungeonGeneration
{
    /*
     * Generates Placement map for dungeon randomly. 
     * Save in PlacementColors and also save as png for debugging (can be deleted later)
     */ 

    public class LevelGenerator
    {
        public string ContentPath;
        public System.Drawing.Bitmap Map;
        public EnvironmentController Controller;

        public Color Wall = new Color(1, 0, 0);
        public Color Floor = new Color(100, 100, 0);
        public Color Nothing = new Color(0, 0, 0);  // for more drawing performance

        public LevelGenerator(EnvironmentController controller){
            //gets >current< content path
            //at first gets path of debug directory and then replace the end to get path of content folder
            ContentPath = Path.GetDirectoryName(Environment.CurrentDirectory).Replace(@"EmodiaQuest\bin\x86", @"EmodiaQuestContent\");

            this.Controller = controller;

            Map = new System.Drawing.Bitmap(Settings.Instance.DungeonMapWidth, Settings.Instance.DungeonMapHeight);

            PlaceRooms();
        }

        /// <summary>
        /// Places rooms and floors randomly
        /// </summary>
        private void PlaceRooms() {
            // initialise Map with black
            for (int i = 0; i < Settings.Instance.DungeonMapWidth; i++)
            {
                for (int j = 0; j < Settings.Instance.DungeonMapHeight; j++)
                {
                    Map.SetPixel(i, j, System.Drawing.Color.Black);
                    Controller.PlacementColors[i, j] = Wall;
                } 
            }

		    // create list for room storage for easy access
		    List<Room> rooms = new List<Room>();

            // Center of room
            Point newCenter = new Point();

            Random rnd = new Random();

		    // randomize values for each room
            for (int i = 0; i < Settings.Instance.MaxRooms; i++)
            {
                int w = Settings.Instance.MinRoomSize + rnd.Next(Settings.Instance.MaxRoomSize - Settings.Instance.MinRoomSize + 1);
                int h = Settings.Instance.MinRoomSize + rnd.Next(Settings.Instance.MaxRoomSize - Settings.Instance.MinRoomSize + 1);
                int x = rnd.Next(Settings.Instance.DungeonMapWidth - w - 1) + 1;
                int y = rnd.Next(Settings.Instance.DungeonMapHeight - h - 1) + 1;
			        
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
			        CreateRoom(newRoom);

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
			    }
		        if(!failed) rooms.Add(newRoom);
		    }

            // setting start point of player
            for (int i = 0; i < Settings.Instance.DungeonMapWidth; i++)
            {
                for (int j = 0; j < Settings.Instance.DungeonMapHeight; j++)
                {
                    if (Controller.PlacementColors[i, j] == Floor)
                    {
                        Map.SetPixel(i, j, System.Drawing.Color.Red);
                        Controller.StartPoint = new Vector2(i * Settings.Instance.GridSize, j * Settings.Instance.GridSize);

                        //quit both loops
                        i = Settings.Instance.DungeonMapWidth;
                        j = Settings.Instance.DungeonMapHeight;
                    }
                }
            }

            DeleteWalls();

            //draw things in new image
            System.Drawing.Graphics gimage = System.Drawing.Graphics.FromImage(Map);
            gimage.DrawImage(Map, 0, 0);

            //save new image
            Map.Save(ContentPath + @"maps\" + "Dungeon_PlacementMapDebug_forreasons_.png", ImageFormat.Png);
	    }

        public void CreateRoom(Room room)
        {
            for (int i = (int)room.X; i < room.X + room.Width; i++)
            {
                for (int j = (int)room.Y; j < room.Y + room.Height; j++)
                {
                    Map.SetPixel(i, j, System.Drawing.Color.White);
                    Controller.PlacementColors[i, j] = Floor;
                }
            }
        }

        // create horizontal corridor to connect rooms
        private void HCorridor(int x1, int x2, int y) {
            for (int i = (int)Math.Min(x1, x2); i < (int)Math.Max(x1, x2)+1; i++){
			    // destory the tiles to "carve" out corridor
                Map.SetPixel(i, y, System.Drawing.Color.White);
                Controller.PlacementColors[i, y] = Floor;
		    }
	    }

	    // create vertical corridor to connect rooms
	    private void VCorridor(int y1, int y2, int x) {
            for (int i = (int)Math.Min(y1, y2); i < (int)Math.Max(y1, y2) + 1; i++) {
			    // destroy the tiles to "carve" out corridor
                Map.SetPixel(x, i, System.Drawing.Color.White);
                Controller.PlacementColors[x, i] = Floor;
		    }
	    }

        /// <summary>
        /// Deletes all not reachable walls. 
        /// Now not drawing all walls -> better performance
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
                    if (Controller.PlacementColors[i, j] == Wall &&
                        Controller.PlacementColors[i + 1, j] == Wall &&
                        Controller.PlacementColors[i - 1, j] == Wall &&
                        Controller.PlacementColors[i, j + 1] == Wall &&
                        Controller.PlacementColors[i, j - 1] == Wall &&
                        Controller.PlacementColors[i + 1, j + 1] == Wall &&
                        Controller.PlacementColors[i - 1, j + 1] == Wall &&
                        Controller.PlacementColors[i + 1, j - 1] == Wall &&
                        Controller.PlacementColors[i - 1, j - 1] == Wall)
                    {
                        walls.Add(new Point(i, j));
                    }
                }
            }

            // set all pixels in the List to "nothing"
            foreach (Point item in walls)
            {
                Controller.PlacementColors[item.X, item.Y] = Nothing;
                Map.SetPixel(item.X, item.Y, System.Drawing.Color.White);
            }
        }
    }
}
