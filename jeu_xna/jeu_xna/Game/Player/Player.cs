using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Content;

namespace jeu_xna
{
    public enum Direction
    {
        Left, Right
    };

    class Player
    {
        // FIELDS
        public int vie;

        string name;
        SpriteFont display_name;
        int player_number;
        Texture2D photo_identité;
        Texture2D BlankTexture; //nécéssaire pour la barre de vie
        SoundEffect jump_sound; //son joué au début du saut

        public int can_jump = 0;

        public Rectangle Hitbox;
        Texture2D Joueur;
        Keys saut, droite, gauche;

        Direction Direction;
        int Frame; //image du personnage affichée
        SpriteEffects Effect; // effet miroir

        bool jump1; //bloque les sauts temporairement
        bool Animation;
        public bool KeyDown_up;
        int Timer; //timer pour l'animation du personnage
        int Timer_sound; //timer pour les bruitages du personnage

        int Speed = 3;
        int AnimationSpeed = 14;
        int AnimationSound = 24;

        //saut
        int jump_speed, jump_speed_initial;
        bool jump, is_jumping;

        Keys attaque1;

        // CONSTRUCTOR
        public Player(TextureCaracter texturecaracter, int x, int y, Direction direction, Keys saut, Keys droite, Keys gauche, string name, int player_number, ContentManager Content, Keys attaque1)
        {
            vie = 100; //vie initiale du personnage

            this.name = name;
            this.player_number = player_number;
            display_name = Content.Load<SpriteFont>("display_name");
            BlankTexture = Content.Load<Texture2D>(@"Sprites\Personnages\BlankTexture");
            this.photo_identité = texturecaracter.identity;
            this.jump_sound = texturecaracter.jump;

            //DIRECTIONS DU PERSONNAGE
            this.saut = saut;
            this.gauche = gauche;
            this.droite = droite;

            this.Joueur = texturecaracter.personnage; //texture du personnage
            Hitbox = new Rectangle(x, y, 95, 200);
            Frame = 1; //texture affichée lorsque toutes les touches sont relachées
            Effect = SpriteEffects.None;
            Direction = direction;

            Animation = true; //utilisé pour le retour à la première image lors de l'animation de la texture du personnage

            Timer = 0; //timer pour la texture du personnage
            Timer_sound = 0; //timer pour les bruitages

            jump_speed_initial = 20;
            jump_speed = 1;
            KeyDown_up = false; //indique si la touche précédement enfoncée est la touche du saut
            jump = false; //indique si la hauteur maximale du saut a déjà été atteinte
            is_jumping = false; //est en train de sauter
            jump1 = false;

            this.attaque1 = attaque1;
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
                if (KeyDown_up == false && !jump1)
                {
                    jump_sound.Play();
                    KeyDown_up = true;
                    is_jumping = true;
                    Hitbox.Y -= jump_speed_initial;
                    can_jump = 0;
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

            if (KeyDown_up && !jump1)
            {
                jump1 = true;
            }

            if (jump1)
            {
                if (can_jump <= 20)
                {
                    can_jump++;
                }

                else
                {
                    can_jump = 0;
                    jump1 = false;
                }
            }

            Saut(); //gestion du saut
        }

        //GESTION DU SAUT
        private void Saut()
        {
            if (is_jumping) //est en train de sauter
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
            if (player_number == 1)
            {
                spriteBatch.DrawString(display_name, name, new Vector2(295 - display_name.MeasureString(name).X, 500), Color.White);
            }

            else if (player_number == 2)
            {
                spriteBatch.DrawString(display_name, name, new Vector2((Game1.graphics1.GraphicsDevice.Viewport.Width - 380) + display_name.MeasureString(name).X, 500), Color.White);
            }

            if (player_number == 2 && GameMain.timer_combat_secondes < 0)
            {
                spriteBatch.Draw(Joueur, Hitbox, new Rectangle((Frame - 1) * 95, 0, 95, 200), Color.White, 0f, Vector2.Zero, SpriteEffects.FlipHorizontally, 0f);
            }

            else
            {
                spriteBatch.Draw(Joueur, Hitbox, new Rectangle((Frame - 1) * 95, 0, 95, 200), Color.White, 0f, Vector2.Zero, Effect, 0f);
            }

            
        }

        public void GenerateBar(int Current, int Max, SpriteBatch spriteBatch)
        {
            int x = 0;
            int y = 560;
            int BarWidth = 250;
            int BarHeight = 10;
            int BoarderOffSet = 2;
            Double PercentToDraw = (Double)Current / Max;
            Double EachPercentWidth = (Double)BarWidth / Max;

            if (player_number == 1)
            {
                x = 40;
            }

            else if (player_number == 2)
            {
                x = Game1.graphics1.GraphicsDevice.Viewport.Width - BarWidth - 40;
            }

            //Boarder Rectangle
            Rectangle RecBoarder = new Rectangle(x, y, BarWidth + (BoarderOffSet * 2), BarHeight + (BoarderOffSet * 2));


            //Bar Background Rectangle
            Rectangle RecBackGround = new Rectangle(x + BoarderOffSet, y + BoarderOffSet, BarWidth, BarHeight);


            //Fill Rectangle
            Rectangle RecFill = new Rectangle(x + BoarderOffSet, y + BoarderOffSet, (int)((PercentToDraw * 100) * EachPercentWidth), BarHeight);

            spriteBatch.Draw(BlankTexture, RecBoarder, Color.White);
            spriteBatch.Draw(BlankTexture, RecBackGround, Color.Gray);
            spriteBatch.Draw(BlankTexture, RecFill, Color.Red);
        }

        public void GeneratePicture(SpriteBatch spriteBatch)
        {
            int x = 0;
            int y = 500;
            int BarWidth = 50;
            int BarHeight = 50;
            int BoarderOffSet = 2;

            if (player_number == 1)
            {
                x = 40;
            }

            else if (player_number == 2)
            {
                x = Game1.graphics1.GraphicsDevice.Viewport.Width - BarWidth - 40;
            }

            //Boarder Rectangle
            Rectangle RecBoarder = new Rectangle(x, y, BarWidth + (BoarderOffSet * 2), BarHeight + (BoarderOffSet * 2));


            //Bar Background Rectangle
            Rectangle RecBackGround = new Rectangle(x + BoarderOffSet, y + BoarderOffSet, BarWidth, BarHeight);

            //picture rectangle
            Rectangle PictureRectangle = new Rectangle(x + BoarderOffSet, y + BoarderOffSet, BarWidth, BarHeight);

            spriteBatch.Draw(BlankTexture, RecBoarder, Color.White);
            spriteBatch.Draw(BlankTexture, RecBackGround, Color.Gray);

            if (player_number == 1)
            {
                spriteBatch.Draw(photo_identité, PictureRectangle, Color.White);
            }

            else if (player_number == 2)
            {
                spriteBatch.Draw(photo_identité, PictureRectangle, new Rectangle(0, 0, 50, 50), Color.White, 0f, Vector2.Zero, SpriteEffects.FlipHorizontally, 0f);
            }
        }
    }
}
