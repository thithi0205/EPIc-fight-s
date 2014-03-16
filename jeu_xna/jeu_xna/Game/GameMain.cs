using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;

namespace jeu_xna
{
    class GameMain
    {
        // FIELD
        Player LocalPlayer;

        // CONSTRUCTOR
        public GameMain()
        {
            LocalPlayer = new Player(Ressources.personnage);
        }

        // METHODS

        // UPDATE & DRAW
        public void Update(MouseState mouse, KeyboardState keyboard)
        {
            LocalPlayer.Update(mouse, keyboard);

            //DEBUGGING
            #region Debuging
            Console.Clear();
            Console.WriteLine("personnage : x = " + LocalPlayer.Hitbox.X + " ; y = " + LocalPlayer.Hitbox.Y + "\n");
            Console.WriteLine("volume musique : " + MainMenu.mediaplayer_volume + "\n");
            Console.WriteLine("volume bruitages : " + MainMenu.bruitage_volume + "\n");
            Console.WriteLine("Playing\n");
            #endregion
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Ressources.Fond, Vector2.Zero, Color.White);
            LocalPlayer.Draw(spriteBatch);
        }
    }
}
