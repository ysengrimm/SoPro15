using System;

namespace EmodiaQuest
{
#if WINDOWS || XBOX
    static class Programm
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (EmodiaQuest_Game game = new EmodiaQuest_Game())
            {
                game.Run();
            }
        }
    }
#endif
}

