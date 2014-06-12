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

        public static MenuButton option, retour, menu_principal, quitter, menu_principal_fin;
        static Texture2D ready, fight;
        static SpriteFont chrono, game_over;

        public static bool EndGame, send_once, can_display_score;

        public GameMain()
        { }

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
            EndGame = false;
            send_once = true;
            can_display_score = true;
        }

        public static void LoadContent(ContentManager Content)
        {
            //CREATION DES JOUEURS
            if (!VarTemp.is_connected)
            {
                VarTemp.player = Ressources.caracters[personnage_choisi1].name;
            }

            LocalPlayer1 = new Player(Ressources.caracters[personnage_choisi1], 275, 230, Direction.Right, VarTemp.up1, VarTemp.right1, VarTemp.left1, VarTemp.accroupi_1, VarTemp.player, 1, Content, VarTemp.attack1_1, VarTemp.attack1_2, VarTemp.attack1_3, VarTemp.attack1_4);
            LocalPlayer2 = new Player(Ressources.caracters[personnage_choisi2], 425, 230, Direction.Left, VarTemp.up2, VarTemp.right2, VarTemp.left2, VarTemp.accroupi_2, Ressources.caracters[personnage_choisi2].name, 2, Content, VarTemp.attack2_1, VarTemp.attack2_2, VarTemp.attack2_3, VarTemp.attack2_4);

            option = new MenuButton(Content.Load<Texture2D>(@"Sprites\MainMenu\bouton_options"), new Vector2(300, 300));
            option.Calcul_de_la_mort(Game1.graphics1);

            chrono = Content.Load<SpriteFont>("chronomètre");
            retour = new MenuButton(Content.Load<Texture2D>(@"Sprites\MainMenu\Options\bouton_retour"), new Vector2(Game1.graphics1.GraphicsDevice.Viewport.Width - (20 + 145), 520));
            menu_principal = new MenuButton(Content.Load<Texture2D>(@"Sprites\MainMenu\bouton_menu-principal"), new Vector2(255, 200));
            menu_principal_fin = new MenuButton(Content.Load<Texture2D>(@"Sprites\MainMenu\bouton_menu-principal"), new Vector2(1, 1));
            menu_principal_fin.Calcul_de_la_mort(Game1.graphics1);
            menu_principal_fin.position.Y = (Ressources.fields[terrain_choisi].Height / 2) - 20;
            menu_principal.Calcul_de_la_mort(Game1.graphics1);

            quitter = new MenuButton(Content.Load<Texture2D>(@"Sprites\MainMenu\bouton_quitter"), new Vector2(300, 400));
            quitter.Calcul_de_la_mort(Game1.graphics1);
            Options.LoadContent(Content);

            ready = Content.Load<Texture2D>(@"Sprites\Game\ready");
            fight = Content.Load<Texture2D>(@"Sprites\Game\fight");
            game_over = Content.Load<SpriteFont>("game_over");

            ChangeControls.LoadContent(Content);
        }

        // UPDATE & DRAW
        public void Update(MouseState mouse, KeyboardState keyboard)
        {
            option.Update(MainMenu.mouse);
            retour.Update(MainMenu.mouse);
            menu_principal.Update(MainMenu.mouse);
            quitter.Update(MainMenu.mouse);

            if (keyboard.IsKeyDown(Keys.Escape) && VarTemp.CurrentGameState == GameState.Playing && !WasKeyDown_Escape)
            {
                VarTemp.CurrentGameState = GameState.Pause;
                WasKeyDown_Escape = true;
            }

            else if (keyboard.IsKeyDown(Keys.Escape) && (VarTemp.CurrentGameState == GameState.Pause || VarTemp.CurrentGameState == GameState.Options || VarTemp.CurrentGameState == GameState.Commandes) && !WasKeyDown_Escape)
            {
                VarTemp.CurrentGameState = GameState.Playing;
                WasKeyDown_Escape = true;
            }

            else if (keyboard.IsKeyUp(Keys.Escape))
            {
                WasKeyDown_Escape = false;
            }

            switch (VarTemp.CurrentGameState)
            {
                case GameState.Playing:
                    if (timer_combat_secondes >= 0)
                    {
                        LocalPlayer1.Update(mouse, keyboard);
                        LocalPlayer2.Update(mouse, keyboard);
                    }

                    if (!LocalPlayer1.win && !LocalPlayer2.win)
                    {
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
                    }

                    else
                    {
                        if (menu_principal_fin.isClicked)
                        {
                            Program.thread_menu = new Thread(new ThreadStart(Program.Menu));
                            Program.thread_menu.Start();
                            MainMenu.thread_jeu.Abort();
                        }
                    }
                break;

                case GameState.Pause:
                    if (option.isClicked)
                    {
                        VarTemp.CurrentGameState = GameState.Options;
                        Options.was_cliqued = true;
                    }

                    else if (retour.isClicked)
                    {
                        VarTemp.CurrentGameState = GameState.Playing;
                    }

                    else if (menu_principal.isClicked)
                    {
                        VarTemp.CurrentGameState = GameState.MainMenu;
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
            if (VarTemp.CurrentGameState == GameState.Playing)
                Console.WriteLine("Playing\n");
            else if (VarTemp.CurrentGameState == GameState.Pause)
                Console.WriteLine("Pause\n");
            else if (VarTemp.CurrentGameState == GameState.Options)
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
            Console.WriteLine("life player 1 : " + LocalPlayer1.vie + "     energy player 1 : " + LocalPlayer1.energy);
            Console.WriteLine("life player 2 : " + LocalPlayer2.vie + "     energy player 2 : " + LocalPlayer2.energy + "\n");
            Console.WriteLine("limit_jump 1 : " + LocalPlayer1.limit_jump + "     limit_jump 2 : " + LocalPlayer2.limit_jump + "\n");
            if (LocalPlayer1.win)
                Console.WriteLine("player 1 win");
            else if (LocalPlayer2.win)
                Console.WriteLine("player 2 win\n");
            #endregion
             
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            switch (VarTemp.CurrentGameState)
            {
                case GameState.Playing:
                    spriteBatch.Draw(Ressources.fields[terrain_choisi], new Rectangle(0, 0, 800, 480), Color.White);
                    LocalPlayer1.Draw(spriteBatch);
                    LocalPlayer2.Draw(spriteBatch);

                    if (timer_combat_secondes < 10)
                    {
                        if (timer_combat_secondes < 0)
                        {
                            spriteBatch.DrawString(chrono, "0 : 00", new Vector2((Game1.graphics1.GraphicsDevice.Viewport.Width / 2) - 55, ((Game1.graphics1.GraphicsDevice.Viewport.Height + 480) / 2) - 25), Color.White);
                            spriteBatch.Draw(ready, new Vector2((Game1.graphics1.GraphicsDevice.Viewport.Width - ready.Width) / 2, 480 / 2), Color.White);
                        }

                        else
                        {
                            spriteBatch.DrawString(chrono, timer_combat_minutes + " : 0" + timer_combat_secondes, new Vector2((Game1.graphics1.GraphicsDevice.Viewport.Width / 2) - 55, ((Game1.graphics1.GraphicsDevice.Viewport.Height + 480) / 2) - 25), Color.White);
                        }

                        if (timer_combat_secondes >= 0 && !was_displayed)
                        {
                            spriteBatch.Draw(fight, new Vector2((Game1.graphics1.GraphicsDevice.Viewport.Width - ready.Width) / 2, 480 / 2), Color.White);

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

                    LocalPlayer1.GenerateBar(LocalPlayer1.vie, 100, Color.Red, spriteBatch, 40, 560);
                    LocalPlayer2.GenerateBar(LocalPlayer2.vie, 100, Color.Red, spriteBatch, Game1.graphics1.GraphicsDevice.Viewport.Width - 290, 560);

                    LocalPlayer1.GenerateBar(LocalPlayer1.energy, 100, Color.Blue, spriteBatch, 40, 580);
                    LocalPlayer2.GenerateBar(LocalPlayer2.energy, 100, Color.Blue, spriteBatch, Game1.graphics1.GraphicsDevice.Viewport.Width - 290, 580);

                    LocalPlayer1.GeneratePicture(spriteBatch);
                    LocalPlayer2.GeneratePicture(spriteBatch);

                    if (LocalPlayer1.vie == 0 || LocalPlayer2.vie == 0)
                    {
                        if (LocalPlayer1.vie != 0)
                        {
                            LocalPlayer1.win = true;
                        }

                        else if (LocalPlayer2.vie != 0)
                        {
                            LocalPlayer2.win = true;
                        }

                        if (LocalPlayer1.win)
                        {
                            spriteBatch.DrawString(game_over, LocalPlayer1.name + " wins !", new Vector2((Game1.graphics1.GraphicsDevice.Viewport.Width - game_over.MeasureString(LocalPlayer1.name + " wins !").Length()) / 2, ((480 - game_over.MeasureString(LocalPlayer1.name + " wins !").Y) / 2) - 50), new Color(146, 22, 22));

                            try
                            {
                                if (send_once && VarTemp.is_connected)
                                {
                                    send_once = false;
                                    VarTemp.victory = new Connexion(Convert.ToInt32(Program.test[0]), "http://epic-fights.sebb-dev.org/launcher/victory.php");
                                    VarTemp.victoire_defaite = VarTemp.victory.Connect("victoire");
                                    VarTemp.string_board_bis = VarTemp.victoire_defaite.Split(new Char[] { ':' });
                                    VarTemp.nb_victory = Convert.ToInt32(VarTemp.string_board_bis[0]);
                                    VarTemp.nb_defaites = Convert.ToInt32(VarTemp.string_board_bis[1]);
                                    can_display_score = true;
                                }
                            }

                            catch
                            {
                                can_display_score = false;
                            }

                            if (VarTemp.is_connected)
                            {
                                if (can_display_score)
                                {
                                    spriteBatch.DrawString(game_over, "Number of victories : " + VarTemp.nb_victory + "\nNumber of defeats : " + VarTemp.nb_defaites, new Vector2((Game1.graphics1.GraphicsDevice.Viewport.Width - game_over.MeasureString("Number of victories : " + VarTemp.nb_victory).Length()) / 2, menu_principal_fin.position.Y + 50), new Color(146, 22, 22));
                                }

                                else
                                {
                                    spriteBatch.DrawString(game_over, "Connection error", new Vector2((Game1.graphics1.GraphicsDevice.Viewport.Width - game_over.MeasureString("Connection error").Length()) / 2, menu_principal_fin.position.Y + 50), new Color(146, 22, 22));
                                }
                            }
                        }

                        else if (LocalPlayer2.win)
                        {
                            spriteBatch.DrawString(game_over, LocalPlayer2.name + " wins !", new Vector2((Game1.graphics1.GraphicsDevice.Viewport.Width - game_over.MeasureString(LocalPlayer2.name + " wins !").Length()) / 2, ((480 - game_over.MeasureString(LocalPlayer2.name + " wins !").Y) / 2) - 50), new Color(146, 22, 22));

                            try
                            {
                                if (send_once && VarTemp.is_connected)
                                {
                                    send_once = false;
                                    VarTemp.victory = new Connexion(Convert.ToInt32(Program.test[0]), "http://epic-fights.sebb-dev.org/launcher/victory.php");
                                    VarTemp.victoire_defaite = VarTemp.victory.Connect("defaite");
                                    VarTemp.string_board_bis = VarTemp.victoire_defaite.Split(new Char[] { ':' });
                                    VarTemp.nb_victory = Convert.ToInt32(VarTemp.string_board_bis[0]);
                                    VarTemp.nb_defaites = Convert.ToInt32(VarTemp.string_board_bis[1]);
                                }
                            }

                            catch
                            {
                                can_display_score = false;
                            }

                            if (VarTemp.is_connected)
                            {
                                if (can_display_score)
                                {
                                    spriteBatch.DrawString(game_over, "Number of victories : " + VarTemp.nb_victory + "\nNumber of defeats : " + VarTemp.nb_defaites, new Vector2((Game1.graphics1.GraphicsDevice.Viewport.Width - game_over.MeasureString("Number of victories : " + VarTemp.nb_victory).Length()) / 2, menu_principal_fin.position.Y + 50), new Color(146, 22, 22));
                                }

                                else
                                {
                                    spriteBatch.DrawString(game_over, "Connection error", new Vector2((Game1.graphics1.GraphicsDevice.Viewport.Width - game_over.MeasureString("Connection error").Length()) / 2, menu_principal_fin.position.Y + 50), new Color(146, 22, 22));
                                }
                            }
                        }

                        menu_principal_fin.Draw(spriteBatch);
                    }
                    break;

                case GameState.Pause:
                    Menu.Draw(spriteBatch);
                    spriteBatch.DrawString(Options.options, "PAUSE", new Vector2((Game1.graphics1.GraphicsDevice.Viewport.Width - Options.options.MeasureString("PAUSE").Length()) / 2, 0), Color.White);
                    option.Draw(spriteBatch);
                    retour.Draw(spriteBatch);
                    menu_principal.Draw(spriteBatch);
                    quitter.Draw(spriteBatch);
                    break;

                case GameState.Options:
                    Options.Draw(spriteBatch);
                    break;

                case GameState.Commandes:
                    ChangeControls.Draw(spriteBatch);
                    break;
            }
        }
    }
}
