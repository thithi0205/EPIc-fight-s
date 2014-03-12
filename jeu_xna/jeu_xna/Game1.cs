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
//using Microsoft.Xna.Framework.Input.Touch;

namespace jeu_xna
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        public static GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        #region Menu_buttons
        MenuButton play, option; //menu principal



        enum GameState
        {
            MainMenu,
            Options,
            Playing
        }
        GameState CurrentGameState = GameState.MainMenu;
        #endregion

        GameMain Main;

        public Game1()
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
           /* if (CurrentGameState == GameState.Playing)
            {
                Ressources.LoadContent_Sprites(Content);
            }

            Ressources.LoadContent_Sounds(Content);*/
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            #region Load_Button
            #region MainMenu
            play = new MenuButton(Content.Load<Texture2D>(@"Sprites\MainMenu\button_jouer"), new Vector2(300, graphics.GraphicsDevice.Viewport.Height / 2));
            option = new MenuButton(Content.Load<Texture2D>(@"Sprites\MainMenu\button_options"), new Vector2(300, 400));
            #endregion
            #endregion

            Ressources.LoadContent_Sprites(Content);
            Ressources.LoadContent_Sounds(Content);

            Main = new GameMain();
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

                case GameState.Playing:
                    Main.Update(Mouse.GetState(), Keyboard.GetState());
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

                case GameState.Playing:
                    Main.Draw(spriteBatch);
                    break;
            }
            spriteBatch.End();

            //Main.Draw(spriteBatch);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
