using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace jeu_xna
{
    public enum Direction
    {
        Left, Right
    };

    class Player
    {
        // FIELDS
        public Rectangle Hitbox;
        Texture2D Joueur;
        Keys saut, droite, gauche;

        Direction Direction;
        int Frame;
        SpriteEffects Effect; // effet miroir
        
        bool Animation;
        bool KeyDown_up;
        int Timer;
        int Timer_sound;

        int Speed = 5;
        int AnimationSpeed = 14;
        int AnimationSound = 24;

        //saut
        int jump_speed, jump_speed_initial;
        bool jump, is_jumping;

        // CONSTRUCTOR
        public Player(Texture2D Joueur, int x, int y, Direction direction, Keys saut, Keys droite, Keys gauche)
        {
            this.saut = saut;
            this.gauche = gauche;
            this.droite = droite;
            this.Joueur = Joueur;
            Hitbox = new Rectangle(x, y, 95, 200);
            Frame = 1; //texture affichée lorsque toutes les touches sont relachées
            Effect = SpriteEffects.None;
            Direction = direction;

            Animation = true; //utilisé pour le retour à la première image lors de l'animation de la texture du personnage

            Timer = 0; //timer pour la texture du personnage
            Timer_sound = 0; //timer pour les bruitages

            jump_speed_initial = 20; //25
            jump_speed = 1;
            KeyDown_up = false; //indique si la touche précédement enfoncée est la touche du saut
            jump = false; //indique si la hauteur maximale du saut a déjà été atteinte
            is_jumping = false; //est en train de sauter
        }

        // METHODS
        public void Animate()
        {
            //animation texture personnage
            Timer++;
            if (Timer == AnimationSpeed)
            {
                Timer = 0;
                if (Animation)
                {
                    Frame++;

                    if (Frame > 4)
                    {
                        Frame = 2;
                        Animation = true;
                    }
                }

                else
                {
                    Frame++;

                    if (Frame < 2)
                    {
                        Frame = 2;
                        Animation = false;
                    }
                }
            }

            //animation son pas personnage
            Timer_sound++;
            if (Timer_sound == AnimationSound)
            {
                Timer_sound = 0;
                Ressources.Pas.Play();
            }
            
        }

        // UPDATE & DRAW
        //DEPLACEMENT DU PERSONNAGE
        public void Update(MouseState MouseState, KeyboardState keyboard)
        {
            if (is_jumping)
            {
                Speed = 10;
            }

            else
            {
                Speed = 5;
            }

            if (keyboard.IsKeyDown(gauche))
            {
                Hitbox.X -= Speed;
                Direction = Direction.Left;

                if (!is_jumping)
                {
                    Animate();
                }
            }

            else if (keyboard.IsKeyDown(droite))
            {
                Hitbox.X += Speed;
                Direction = Direction.Right;

                if (!is_jumping)
                {
                    Animate();
                }
            }

            if (keyboard.IsKeyDown(saut))
            {
                if (KeyDown_up == false)
                {
                    Ressources.Jump.Play();
                    KeyDown_up = true;
                    is_jumping = true;
                    Hitbox.Y -= jump_speed_initial;
                }
            }

            if (keyboard.IsKeyUp(gauche) && keyboard.IsKeyUp(droite) && keyboard.IsKeyUp(saut))
            {
                Frame = 1;
                Timer = 0;
            }

            switch (Direction)
            {
                case Direction.Left:
                    Effect = SpriteEffects.FlipHorizontally;
                    break;
                case Direction.Right:
                    Effect = SpriteEffects.None;
                    break;
            }

            //Collisions bords écran
            if (Hitbox.X < 0)
            {
                Hitbox.X = Game1.graphics1.GraphicsDevice.Viewport.Width - Hitbox.Width;
            }

            else if (Hitbox.X > Game1.graphics1.GraphicsDevice.Viewport.Width - Hitbox.Width)
            {
                Hitbox.X = 0;
            }

            Saut(); //gestion du saut
        }

        //GESTION DU SAUT
        private void Saut()
        {
            if(is_jumping) //est en train de sauter
            {
                if (!jump) //n'a pas atteint la hauteur maximale du saut
                {
                    if (Hitbox.Y > 20) //20
                    {
                        jump_speed_initial -= jump_speed;
                        Hitbox.Y -= jump_speed_initial;
                    }

                    else
                    {
                        jump = true;
                    }
                }

                else if (jump)
                {
                    jump_speed_initial = 3; //3
                    jump_speed = 11; //11

                    if (Hitbox.Y < 230)
                    {
                        jump_speed_initial += jump_speed;
                        Hitbox.Y += jump_speed_initial;
                    }

                    else
                    {
                        Ressources.jump_end_sound.Play();
                        KeyDown_up = false;
                        jump_speed_initial = 20;
                        jump_speed = 1;
                        is_jumping = false;
                        jump = false;
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Joueur, Hitbox,
                new Rectangle((Frame - 1) * 95, 0, 95, 200), 
                Color.White, 0f, Vector2.Zero, Effect, 0f);
        }
    }
}
