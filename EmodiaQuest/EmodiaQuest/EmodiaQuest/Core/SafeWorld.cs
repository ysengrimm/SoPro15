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
        private static SafeWorld instance;

        private SafeWorld() { }

        public static SafeWorld Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SafeWorld();
                }
                return instance;
            }
        }
        public EnvironmentController Controller;
        public Texture2D CollisionMap, PlacementMap, ItemMap;
        public ContentManager Content;
        public Skybox Skybox;

        public List<NPC> NPCList;
        

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
            Skybox = new Skybox(Content.Load<Model>("fbxContent/skybox"), new Vector2(250, 250));
            // Environment Controller dor the Safeworld
            Controller = new EnvironmentController(WorldState.Safeworld, Content);
            // load some Maps
            PlacementMap = Content.Load<Texture2D>("maps/safeWorld_PlacementMap");
            ItemMap = Content.Load<Texture2D>("maps/safeWorld_ItemMap");

            // generate some Maps
            Controller.CreatePlacementMap(PlacementMap);
            Controller.CreateItemMap(ItemMap);

            // Walls
            EnvironmentController.Object wall1 = new EnvironmentController.Object(Content.Load<Model>("fbxContent/gameobjects/wall1"), new Color(1, 0, 0), new Vector2(1, 1), "wall1", false); Controller.CollisionObjList.Add(wall1);
            EnvironmentController.Object wall2 = new EnvironmentController.Object(Content.Load<Model>("fbxContent/gameobjects/wall2"), new Color(2, 0, 0), new Vector2(1, 1), "wall2", false); Controller.CollisionObjList.Add(wall2);
            // Buildings
            EnvironmentController.Object house2 = new EnvironmentController.Object(Content.Load<Model>("fbxContent/gameobjects/Wohnhaus2_30x10/Wohnhaus2_dim30x10"), new Color(10, 0, 0), new Vector2(1, 3), "house1", false); Controller.CollisionObjList.Add(house2);
            EnvironmentController.Object taverne = new EnvironmentController.Object(Content.Load<Model>("fbxContent/gameobjects/Taverne_30x10/Taverne_dim30x10"), new Color(11, 0, 0), new Vector2(1, 3), "taverne", false); Controller.CollisionObjList.Add(taverne);
            EnvironmentController.TeleObject wallDoor = new EnvironmentController.TeleObject(Content.Load<Model>("fbxContent/gameobjects/mauerTor30x10"), new Color(3, 0, 0), new Vector2(1, 3), new Vector2(0, 0), "wallDoor", false); Controller.TeleporterObjList.Add(wallDoor);
            // Grounds
            EnvironmentController.Object brownWay = new EnvironmentController.Object(Content.Load<Model>("fbxContent/gameobjects/brownway_dim10x10"), new Color(100, 100, 0), new Vector2(1, 1), "brownWay", false);
            EnvironmentController.Object grasGround = new EnvironmentController.Object(Content.Load<Model>("fbxContent/gameobjects/grasGround_dim10x10"), new Color(0, 100, 0), new Vector2(1, 1), "grasGround", false);
           
            // Insert objects
            Controller.InsertObj(Controller.Wall, wall1.Model, wall1.Color, 0, wall1.Name, wall1.IsRandomStuff);
            Controller.InsertObj(Controller.Wall, wall2.Model, wall2.Color, 0, wall2.Name, wall2.IsRandomStuff);
            Controller.InsertObj(Controller.Ground, brownWay.Model, brownWay.Color, 0, brownWay.Name, brownWay.IsRandomStuff);
            Controller.InsertObj(Controller.Ground, grasGround.Model, grasGround.Color, 0, grasGround.Name, grasGround.IsRandomStuff);
            Controller.InsertObj(Controller.Buildings, house2.Model, house2.Color, 0, house2.Name, house2.IsRandomStuff);
            Controller.InsertObj(Controller.Buildings, taverne.Model, taverne.Color, 0, taverne.Name, taverne.IsRandomStuff);
            Controller.InsertObj(Controller.Teleporter, wallDoor.Model, wallDoor.Color, 0, wallDoor.Name, wallDoor.IsRandomStuff);

            //now after all collision objects are choosen generate and load collision map
            
            Controller.GenerateCollisionMap(Content);

            
            // Create NPCList
            NPCList = new List<NPC>();
            // Create NPCs
            NPC Jack = new NPC(new Vector2(230,330), Controller, NPC.NPCName.Jack, NPCProfession.Wirt);
            NPC Yorlgon = new NPC(new Vector2(200,220), Controller, NPC.NPCName.Yorlgon, NPCProfession.Schmied);
            NPC Konstantin = new NPC(new Vector2(200, 240), Controller, NPC.NPCName.Konstantin, NPCProfession.Haendler);

            Jack.LoadContent(Content);
            Yorlgon.LoadContent(Content);
            Konstantin.LoadContent(Content);
            
            NPCList.Add(Jack);
            NPCList.Add(Yorlgon);
            NPCList.Add(Konstantin);
            
        }

        /// <summary>
        /// Should be called in the Ingame Class, after the Player and the Collision Handler were Loaded
        /// </summary>
        public void CreateRandomStuff()
        {
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

            // Insert Random Stuff (after the collisionMap was generated, we don´t want random stuff in walls and buildings)
            Controller.InsertRandomStuffObj(Controller.RandomStuff, Gras_1.Model, 500, Gras_1.Name, Gras_1.IsRandomStuff);
            Controller.InsertRandomStuffObj(Controller.RandomStuff, Gras_2.Model, 500, Gras_2.Name, Gras_2.IsRandomStuff);
            Controller.InsertRandomStuffObj(Controller.RandomStuff, Gras_3.Model, 500, Gras_3.Name, Gras_3.IsRandomStuff);
            Controller.InsertRandomStuffObj(Controller.RandomStuff, Busch_1.Model, 300, Busch_1.Name, Busch_1.IsRandomStuff);
            Controller.InsertRandomStuffObj(Controller.RandomStuff, Busch_2.Model, 300, Busch_2.Name, Busch_2.IsRandomStuff);
            Controller.InsertRandomStuffObj(Controller.RandomStuff, Busch_3.Model, 300, Busch_3.Name, Busch_3.IsRandomStuff);
            Controller.InsertRandomStuffObj(Controller.RandomStuff, Stein_1.Model, 4000, Stein_1.Name, Stein_1.IsRandomStuff);
            Controller.InsertRandomStuffObj(Controller.RandomStuff, Stein_2.Model, 4000, Stein_2.Name, Stein_2.IsRandomStuff);
            Controller.InsertRandomStuffObj(Controller.RandomStuff, Stein_3.Model, 4000, Stein_3.Name, Stein_3.IsRandomStuff);

            // After everything in the environment is added we load the stuff for the objects (textures etc.)
            Controller.LoadEnvironment(Content);
        }

        public void UpdateSafeworld(GameTime gametime)
        {
            // Update for the Skybox
            Skybox.Position = new Vector3(Player.Instance.Position.X, 70, Player.Instance.Position.Y);

            // Updates NPCs
            foreach(NPC npc in NPCList)
            {
                npc.Update(gametime);
            }
            Controller.UpdateEnvironment(gametime);
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
            //Draw the NPCs
            foreach(NPC npc in NPCList)
            {
                npc.Draw(world, view, projection);
            }
           
        }

        private void DrawEnvironment(Matrix world, Matrix view, Matrix projection)
        {
            Controller.DrawEnvironment(world, view, projection);
            Skybox.Draw(world, view, projection, Ingame.Instance.SkyBoxTex_Interstellar);
        }

    }
}
