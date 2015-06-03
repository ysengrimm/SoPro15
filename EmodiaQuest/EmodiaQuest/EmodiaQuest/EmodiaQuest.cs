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
    public class EmodiaQuest : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Renderer rendering = Renderer.Instance;
        SafeWorld safeWorld;

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

        public EmodiaQuest()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Don´t use this method, we are using the LoadContent for everytrhing, which is called once at the beginning of the game.   
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            
            // TODO: use this.Content to load your game content here
            safeWorld = new SafeWorld(Content);
            safeWorld.loadContent();

            //Initialize the matrizes with reasonable values
            world = Matrix.CreateTranslation(new Vector3(0, 0, 0));
            view = Matrix.CreateLookAt(new Vector3(0, 80, 0), new Vector3(40, 0, 40), Vector3.UnitY);
            projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45), 800f / 480f, 0.1f, 200f);
            //initialize the rendering with the matrizes
            rendering.updateProjection(projection);
            rendering.updateWorld(world);
            rendering.updateView(view);
            
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
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            
            rendering.updateWorld(world);
            rendering.updateView(view);
            rendering.updateProjection(projection);
            
            // TODO: Add your update logic here
            
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            rendering.drawSafeWorld(safeWorld);
            base.Draw(gameTime);
        }
    }
}
