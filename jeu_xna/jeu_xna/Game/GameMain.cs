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

namespace jeu_xna
{
    class GameMain : Microsoft.Xna.Framework.Game
    {
        // FIELD
        Player LocalPlayer1, LocalPlayer2;
        bool WasKeyDown_Escape = false;

        public static MenuButton option;
        static Texture2D background;
        //static SpriteFont volume_affichage;

        //static SoundEffect test_volume_bruitage;

        //static bool was_cliqued;

        // CONSTRUCTOR
        public GameMain(ContentManager Content)
        {
            //CREATION DES JOUEURS
            LocalPlayer1 = new Player(Ressources.personnage, 300, 230, Direction.Right);
            LocalPlayer2 = new Player(Ressources.personnage, 400, 230, Direction.Left);
        }

        // METHODS
        public static void Initialize()
        {
            //GameMain.was_cliqued = false;
            Options.mediaplayer_volume = MediaPlayer.Volume;
            Options.bruitage_volume = SoundEffect.MasterVolume; 
        }

        public static void LoadContent(ContentManager Content)
        {
            option = new MenuButton(Content.Load<Texture2D>(@"Sprites\MainMenu\button_options"), new Vector2(300, 400));
            background = Content.Load<Texture2D>(@"Sprites\MainMenu\Options\background");
            /*plus_musique = new MenuButton(Content.Load<Texture2D>(@"Sprites\MainMenu\Options\+"), new Vector2(250, 100));
            moins_musique = new MenuButton(Content.Load<Texture2D>(@"Sprites\MainMenu\Options\-"), new Vector2(400, 100));
            plus_bruitages = new MenuButton(Content.Load<Texture2D>(@"Sprites\MainMenu\Options\+"), new Vector2(250, 170));
            moins_bruitages = new MenuButton(Content.Load<Texture2D>(@"Sprites\MainMenu\Options\-"), new Vector2(400, 170));
            bouton_retour = new MenuButton(Content.Load<Texture2D>(@"Sprites\MainMenu\Options\bouton_retour"), new Vector2(20, 520));

            volume = Content.Load<Texture2D>(@"Sprites\MainMenu\Options\volume");
            volume_musique = Content.Load<Texture2D>(@"Sprites\MainMenu\Options\volume_musique");
            volume_bruitage = Content.Load<Texture2D>(@"Sprites\MainMenu\Options\volume_bruitages");
            volume_affichage = Content.Load<SpriteFont>(@"Sprites\MainMenu\Options\Font\volume_musique");

            test_volume_bruitage = Content.Load<SoundEffect>(@"Sounds\Personnage\jump1");*/
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

            else if(keyboard.IsKeyUp(Keys.Escape))
            {
                WasKeyDown_Escape = false;
            }

            switch (MainMenu.CurrentGameState)
            {
                case GameState.Playing:
                    LocalPlayer1.Update(mouse, keyboard);
                    LocalPlayer2.Update(mouse, keyboard);
                    break;

                case GameState.Pause:
                    //IsMouseVisible = true;
                    /*if (!pause)
                    {
                        IsMouseVisible = true;
                        //Mouse.SetPosition(0, 0);
                        pause = true;
                    }*/
                    if (option.isClicked)
                    {
                        MainMenu.CurrentGameState = GameState.Options;
                    }
                    
                    break;

                case GameState.Options:
                    

                    /*#region OptionsMenu update
                    if (bouton_retour.isClicked)
                    {
                        MainMenu.CurrentGameState = GameState.Options;
                    }

                    else if (plus_musique.isClicked && !was_cliqued)
                    {
                        MediaPlayer.Volume = MediaPlayer.Volume + 0.01f;
                        was_cliqued = true;
                    }

                    else if (moins_musique.isClicked && !was_cliqued)
                    {
                        MediaPlayer.Volume = MediaPlayer.Volume - 0.01f;
                        was_cliqued = true;
                    }

                    else if (plus_bruitages.isClicked && !was_cliqued)
                    {
                        if (SoundEffect.MasterVolume + 0.01f > 1)
                        {
                            SoundEffect.MasterVolume = 1;
                        }

                        else
                        {
                            SoundEffect.MasterVolume = SoundEffect.MasterVolume + 0.01f;
                        }
                        test_volume_bruitage.Play();
                        was_cliqued = true;
                    }

                    else if (moins_bruitages.isClicked && !was_cliqued)
                    {
                        if (SoundEffect.MasterVolume - 0.01f < 0)
                        {
                            SoundEffect.MasterVolume = 0;
                        }

                        else
                        {
                            SoundEffect.MasterVolume = SoundEffect.MasterVolume - 0.01f;
                        }
                        test_volume_bruitage.Play();
                        was_cliqued = true;
                    }

                    else if (mouse.LeftButton == ButtonState.Released && mouse.RightButton == ButtonState.Released)
                    {
                        was_cliqued = false;
                    }

                    plus_musique.Update(mouse);
                    moins_musique.Update(mouse);
                    plus_bruitages.Update(mouse);
                    moins_bruitages.Update(mouse);
                    bouton_retour.Update(mouse);
                    #endregion*/
                    Options.Update();
                    
                    break;
            }

            
            //DEBUGGING
            #region Debuging
            Console.Clear();
            Console.WriteLine("mouse : x = " + mouse.X + " ; y = " + mouse.Y + "\n");
            Console.WriteLine("personnage : x = " + LocalPlayer1.Hitbox.X + " ; y = " + LocalPlayer1.Hitbox.Y + "\n");
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
                    LocalPlayer1.Draw(spriteBatch);
                    LocalPlayer2.Draw(spriteBatch);
                    break;

                case GameState.Pause:
                    spriteBatch.Draw(background, Vector2.Zero, Color.White);
                    option.Draw(spriteBatch);
                    break;

                case GameState.Options:
                    /*int display_mediaplayer_volume = (int)(Options.mediaplayer_volume * 100);
                    int display_bruitages_volume = (int)(Options.bruitage_volume * 100);
                    spriteBatch.Draw(background, Vector2.Zero, Color.White);
                    spriteBatch.Draw(volume, new Vector2(30, 30), Color.White);
                    spriteBatch.Draw(volume_musique, new Vector2(18, 100), Color.White);
                    spriteBatch.Draw(volume_bruitage, new Vector2(22, 170), Color.White);
                    spriteBatch.DrawString(volume_affichage, string.Format(Convert.ToString(display_mediaplayer_volume)), new Vector2(320, 95), Color.Black);
                    spriteBatch.DrawString(volume_affichage, string.Format(Convert.ToString(display_bruitages_volume)), new Vector2(320, 165), Color.Black);
                    plus_musique.Draw(spriteBatch);
                    moins_musique.Draw(spriteBatch);
                    plus_bruitages.Draw(spriteBatch);
                    moins_bruitages.Draw(spriteBatch);
                    bouton_retour.Draw(spriteBatch);*/
                    Options.Draw(spriteBatch, Content);
                    break;

            }
        }
    }
}
