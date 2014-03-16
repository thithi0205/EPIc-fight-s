using System;
using System.Threading;

namespace jeu_xna
{
#if WINDOWS || XBOX
    static class Program
    {
        public static void Main(string[] args)
        {
            Thread thread_menu = new Thread(new ThreadStart(Menu));
            Thread thread_jeu = new Thread(jeu);
            if (MainMenu.CurrentGameState == GameState.MainMenu)
            {
                thread_menu.Start();
            }

            else if (MainMenu.CurrentGameState == GameState.Playing)
            {
                thread_menu.Abort();
                thread_jeu.Start();
            }
        }

        public static void Menu()
        {
            using (MainMenu game = new MainMenu())
            {
            if (MainMenu.CurrentGameState == GameState.MainMenu)
            {
                game.Run();   
            }

            else
            {
                game.Exit();
            }
            }
        }

        public static void jeu()
        {
            using (Game1 game1 = new Game1())
            {
                game1.Run();
            }
        }
    }
#endif
}

