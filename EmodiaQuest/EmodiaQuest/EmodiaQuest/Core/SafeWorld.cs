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
        public Texture2D CollisionMap, PlacementMap;
        public ContentManager content;
        public Model wall1, wall2, wall3, wall4, wall5, wall6, wall7, wall8; // wall with direction to bottom, right, top, left.
        public Model brownWay, grasGround;
        public Model house1, house2;
        public Model Groundplate1;

        public SafeWorld(ContentManager content)
        {
            this.content = content;
            controller = new EnvironmentController();
        }

        /// <summary>
        /// Method for initialising Models and so on in SafeWorld
        /// </summary>
        public override void initialise(){}

        /// <summary>
        /// Method for loading relevant content for SafeWorld
        /// </summary>
        public override void loadContent()
        {
            // Maps
            PlacementMap = content.Load<Texture2D>("maps/safeWorld_PlacementMap");
            CollisionMap = content.Load<Texture2D>("maps/safeWorld_CollisionMap");
            controller.createPlacementMap(PlacementMap);
            controller.createCollisionMap(CollisionMap);
            // Walls
            wall1 = content.Load<Model>("fbxContent/gameobjects/wall1"); Color wall1C = new Color(1, 0, 0); 
            wall2 = content.Load<Model>("fbxContent/gameobjects/wall2"); Color wall2C = new Color(2, 0, 0);
            wall3 = content.Load<Model>("fbxContent/gameobjects/wall3"); Color wall3C = new Color(3, 0, 0);
            wall4 = content.Load<Model>("fbxContent/gameobjects/wall4"); Color wall4C = new Color(4, 0, 0);
            wall5 = content.Load<Model>("fbxContent/gameobjects/wall5"); Color wall5C = new Color(5, 0, 0);
            wall6 = content.Load<Model>("fbxContent/gameobjects/wall6"); Color wall6C = new Color(6, 0, 0);
            wall7 = content.Load<Model>("fbxContent/gameobjects/wall7"); Color wall7C = new Color(7, 0, 0);
            wall8 = content.Load<Model>("fbxContent/gameobjects/wall8"); Color wall8C = new Color(8, 0, 0);
            // Grounds
            brownWay = content.Load<Model>("fbxContent/gameobjects/brownway_dim10x10"); Color brownWayC = new Color(100, 100, 100);
            grasGround = content.Load<Model>("fbxContent/gameobjects/grasGround_dim10x10"); Color greenGroundC = new Color(0, 100, 0);
            // Buildings
            house1 = content.Load<Model>("fbxContent/gameobjects/haus1_dim30x10"); Color house1C = new Color(100, 0, 0);
            house2 = content.Load<Model>("fbxContent/gameobjects/haus2_dim10x30"); Color house2C = new Color(101, 0, 0);


            controller.insertObj(controller.wall, wall1, wall1C, 0);
            controller.insertObj(controller.wall, wall2, wall2C, 0);
            controller.insertObj(controller.wall, wall3, wall3C, 0);
            controller.insertObj(controller.wall, wall4, wall4C, 0);
            controller.insertObj(controller.wall, wall5, wall5C, 0);
            controller.insertObj(controller.wall, wall6, wall6C, 0);
            controller.insertObj(controller.wall, wall7, wall7C, 0);
            controller.insertObj(controller.wall, wall8, wall8C, 0);
            controller.insertObj(controller.ground, brownWay, brownWayC, 0);
            controller.insertObj(controller.ground, grasGround, greenGroundC, 0);
            controller.insertObj(controller.buildings, house1, house1C, 0);
            controller.insertObj(controller.buildings, house2, house2C, 0);
        }

        /// <summary>
        /// Method for drawing the whole GameScreen
        /// Especially the following:
        /// Environment, NPCs, HUD, Player
        /// </summary>
        /// <param name="world">the world Matrix, which will be used for rendering</param>
        /// <param name="view">the view Matrix, which will be used for the rendering</param>
        /// <param name="projection">the projection Matrix, which will be used for the rendering</param>
        public override void drawGameScreen(Matrix world, Matrix view, Matrix projection)
        {
            drawEnvironment(world, view, projection);
            //drawNPCs();
            //drawHUD();
            //drawPlayer(); <--- nope is in EmodiaQuest.cs
            
        }

        private void drawEnvironment(Matrix world, Matrix view, Matrix projection)
        {
            controller.drawEnvironment(world, view, projection);
        }

    }
}
