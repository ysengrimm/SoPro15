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

namespace EmodiaQuest.Core
{
    /// <summary>
    /// Implement everything which is in the game + the special things of the SafeWorld, like the Caste and stuff
    /// </summary>
    public class SafeWorld : Game
    {
        public EnvironmentController Controller;
        public Texture2D CollisionMap, PlacementMap, ItemMap;
        public ContentManager Content;

        public Human Enemy;
        
        public SafeWorld(ContentManager content)
        {
            this.Content = content;
            Controller = new EnvironmentController();
        }

        /// <summary>
        /// Method for initialising Models and so on in SafeWorld
        /// </summary>
        public override void Initialise(){}
        /// <summary>
        /// Method for loading relevant content for SafeWorld
        /// </summary>
        public override void LoadContent()
        {
            // load some Maps
            PlacementMap = Content.Load<Texture2D>("maps/safeWorld_PlacementMap");
            ItemMap = Content.Load<Texture2D>("maps/safeWorld_ItemMap");

            // generate some Maps
            Controller.CreatePlacementMap(PlacementMap);
            Controller.CreateItemMap(ItemMap);

            // Walls
            EnvironmentController.Object wall1 = new EnvironmentController.Object(Content.Load<Model>("fbxContent/gameobjects/wall1"), new Color(1, 0, 0), new Vector2(1, 1)); Controller.CollisionObjList.Add(wall1);
            EnvironmentController.Object wall2 = new EnvironmentController.Object(Content.Load<Model>("fbxContent/gameobjects/wall2"), new Color(2, 0, 0), new Vector2(1, 1)); Controller.CollisionObjList.Add(wall2);
            // Buildings
            EnvironmentController.Object house1 = new EnvironmentController.Object(Content.Load<Model>("fbxContent/gameobjects/haus1_dim30x10"), new Color(100, 0, 0), new Vector2(1, 3)); Controller.CollisionObjList.Add(house1);
            // Grounds
            EnvironmentController.Object brownWay = new EnvironmentController.Object(Content.Load<Model>("fbxContent/gameobjects/brownway_dim10x10"), new Color(100, 100, 0), new Vector2(1, 1));
            EnvironmentController.Object grasGround = new EnvironmentController.Object(Content.Load<Model>("fbxContent/gameobjects/grasGround_dim10x10"), new Color(0, 100, 0), new Vector2(1, 1));
            // Items
            EnvironmentController.Object item = new EnvironmentController.Object(Content.Load<Model>("fbxContent/items/Point"), new Color(255, 0, 0), new Vector2(1, 1));

            // Insert objects
            Controller.InsertObj(Controller.Wall, wall1.Model, wall1.Color, 0);
            Controller.InsertObj(Controller.Wall, wall2.Model, wall2.Color, 0);
            Controller.InsertObj(Controller.Ground, brownWay.Model, brownWay.Color, 0);
            Controller.InsertObj(Controller.Ground, grasGround.Model, grasGround.Color, 0);
            Controller.InsertObj(Controller.Buildings, house1.Model, house1.Color, 0);
            // Insert items
            Controller.InsertItem(Controller.Items, item.Model, item.Color, 0);

            //now after all collision objects are choosen generate and load collision map
            //Controller.GenerateCollisionMap();
            CollisionMap = Content.Load<Texture2D>("maps/safeWorld_CollisionMap");
            Controller.CreateCollisionMap(CollisionMap);

            // temporary enemy testing
            Enemy = new Human(new Vector3(250, 0, 300), Controller);
            Enemy.LoadContent(this.Content);
        }

        //just for testing the enemy
        public void UpdateSafeworld(GameTime gametime)
        {
            Enemy.Update(gametime);
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
            Enemy.Draw(world, view, projection);
            //drawNPCs();
            //drawHUD();
            //drawPlayer(); <--- nope is in EmodiaQuest.cs
            
        }

        private void DrawEnvironment(Matrix world, Matrix view, Matrix projection)
        {
            Controller.DrawEnvironment(world, view, projection);
        }

    }
}
