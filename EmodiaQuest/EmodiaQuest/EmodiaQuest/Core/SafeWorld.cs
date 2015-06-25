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
using EmodiaQuest.Core;
using EmodiaQuest.Rendering;

namespace EmodiaQuest.Core
{
    /// <summary>
    /// Implement everything which is in the game + the special things of the SafeWorld, like the Caste and stuff
    /// </summary>
    public class SafeWorld : Game
    {
        public EnvironmentController controller;
        public Texture2D CollisionMap, PlacementMap, ItemMap;
        public ContentManager content;
        public Model wall1, wall2, wall3, wall4, wall5, wall6, wall7, wall8; // wall with direction to bottom, right, top, left.
        public Model brownWay, grasGround;
        public Model house1, house2;
        public Model item;
        public Model Groundplate1;

        public SafeWorld(ContentManager content)
        {
            this.content = content;
            controller = new EnvironmentController();
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
            // Maps
            PlacementMap = content.Load<Texture2D>("maps/safeWorld_PlacementMap");
            CollisionMap = content.Load<Texture2D>("maps/safeWorld_CollisionMap");
            ItemMap = content.Load<Texture2D>("maps/safeWorld_ItemMap");
            controller.CreatePlacementMap(PlacementMap);
            controller.CreateCollisionMap(CollisionMap);
            controller.CreateItemMap(ItemMap);
            // Walls
            wall1 = content.Load<Model>("fbxContent/gameobjects/wall1"); Color wall1C = new Color(1, 0, 0); //normal wall
            wall2 = content.Load<Model>("fbxContent/gameobjects/wall2"); Color wall5C = new Color(2, 0, 0); //corner wall

            // Grounds
            brownWay = content.Load<Model>("fbxContent/gameobjects/brownway_dim10x10"); Color brownWayC = new Color(100, 100, 0);
            grasGround = content.Load<Model>("fbxContent/gameobjects/grasGround_dim10x10"); Color greenGroundC = new Color(0, 100, 0);
            // Buildings
            house1 = content.Load<Model>("fbxContent/gameobjects/haus1_dim30x10"); Color house1C = new Color(100, 0, 0);
            // Items
            item = content.Load<Model>("fbxContent/items/Point"); Color itemC = new Color(255, 0, 0);
            // Insert objects
            controller.InsertObj(controller.wall, wall1, wall1C, 0);
            controller.InsertObj(controller.wall, wall2, wall5C, 0);
            controller.InsertObj(controller.ground, brownWay, brownWayC, 0);
            controller.InsertObj(controller.ground, grasGround, greenGroundC, 0);
            controller.InsertObj(controller.buildings, house1, house1C, 0);
            // Insert items
            controller.InsertItem(controller.items, item, itemC, 0);
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
            //drawNPCs();
            //drawHUD();
            //drawPlayer(); <--- nope is in EmodiaQuest.cs
            
        }

        private void DrawEnvironment(Matrix world, Matrix view, Matrix projection)
        {
            controller.DrawEnvironment(world, view, projection);
        }

    }
}
