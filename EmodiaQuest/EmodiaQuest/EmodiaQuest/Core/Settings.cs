using Microsoft.Xna.Framework;

namespace EmodiaQuest.Core
{
    class Settings
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
        public Vector2 Resolution = PossibleResolutions[0];

        public bool Fullscreen;

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

            //TODO Resotion Fix
            //Resolution = PossibleResolutions[0];

            Fullscreen = false;

            ViewDistance = 900f;

            Volume = 1.0f;

            PlayerSpeed = 0.25f;

            PlayerRotationSpeed = 2f;

            MaxPlayerHealth = 100f;

            HumanEnemySpeed = 0.5f;

            MaxHumanEnemyHealth = 100f;
        }


    }
}
