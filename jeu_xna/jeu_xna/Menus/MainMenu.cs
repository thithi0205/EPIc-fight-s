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
    public enum GameState
    {
        MainMenu,
        Options,
        Playing
    }


    public class MainMenu : Microsoft.Xna.Framework.Game
    {
        public static GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        #region volume bruitages + musique
        Song musique;
        SoundEffect test_volume_bruitage;
        public static float mediaplayer_volume, bruitage_volume;
        #endregion

        #region Menu_buttons
        MenuButton play, option, plus_musique, moins_musique, plus_bruitages, moins_bruitages, bouton_retour; //menu principal
        public static GameState CurrentGameState;
        bool was_cliqued;
        #endregion

        public MainMenu()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = 600;
            graphics.PreferredBackBufferWidth = 800;
            graphics.ApplyChanges();
            Content.RootDirectory = "Content";
        }

        //INITIALIZE
        protected override void Initialize()
        {
            CurrentGameState = GameState.MainMenu;
            was_cliqued = false;
            IsMouseVisible = true;
            base.Initialize();
        }

        //LOADCONTENT
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            musique = Content.Load<Song>(@"Sounds\Musique\Son Game 1");
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = 0.5f;
            SoundEffect.MasterVolume = 0.5f;
            MediaPlayer.Play(musique);
            test_volume_bruitage = Content.Load<SoundEffect>(@"Sounds\Personnage\jump1");

            //CHARGEMENT DES BOUTONS
            #region Load_Button

            //BOUTONS DU MENU PRINCIPAL
            #region MainMenu
            play = new MenuButton(Content.Load<Texture2D>(@"Sprites\MainMenu\button_jouer"), new Vector2(300, graphics.GraphicsDevice.Viewport.Height / 2));
            option = new MenuButton(Content.Load<Texture2D>(@"Sprites\MainMenu\button_options"), new Vector2(300, 400));
            #endregion

            //BOUTONS DU MENU OPTION
            #region OptionsMenu
            plus_musique = new MenuButton(Content.Load<Texture2D>(@"Sprites\MainMenu\Options\+"), new Vector2(250, 100));
            moins_musique = new MenuButton(Content.Load<Texture2D>(@"Sprites\MainMenu\Options\-"), new Vector2(400, 100));
            plus_bruitages = new MenuButton(Content.Load<Texture2D>(@"Sprites\MainMenu\Options\+"), new Vector2(250, 170));
            moins_bruitages = new MenuButton(Content.Load<Texture2D>(@"Sprites\MainMenu\Options\-"), new Vector2(400, 170));
            bouton_retour = new MenuButton(Content.Load<Texture2D>(@"Sprites\MainMenu\Options\bouton_retour"), new Vector2(20, 520));
            #endregion

            #endregion
        }

        //UNLOADCONTENT
        protected override void UnloadContent()
        {
            Content.Unload();
        }

        //UPDATE
        protected override void Update(GameTime gameTime)
        {
            MouseState mouse = Mouse.GetState();
            mediaplayer_volume = MediaPlayer.Volume;
            bruitage_volume = SoundEffect.MasterVolume;

            //DEBUGgING
            #region Debugging
            Console.Clear();
            Console.WriteLine("mouse : x = " + mouse.X + " ; y = " + mouse.Y + "\n");
            Console.WriteLine("volume musique : " + mediaplayer_volume + "\n");
            Console.WriteLine("volume bruitages : " + bruitage_volume + "\n");
            if (CurrentGameState == GameState.MainMenu)
                Console.WriteLine("MainMenu\n");
            else if (CurrentGameState == GameState.Options)
                Console.WriteLine("Options\n");
            else if (CurrentGameState == GameState.Playing)
                Console.WriteLine("Playing\n");
            #endregion

            //MISE A JOUR DES BOUTONS
            #region Buttons update

            //BOUTONS DU MENU PRINCIPAL
            #region MainMenu buttons
            play.Update(mouse);
            option.Update(mouse);
            #endregion

            //BOUTONS DU MENU OPTIONS
            #region Options buttons
            plus_musique.Update(mouse);
            moins_musique.Update(mouse);
            plus_bruitages.Update(mouse);
            moins_bruitages.Update(mouse);
            bouton_retour.Update(mouse);
            #endregion

            #endregion

            switch (CurrentGameState)
            {
                case GameState.MainMenu:

                    //MISE A JOUR DU MENU PRINCIPAL
                    #region MainMenu update
                    if (play.isClicked)
                    {
                        CurrentGameState = GameState.Playing;
                    }

                    else if (option.isClicked)
                    {
                        CurrentGameState = GameState.Options;
                    }
                    #endregion
                    break;

                case GameState.Options:

                    //MISE A JOUR DU MENU OPTIONS
                    #region OptionsMenu update
                    if (bouton_retour.isClicked)
                    {
                        CurrentGameState = GameState.MainMenu;
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
                    #endregion
                    break;

                case GameState.Playing:

                    //LANCEMENT DU JEU
                    this.Exit();
                    Program.Main(args);
                    break;
            }

            base.Update(gameTime);
        }

        //DRAW
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();
            switch (CurrentGameState)
            {
                case GameState.MainMenu:

                    //AFFICHAGE DU MENU PRINCIPAL
                    #region MainMenu draw
                    spriteBatch.Draw(Content.Load<Texture2D>(@"Sprites\MainMenu\Main_menu"), Vector2.Zero, Color.White);
                    option.Draw(spriteBatch);
                    play.Draw(spriteBatch);
                    #endregion
                    break;

                case GameState.Options:

                    //AFFICHAGE DU MENU OPTIONS
                    #region OptionsMenu draw
                    int display_mediaplayer_volume = (int)(mediaplayer_volume * 100);
                    int display_bruitages_volume = (int)(bruitage_volume * 100);
                    spriteBatch.Draw(Content.Load<Texture2D>(@"Sprites\MainMenu\Options\background"), Vector2.Zero, Color.White);
                    spriteBatch.Draw(Content.Load<Texture2D>(@"Sprites\MainMenu\Options\volume"), new Vector2(30, 30), Color.White);
                    spriteBatch.Draw(Content.Load<Texture2D>(@"Sprites\MainMenu\Options\volume_musique"), new Vector2(18, 100), Color.White);
                    spriteBatch.Draw(Content.Load<Texture2D>(@"Sprites\MainMenu\Options\volume_bruitages"), new Vector2(22, 170), Color.White);
                    spriteBatch.DrawString(Content.Load<SpriteFont>(@"Sprites\MainMenu\Options\Font\volume_musique"), string.Format(Convert.ToString(display_mediaplayer_volume)), new Vector2(320, 95), Color.Black);
                    spriteBatch.DrawString(Content.Load<SpriteFont>(@"Sprites\MainMenu\Options\Font\volume_musique"), string.Format(Convert.ToString(display_bruitages_volume)), new Vector2(320, 165), Color.Black);
                    plus_musique.Draw(spriteBatch);
                    moins_musique.Draw(spriteBatch);
                    plus_bruitages.Draw(spriteBatch);
                    moins_bruitages.Draw(spriteBatch);
                    bouton_retour.Draw(spriteBatch);
                    #endregion
                    break;

                case GameState.Playing:
                    //lancement du jeu dans Programm.cs
                    break;
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }

        public string[] args { get; set; }
    }
}
