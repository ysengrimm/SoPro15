﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EmodiaQuest.Core
{
    public class Settings
    {
        private static Settings instance;

        private Settings() { }

        public static Settings Instance
        {
            get { return instance ?? (instance = new Settings()); }
        }

        /********************************
         *      General Settings        *
         ********************************/
        /// <summary>
        /// Debug output
        /// </summary>
        public bool DebugMode;

        /// <summary>
        /// Size of a grid element.
        /// </summary>
        public int GridSize;

        /********************************
         *      Graphics Settings        *
         ********************************/
        /// <summary>
        /// All possible resolutions (all 16:9)
        /// </summary>
        /// //TODO No static
        public static Vector2[] PossibleResolutions = { new Vector2(854, 480), new Vector2(1280, 720), new Vector2(1920, 1080) };

        /// <summary>
        /// Resolution for the game
        /// </summary>

        //public Vector2 Resolution = new Vector2(1280, 720);

        /// <summary>
        /// Copy of the Graphics Device
        /// </summary>
        public GraphicsDeviceManager GraphicsCopy { get; set; }

        private Vector2 resolution = PossibleResolutions[0];
        public Vector2 Resolution
        {
            get{return this.resolution;}
            set
            {
                this.resolution = value;
                float monitorWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
                float monitorHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
                if(value.X > monitorWidth)
                {
                    this.resolution.X = monitorWidth;
                    this.resolution.Y = monitorHeight;
                    GraphicsCopy.IsFullScreen = true;
                    GraphicsCopy.ApplyChanges();
                }
                if (value.Y > monitorHeight)
                {
                    this.resolution.X = monitorWidth;
                    this.resolution.Y = monitorHeight;
                    GraphicsCopy.IsFullScreen = true;
                    GraphicsCopy.ApplyChanges();
                }
            }
        }

        private bool fullscreen = false;

        public bool Fullscreen
        {
            get { return fullscreen; }
            set 
            {
                if(value!= this.fullscreen)
                {
                    GraphicsCopy.IsFullScreen = value;
                    GraphicsCopy.ApplyChanges();
                }
                this.fullscreen = value;

            }
        }


        /// <summary>
        /// Far plane distance
        /// </summary>
        public float ViewDistance;

        /********************************
         *      Audio Settings        *
         ********************************/
        /// <summary>
        /// Volume of the sound
        /// </summary>
        public float Volume;

        /********************************
         *      Player Settings        *
         ********************************/
        /// <summary>
        /// Player movement speed.
        /// </summary>
        public float PlayerSpeed;

        /// <summary>
        /// Player camera rotation speed.
        /// </summary>
        public float PlayerRotationSpeed;

        /// <summary>
        /// Player health
        /// </summary>
        public float MaxPlayerHealth;


        /********************************
         *      Enemy Settings        *
         ********************************/

        /// <summary>
        /// Human enemy speed
        /// </summary>
        public float HumanEnemySpeed;

        /// <summary>
        /// Player health
        /// </summary>
        public float MaxHumanEnemyHealth;

        


        public void loadContent()
        {
            DebugMode = false;

            GridSize = 10;

            //TODO Resolution Fix
            //Resolution = PossibleResolutions[2];

            Fullscreen = false;

            ViewDistance = 900f;

            Volume = 1.0f;

            PlayerSpeed = 0.25f;

            PlayerRotationSpeed = 2f;

            MaxPlayerHealth = 100.0f;

            HumanEnemySpeed = 0.5f;

            MaxHumanEnemyHealth = 100f;
        }


    }
}
