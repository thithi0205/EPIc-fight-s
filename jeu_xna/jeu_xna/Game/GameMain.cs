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
        Player LocalPlayer;
        bool WasKeyDown_Escape = false;
        Texture2D patate;

        // CONSTRUCTOR
        public GameMain(ContentManager Content)
        {
            //Content.RootDirectory = "Content";
            LocalPlayer = new Player(Ressources.personnage);
            patate = Content.Load<Texture2D>(@"Sprites\MainMenu\Options\background");
        }

        // METHODS

        // UPDATE & DRAW
        public void Update(MouseState mouse, KeyboardState keyboard)
        {
            if (keyboard.IsKeyDown(Keys.Escape) && MainMenu.CurrentGameState == GameState.Playing && !WasKeyDown_Escape)
            {
                MainMenu.CurrentGameState = GameState.Pause;
                WasKeyDown_Escape = true;
            }

            else if (keyboard.IsKeyDown(Keys.Escape) && MainMenu.CurrentGameState == GameState.Pause && !WasKeyDown_Escape)
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
                    LocalPlayer.Update(mouse, keyboard);
                    break;

                case GameState.Pause:
                    break;

            }
            //LocalPlayer.Update(mouse, keyboard);

            //DEBUGGING
            #region Debuging
            Console.Clear();
            Console.WriteLine("personnage : x = " + LocalPlayer.Hitbox.X + " ; y = " + LocalPlayer.Hitbox.Y + "\n");
            Console.WriteLine("volume musique : " + MainMenu.mediaplayer_volume + "\n");
            Console.WriteLine("volume bruitages : " + MainMenu.bruitage_volume + "\n");
            if (MainMenu.CurrentGameState == GameState.Playing)
                Console.WriteLine("Playing\n");
            else if (MainMenu.CurrentGameState == GameState.Pause)
                Console.WriteLine("Pause\n");
            #endregion
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            switch (MainMenu.CurrentGameState)
            {
                case GameState.Playing:
                    spriteBatch.Draw(Ressources.Fond, Vector2.Zero, Color.White);
                    LocalPlayer.Draw(spriteBatch);
                    break;

                case GameState.Pause:
                    spriteBatch.Draw(patate, Vector2.Zero, Color.White);
                    break;

            }
        }
    }
}
