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

    public class Ingame
    {
        private static Ingame instance;

        private Ingame() { }

        public static Ingame Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Ingame();
                }
                return instance;
            }
        }

        void IngameEventValue(object source, ChangeValueEvent e)
        {
            switch (e.Function)
            {
                case "hp":
                    Console.WriteLine("HP is set to: " + e.ChangeValue);
                    break;
                default:
                    Console.WriteLine("Function name does not exist");
                    break;
            }
        }

        public WorldState ActiveWorld;
        public ContentManager Content;
        /// <summary>
        /// A List of Dungeons we are albe to switch to
        /// </summary>
        public Dungeon Dungeon;
        /// <summary>
        /// Textures for the Skyboxes in both worlds
        /// </summary>
        public Texture2D SkyBoxTex_Interstellar, SkyBoxTex_ViolentDays;
        /// <summary>
        /// Loading the safeworld and all dungeons
        /// </summary>
        public void LoadIngame(ContentManager content, Vector2 screenSize)
        {
            
            this.Content = content;
            // Loading skybox textures
            SkyBoxTex_Interstellar = Content.Load<Texture2D>("Texturen/Skybox/interstellar_large");
            SkyBoxTex_ViolentDays = Content.Load<Texture2D>("Texturen/Skybox/violentdays_large");

            // Quests
            QuestController.Instance.LoadContent(Content);

            ActiveWorld = WorldState.Safeworld;
            // loading Safeworld
            SafeWorld.Instance.LoadContent(Content);

            // setting the collision for the safeworld as default
            CollisionHandler.Instance.SetEnvironmentController(SafeWorld.Instance.Controller);

            // Loading Player
            Player.Instance.Position = new Vector2(250, 350);
            Player.Instance.CollisionHandler = CollisionHandler.Instance;
            Player.Instance.WindowSize = screenSize;
            Player.Instance.ContentMngr = Content;
            Player.Instance.LoadContent();
            Player.Instance.GameEnv = SafeWorld.Instance.Controller;

            // filling the safeworld with random Stuff
            SafeWorld.Instance.CreateRandomStuff();

            //Initialize the matrizes with reasonable values
            Renderer.Instance.World = Matrix.CreateTranslation(new Vector3(0, 0, 0));
            Renderer.Instance.View = Matrix.CreateLookAt(new Vector3(Player.Instance.Position.X + 3f, 3, Player.Instance.Position.Y + 3f), new Vector3(Player.Instance.Position.X, 1, Player.Instance.Position.Y), Vector3.UnitY);
            Renderer.Instance.Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45), (float)Settings.Instance.Resolution.X / Settings.Instance.Resolution.Y, 0.1f, Settings.Instance.ViewDistance); //Setting farplane = render distance

        }

        public void UpdateIngame(GameTime gameTime)
        {
            // Update camera and view matrices
            Vector3 cameraPos = Vector3.Transform(new Vector3(Player.Instance.Position.X + 7f, 2.5f, Player.Instance.Position.Y + 7f) - new Vector3(Player.Instance.Position.X, 4, Player.Instance.Position.Y),
                Matrix.CreateRotationY((float)(Player.Instance.Angle + Math.PI * 0.75))) + new Vector3(Player.Instance.Position.X, 5, Player.Instance.Position.Y);
            Renderer.Instance.View = Matrix.CreateLookAt(cameraPos, new Vector3(Player.Instance.Position.X, 2.5f, Player.Instance.Position.Y), Vector3.UnitY);

            // Playerupdate
            Player.Instance.Update(gameTime);

            // Decides, which world should be updated
            switch (ActiveWorld)
            {
                case WorldState.Safeworld:
                    SafeWorld.Instance.UpdateSafeworld(gameTime);
                    break;
                case WorldState.Dungeon:
                    this.Dungeon.UpdateDungeon(gameTime);
                    break;
            }

            // Netstat update
            EmodiaQuest.Core.NetGraph.Instance.Update(gameTime, Player.Instance.Position.X, Player.Instance.Position.Y, Player.Instance.ActivePlayerState.ToString(), Player.Instance.LastPlayerState.ToString(), Player.Instance.Hp);
            // TODO: HUD update (playerhealth etc.)

        }

        public void DrawIngame(SpriteBatch spriteBatch)
        {
            switch (ActiveWorld)
            {
                case WorldState.Safeworld:
                    Renderer.Instance.DrawSafeWorld(SafeWorld.Instance);
                    break;
                case WorldState.Dungeon:
                    Renderer.Instance.DrawDungeon(Dungeon);
                    break;
            }
            Renderer.Instance.DrawPlayer(Player.Instance);

            // NetStat
            EmodiaQuest.Core.NetGraph.Instance.Draw(spriteBatch);
            // HUD
            EmodiaQuest.Core.GUI.Screens.HUD_GUI.Instance.draw(spriteBatch);
            // TODO: Damage Numbers over enemies
            // TODO: dialogue window for interaction with npcs
        }

        public void ChangeToDungeon()
        {
            CreateNewDungeon();     // everytime the player enters the dungeon it generates a new one
            ActiveWorld = WorldState.Dungeon;
            Player.Instance.GameEnv = this.Dungeon.Controller;
            Player.Instance.Position = this.Dungeon.Controller.StartPoint;
            CollisionHandler.Instance.SetEnvironmentController(Dungeon.Controller);
            Dungeon.CreateRandomStuff();
        }

        public void ChangeToSafeworld()
        {
            Player.Instance.BulletList.Clear();
            ActiveWorld = WorldState.Safeworld;
            Player.Instance.GameEnv = SafeWorld.Instance.Controller;
            Player.Instance.Position = new Vector2(170, 330);
            CollisionHandler.Instance.SetEnvironmentController(SafeWorld.Instance.Controller);
        }

        /// <summary>
        /// Generating and loading new dungeon
        /// </summary>
        public void CreateNewDungeon()
        {     
            Dungeon = new Dungeon(100, 100, 100);
            Dungeon.LoadContent(Content);
        }
    }
}
