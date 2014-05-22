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

    public class Game1 : Microsoft.Xna.Framework.Game
    {
        public static GraphicsDeviceManager graphics1;
        SpriteBatch spriteBatch;
        GameMain Main;
        KeyboardState keyboard;

        public Game1()
        {
            graphics1 = new GraphicsDeviceManager(this);
            graphics1.PreferredBackBufferHeight = 600;
            graphics1.PreferredBackBufferWidth = 800;
            graphics1.ApplyChanges();
            //graphics1.ToggleFullScreen();
            Content.RootDirectory = "Content";
        }

        //INITIALIZE
        protected override void Initialize()
        {
            IsMouseVisible = true;
            Options.is_mainmenu = false;
            GameMain.Initialize();
            base.Initialize();
        }

        //LOADCONTENT
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Ressources.LoadContent_Sprites(Content);
            Ressources.LoadContent_Sounds(Content);
            GameMain.LoadContent(Content);

            Main = new GameMain(Content);
        }

        //UNLOADCONTENT
        protected override void UnloadContent()
        {
            Content.Unload();
        }

        //UPDATE
        protected override void Update(GameTime gameTime)
        {
            Options.plus_musique.Update(MainMenu.mouse);
            Options.moins_musique.Update(MainMenu.mouse);
            Options.plus_bruitages.Update(MainMenu.mouse);
            Options.moins_bruitages.Update(MainMenu.mouse);
            Options.bouton_retour.Update(MainMenu.mouse);
            Options.bouton_commande.Update(MainMenu.mouse);

            ChangeControls.left1.Update(keyboard);
            ChangeControls.right1.Update(keyboard);
            ChangeControls.up1.Update(keyboard);
            ChangeControls.attack1_1.Update(keyboard);
            ChangeControls.attack1_2.Update(keyboard);
            ChangeControls.attack1_3.Update(keyboard);

            ChangeControls.left2.Update(keyboard);
            ChangeControls.right2.Update(keyboard);
            ChangeControls.up2.Update(keyboard);
            ChangeControls.attack2_1.Update(keyboard);
            ChangeControls.attack2_2.Update(keyboard);
            ChangeControls.attack2_3.Update(keyboard);


            MainMenu.mouse = Mouse.GetState();
            Mouse.WindowHandle = Window.Handle;
            keyboard = Keyboard.GetState();

            Main.Update(MainMenu.mouse, keyboard);
        }

        //DRAW
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            Main.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
     }
}

