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

        public int EnemiesAlive;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="numEnemies"> The numbers of Enemies, which will be placed in this Dungeon. Number should be mod possible enemy types == 0</param>
        /// <param name="mapWidth"></param>
        /// <param name="mapHeight"></param>
        /// <param name="enemy"> Questenemy, wich is essential to be in dungeon </param>
        public Dungeon(int numEnemies, int mapWidth, int mapHeight)
        {
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
            generator = new LevelGenerator(Controller);            

            // Walls
            EnvironmentController.Object wall = new EnvironmentController.Object(Content.Load<Model>("fbxContent/gameobjects/NormalWall_10x10/NormalWall_10x10"), ColorListDungeon.Instance.Wall, new Vector2(1, 1), "wall", false); Controller.CollisionObjList.Add(wall);
            // Grounds
            EnvironmentController.Object ground = new EnvironmentController.Object(Content.Load<Model>("fbxContent/gameobjects/Ground_10x10/Ground_10x10"), ColorListDungeon.Instance.Ground, new Vector2(1, 1), "ground", false);
            // Teleporter
            EnvironmentController.TeleObject teleporter = new EnvironmentController.TeleObject(Content.Load<Model>("fbxContent/gameobjects/Teleporter_10x10/Teleporter_10x10"), ColorListDungeon.Instance.Teleporter, new Vector2(1, 1), new Vector2(0, 0), "teleporter", false); Controller.TeleporterObjList.Add(teleporter);

            // Nothing
            // this is important for level generation 
            // it allows to build black pixels in collision map for every point in map, wich has no model
            // (it's for performance)
            // Do not insert in the map !!!
            EnvironmentController.Object nothing = new EnvironmentController.Object(null, ColorListDungeon.Instance.Nothing, Vector2.One, "nothing", false); Controller.CollisionObjList.Add(nothing);

            EnvironmentController.Object item = new EnvironmentController.Object(Content.Load<Model>("fbxContent/items/Point"), ColorListDungeon.Instance.Item, new Vector2(1, 1), "item", false);

            // Insert objects
            Controller.InsertObj(Controller.Wall, wall.Model, wall.Color, 0, wall.Name, wall.IsRandomStuff);
            Controller.InsertObj(Controller.Ground, ground.Model,ground.Color, 0, ground.Name, ground.IsRandomStuff);
            Controller.InsertObj(Controller.Teleporter, teleporter.Model, teleporter.Color, 0, teleporter.Name, teleporter.IsRandomStuff);

            Controller.InsertItem(Controller.Items, item.Model, item.Color, 0, item.Name, item.IsRandomStuff);

            //now after all collision objects are choosen generate collision map
            Controller.GenerateCollisionMap(Content);

            foreach (Enemy enemy in generator.EnemyList)
            {
                enemy.LoadContent(Content);
            }             
        }

        /// <summary>
        /// Should be called in the Ingame Class, after the Player and the Collision Handler were Loaded
        /// </summary>
        public void CreateRandomStuff()
        {
            Controller.RandomStuff.Clear();
            // Random Stuff
            EnvironmentController.Object Gras_1 = new EnvironmentController.Object(Content.Load<Model>("fbxContent/gameobjects/RandomStuff/Gras_1/Gras_1"), new Color(300, 300, 0), new Vector2(0, 0), "Gras_1", true);
            EnvironmentController.Object Gras_2 = new EnvironmentController.Object(Content.Load<Model>("fbxContent/gameobjects/RandomStuff/Gras_2/Gras_2"), new Color(300, 300, 0), new Vector2(0, 0), "Gras_2", true);
            EnvironmentController.Object Gras_3 = new EnvironmentController.Object(Content.Load<Model>("fbxContent/gameobjects/RandomStuff/Gras_3/Gras_3"), new Color(300, 300, 0), new Vector2(0, 0), "Gras_3", true);
            EnvironmentController.Object Busch_1 = new EnvironmentController.Object(Content.Load<Model>("fbxContent/gameobjects/RandomStuff/Busch_1/Busch_1"), new Color(300, 300, 0), new Vector2(0, 0), "Busch_1", true);
            EnvironmentController.Object Busch_2 = new EnvironmentController.Object(Content.Load<Model>("fbxContent/gameobjects/RandomStuff/Busch_2/Busch_2"), new Color(300, 300, 0), new Vector2(0, 0), "Busch_2", true);
            EnvironmentController.Object Busch_3 = new EnvironmentController.Object(Content.Load<Model>("fbxContent/gameobjects/RandomStuff/Busch_3/Busch_3"), new Color(300, 300, 0), new Vector2(0, 0), "Busch_3", true);
            EnvironmentController.Object Stein_1 = new EnvironmentController.Object(Content.Load<Model>("fbxContent/gameobjects/RandomStuff/Stein_1/Stein_1"), new Color(300, 300, 0), new Vector2(0, 0), "Stein_1", true);
            EnvironmentController.Object Stein_2 = new EnvironmentController.Object(Content.Load<Model>("fbxContent/gameobjects/RandomStuff/Stein_2/Stein_2"), new Color(300, 300, 0), new Vector2(0, 0), "Stein_2", true);
            EnvironmentController.Object Stein_3 = new EnvironmentController.Object(Content.Load<Model>("fbxContent/gameobjects/RandomStuff/Stein_3/Stein_3"), new Color(300, 300, 0), new Vector2(0, 0), "Stein_3", true);
            EnvironmentController.Object Holzplanke_1 = new EnvironmentController.Object(Content.Load<Model>("fbxContent/gameobjects/RandomStuff/Holzplanke_1/Holzplanke_1"), new Color(300, 300, 0), new Vector2(0, 0), "Holzplanke_1", true);
            EnvironmentController.Object Kaputter_Flugzeugmotor_1 = new EnvironmentController.Object(Content.Load<Model>("fbxContent/gameobjects/RandomStuff/Kaputter_Flugzeugmotor_1/Kaputter_Flugzeugmotor_1"), new Color(300, 300, 0), new Vector2(0, 0), "Kaputter_Flugzeugmotor_1", true);
            EnvironmentController.Object MetallStück_1 = new EnvironmentController.Object(Content.Load<Model>("fbxContent/gameobjects/RandomStuff/MetallStück_1/MetallStück_1"), new Color(300, 300, 0), new Vector2(0, 0), "MetallStück_1", true);
            EnvironmentController.Object MetallStück_2 = new EnvironmentController.Object(Content.Load<Model>("fbxContent/gameobjects/RandomStuff/MetallStück_2/MetallStück_2"), new Color(300, 300, 0), new Vector2(0, 0), "MetallStück_2", true);
            EnvironmentController.Object Tonne_1 = new EnvironmentController.Object(Content.Load<Model>("fbxContent/gameobjects/RandomStuff/Tonne_1/Tonne_1"), new Color(300, 300, 0), new Vector2(0, 0), "Tonne_1", true);
            EnvironmentController.Object Tumbleweed_1 = new EnvironmentController.Object(Content.Load<Model>("fbxContent/gameobjects/RandomStuff/Tumbleweed_1/Tumbleweed_1"), new Color(300, 300, 0), new Vector2(0, 0), "Tumbleweed_1", true);


            // Insert Random Stuff (after the collisionMap was generated, we don´t want random stuff in walls and buildings)
            Controller.InsertRandomStuffObj(Controller.RandomStuff, Gras_1.Model, 100, Gras_1.Name, Gras_1.IsRandomStuff);
            Controller.InsertRandomStuffObj(Controller.RandomStuff, Gras_2.Model, 100, Gras_2.Name, Gras_2.IsRandomStuff);
            Controller.InsertRandomStuffObj(Controller.RandomStuff, Gras_3.Model, 100, Gras_3.Name, Gras_3.IsRandomStuff);
            Controller.InsertRandomStuffObj(Controller.RandomStuff, Busch_1.Model, 100, Busch_1.Name, Busch_1.IsRandomStuff);
            Controller.InsertRandomStuffObj(Controller.RandomStuff, Busch_2.Model, 100, Busch_2.Name, Busch_2.IsRandomStuff);
            Controller.InsertRandomStuffObj(Controller.RandomStuff, Busch_3.Model, 100, Busch_3.Name, Busch_3.IsRandomStuff);
            Controller.InsertRandomStuffObj(Controller.RandomStuff, Stein_1.Model, 1000, Stein_1.Name, Stein_1.IsRandomStuff);
            Controller.InsertRandomStuffObj(Controller.RandomStuff, Stein_2.Model, 1000, Stein_2.Name, Stein_2.IsRandomStuff);
            Controller.InsertRandomStuffObj(Controller.RandomStuff, Stein_3.Model, 1000, Stein_3.Name, Stein_3.IsRandomStuff);
            Controller.InsertRandomStuffObj(Controller.RandomStuff, Holzplanke_1.Model, 100, Holzplanke_1.Name, Holzplanke_1.IsRandomStuff);
            Controller.InsertRandomStuffObj(Controller.RandomStuff, Kaputter_Flugzeugmotor_1.Model, 100, Kaputter_Flugzeugmotor_1.Name, Kaputter_Flugzeugmotor_1.IsRandomStuff);
            Controller.InsertRandomStuffObj(Controller.RandomStuff, MetallStück_1.Model, 100, MetallStück_1.Name, MetallStück_1.IsRandomStuff);
            Controller.InsertRandomStuffObj(Controller.RandomStuff, MetallStück_2.Model, 100, MetallStück_2.Name, MetallStück_2.IsRandomStuff);
            Controller.InsertRandomStuffObj(Controller.RandomStuff, Tonne_1.Model, 100, Tonne_1.Name, Tonne_1.IsRandomStuff);
            Controller.InsertRandomStuffObj(Controller.RandomStuff, Tumbleweed_1.Model, 100, Tumbleweed_1.Name, Tumbleweed_1.IsRandomStuff);

            // After everything in the environment is added we load the stuff for the objects (textures etc.)
            Controller.LoadEnvironment(Content);
        }

        public void UpdateDungeon(GameTime gametime)
        {
            EnemiesAlive = 0;
            foreach (Enemy enemy in generator.EnemyList)
            {
                if (enemy.IsAlive)
                {
                    EnemiesAlive++;
                }
                enemy.Update(gametime);
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
