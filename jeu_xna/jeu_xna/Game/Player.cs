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
        Rectangle Hitbox;
        Texture2D Joueur;

        Direction Direction;
        int Frame;
        SpriteEffects Effect; // effet miroir
        
        bool Animation;
        bool KeyDown_up;
        int Timer;
        int Timer_sound;

        int Speed = 2;
        int AnimationSpeed = 10;
        int AnimationSound = 20;

        //saut
        int jump_speed, jump_speed_initial, temp_hitbox_y;
        bool jump, is_jumping;

        // CONSTRUCTOR
        public Player(Texture2D Joueur)
        {
            this.Joueur = Joueur;
            Hitbox = new Rectangle(300, 230, 95, 200);
            temp_hitbox_y = Hitbox.Y;
            Frame = 1;
            Effect = SpriteEffects.None;
            Direction = Direction.Right;

            Animation = true;

            Timer = 0;
            Timer_sound = 0;

            jump_speed_initial = 25;
            jump_speed = 1;
            KeyDown_up = false;
            jump = false;
            is_jumping = false;
        }

        // METHODS
        public void Animate()
        {
            //animation personnage
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
        public void Update(KeyboardState keyboard)
        {
            if (keyboard.IsKeyDown(Keys.Left))
            {
                Hitbox.X -= Speed;
                Direction = Direction.Left;

                if (!is_jumping)
                {
                    Animate();
                }
            }

            else if (keyboard.IsKeyDown(Keys.Right))
            {
                Hitbox.X += Speed;
                Direction = Direction.Right;

                if (!is_jumping)
                {
                    Animate();
                }
            }

            if (keyboard.IsKeyDown(Keys.Up))
            {
                if (KeyDown_up == false)
                {
                    Ressources.Jump.Play();
                    KeyDown_up = true;
                    is_jumping = true;
                    Hitbox.Y -= jump_speed_initial;
                }
            }

            if (keyboard.IsKeyUp(Keys.Left) && keyboard.IsKeyUp(Keys.Right) && keyboard.IsKeyUp(Keys.Up))
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

        private void Saut()
        {
            if(is_jumping) //is_jumping initialisé à false
            {
                if (!jump) //jump initialisé à false
                {
                    if (Hitbox.Y > 20)
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
                    jump_speed_initial = 3;
                    jump_speed = 16;

                    if (Hitbox.Y < 230)
                    {
                        jump_speed_initial += jump_speed;
                        Hitbox.Y += jump_speed_initial;
                    }

                    else
                    {
                        Ressources.jump_end_sound.Play();
                        KeyDown_up = false;
                        jump_speed_initial = 25;
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
