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
            base.Initialize();
        }

        //LOADCONTENT
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Ressources.LoadContent_Sprites(Content);
            Ressources.LoadContent_Sounds(Content);

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
            Main.Update(Mouse.GetState(), Keyboard.GetState());
            base.Update(gameTime);
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
