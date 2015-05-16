using System;

namespace EmodiaQuest
{
#if WINDOWS || XBOX
    static class EmodiaQuest
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (EmodiaQuestMain game = new EmodiaQuestMain())
            {
                game.Run();
            }
        }
    }
#endif
}

