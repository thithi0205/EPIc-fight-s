using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace jeu_xna
{
    class Attack
    {
        public int nb_frames, largeur_image; //largeur_image = largeur maximale de la texture de l'attaque, frame_attack = frame qui correspond au moment où l'attaque sera effectuée
        public int dégat_min, dégat_max, x, y, width, height, frame_attack; 
        public int displayed_picture; //permet de sélectionner la texture de l'attaque à afficher
        public Texture2D frames; //textures de l'attaque
        public SoundEffect sound_attack;

        public Attack(int nb_frames, int dégat_min, int dégat_max, Texture2D frames, int x, int y, int width, int height, int largeur_image, int frame_attack, SoundEffect sound_attack)
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
            this.sound_attack = sound_attack;
        }

        public void draw(int x, int y, SpriteBatch spriteBatch, Texture2D texture, SpriteEffects effect)
        {
            spriteBatch.Draw(texture, new Rectangle(x, y, largeur_image, texture.Height), new Rectangle((displayed_picture - 1) * largeur_image, 0, largeur_image, texture.Height), Color.White, 0f, Vector2.Zero, effect, 0f);
        }

        public static void attacking(int player_number, Attack current_attack)
        {
            if (player_number == 1)
            {
                if (GameMain.LocalPlayer1.attaque.Intersects(GameMain.LocalPlayer2.Hitbox) && !GameMain.LocalPlayer2.is_dead) //le joueur 1 attaque le joueur 2
                {
                    Random degat = new Random();
                    Random energy_minus = new Random();
                    GameMain.LocalPlayer2.vie = GameMain.LocalPlayer2.vie - degat.Next(current_attack.dégat_min, current_attack.dégat_max);
                    GameMain.LocalPlayer2.is_attacked = true;
                    GameMain.LocalPlayer2.energy = GameMain.LocalPlayer2.energy - energy_minus.Next(1, 30);

                    if (!GameMain.LocalPlayer2.is_accroupi)
                    {
                        GameMain.LocalPlayer2.limit_jump = 200;
                    }

                    else
                    {
                        GameMain.LocalPlayer2.limit_jump = 230;
                    }

                    GameMain.LocalPlayer2.small_jump = true;
                    GameMain.LocalPlayer2.Hitbox.Y -= GameMain.LocalPlayer2.jump_speed_initial;
                    GameMain.LocalPlayer2.is_jumping = true;
                    GameMain.LocalPlayer2.jump_speed_initial = 10;
                    GameMain.LocalPlayer2.texturecaracter.pain.Play();

                    if (GameMain.LocalPlayer1.Effect == SpriteEffects.None)
                    {
                        GameMain.LocalPlayer2.small_jump_val = 10;
                    }

                    else if (GameMain.LocalPlayer1.Effect == SpriteEffects.FlipHorizontally)
                    {
                        GameMain.LocalPlayer2.small_jump_val = -10;
                    }

                    if (current_attack != GameMain.LocalPlayer1.texturecaracter.attaque3)
                    {
                        Random energy_add = new Random();
                        GameMain.LocalPlayer1.energy = GameMain.LocalPlayer1.energy + energy_add.Next(0, 30);
                    }

                    if (GameMain.LocalPlayer1.energy > 100)
                    {
                        GameMain.LocalPlayer1.energy = 100;
                    }

                    if (GameMain.LocalPlayer2.energy < 0)
                    {
                        GameMain.LocalPlayer2.energy = 0;
                    }
                }
            }

            else if (player_number == 2)
            {
                if (GameMain.LocalPlayer2.attaque.Intersects(GameMain.LocalPlayer1.Hitbox) && !GameMain.LocalPlayer1.is_dead) //le joueur 2 attaque le joueur 1
                {
                    Random degat = new Random();
                    Random energy_minus = new Random();
                    GameMain.LocalPlayer1.vie = GameMain.LocalPlayer1.vie - degat.Next(current_attack.dégat_min, current_attack.dégat_max);
                    GameMain.LocalPlayer1.is_attacked = true;
                    GameMain.LocalPlayer1.energy = GameMain.LocalPlayer1.energy - energy_minus.Next(1, 30);

                    if (!GameMain.LocalPlayer1.is_accroupi)
                    {
                        GameMain.LocalPlayer1.limit_jump = 200;
                    }

                    else
                    {
                        GameMain.LocalPlayer1.limit_jump = 230;
                    }

                    GameMain.LocalPlayer1.small_jump = true;
                    GameMain.LocalPlayer1.Hitbox.Y -= GameMain.LocalPlayer1.jump_speed_initial;
                    GameMain.LocalPlayer1.is_jumping = true;
                    GameMain.LocalPlayer1.jump_speed_initial = 10;
                    GameMain.LocalPlayer1.texturecaracter.pain.Play();

                    if (GameMain.LocalPlayer2.Effect == SpriteEffects.None)
                    {
                        GameMain.LocalPlayer1.small_jump_val = 10;
                    }

                    else if (GameMain.LocalPlayer2.Effect == SpriteEffects.FlipHorizontally)
                    {
                        GameMain.LocalPlayer1.small_jump_val = -10;
                    }

                    if (current_attack != GameMain.LocalPlayer2.texturecaracter.attaque3)
                    {
                        Random energy_add = new Random();
                        GameMain.LocalPlayer2.energy = GameMain.LocalPlayer2.energy + energy_add.Next(0, 30);
                    }

                    if (GameMain.LocalPlayer2.energy > 100)
                    {
                        GameMain.LocalPlayer2.energy = 100;
                    }

                    if (GameMain.LocalPlayer1.energy < 0)
                    {
                        GameMain.LocalPlayer1.energy = 0;
                    }
                }
            }
        }
    }
}
