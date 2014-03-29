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
    public enum GameState
    {
        MainMenu,
        ChoiceMenuCaracter,
        ChoiceMenuBattlefield,
        Options,
        Playing,
        Pause
    }


    public class MainMenu : Microsoft.Xna.Framework.Game
    {
        public static GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public static Thread thread_jeu;

        public static MouseState mouse;

        Song musique;

        #region Menu_buttons
        MenuButton play, option, quitter; //menu principal
        public static GameState CurrentGameState;

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
            ChoiceMenuCaracter.Initialise();
            Options.is_mainmenu = true;
            CurrentGameState = GameState.MainMenu;
            IsMouseVisible = true;
            base.Initialize();
        }

        //LOADCONTENT
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            musique = Content.Load<Song>(@"Sounds\Musique\Son Game 1");
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(musique);

            //CHARGEMENT DES BOUTONS
            #region Load_Button

            //BOUTONS DU MENU PRINCIPAL
            #region MainMenu
            play = new MenuButton(Content.Load<Texture2D>(@"Sprites\MainMenu\button_jouer"), new Vector2(300, graphics.GraphicsDevice.Viewport.Height / 2));
            option = new MenuButton(Content.Load<Texture2D>(@"Sprites\MainMenu\button_options"), new Vector2(300, 400));
            quitter = new MenuButton(Content.Load<Texture2D>(@"Sprites\MainMenu\bouton_quitter"), new Vector2(300, 500));
            #endregion

            Options.LoadContent(Content);

            #endregion

            ChoiceMenuCaracter.LoadContent(Content);
            ChoiceMenuBattlefield.LoadContent(Content);
        }

        //UNLOADCONTENT
        protected override void UnloadContent()
        {
            Content.Unload();
        }

        //UPDATE
        protected override void Update(GameTime gameTime)
        {
            mouse = Mouse.GetState();
            Options.plus_musique.Update(mouse);
            Options.moins_musique.Update(mouse);
            Options.plus_bruitages.Update(mouse);
            Options.moins_bruitages.Update(mouse);
            Options.bouton_retour.Update(mouse);
            ChoiceMenuCaracter.caracter1.Update(mouse);

            //DEBUGgING
            #region Debugging
            Console.Clear();
            Console.WriteLine("mouse : x = " + mouse.X + " ; y = " + mouse.Y + "\n");
            Console.WriteLine("volume musique : " + Options.mediaplayer_volume + "\n");
            Console.WriteLine("volume bruitages : " + Options.bruitage_volume + "\n");
            if (CurrentGameState == GameState.MainMenu)
                Console.WriteLine("MainMenu\n");
            else if (CurrentGameState == GameState.Options)
                Console.WriteLine("Options\n");
            else if (CurrentGameState == GameState.Playing)
                Console.WriteLine("Playing\n");
            else if (CurrentGameState == GameState.ChoiceMenuCaracter)
                Console.WriteLine("ChoiceMenuCaracter\n");
            else if (CurrentGameState == GameState.ChoiceMenuBattlefield)
                Console.WriteLine("ChoiceMenuBattlefield\n");
            #endregion

            //BOUTONS DU MENU PRINCIPAL
            #region MainMenu buttons
            play.Update(mouse);
            option.Update(mouse);
            quitter.Update(mouse);
            #endregion

            switch (CurrentGameState)
            {
                case GameState.MainMenu:

                    //MISE A JOUR DU MENU PRINCIPAL
                    #region MainMenu update
                    if (play.isClicked)
                    {
                        CurrentGameState = GameState.ChoiceMenuCaracter;
                    }

                    else if (option.isClicked)
                    {
                        CurrentGameState = GameState.Options;
                    }

                    else if (quitter.isClicked)
                    {
                        this.Exit();
                    }
                    #endregion
                    break;

                case GameState.Options:

                    Options.Update();
                    break;

                case GameState.ChoiceMenuCaracter:
                    ChoiceMenuCaracter.Update();
                    break;

                case GameState.ChoiceMenuBattlefield:
                    ChoiceMenuBattlefield.Update();
                    break;

                case GameState.Playing:

                    //LANCEMENT DU JEU
                    thread_jeu = new Thread(jeu);
                    thread_jeu.Start();
                    IsMouseVisible = false;
                    Program.thread_menu.Abort();
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
                    quitter.Draw(spriteBatch);
                    #endregion
                    break;

                case GameState.Options:
                    Options.Draw(spriteBatch, Content);
                    break;

                case GameState.ChoiceMenuCaracter:
                    ChoiceMenuCaracter.Draw(spriteBatch);
                    break;

                case GameState.ChoiceMenuBattlefield:
                    ChoiceMenuBattlefield.Draw(spriteBatch);
                    break;

                case GameState.Playing:
                    //affichage géré par le deuxième thread (thread_jeu)
                    break;
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }


        public static void jeu()
        {
            using (Game1 game1 = new Game1())
            {
                game1.Run();
            }
        }
    }
}
