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

        public static GameStates_Overall Gamestate_Game = GameStates_Overall.IngameScreen;


        Renderer rendering = Renderer.Instance;
        SafeWorld safeWorld;
        CollisionHandler collisionHandler;

        // TODO move to InGameScreen
        private Player player;

        private Vector2 windowSize;

        /// <summary>
        /// Stores the world matrix for the model, which transforms the 
        /// model to be in the correct position, scale, and rotation
        /// in the game world.
        /// </summary>
        private Matrix world;

        /// <summary>
        /// Stores the view matrix for the model, which gets the model
        /// in the right place, relative to the camera.
        /// </summary>
        private Matrix view;

        /// <summary>
        /// Stores the projection matrix, which gets the model projected
        /// onto the screen in the correct way.  Essentially, this defines the
        /// properties of the camera you are using.
        /// </summary>
        private Matrix projection;

        public EmodiaQuest_Game()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
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
            EmodiaQuest.Core.GUI.Screens.Start_GUI.Instance.loadContent(Content);
            EmodiaQuest.Core.GUI.Screens.Menu_GUI.Instance.loadContent(Content);

            safeWorld = new SafeWorld(Content);
            safeWorld.loadContent();
            // Initialize Player
            windowSize = new Vector2(GraphicsDevice.Viewport.Bounds.Width, GraphicsDevice.Viewport.Bounds.Height);
            player = new Player(new Vector2(40, 40), collisionHandler, windowSize);
            player.Model = Content.Load<Model>("fbxContent/mainchar_sopro_sculp3sub_colored");
            //Initialize the matrizes with reasonable values
            world = Matrix.CreateTranslation(new Vector3(0, 0, 0));
            view = Matrix.CreateLookAt(new Vector3(player.Position.X + 5f, 5, player.Position.Y + 5f), new Vector3(player.Position.X, 2, player.Position.Y), Vector3.UnitY);
            projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45), 800f / 480f, 0.1f, 200f);
            //initialize the rendering with the matrizes
            rendering.UpdateProjection(projection);
            rendering.UpdateWorld(world);
            rendering.UpdateView(view);

            collisionHandler = new CollisionHandler(safeWorld.controller);




            player = new Player(new Vector2(100, 100), collisionHandler, windowSize);
            player.Model = Content.Load<Model>("fbxContent/mainchar_sopro_sculp3sub_colored");
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
            switch (Gamestate_Game)
            {
                case GameStates_Overall.StartScreen:
                    EmodiaQuest.Core.GUI.Controls_GUI.Instance.update();
                    EmodiaQuest.Core.GUI.Screens.Start_GUI.Instance.update();
                    break;
                case GameStates_Overall.MenuScreen:
                    EmodiaQuest.Core.GUI.Controls_GUI.Instance.update();
                    EmodiaQuest.Core.GUI.Screens.Menu_GUI.Instance.update();
                    break;
                case GameStates_Overall.IngameScreen:


                    // Allows the game to exit
                    if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                        this.Exit();

                    //Close Game with Escape
                    KeyboardState kState = Keyboard.GetState();
                    if (kState.IsKeyDown(Keys.Escape))
                        this.Exit();

                    view = Matrix.CreateLookAt(new Vector3(player.Position.X + 15f, 20, player.Position.Y + 15f), new Vector3(player.Position.X, 5, player.Position.Y), Vector3.UnitY);
                    rendering.UpdateWorld(world);
                    rendering.UpdateView(view);
                    rendering.UpdateProjection(projection);

                    // TODO: Add your update logic here

                    MouseState ms = Mouse.GetState();
                    player.Update(gameTime, ms);

                    break;
                case GameStates_Overall.OptionsScreen:
                    break;
                default:
                    break;
            }





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
                    GraphicsDevice.DepthStencilState = new DepthStencilState() { DepthBufferEnable = true };
                    break;
                case GameStates_Overall.MenuScreen:
                    this.IsMouseVisible = true;
                    EmodiaQuest.Core.GUI.Screens.Menu_GUI.Instance.draw(spriteBatch);
                    GraphicsDevice.DepthStencilState = new DepthStencilState() { DepthBufferEnable = true };
                    break;
                case GameStates_Overall.IngameScreen:
                    this.IsMouseVisible = false;
                    rendering.DrawSafeWorld(safeWorld);
                    player.Draw(rendering.world, rendering.view, rendering.projection);
                    break;
                case GameStates_Overall.OptionsScreen:
                    break;
                default:
                    break;
            }



            base.Draw(gameTime);
        }
    }
}
