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
            using (EmodiaQuest game = new EmodiaQuest())
            {
                game.Run();
            }
        }
    }
#endif
}

