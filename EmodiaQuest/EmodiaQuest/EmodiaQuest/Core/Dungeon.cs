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

        public LevelGenerator generator;

        private EnemyType[] enemies;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="numEnemies"> The numbers of Enemies, which will be placed in this Dungeon</param>
        /// <param name="mapWidth"></param>
        /// <param name="mapHeight"></param>
        /// <param name="enemies"> Array of enemies to appear in the dungeon </param>
        public Dungeon(int numEnemies, int mapWidth, int mapHeight, EnemyType[] enemies)
        {
            this.enemies = enemies;

            Settings.Instance.DungeonMapSize = mapWidth;
            Settings.Instance.DungeonMapSize = mapHeight;

            Controller = new EnvironmentController(WorldState.Dungeon, Content);

            Settings.Instance.NumEnemies = numEnemies;
        }

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

            //gets >current< content path
            //at first gets path of debug directory and then replace the end to get path of content folder
            string contentPath = Path.GetDirectoryName(Environment.CurrentDirectory).Replace(@"EmodiaQuest\bin\x86", @"EmodiaQuestContent\");
            
            Skybox = new Skybox(Content.Load<Model>("fbxContent/skybox"), new Vector2(250, 250));

            // generate new dungeon
            generator = new LevelGenerator(Controller, enemies);            

            // Walls
            EnvironmentController.Object wall = new EnvironmentController.Object(Content.Load<Model>("fbxContent/gameobjects/NormalWall_10x10/NormalWall_10x10"), ColorListDungeon.Instance.Wall, new Vector2(1, 1), "wall", false); Controller.CollisionObjList.Add(wall);
            // Grounds
            EnvironmentController.Object ground = new EnvironmentController.Object(Content.Load<Model>("fbxContent/gameobjects/Ground_10x10/Ground_10x10"), ColorListDungeon.Instance.Ground, new Vector2(1, 1), "ground", false);
            // Items
            EnvironmentController.Object item = new EnvironmentController.Object(Content.Load<Model>("fbxContent/items/Point"), ColorListDungeon.Instance.Item, new Vector2(1, 1), "item", false);
            // Teleporter
            EnvironmentController.TeleObject teleporter = new EnvironmentController.TeleObject(Content.Load<Model>("fbxContent/gameobjects/Teleporter_10x10/Teleporter_10x10"), ColorListDungeon.Instance.Teleporter, new Vector2(1, 1), new Vector2(0, 0), "teleporter", false); Controller.TeleporterObjList.Add(teleporter);

            // Nothing
            // this is important for level generation 
            // it allows to build black pixels in collision map for every point in map, wich has no model
            // (it's for performance)
            // Do not insert in the map !!!
            EnvironmentController.Object nothing = new EnvironmentController.Object(null, ColorListDungeon.Instance.Nothing, Vector2.One, "nothing", false); Controller.CollisionObjList.Add(nothing);

            // Insert objects
            Controller.InsertObj(Controller.Wall, wall.Model, wall.Color, 0, wall.Name, wall.IsRandomStuff);
            Controller.InsertObj(Controller.Ground, ground.Model,ground.Color, 0, ground.Name, ground.IsRandomStuff);
            Controller.InsertObj(Controller.Teleporter, teleporter.Model, teleporter.Color, 0, teleporter.Name, teleporter.IsRandomStuff);
            // Insert items
            Controller.InsertItem(Controller.Items, item.Model, item.Color, 0, item.Name, item.IsRandomStuff);

            //now after all collision objects are choosen generate collision map
            Controller.GenerateCollisionMap(Content);

            foreach (Enemy enemy in generator.EnemyList)
            {
                enemy.LoadContent(Content);
            }             
        }
        
        public void UpdateDungeon(GameTime gametime)
        {
            foreach (Enemy enemy in generator.EnemyList)
            {
                enemy.Update(gametime); ;
            }
            Controller.UpdateEnvironment(gametime);

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
            foreach (Enemy enemy in generator.EnemyList)
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
