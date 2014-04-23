using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace jeu_xna
{
    class Attack
    {
        int nb_frames, dégat_min, dégat_max;
        Texture2D[] frames;

        public Attack(int nb_frames, int dégat_min, int dégat_max, Texture2D[] frames)
        {
            this.nb_frames = nb_frames;
            this.dégat_min = dégat_min;
            this.dégat_max = dégat_max;
            this.frames = frames;
        }

        public static void draw(int x, int y, SpriteBatch spriteBatch, Texture2D texture)
        {
            spriteBatch.Draw(texture, new Vector2(x, y), Color.White); 
        }
    }
}
