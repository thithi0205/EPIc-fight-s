using System;
using System.Threading;

namespace jeu_xna
{
#if WINDOWS || XBOX
    static class Program
    {
        public static Thread thread_menu;

        public static void Main(string[] args)
        {
            Program.thread_menu = new Thread(new ThreadStart(Menu));

            if (State.CurrentGameState == GameState.MainMenu)
            {
                thread_menu.Start();
            }
        }

        public static void Menu()
        {
            using (MainMenu game = new MainMenu())
            {
                game.Run();
            }
        }
    }
#endif
}

