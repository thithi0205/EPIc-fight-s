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
        public int nb_frames, dégat_min, dégat_max, width, height, x, y;
        public Texture2D[] frames;

        public Attack(int nb_frames, int dégat_min, int dégat_max, Texture2D[] frames, int width, int height, int x, int y)
        {
            this.nb_frames = nb_frames;
            this.dégat_min = dégat_min;
            this.dégat_max = dégat_max;
            this.frames = frames;
            this.width = width;
            this.height = height;
            this.x = x;
            this.y = y;
        }

        public static void draw(int x, int y, SpriteBatch spriteBatch, Texture2D texture)
        {
            spriteBatch.Draw(texture, new Vector2(x, y), Color.White); 
        }

        public static void attacking(int player_number, Attack current_attack)
        {
            if (player_number == 1)
            {
                if (GameMain.LocalPlayer1.attaque.Intersects(GameMain.LocalPlayer2.Hitbox)) //le joueur 1 attaque le joueur 2
                {
                    Random random = new Random();
                    int degat = random.Next(current_attack.dégat_min, current_attack.dégat_max);
                    GameMain.LocalPlayer2.vie = GameMain.LocalPlayer2.vie - degat;
                    GameMain.LocalPlayer2.is_attacked = true;
                }
            }

            else if (player_number == 2)
            {
                if (GameMain.LocalPlayer2.attaque.Intersects(GameMain.LocalPlayer1.Hitbox)) //le joueur 2 attaque le joueur 1
                {
                    Random random = new Random();
                    int degat = random.Next(current_attack.dégat_min, current_attack.dégat_max);
                    GameMain.LocalPlayer1.vie = GameMain.LocalPlayer1.vie - degat;
                    GameMain.LocalPlayer1.is_attacked = true;
                }
            }
        }
    }
}
