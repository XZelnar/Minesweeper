using System;

namespace MineSweeper
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (MineSweeper game = new MineSweeper())
            {
                game.Run();
            }
        }
    }
#endif
}

