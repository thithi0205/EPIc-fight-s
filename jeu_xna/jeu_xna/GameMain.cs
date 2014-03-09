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
            LocalPlayer = new Player();
        }

        // METHODS

        // UPDATE & DRAW
        public void Update(MouseState mouse, KeyboardState keyboard)
        {
            LocalPlayer.Update(mouse, keyboard);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Ressources.Fond, Vector2.Zero, Color.White);
            LocalPlayer.Draw(spriteBatch);
        }
    }
}
