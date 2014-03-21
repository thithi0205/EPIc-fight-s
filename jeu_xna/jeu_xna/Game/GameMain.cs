using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Threading;

namespace jeu_xna
{
    class GameMain : Microsoft.Xna.Framework.Game
    {
        // FIELD
        Player LocalPlayer1, LocalPlayer2;
        bool WasKeyDown_Escape = false;
        static int timer_fps, timer_combat;

        public static MenuButton option, retour, menu_principal;
        static Texture2D background;
        static SpriteFont chrono;

        // CONSTRUCTOR
        public GameMain(ContentManager Content)
        {
            //CREATION DES JOUEURS
            LocalPlayer1 = new Player(Ressources.personnage, 300, 230, Direction.Right, Keys.Z, Keys.D, Keys.Q);
            LocalPlayer2 = new Player(Ressources.personnage, 400, 230, Direction.Left, Keys.Up, Keys.Right, Keys.Left);
        }

        // METHODS
        public static void Initialize()
        {
            Options.mediaplayer_volume = MediaPlayer.Volume;
            Options.bruitage_volume = SoundEffect.MasterVolume;
            timer_fps = 0;
            timer_combat = 30;
        }

        public static void LoadContent(ContentManager Content)
        {
            option = new MenuButton(Content.Load<Texture2D>(@"Sprites\MainMenu\button_options"), new Vector2(300, 300));
            background = Content.Load<Texture2D>(@"Sprites\MainMenu\Options\background");
            chrono = Content.Load<SpriteFont>("chronomètre");
            retour = new MenuButton(Content.Load<Texture2D>(@"Sprites\MainMenu\Options\bouton_retour"), new Vector2(Game1.graphics1.GraphicsDevice.Viewport.Width - (20 + 145), 520));
            menu_principal = new MenuButton(Content.Load<Texture2D>(@"Sprites\MainMenu\bouton_menu-principal"), new Vector2(255, 400));
            Options.LoadContent(Content);
        }

        // UPDATE & DRAW
        public void Update(MouseState mouse, KeyboardState keyboard)
        {
            if (keyboard.IsKeyDown(Keys.Escape) && MainMenu.CurrentGameState == GameState.Playing && !WasKeyDown_Escape)
            {
                MainMenu.CurrentGameState = GameState.Pause;
                WasKeyDown_Escape = true;
            }

            else if (keyboard.IsKeyDown(Keys.Escape) && (MainMenu.CurrentGameState == GameState.Pause || MainMenu.CurrentGameState == GameState.Options) && !WasKeyDown_Escape)
            {
                MainMenu.CurrentGameState = GameState.Playing;
                WasKeyDown_Escape = true;
            }

            else if (keyboard.IsKeyUp(Keys.Escape))
            {
                WasKeyDown_Escape = false;
            }

            switch (MainMenu.CurrentGameState)
            {
                case GameState.Playing:
                    LocalPlayer1.Update(mouse, keyboard);
                    LocalPlayer2.Update(mouse, keyboard);
                    timer_fps++;

                    if (timer_fps == 60)
                    {
                        timer_combat--;
                        timer_fps = 0;
                    }

                    if (timer_combat < 0)
                    {
                        timer_combat = 0;
                    }

                    break;

                case GameState.Pause:
                    if (option.isClicked)
                    {
                        MainMenu.CurrentGameState = GameState.Options;
                    }

                    else if (retour.isClicked)
                    {
                        MainMenu.CurrentGameState = GameState.Playing;
                    }

                    else if (menu_principal.isClicked)
                    {
                        MainMenu.CurrentGameState = GameState.MainMenu;
                        Program.thread_menu = new Thread(new ThreadStart(Program.Menu));
                        Program.thread_menu.Start();
                        MainMenu.thread_jeu.Abort();
                    }
                    break;

                case GameState.Options:
                    Options.Update();
                    break;
            }

            //DEBUGGING
            #region Debuging
            Console.Clear();
            Console.WriteLine("mouse : x = " + mouse.X + " ; y = " + mouse.Y + "\n");
            Console.WriteLine("joueur 1 : x = " + LocalPlayer1.Hitbox.X + " ; y = " + LocalPlayer1.Hitbox.Y + "\n");
            Console.WriteLine("joueur 2 : x = " + LocalPlayer2.Hitbox.X + " ; y = " + LocalPlayer2.Hitbox.Y + "\n");
            Console.WriteLine("volume musique : " + Options.mediaplayer_volume + "\n");
            Console.WriteLine("volume bruitages : " + Options.bruitage_volume + "\n");
            if (MainMenu.CurrentGameState == GameState.Playing)
                Console.WriteLine("Playing\n");
            else if (MainMenu.CurrentGameState == GameState.Pause)
                Console.WriteLine("Pause\n");
            else if (MainMenu.CurrentGameState == GameState.Options)
                Console.WriteLine("Options\n");
            #endregion
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            switch (MainMenu.CurrentGameState)
            {
                case GameState.Playing:
                    spriteBatch.Draw(Ressources.Fond, Vector2.Zero, Color.White);
                    spriteBatch.DrawString(chrono, Convert.ToString(timer_combat), new Vector2((Game1.graphics1.GraphicsDevice.Viewport.Width / 2) - 30, ((Game1.graphics1.GraphicsDevice.Viewport.Height + 480) / 2) - 25), Color.White);
                    LocalPlayer1.Draw(spriteBatch);
                    LocalPlayer2.Draw(spriteBatch);
                    break;

                case GameState.Pause:
                    spriteBatch.Draw(background, Vector2.Zero, Color.White);
                    option.Draw(spriteBatch);
                    retour.Draw(spriteBatch);
                    menu_principal.Draw(spriteBatch);
                    break;

                case GameState.Options:
                    Options.Draw(spriteBatch, Content);
                    break;

            }
        }
    }
}
