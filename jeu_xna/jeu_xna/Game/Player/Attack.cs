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
        public int nb_frames, largeur_image; //largeur_image = largeur maximale de la texture de l'attaque, frame_attack = frame qui correspond au moment où l'attaque sera effectuée
        public int dégat_min, dégat_max, x, y, width, height, frame_attack; 
        public int displayed_picture; //permet de sélectionner la texture de l'attaque à afficher
        public Texture2D frames; //textures de l'attaque

        public Attack(int nb_frames, int dégat_min, int dégat_max, Texture2D frames, int x, int y, int width, int height, int largeur_image, int frame_attack)
        {
            this.nb_frames = nb_frames;
            this.dégat_min = dégat_min;
            this.dégat_max = dégat_max;
            this.frames = frames;
            displayed_picture = 0;
            this.largeur_image = largeur_image;
            this.frame_attack = frame_attack;
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
        }

        public void draw(int x, int y, SpriteBatch spriteBatch, Texture2D texture, SpriteEffects effect)
        {
            spriteBatch.Draw(texture, new Rectangle(x, y, largeur_image, 200), new Rectangle((displayed_picture - 1) * largeur_image, 0, largeur_image, 200), Color.White, 0f, Vector2.Zero, effect, 0f);
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
