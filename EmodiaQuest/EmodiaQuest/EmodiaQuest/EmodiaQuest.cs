using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using EmodiaQuest.Core;
using EmodiaQuest.Rendering;

namespace EmodiaQuest
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class EmodiaQuest_Game : Microsoft.Xna.Framework.Game
    {
        // Only this two and the enum-init should be here
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public static GameStates_Overall Gamestate_Game = GameStates_Overall.StartScreen;
        SafeWorld safeWorld;

        private Vector2 screenSize;

        public EmodiaQuest_Game()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            // set window size
            graphics.PreferredBackBufferWidth = (int)Settings.Instance.Resolution.X;
            graphics.PreferredBackBufferHeight = (int) Settings.Instance.Resolution.Y;

            // set fullscreen
            graphics.IsFullScreen = Settings.Instance.Fullscreen;
        }


        protected override void Initialize()
        {
            // TODO: Don´t use this method, we are using LoadContent-Method
            base.Initialize();
        }

        protected override void LoadContent()
        {
            // This has to stay here!
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // This is how it should look:
            // Notice how it can be called without an instance!
            
            // GUI init
            EmodiaQuest.Core.GUI.Controls_GUI.Instance.loadContent();
            EmodiaQuest.Core.GUI.Settings_GUI.Instance.loadContent(Content);
            //EmodiaQuest.Core.GUI.Controls_GUI.Instance.Mouse_GUI.
            EmodiaQuest.Core.GUI.Screens.Start_GUI.Instance.loadContent(Content);
            EmodiaQuest.Core.GUI.Screens.Menu_GUI.Instance.loadContent(Content);
            EmodiaQuest.Core.GUI.Screens.Options_GUI.Instance.loadContent(Content);

            EmodiaQuest.Core.NetGraph.Instance.LoadContent(Content);

            // Safeworld Init
            safeWorld = new SafeWorld(Content);
            safeWorld.LoadContent();
            // Collision Init
            CollisionHandler.Instance.SetEnvironmentController(safeWorld.Controller);

            // set screen size
            screenSize = new Vector2(GraphicsDevice.Viewport.Bounds.Width, GraphicsDevice.Viewport.Bounds.Height);

            // Init player
            Player.Instance.Position = new Vector2(250, 350);
            Player.Instance.CollisionHandler = CollisionHandler.Instance;
            Player.Instance.WindowSize = screenSize;
            Player.Instance.ContentMngr = Content;
            Player.Instance.LoadContent();

            //Initialize the matrizes with reasonable values
            Renderer.Instance.World = Matrix.CreateTranslation(new Vector3(0, 0, 0));
            Renderer.Instance.View = Matrix.CreateLookAt(new Vector3(Player.Instance.Position.X + 3f, 3, Player.Instance.Position.Y + 3f), new Vector3(Player.Instance.Position.X, 1, Player.Instance.Position.Y), Vector3.UnitY);
            Renderer.Instance.Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45), Settings.Instance.Resolution.X / Settings.Instance.Resolution.Y, 0.1f, Settings.Instance.ViewDistance); //Setting farplane = render distance

            Mouse.WindowHandle = Window.Handle;
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            KeyboardState kState = Keyboard.GetState();
            switch (Gamestate_Game)
            {
                case GameStates_Overall.StartScreen:
                    EmodiaQuest.Core.GUI.Controls_GUI.Instance.update();
                    EmodiaQuest.Core.GUI.Screens.Start_GUI.Instance.update();
                    if (kState.IsKeyDown(Keys.Escape))
                        this.Exit();
                    break;
                case GameStates_Overall.MenuScreen:
                    EmodiaQuest.Core.GUI.Controls_GUI.Instance.update();
                    EmodiaQuest.Core.GUI.Screens.Menu_GUI.Instance.update();
                    if (kState.IsKeyDown(Keys.Escape))
                        this.Exit();
                    break;
                case GameStates_Overall.IngameScreen:

                    // Allows the game to exit
                    if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                        this.Exit();
                    //Close Game with Escape
                    if (kState.IsKeyDown(Keys.Escape))
                        this.Exit();

                    Vector3 cameraPos = Vector3.Transform(new Vector3(Player.Instance.Position.X + 9f, 5, Player.Instance.Position.Y + 9f) - new Vector3(Player.Instance.Position.X, 4, Player.Instance.Position.Y), 
                        Matrix.CreateRotationY((float) (Player.Instance.Angle + Math.PI * 0.75))) + new Vector3(Player.Instance.Position.X, 5, Player.Instance.Position.Y);
                    Renderer.Instance.View = Matrix.CreateLookAt(cameraPos, new Vector3(Player.Instance.Position.X, 5, Player.Instance.Position.Y), Vector3.UnitY);

                    MouseState mState = Mouse.GetState();
                    Player.Instance.Update(gameTime, mState);

                    // check if game is in focus
                    Player.Instance.GameIsInFocus = IsActive;
                    //just for enemytesting in the safeworld
                    safeWorld.UpdateSafeworld(gameTime);
                    // HUD/NetStat
                    EmodiaQuest.Core.NetGraph.Instance.Update(gameTime, Player.Instance.Position.X, Player.Instance.Position.Y, Player.Instance.PlayerState.ToString());

                    break;
                case GameStates_Overall.OptionsScreen:
                    EmodiaQuest.Core.GUI.Controls_GUI.Instance.update();
                    EmodiaQuest.Core.GUI.Screens.Options_GUI.Instance.update();
                    if (kState.IsKeyDown(Keys.Escape))
                        this.Exit();
                    break;
                default:
                    break;
            }

            // if we change the window size outside the constructor (e.g. dynamic window size or fullscreen on/off) we need to call this 
            //graphics.ApplyChanges();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            switch (Gamestate_Game)
            {
                case GameStates_Overall.StartScreen:
                    this.IsMouseVisible = true;
                    EmodiaQuest.Core.GUI.Screens.Start_GUI.Instance.draw(spriteBatch);
                    break;
                case GameStates_Overall.MenuScreen:
                    
                    this.IsMouseVisible = true;
                    EmodiaQuest.Core.GUI.Screens.Menu_GUI.Instance.draw(spriteBatch);
                    
                    break;
                case GameStates_Overall.IngameScreen:
                    this.IsMouseVisible = false;
                    GraphicsDevice.DepthStencilState = new DepthStencilState { DepthBufferEnable = true };
                    Renderer.Instance.DrawSafeWorld(safeWorld);
                    Renderer.Instance.DrawPlayer(Player.Instance);

                    // HUD/NetStat
                    EmodiaQuest.Core.NetGraph.Instance.Draw(spriteBatch);
                    
                    break;
                case GameStates_Overall.OptionsScreen:
                    
                    this.IsMouseVisible = true;
                    EmodiaQuest.Core.GUI.Screens.Options_GUI.Instance.draw(spriteBatch);
                    break;
                default:
                    break;
            }

            base.Draw(gameTime);
        }
    }
}
