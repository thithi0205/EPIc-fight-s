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
    public class MainMenu : Microsoft.Xna.Framework.Game
    {
        public static GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public static Thread thread_jeu;

        static Texture2D background, epic_fight_s;
        static SpriteFont team;

        public static MouseState mouse;

        Song musique;

        #region Menu_buttons
        MenuButton play, option, quitter, mainmenu; //menu principal

        #endregion

        public MainMenu()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = 600;
            graphics.PreferredBackBufferWidth = 800;
            //graphics.ApplyChanges();
            Content.RootDirectory = "Content";
        }

        //INITIALIZE
        protected override void Initialize()
        {
            ChoiceMenuCaracter.Initialise();
            Options.is_mainmenu = true;
            VarTemp.CurrentGameState = GameState.MainMenu;
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

            mainmenu = new MenuButton(Content.Load<Texture2D>(@"Sprites\MainMenu\bouton_menu-principal"), new Vector2(280, 500));
            background = Content.Load<Texture2D>(@"Sprites\MainMenu\Options\background");
            epic_fight_s = Content.Load<Texture2D>(@"Sprites\MainMenu\epic_fight's");
            team = Content.Load<SpriteFont>("team");
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

            #region mise à jour des boutons des menus
            Options.plus_musique.Update(mouse);
            Options.moins_musique.Update(mouse);
            Options.plus_bruitages.Update(mouse);
            Options.moins_bruitages.Update(mouse);
            Options.bouton_retour.Update(mouse);

            ChoiceMenuCaracter.caracter1.Update(mouse);
            ChoiceMenuCaracter.retour.Update(mouse);
            ChoiceMenuCaracter.terrain.Update(mouse);

            ChoiceMenuBattlefield.jouer.Update(mouse);
            ChoiceMenuBattlefield.terrain1.Update(mouse);
            ChoiceMenuBattlefield.terrain2.Update(mouse);
            #endregion

            //DEBUGgING
            #region Debugging
            Console.Clear();
            Console.WriteLine("mouse : x = " + mouse.X + " ; y = " + mouse.Y + "\n");
            Console.WriteLine("volume musique : " + Options.mediaplayer_volume + "\n");
            Console.WriteLine("volume bruitages : " + Options.bruitage_volume + "\n");
            if (VarTemp.CurrentGameState == GameState.MainMenu)
                Console.WriteLine("MainMenu\n");
            else if (VarTemp.CurrentGameState == GameState.Options)
                Console.WriteLine("Options\n");
            else if (VarTemp.CurrentGameState == GameState.Playing)
                Console.WriteLine("Playing\n");
            else if (VarTemp.CurrentGameState == GameState.ChoiceMenuCaracter)
                Console.WriteLine("ChoiceMenuCaracter\n");
            else if (VarTemp.CurrentGameState == GameState.ChoiceMenuBattlefield)
                Console.WriteLine("ChoiceMenuBattlefield\n");
            #endregion

            //BOUTONS DU MENU PRINCIPAL
            #region MainMenu buttons
            play.Update(mouse);
            option.Update(mouse);
            quitter.Update(mouse);
            #endregion

            switch (VarTemp.CurrentGameState)
            {
                case GameState.MainMenu:
                    //MISE A JOUR DU MENU PRINCIPAL
                    #region MainMenu update
                    if (play.isClicked && !ChoiceMenuCaracter.was_cliqued)
                    {
                        VarTemp.CurrentGameState = GameState.ChoiceMenuCaracter;
                    }

                    else if (option.isClicked && !ChoiceMenuCaracter.was_cliqued)
                    {
                        VarTemp.CurrentGameState = GameState.Options;
                    }

                    else if (quitter.isClicked && !ChoiceMenuCaracter.was_cliqued)
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

            #region mise à jour bouton Menu principal choix personnages et terrain de combat
            mainmenu.Update(mouse);

            if (VarTemp.CurrentGameState == GameState.ChoiceMenuCaracter || VarTemp.CurrentGameState == GameState.ChoiceMenuBattlefield)
            {
                if (mainmenu.isClicked)
                {
                    VarTemp.CurrentGameState = GameState.MainMenu;
                    ChoiceMenuCaracter.was_cliqued = true;
                    ChoiceMenuBattlefield.choisi = false;
                    ChoiceMenuCaracter.player = 1;
                }
            }

            if (MainMenu.mouse.LeftButton == ButtonState.Released && MainMenu.mouse.RightButton == ButtonState.Released)
            {
                ChoiceMenuCaracter.was_cliqued = false;
            }
            #endregion

            base.Update(gameTime);
        }

        //DRAW
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();
            switch (VarTemp.CurrentGameState)
            {
                case GameState.MainMenu:

                    //AFFICHAGE DU MENU PRINCIPAL
                    #region MainMenu draw
                    spriteBatch.Draw(background, Vector2.Zero, Color.White);
                    spriteBatch.Draw(epic_fight_s, new Vector2(100, 0), Color.White);
                    option.Draw(spriteBatch);
                    play.Draw(spriteBatch);
                    quitter.Draw(spriteBatch);
                    spriteBatch.DrawString(team, "By Ubidah !", new Vector2(730, 580), Color.Black);
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

            if (VarTemp.CurrentGameState == GameState.ChoiceMenuCaracter || VarTemp.CurrentGameState == GameState.ChoiceMenuBattlefield)
            {
                mainmenu.Draw(spriteBatch);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }


        public static void jeu() //lancement du jeu
        {
            using (Game1 game1 = new Game1())
            {
                game1.Run();
            }
        }
    }
}
