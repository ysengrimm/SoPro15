﻿using System;
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
using EmodiaQuest.Core;
using EmodiaQuest.Rendering;
using EmodiaQuest.Core.NPCs;
using EmodiaQuest.Core.DungeonGeneration;

namespace EmodiaQuest.Core
{
    public class Dungeon : Game
    {
        public EnvironmentController Controller;
        public Texture2D CollisionMap, PlacementMap, ItemMap;
        public ContentManager Content;
        public Skybox Skybox;

        public List<Enemy> EnemyList;

        /// <summary>
        /// placement collision radius
        /// </summary>
        public float CR;

        private int numEnemies;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="numEnemies"> The numbers of Enemies, which will be placed in this Dungeon</param>
        public Dungeon(int numEnemies, int mapWidth, int mapHeight)
        {
            Settings.Instance.DungeonMapSize = mapWidth;
            Settings.Instance.DungeonMapSize = mapHeight;

            Controller = new EnvironmentController(WorldState.Dungeon);
            this.numEnemies = numEnemies;
        }
        private Random r;

        /// <summary>
        /// Method for initialising Models and so on in SafeWorld
        /// </summary>
        public override void Initialise(){}
        /// <summary>
        /// Method for loading relevant content for SafeWorld
        /// </summary>
        public override void LoadContent(ContentManager content)
        {
            this.Content = content;

            CR = 5;
            //gets >current< content path
            //at first gets path of debug directory and then replace the end to get path of content folder
            string contentPath = Path.GetDirectoryName(Environment.CurrentDirectory).Replace(@"EmodiaQuest\bin\x86", @"EmodiaQuestContent\");
            
            Skybox = new Skybox(Content.Load<Model>("fbxContent/skybox"), new Vector2(250, 250));
            
            // generate new dungeon
            LevelGenerator gen = new LevelGenerator(Controller);

            //initialise enemy array
            Controller.CreateEnemyArray();

            // Walls
            EnvironmentController.Object wall1 = new EnvironmentController.Object(Content.Load<Model>("fbxContent/gameobjects/wall1"), new Color(1, 0, 0), new Vector2(1, 1)); Controller.CollisionObjList.Add(wall1);
            EnvironmentController.Object wall2 = new EnvironmentController.Object(Content.Load<Model>("fbxContent/gameobjects/wall2"), new Color(2, 0, 0), new Vector2(1, 1)); Controller.CollisionObjList.Add(wall2);
            // Buildings
            EnvironmentController.Object house1 = new EnvironmentController.Object(Content.Load<Model>("fbxContent/gameobjects/haus1_dim30x10"), new Color(100, 0, 0), new Vector2(1, 3)); Controller.CollisionObjList.Add(house1);
            EnvironmentController.TeleObject wallDoor = new EnvironmentController.TeleObject(Content.Load<Model>("fbxContent/gameobjects/mauerTor30x10"), new Color(3, 0, 0), new Vector2(1, 3), new Vector2(0, 0)); Controller.TeleporterObjList.Add(wallDoor);
            // Grounds
            EnvironmentController.Object brownWay = new EnvironmentController.Object(Content.Load<Model>("fbxContent/gameobjects/grasGround_dim10x10"), new Color(100, 100, 0), new Vector2(1, 1));
            EnvironmentController.Object grasGround = new EnvironmentController.Object(Content.Load<Model>("fbxContent/gameobjects/grasGround_dim10x10"), new Color(0, 100, 0), new Vector2(1, 1));
            // Items
            EnvironmentController.Object item = new EnvironmentController.Object(Content.Load<Model>("fbxContent/items/Point"), new Color(255, 0, 0), new Vector2(1, 1));

            // Nothing
            // this is important for level generation 
            // it allows to build black pixels in collision map for every point in map, wich has no model
            // (it's for performance)
            // Do not insert in the map !!!
            EnvironmentController.Object nothing = new EnvironmentController.Object(null, new Color(0, 0, 0), Vector2.One); Controller.CollisionObjList.Add(nothing);

            // Insert objects
            Controller.InsertObj(Controller.Wall, wall1.Model, wall1.Color, 0);
            Controller.InsertObj(Controller.Wall, wall2.Model, wall2.Color, 0);
            Controller.InsertObj(Controller.Ground, brownWay.Model, brownWay.Color, 0);
            Controller.InsertObj(Controller.Ground, grasGround.Model, grasGround.Color, 0);
            Controller.InsertObj(Controller.Buildings, house1.Model, house1.Color, 0);
            Controller.InsertObj(Controller.Teleporter, wallDoor.Model, wallDoor.Color, 0);
            // Insert items
            Controller.InsertItem(Controller.Items, item.Model, item.Color, 0);

            //now after all collision objects are choosen generate collision map
            Controller.GenerateCollisionMap(Content);

            // temporary enemy testing
            EnemyList = new List<Enemy>();
            r = new Random();
            for(int i = 0; i < numEnemies; i++)
            {
                float x1 = (float)r.NextDouble() * 500;
                float y1 = (float)r.NextDouble() * 500;
                int x = (int) x1 / Settings.Instance.GridSize;
                int y = (int) y1 / Settings.Instance.GridSize;
                if(Controller.CollisionColors[x, y] != Color.Black && Controller.enemyArray[x,y].Count < 1)
                {
                    if (Controller.CollisionColors[(int)(x1 + CR) / Settings.Instance.GridSize, (int)(y1 + CR) / Settings.Instance.GridSize] != Color.Black && Controller.CollisionColors[(int)(x1 - CR) / Settings.Instance.GridSize, (int)(y1 - CR) / Settings.Instance.GridSize] != Color.Black)
                    {
                        if (Controller.CollisionColors[(int)(x1 - CR) / Settings.Instance.GridSize, (int)(y1 + CR) / Settings.Instance.GridSize] != Color.Black && Controller.CollisionColors[(int)(x1 + CR) / Settings.Instance.GridSize, (int)(y1 - CR) / Settings.Instance.GridSize] != Color.Black)
                        {
                            if (Controller.CollisionColors[(int)(x1 - CR) / Settings.Instance.GridSize, y] != Color.Black && Controller.CollisionColors[(int)(x1 + CR) / Settings.Instance.GridSize, y] != Color.Black)
                            {
                                if (Controller.CollisionColors[x, (int)(y1 - CR) / Settings.Instance.GridSize] != Color.Black && Controller.CollisionColors[x, (int)(y1 + CR) / Settings.Instance.GridSize] != Color.Black)
                                {
                                    //Enemy enemy = new Enemy(new Vector2(x1, y1), Controller, EnemyType.NPCTest);
                                    Enemy enemy = new Enemy(new Vector2(x1, y1), Controller, EnemyType.Monster3);
                                    EnemyList.Add(enemy);
                                }
                            }
                        }
                    }
                }
                else
                {
                    //Console.WriteLine("a enemy would be placed an a rock! Try again to place the Enemy");
                    i--;
                }
            }
            foreach (Enemy enemy in EnemyList)
            {
                enemy.LoadContent(Content);
            }
            //Console.WriteLine(EnemyList.Count);
        }

        
        public void UpdateDungeon(GameTime gametime)
        {
            //just for testing the enemy
            foreach (Enemy enemy in EnemyList)
            {
                enemy.Update(gametime);
            }

            // Update for the Skybox
            Skybox.Position = new Vector3(Player.Instance.Position.X, 70, Player.Instance.Position.Y);
        }

        /// <summary>
        /// Method for drawing the whole GameScreen
        /// Especially the following:
        /// Environment, NPCs, HUD, Player
        /// </summary>
        /// <param name="world">the world Matrix, which will be used for rendering</param>
        /// <param name="view">the view Matrix, which will be used for the rendering</param>
        /// <param name="projection">the projection Matrix, which will be used for the rendering</param>
        public override void DrawGameScreen(Matrix world, Matrix view, Matrix projection)
        {
            DrawEnvironment(world, view, projection);
            foreach (Enemy enemy in EnemyList)
            {
                enemy.Draw(world, view, projection);
            }          
        }

        private void DrawEnvironment(Matrix world, Matrix view, Matrix projection)
        {
            Controller.DrawEnvironment(world, view, projection);
            Skybox.Draw(world, view, projection, Ingame.Instance.SkyBoxTex_ViolentDays);
        }

    }
}
