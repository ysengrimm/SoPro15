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
        public static GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public static GameStates_Overall Gamestate_Game = GameStates_Overall.StartScreen;
        public static GameStates_Overall Gamestate_Game_SaveUp = GameStates_Overall.StartScreen;
        public static GameStates_Overall Gamestate_Game_Continue = GameStates_Overall.IngameScreen;

        private Vector2 screenSize;

        public EmodiaQuest_Game()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            // What is this doing here???
            // set window size
            graphics.PreferredBackBufferWidth = Settings.Instance.Resolution.X;
            graphics.PreferredBackBufferHeight = Settings.Instance.Resolution.Y;

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

            // Settings init
            EmodiaQuest.Core.Settings.Instance.loadContent();
            EmodiaQuest.Core.Settings.Instance.GraphicsCopy = graphics;

            // ItemList Init
            EmodiaQuest.Core.Items.ItemTestClass.Instance.loadContent();

            // GUI init
            EmodiaQuest.Core.GUI.Controls_GUI.Instance.loadContent();
            EmodiaQuest.Core.GUI.Settings_GUI.Instance.loadContent(Content);
            //EmodiaQuest.Core.GUI.Controls_GUI.Instance.Mouse_GUI.
            EmodiaQuest.Core.GUI.Screens.Start_GUI.Instance.loadContent(Content);
            EmodiaQuest.Core.GUI.Screens.Menu_GUI.Instance.loadContent(Content);
            EmodiaQuest.Core.GUI.Screens.Options_GUI.Instance.loadContent(Content);
            EmodiaQuest.Core.GUI.Screens.Options_GUI.Instance.GraphicsCopy = graphics;
            EmodiaQuest.Core.GUI.Screens.HUD_GUI.Instance.loadContent(Content);
            EmodiaQuest.Core.GUI.Screens.Inventory_GUI.Instance.loadContent(Content);
            EmodiaQuest.Core.GUI.Screens.NPCTalk_GUI.Instance.loadContent(Content);
            EmodiaQuest.Core.GUI.Screens.Keybinding_GUI.Instance.loadContent(Content);
            EmodiaQuest.Core.GUI.Screens.Intro_GUI.Instance.loadContent(Content);
            EmodiaQuest.Core.GUI.Screens.Credits_GUI.Instance.loadContent(Content);
            EmodiaQuest.Core.GUI.Screens.Death_GUI.Instance.loadContent(Content);
            EmodiaQuest.Core.GUI.Screens.End_GUI.Instance.loadContent(Content);

            EmodiaQuest.Core.NetGraph.Instance.LoadContent(Content);

            // set screen size
            screenSize = new Vector2(GraphicsDevice.Viewport.Bounds.Width, GraphicsDevice.Viewport.Bounds.Height);

            TextMessage.Instance.loadContent(Content);

            Ingame.Instance.LoadIngame(Content, screenSize);

            // Load sound
            Jukebox.Instance.LoadJukebox(Content);

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

            if (Gamestate_Game != GameStates_Overall.Pause)
                EmodiaQuest.Core.GUI.Controls_GUI.Instance.update(gameTime);

            // Update Sound
            Jukebox.Instance.UpdateJukebox(gameTime, IsActive);

            if (!IsActive && Gamestate_Game != GameStates_Overall.StartScreen && Gamestate_Game != GameStates_Overall.Pause)
            {
                Gamestate_Game_SaveUp = Gamestate_Game;
                Gamestate_Game = GameStates_Overall.Pause;
            }

            if (IsActive && Gamestate_Game == GameStates_Overall.Pause)
                Gamestate_Game = Gamestate_Game_SaveUp;
                //Gamestate_Game = GameStates_Overall.MenuScreen;
                

            switch (Gamestate_Game)
            {
                case GameStates_Overall.StartScreen:
                    //EmodiaQuest.Core.GUI.Controls_GUI.Instance.update();
                    EmodiaQuest.Core.GUI.Screens.Start_GUI.Instance.update();
                    if (kState.IsKeyDown(Keys.Escape))
                        EmodiaQuest_Game.Gamestate_Game = GameStates_Overall.MenuScreen;
                    break;
                case GameStates_Overall.MenuScreen:
                    //EmodiaQuest.Core.GUI.Controls_GUI.Instance.update();
                    EmodiaQuest.Core.GUI.Screens.Menu_GUI.Instance.update();
                    //if (kState.IsKeyDown(Keys.Escape))
                    //    this.Exit();
                    break;
                case GameStates_Overall.IngameScreen:

                    // Allows the game to exit
                    if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                        this.Exit();
                    //Close Game with Escape
                    //if (kState.IsKeyDown(Keys.Escape))
                    //    this.Exit();

                    // check if game is in focus
                    Player.Instance.GameIsInFocus = IsActive;
                    
                    Ingame.Instance.UpdateIngame(gameTime);

                    // HUD/NetStat
                    EmodiaQuest.Core.NetGraph.Instance.Update(gameTime, Player.Instance.Position.X, Player.Instance.Position.Y, Player.Instance.ActivePlayerState.ToString(), Player.Instance.LastPlayerState.ToString(), Player.Instance.Hp);
                    EmodiaQuest.Core.GUI.Screens.HUD_GUI.Instance.update();
                    break;
                case GameStates_Overall.OptionsScreen:
                    //EmodiaQuest.Core.GUI.Controls_GUI.Instance.update();
                    EmodiaQuest.Core.GUI.Screens.Options_GUI.Instance.update();
                    //if (kState.IsKeyDown(Keys.Escape))
                    //    this.Exit();
                    break;
                case GameStates_Overall.InventoryScreen:
                    EmodiaQuest.Core.GUI.Screens.Inventory_GUI.Instance.update();
                    //if (kState.IsKeyDown(Keys.Escape))
                    //    this.Exit();
                    break;
                case GameStates_Overall.NPCScreen:
                    EmodiaQuest.Core.GUI.Screens.NPCTalk_GUI.Instance.update();
                    //if (kState.IsKeyDown(Keys.Escape))
                    //    this.Exit();
                    break;
                case GameStates_Overall.Pause:
                    //EmodiaQuest.Core.GUI.Screens.Inventory_GUI.Instance.update();
                    break;
                case GameStates_Overall.KeyBindingsScreen:
                    EmodiaQuest.Core.GUI.Screens.Keybinding_GUI.Instance.update();
                    //if (kState.IsKeyDown(Keys.Escape))
                    //    this.Exit();
                    break;
                case GameStates_Overall.IntroScreen:
                    EmodiaQuest.Core.GUI.Screens.Intro_GUI.Instance.update();
                    if (kState.IsKeyDown(Keys.Escape))
                        EmodiaQuest_Game.Gamestate_Game = GameStates_Overall.IngameScreen;
                    break;
                case GameStates_Overall.CreditsScreen:
                    EmodiaQuest.Core.GUI.Screens.Credits_GUI.Instance.update();
                    //if (kState.IsKeyDown(Keys.Escape))
                    //    this.Exit();
                    break;
                case GameStates_Overall.DeathScreen:
                    EmodiaQuest.Core.GUI.Screens.Death_GUI.Instance.update();
                    if (kState.IsKeyDown(Keys.Escape))
                        this.Exit();
                    break;
                case GameStates_Overall.EndScreen:
                    EmodiaQuest.Core.GUI.Screens.End_GUI.Instance.update();
                    break;
                default:
                    Console.WriteLine("Wrong Gamestate chosen.");
                    break;
            }

            // if we change the window size outside the constructor (e.g. dynamic window size or fullscreen on/off) we need to call this 
            //graphics.ApplyChanges();

            // Update of TextMessages
            if (Gamestate_Game != GameStates_Overall.Pause)
                TextMessage.Instance.update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game graphic is drawn.
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
                    Ingame.Instance.DrawIngame(spriteBatch);
                    break;
                case GameStates_Overall.OptionsScreen:

                    this.IsMouseVisible = true;
                    EmodiaQuest.Core.GUI.Screens.Options_GUI.Instance.draw(spriteBatch);
                    break;
                case GameStates_Overall.InventoryScreen:

                    this.IsMouseVisible = true;
                    EmodiaQuest.Core.GUI.Screens.Inventory_GUI.Instance.draw(spriteBatch);
                    break;
                case GameStates_Overall.NPCScreen:

                    this.IsMouseVisible = true;
                    EmodiaQuest.Core.GUI.Screens.NPCTalk_GUI.Instance.draw(spriteBatch);
                    break;
                case GameStates_Overall.Pause:
                    //EmodiaQuest.Core.GUI.Screens.Inventory_GUI.Instance.update();
                    break;
                case GameStates_Overall.KeyBindingsScreen:

                    this.IsMouseVisible = true;
                    EmodiaQuest.Core.GUI.Screens.Keybinding_GUI.Instance.draw(spriteBatch);
                    break;
                case GameStates_Overall.IntroScreen:

                    this.IsMouseVisible = true;
                    EmodiaQuest.Core.GUI.Screens.Intro_GUI.Instance.draw(spriteBatch);
                    break;
                case GameStates_Overall.CreditsScreen:

                    this.IsMouseVisible = true;
                    EmodiaQuest.Core.GUI.Screens.Credits_GUI.Instance.draw(spriteBatch);
                    break;
                case GameStates_Overall.DeathScreen:

                    this.IsMouseVisible = false;
                    EmodiaQuest.Core.GUI.Screens.Death_GUI.Instance.draw(spriteBatch);
                    break;
                case GameStates_Overall.EndScreen:

                    this.IsMouseVisible = false;
                    EmodiaQuest.Core.GUI.Screens.End_GUI.Instance.draw(spriteBatch);
                    break;
                default:
                    Console.WriteLine("Wrong Gamestate chosen.");
                    break;
            }
            // Drawing of TextMessages
            if (Gamestate_Game != GameStates_Overall.Pause)
                TextMessage.Instance.draw(spriteBatch);

            base.Draw(gameTime);
        }
    }
}
