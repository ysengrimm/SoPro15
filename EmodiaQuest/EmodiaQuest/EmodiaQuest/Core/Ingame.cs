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

namespace EmodiaQuest.Core
{

    public class Ingame : Microsoft.Xna.Framework.Game
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

        public WorldState ActiveWorld;
        GraphicsDeviceManager graphics;
        //public ContentManager Content;
        /// <summary>
        /// The one and only Safeworld
        /// </summary>
        public SafeWorld Safeworld;
        /// <summary>
        /// A List of Dungeons we are albe to switch to
        /// </summary>
        public Dungeon Dungeon;


        /// <summary>
        /// Loading the safeworld and all dungeons
        /// </summary>
        public void LoadIngame(ContentManager content, Vector2 screenSize, GraphicsDeviceManager graphics)
        {
            this.graphics = graphics;
            ActiveWorld = WorldState.Safeworld;
            this.Content = content;
            // loading Safeworld
            Safeworld = new SafeWorld(content);
            Safeworld.LoadContent();
            // loading the dungeon
            Dungeon = new Dungeon(content);       
            Dungeon.LoadContent();

            // setting the collision for the safeworld as default
            CollisionHandler.Instance.SetEnvironmentController(this.Safeworld.Controller);

            Player.Instance.Position = new Vector2(250, 350);
            Player.Instance.CollisionHandler = CollisionHandler.Instance;
            Player.Instance.WindowSize = screenSize;
            Player.Instance.ContentMngr = Content;
            Player.Instance.LoadContent();

        }

        public void UpdateIngame(GameTime gameTime)
        {
            switch(ActiveWorld)
            {
                case WorldState.Safeworld:
                    this.Safeworld.UpdateSafeworld(gameTime);
                    break;
                case WorldState.Dungeon:
                    this.Dungeon.UpdateDungeon(gameTime);
                    break;
            }

            // Camera Update
            Vector3 cameraPos = Vector3.Transform(new Vector3(Player.Instance.Position.X + 9f, 5, Player.Instance.Position.Y + 9f) - new Vector3(Player.Instance.Position.X, 4, Player.Instance.Position.Y),
            Matrix.CreateRotationY((float)(Player.Instance.Angle + Math.PI * 0.75))) + new Vector3(Player.Instance.Position.X, 5, Player.Instance.Position.Y);
            Renderer.Instance.View = Matrix.CreateLookAt(cameraPos, new Vector3(Player.Instance.Position.X, 5, Player.Instance.Position.Y), Vector3.UnitY);

            // Playerupdate
            MouseState mState = Mouse.GetState();
            Player.Instance.Update(gameTime, mState);

            // Netstat update
            EmodiaQuest.Core.NetGraph.Instance.Update(gameTime, Player.Instance.Position.X, Player.Instance.Position.Y, Player.Instance.PlayerState.ToString());

        }

        public void DrawIngame()
        {
            switch (ActiveWorld)
            {
                case WorldState.Safeworld:
                    GraphicsDevice.DepthStencilState = new DepthStencilState { DepthBufferEnable = true };
                    Renderer.Instance.DrawSafeWorld(Safeworld);
                    Renderer.Instance.DrawPlayer(Player.Instance);
                    break;
                case WorldState.Dungeon:
                    Renderer.Instance.DrawDungeon(Dungeon);
                    Renderer.Instance.DrawPlayer(Player.Instance);
                    break;
            }


        }

    }
}
