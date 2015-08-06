using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using EmodiaQuest.Core.GUI;

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
        public static IntVector2[] PossibleResolutions = { new IntVector2(854, 480), new IntVector2(1280, 720), new IntVector2(1920, 1080) };

        /// <summary>
        /// Resolution for the game
        /// </summary>

        //public Vector2 Resolution = new Vector2(1280, 720);

        /// <summary>
        /// Copy of the Graphics Device
        /// </summary>
        public GraphicsDeviceManager GraphicsCopy { get; set; }

        private IntVector2 resolution = PossibleResolutions[0];
        public IntVector2 Resolution
        {
            get{return this.resolution;}
            set
            {
                //this.resolution = value;
                //float monitorWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
                //float monitorHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
                //if(value.X > monitorWidth)
                //{
                //    this.resolution.X = monitorWidth;
                //    this.resolution.Y = monitorHeight;
                //    //GraphicsCopy.IsFullScreen = true;
                //    //GraphicsCopy.ApplyChanges();
                //    this.fullscreen = true;
                //}
                //else if (value.Y > monitorHeight)
                //{
                //    this.resolution.X = monitorWidth;
                //    this.resolution.Y = monitorHeight;
                //    //GraphicsCopy.IsFullScreen = true;
                //    //GraphicsCopy.ApplyChanges();
                //    this.fullscreen = true;
                //}
                //else
                //{
                //    //GraphicsCopy.PreferredBackBufferWidth = resX;
                //    //GraphicsCopy.PreferredBackBufferHeight = resY;

                //}
                //GraphicsCopy.ApplyChanges();

                int monitorWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
                int monitorHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;

                if (value.X > monitorWidth || value.Y > monitorHeight)
                {
                    this.resolution.X = monitorWidth;
                    GraphicsCopy.PreferredBackBufferWidth = this.resolution.X;
                    this.resolution.Y = monitorHeight;
                    GraphicsCopy.PreferredBackBufferHeight = this.resolution.Y;
                    this.Fullscreen = true;
                }
                else
                {
                    this.resolution = value;
                    GraphicsCopy.PreferredBackBufferWidth = this.resolution.X;
                    GraphicsCopy.PreferredBackBufferHeight = this.resolution.Y;
                }
                GraphicsCopy.ApplyChanges();
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
        public float MainVolume;

        public float FXVolume;

        public float MusicVolume;

        public float SpeakerVolume;

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


        /********************************
         *      Map Settings        *
         ********************************/

        public int SafeWorldMapWidth = 50;
        public int SafeWorldMapHeight = 50;

        //for dungeon generation
        public int DungeonMapWidth;
        public int DungeonMapHeight;

        public int MaxRooms = 50;
        public int MinRoomSize = 3;
        public int MaxRoomSize = 5;



        public void loadContent()
        {
            DebugMode = false;

            GridSize = 10;

            //TODO Resolution Fix
            //Resolution = PossibleResolutions[2];

            Fullscreen = false;

            ViewDistance = 900f;

            MainVolume = 1.0f;

            FXVolume = 0.5f;

            MusicVolume = 0.5f;

            SpeakerVolume = 0.75f;

            PlayerSpeed = 0.25f;

            PlayerRotationSpeed = 2f;

            MaxPlayerHealth = 100.0f;

            HumanEnemySpeed = 0.5f;

            MaxHumanEnemyHealth = 100f;
        }


        public enum MeshQuality { High, Middle, Low}

        /// <summary>
        /// Defines different qualities for Meshes, Hight 13.000 vertices, Low = 1500 vertices
        /// </summary>
        public MeshQuality NPCMeshQuality = MeshQuality.High;


    }
}
