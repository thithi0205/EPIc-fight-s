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
        public static Player LocalPlayer1, LocalPlayer2;
        public static int personnage_choisi1, personnage_choisi2, terrain_choisi;
        static bool WasKeyDown_Escape, was_displayed; 
        public static int timer_fps, timer_combat_secondes, timer_combat_minutes;
        static int fps_counter;

        public static MenuButton option, retour, menu_principal, quitter;
        static Texture2D background, picture_identity, ready, fight, game_over;
        static SpriteFont chrono, pause;

        // CONSTRUCTOR
        public GameMain(ContentManager Content)
        {
        }

        // METHODS
        public static new void Initialize()
        {
            Options.mediaplayer_volume = MediaPlayer.Volume;
            Options.bruitage_volume = SoundEffect.MasterVolume;
            timer_fps = 0;
            timer_combat_secondes = -2;
            timer_combat_minutes = 0;
            WasKeyDown_Escape = false;
            was_displayed = false;
            fps_counter = 0;
        }

        public static void LoadContent(ContentManager Content)
        {
            picture_identity = Content.Load<Texture2D>(@"Sprites\Personnages\identité1");

            //CREATION DES JOUEURS
            LocalPlayer1 = new Player(Ressources.caracters[personnage_choisi1], 275, 230, Direction.Right, Keys.Z, Keys.D, Keys.Q, "Player 1", 1, Content, Keys.F);
            LocalPlayer2 = new Player(Ressources.caracters[personnage_choisi2], 425, 230, Direction.Left, Keys.Up, Keys.Right, Keys.Left, "Player 2", 2, Content, Keys.RightShift);

            option = new MenuButton(Content.Load<Texture2D>(@"Sprites\MainMenu\button_options"), new Vector2(300, 300));
            background = Content.Load<Texture2D>(@"Sprites\MainMenu\Options\background");
            chrono = Content.Load<SpriteFont>("chronomètre");
            retour = new MenuButton(Content.Load<Texture2D>(@"Sprites\MainMenu\Options\bouton_retour"), new Vector2(Game1.graphics1.GraphicsDevice.Viewport.Width - (20 + 145), 520));
            menu_principal = new MenuButton(Content.Load<Texture2D>(@"Sprites\MainMenu\bouton_menu-principal"), new Vector2(255, 200));
            quitter = new MenuButton(Content.Load<Texture2D>(@"Sprites\MainMenu\bouton_quitter"), new Vector2(300, 400));
            Options.LoadContent(Content);

            pause = Content.Load<SpriteFont>(@"Sprites\MainMenu\Options\Font\pause");
            ready = Content.Load<Texture2D>(@"Sprites\Game\ready");
            fight = Content.Load<Texture2D>(@"Sprites\Game\fight");
            game_over = Content.Load<Texture2D>(@"Sprites\Game\game_over");

            ChangeControls.LoadContent(Content);
        }

        // UPDATE & DRAW
        public void Update(MouseState mouse, KeyboardState keyboard)
        {
            //Console.Clear();
            GameMain.option.Update(MainMenu.mouse);
            GameMain.retour.Update(MainMenu.mouse);
            GameMain.menu_principal.Update(MainMenu.mouse);
            GameMain.quitter.Update(MainMenu.mouse);

            if (keyboard.IsKeyDown(Keys.Escape) && State.CurrentGameState == GameState.Playing && !WasKeyDown_Escape)
            {
                State.CurrentGameState = GameState.Pause;
                WasKeyDown_Escape = true;
            }

            else if (keyboard.IsKeyDown(Keys.Escape) && (State.CurrentGameState == GameState.Pause || State.CurrentGameState == GameState.Options || State.CurrentGameState == GameState.Commandes) && !WasKeyDown_Escape)
            {
                State.CurrentGameState = GameState.Playing;
                WasKeyDown_Escape = true;
            }

            else if (keyboard.IsKeyUp(Keys.Escape))
            {
                WasKeyDown_Escape = false;
            }

            switch (State.CurrentGameState)
            {
                case GameState.Playing:
                    if (timer_combat_secondes >= 0)
                    {
                        LocalPlayer1.Update(mouse, keyboard);
                        LocalPlayer2.Update(mouse, keyboard);
                    }
                    timer_fps++;

                    //CHRONOMETRE
                    if (timer_fps == 60)
                    {
                        if (timer_combat_secondes >= 59)
                        {
                            timer_combat_minutes++;
                            timer_combat_secondes = 0;
                        }

                        else
                        {
                            timer_combat_secondes++;
                        }
                        
                        timer_fps = 0;
                    }
                    break;

                case GameState.Pause:
                    if (option.isClicked)
                    {
                        State.CurrentGameState = GameState.Options;
                        Options.was_cliqued = true;
                    }

                    else if (retour.isClicked)
                    {
                        State.CurrentGameState = GameState.Playing;
                    }

                    else if (menu_principal.isClicked)
                    {
                        State.CurrentGameState = GameState.MainMenu;
                        Program.thread_menu = new Thread(new ThreadStart(Program.Menu));
                        Program.thread_menu.Start();
                        MainMenu.thread_jeu.Abort();
                    }

                    else if (quitter.isClicked)
                    {
                        MainMenu.thread_jeu.Abort();
                    }
                    break;

                case GameState.Options:
                    Options.Update();
                    break;

                case GameState.Commandes:
                    ChangeControls.Update(keyboard);
                    break;
            }

            //DEBUGGING
            #region Debuging
            Console.Clear();
            Console.WriteLine("mouse : x = " + mouse.X + " ; y = " + mouse.Y + "\n");
            Console.WriteLine("joueur 1 : x = " + LocalPlayer1.Hitbox.X + " ; y = " + LocalPlayer1.Hitbox.Y);
            Console.WriteLine("joueur 2 : x = " + LocalPlayer2.Hitbox.X + " ; y = " + LocalPlayer2.Hitbox.Y + "\n");
            Console.WriteLine("CAN_JUMP joueur 1 : " + LocalPlayer1.can_jump + " joueur 2 : " + LocalPlayer2.can_jump + "\n");
            Console.WriteLine("volume musique : " + Options.mediaplayer_volume);
            Console.WriteLine("volume bruitages : " + Options.bruitage_volume + "\n");
            if (State.CurrentGameState == GameState.Playing)
                Console.WriteLine("Playing\n");
            else if (State.CurrentGameState == GameState.Pause)
                Console.WriteLine("Pause\n");
            else if (State.CurrentGameState == GameState.Options)
                Console.WriteLine("Options\n");
            if (!WasKeyDown_Escape)
                Console.WriteLine("escape not pressed\n");
            if (keyboard.IsKeyDown(Keys.Escape))
                Console.WriteLine("escape pressed\n");
            Console.WriteLine("FRAME COUNTER 1: " + LocalPlayer1.frame_counter);
            Console.WriteLine("FRAME COUNTER 2: " + LocalPlayer2.frame_counter + "\n");
            if (LocalPlayer1.attack)
                Console.WriteLine("attack player 1 = true");
            else if (!LocalPlayer1.attack)
                Console.WriteLine("attack player 1 = false");
            if (LocalPlayer2.attack)
                Console.WriteLine("attack player 2 = true\n");
            else if (!LocalPlayer2.attack)
                Console.WriteLine("attack player 2 = false\n");
            Console.WriteLine("frame_counter_is_attacked 1: " + LocalPlayer1.frame_counter_is_attacked);
            Console.WriteLine("frame_counter_is_attacked 2: " + LocalPlayer2.frame_counter_is_attacked + "\n");
            Console.WriteLine("life player 1 : " + LocalPlayer1.vie);
            Console.WriteLine("life player 2 : " + LocalPlayer2.vie + "\n");
            #endregion
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            switch (State.CurrentGameState)
            {
                case GameState.Playing:
                    spriteBatch.Draw(Ressources.fields[terrain_choisi], new Rectangle(0, 0, 800, 480), Color.White);
                    LocalPlayer1.Draw(spriteBatch);
                    LocalPlayer2.Draw(spriteBatch);

                    if (timer_combat_secondes < 10)
                    {
                        if (timer_combat_secondes < 0)
                        {
                            spriteBatch.DrawString(chrono, "- " + timer_combat_minutes + " : 0" + (timer_combat_secondes) * (-1), new Vector2((Game1.graphics1.GraphicsDevice.Viewport.Width / 2) - 55, ((Game1.graphics1.GraphicsDevice.Viewport.Height + 480) / 2) - 25), Color.White);
                            spriteBatch.Draw(ready, new Vector2(350, 260), Color.White);
                        }

                        else
                        {
                            spriteBatch.DrawString(chrono, timer_combat_minutes + " : 0" + timer_combat_secondes, new Vector2((Game1.graphics1.GraphicsDevice.Viewport.Width / 2) - 55, ((Game1.graphics1.GraphicsDevice.Viewport.Height + 480) / 2) - 25), Color.White);
                        }

                        if (timer_combat_secondes >= 0 && !was_displayed)
                        {
                            spriteBatch.Draw(fight, new Vector2(350, 260), Color.White);

                            if (timer_combat_secondes >= 1)
                            {
                                was_displayed = true;
                            }
                        }

                    }

                    else
                    {
                        spriteBatch.DrawString(chrono, timer_combat_minutes + " : " + timer_combat_secondes, new Vector2((Game1.graphics1.GraphicsDevice.Viewport.Width / 2) - 55, ((Game1.graphics1.GraphicsDevice.Viewport.Height + 480) / 2) - 25), Color.White);
                    }

                    LocalPlayer1.GenerateBar(LocalPlayer1.vie, 100, spriteBatch);
                    LocalPlayer2.GenerateBar(LocalPlayer2.vie, 100, spriteBatch);

                    LocalPlayer1.GeneratePicture(spriteBatch);
                    LocalPlayer2.GeneratePicture(spriteBatch);

                    if (LocalPlayer1.vie == 0 || LocalPlayer2.vie == 0)
                    {
                        if (fps_counter <= 120)
                        {
                            fps_counter++;
                            spriteBatch.Draw(game_over, new Vector2(330, 240), Color.White);
                        }

                        else
                        {
                            fps_counter = 0;
                            State.CurrentGameState = GameState.MainMenu;
                            Program.thread_menu = new Thread(new ThreadStart(Program.Menu));
                            Program.thread_menu.Start();
                            MainMenu.thread_jeu.Abort();
                        }
                    }
                    break;

                case GameState.Pause:
                    spriteBatch.Draw(background, Vector2.Zero, Color.White);
                    spriteBatch.DrawString(pause, "PAUSE", new Vector2(330, 20), Color.Black);
                    option.Draw(spriteBatch);
                    retour.Draw(spriteBatch);
                    menu_principal.Draw(spriteBatch);
                    quitter.Draw(spriteBatch);
                    break;

                case GameState.Options:
                    Options.Draw(spriteBatch, Content);
                    break;

                case GameState.Commandes:
                    ChangeControls.Draw(spriteBatch);
                    break;
            }
        }
    }
}
