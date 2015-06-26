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
        public bool DebugMode = false;

        /********************************
         *      Graphics Settings        *
         ********************************/
        /// <summary>
        /// All possible resolutions (all 16:9)
        /// </summary>
        /// TODO: why static ?!
        public static Vector2[] PossibleResolutions = { new Vector2(854, 480), new Vector2(1280, 720), new Vector2(1920, 1080) };

        /// <summary>
        /// Resolution for the game
        /// </summary>
        public Vector2 Resolution = PossibleResolutions[0];

        public bool Fullscreen = false;

        /// <summary>
        /// Far plane distance
        /// </summary>
        public float ViewDistance = 900f;

        /********************************
         *      Audio Settings        *
         ********************************/
        /// <summary>
        /// Volume of the sound
        /// </summary>
        public float Volume = 1.0f;

        /********************************
         *      Player Settings        *
         ********************************/
        /// <summary>
        /// Player movement speed.
        /// </summary>
        public float PlayerSpeed = 0.5f;

        /// <summary>
        /// Player camera rotation speed.
        /// </summary>
        public float PlayerRotationSpeed = 2f;

        /// <summary>
        /// Player health
        /// </summary>
        public float MaxPlayerHealth = 100f;


        /********************************
         *      Enemy Settings        *
         ********************************/

        /// <summary>
        /// Human enemy speed
        /// </summary>
        public float HumanEnemySpeed = 0.5f;

        /// <summary>
        /// Player health
        /// </summary>
        public float MaxHumanEnemyHealth = 100f;



    }
}
