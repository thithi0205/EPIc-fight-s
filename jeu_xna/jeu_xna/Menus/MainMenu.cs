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
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class MainMenu : Microsoft.Xna.Framework.Game
    {
        public static GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        #region volume bruitages + musique
        Song musique;
        SoundEffect test_volume_bruitage;
        public static float mediaplayer_volume, bruitage_volume;
        #endregion

        //MouseState cliqued;

        #region Menu_buttons
        MenuButton play, option, plus_musique, moins_musique, plus_bruitages, moins_bruitages; //menu principal
        public static GameState CurrentGameState = GameState.MainMenu;
        bool was_cliqued = false;
        #endregion

        public MainMenu()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = 600;
            graphics.PreferredBackBufferWidth = 800;
            graphics.ApplyChanges();
            //graphics.ToggleFullScreen();
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            IsMouseVisible = true;
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            musique = Content.Load<Song>(@"Sounds\Musique\Son Game 1");
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = 0.5f;
            SoundEffect.MasterVolume = 0.5f;
            MediaPlayer.Play(musique);
            test_volume_bruitage = Content.Load<SoundEffect>(@"Sounds\Personnage\jump1");

            #region Load_Button

            #region MainMenu
            play = new MenuButton(Content.Load<Texture2D>(@"Sprites\MainMenu\button_jouer"), new Vector2(300, graphics.GraphicsDevice.Viewport.Height / 2));
            option = new MenuButton(Content.Load<Texture2D>(@"Sprites\MainMenu\button_options"), new Vector2(300, 400));
            #endregion

            #region OptionsMenu
            plus_musique = new MenuButton(Content.Load<Texture2D>(@"Sprites\MainMenu\Options\+"), new Vector2(250, 100));
            moins_musique = new MenuButton(Content.Load<Texture2D>(@"Sprites\MainMenu\Options\-"), new Vector2(400, 100));
            plus_bruitages = new MenuButton(Content.Load<Texture2D>(@"Sprites\MainMenu\Options\+"), new Vector2(250, 170));
            moins_bruitages = new MenuButton(Content.Load<Texture2D>(@"Sprites\MainMenu\Options\-"), new Vector2(400, 170));
            #endregion

            #endregion
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            Content.Unload();
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            MouseState mouse = Mouse.GetState();
            //MouseState cliqued = Mouse.GetState();
            mediaplayer_volume = MediaPlayer.Volume;
            bruitage_volume = SoundEffect.MasterVolume;
            Console.Clear();
            Console.WriteLine("mouse : x = " + mouse.X + " ; y = " + mouse.Y + "\n");
            Console.WriteLine("volume musique : " + mediaplayer_volume + "\n");
            Console.WriteLine("volume bruitages : " + bruitage_volume + "\n");

            switch (CurrentGameState)
            {
                case GameState.MainMenu:
                    if (play.isClicked)
                    {
                        CurrentGameState = GameState.Playing;
                    }

                    else if (option.isClicked)
                    {
                        CurrentGameState = GameState.Options;
                    }

                    
                    play.Update(mouse);
                    option.Update(mouse);
                    break;

                case GameState.Options:
                    if (plus_musique.isClicked && !was_cliqued)
                    {
                        MediaPlayer.Volume = MediaPlayer.Volume + 0.01f;
                        //mediaplayer_volume = MediaPlayer.Volume;
                        //Console.Clear();
                        //Console.WriteLine("volume : " + mediaplayer_volume + "\n");
                        was_cliqued = true;
                    }

                    else if (moins_musique.isClicked && !was_cliqued)
                    {
                        MediaPlayer.Volume = MediaPlayer.Volume - 0.01f;
                        //mediaplayer_volume = MediaPlayer.Volume;
                        //Console.Clear();
                        //Console.WriteLine("volume : " + mediaplayer_volume + "\n");
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
                    break;

                case GameState.Playing:
                    this.Exit();
                    Program.Main(args);
                    break;
            }

            // TODO: Add your update logic here
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();
            switch (CurrentGameState)
            {
                case GameState.MainMenu:
                    spriteBatch.Draw(Content.Load<Texture2D>(@"Sprites\MainMenu\Main_menu"), Vector2.Zero, Color.White);
                    option.Draw(spriteBatch);
                    play.Draw(spriteBatch);
                    break;

                case GameState.Options:
                    int display_mediaplayer_volume = (int)(mediaplayer_volume * 100);
                    int display_bruitages_volume = (int)(bruitage_volume * 100);
                    spriteBatch.Draw(Content.Load<Texture2D>(@"Sprites\MainMenu\Options\background"), Vector2.Zero, Color.White);
                    spriteBatch.Draw(Content.Load<Texture2D>(@"Sprites\MainMenu\Options\volume"), new Vector2(30, 30), Color.White);
                    spriteBatch.Draw(Content.Load<Texture2D>(@"Sprites\MainMenu\Options\volume_musique"), new Vector2(30, 100), Color.White);
                    spriteBatch.Draw(Content.Load<Texture2D>(@"Sprites\MainMenu\Options\volume_bruitages"), new Vector2(30, 170), Color.White);
                    spriteBatch.DrawString(Content.Load<SpriteFont>(@"Sprites\MainMenu\Options\Font\volume_musique"), string.Format(Convert.ToString(display_mediaplayer_volume)), new Vector2(320, 95), Color.Black);
                    spriteBatch.DrawString(Content.Load<SpriteFont>(@"Sprites\MainMenu\Options\Font\volume_musique"), string.Format(Convert.ToString(display_bruitages_volume)), new Vector2(320, 165), Color.Black);
                    plus_musique.Draw(spriteBatch);
                    moins_musique.Draw(spriteBatch);
                    plus_bruitages.Draw(spriteBatch);
                    moins_bruitages.Draw(spriteBatch);
                    break;

                case GameState.Playing:
                    //lancement du jeu dans Programm.cs
                    break;
            }
            spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }

        public string[] args { get; set; }
    }
}
