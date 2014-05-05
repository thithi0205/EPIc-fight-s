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

        public string name;
        SpriteFont display_name;
        int player_number;
        Texture2D photo_identité, BlankTexture;
        SoundEffect jump_sound; //son joué au début du saut

        public int can_jump = 0;

        public Rectangle Hitbox;
        Texture2D Joueur;
        public Keys saut, droite, gauche;

        Direction Direction;
        int Frame; //image du personnage affichée
        SpriteEffects Effect; // effet miroir

        bool jump1; //bloque les sauts temporairement
        bool Animation;
        public bool KeyDown_up;
        int Timer; //timer pour l'animation du personnage
        int Timer_sound; //timer pour les bruitages du personnage

        int Speed;
        int AnimationSpeed;
        int AnimationSound;

        //saut
        int jump_speed, jump_speed_initial;
        bool jump, is_jumping;

        //attaques
        public Keys attaque1, attaque2, attaque3, attack_temp;
        public Attack current_attack;
        public Rectangle attaque;
        public bool is_attacking, can_attack, attack, can_display_attack, is_attacked, display_caracter, has_attaqued; //frame_attack_counter = permet à frame_attack de changer de valeur ou non
        public int frame_counter, frame_counter_is_attacked, frame_attack, temp; //frame_attack = dès que cette valeur dépasse 1, alors n'attaque n'a plus lieu
        public TextureCaracter texturecaracter;

        //mort
        public int dead_alive_frames_counter_display, dead_alive_frames_counter;
        public bool is_dead;

        //victoire
        public bool win;

        // CONSTRUCTOR
        public Player(TextureCaracter texturecaracter, int x, int y, Direction direction, Keys saut, Keys droite, Keys gauche, string name, int player_number, ContentManager Content, Keys attaque1, Keys attaque2, Keys attaque3)
        {
            vie = 100; //vie initiale du personnage

            Speed = 5;
            AnimationSpeed = 14;
            AnimationSound = 24;

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

            if (player_number == 2)
            {
                Effect = SpriteEffects.FlipHorizontally;
            }

            else if (player_number == 1)
            {
                Effect = SpriteEffects.None;
            }

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

            //VARIABLES POUR LES ATTAQUES
            this.attaque1 = attaque1;
            this.attaque2 = attaque2;
            this.attaque3 = attaque3;
            this.texturecaracter = texturecaracter;
            is_attacking = false;
            is_attacked = false;
            can_attack = true;
            attack = false;
            can_display_attack = false;
            attaque = new Rectangle(1, 1, 1, 1);
            frame_counter = 0;
            frame_counter_is_attacked = 0;
            display_caracter = true;
            frame_attack = 0;
            has_attaqued = false;
            temp = 0;
            attack_temp = attaque1;

            //VARIABLES POUR LA MORT
            dead_alive_frames_counter_display = 0;
            dead_alive_frames_counter = 0;
            is_dead = false;

            //VARIABLES POUR LA VICTOIRE
            win = false;
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
            if (is_jumping) //accélération du déplacement sur l'axe des adscisses pendant le saut
            {
                Speed = 10;
            }

            else
            {
                Speed = 5;
            }

            #region directions + saut
            if (keyboard.IsKeyDown(gauche) && !is_attacked && !is_dead)
            {
                Hitbox.X -= Speed;
                Direction = Direction.Left;

                if (!is_jumping)
                {
                    Animate();
                }
            }

            else if (keyboard.IsKeyDown(droite) && !is_attacked && !is_dead)
            {
                Hitbox.X += Speed;
                Direction = Direction.Right;

                if (!is_jumping)
                {
                    Animate();
                }
            }

            if (keyboard.IsKeyDown(saut) && !is_attacked && !is_dead)
            {
                if (KeyDown_up == false && !jump1)
                {
                    jump_sound.Play();
                    KeyDown_up = true;
                    is_jumping = true;
                    Hitbox.Y -= jump_speed_initial;
                    can_jump = 0;
                }
            #endregion
            }

            #region Attaques
            if ((keyboard.IsKeyDown(attaque1) && (can_attack || attack_temp != attaque1) && !is_attacked && !is_dead))
            {
                if (attack_temp != attaque1)
                {
                    frame_attack = 0;
                    frame_counter = 0;
                }

                attack_temp = attaque1;
                current_attack = texturecaracter.attaque1;
                PrepareAttack();
            }

            else if (keyboard.IsKeyDown(attaque2) && (can_attack || attack_temp != attaque2) && !is_attacked && !is_dead)
            {
                if (attack_temp != attaque2)
                {
                    frame_attack = 0;
                    frame_counter = 0;
                }

                attack_temp = attaque2;
                current_attack = texturecaracter.attaque2;
                PrepareAttack();
            }

            else if (keyboard.IsKeyDown(attaque3) && (can_attack || attack_temp != attaque3) && !is_attacked && !is_dead)
            {
                if (attack_temp != attaque3)
                {
                    frame_attack = 0;
                    frame_counter = 0;
                }

                attack_temp = attaque3;
                current_attack = texturecaracter.attaque3;
                PrepareAttack();
            }
            #endregion

            if (keyboard.IsKeyUp(gauche) && keyboard.IsKeyUp(droite) && keyboard.IsKeyUp(saut) && !is_dead)
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

            #region gestion des attaques
            if (current_attack != null)
            {
                if (frame_counter % 9 == 0 && current_attack.displayed_picture < current_attack.nb_frames && attack)
                {
                    current_attack.displayed_picture++;
                }

                if (current_attack.displayed_picture == current_attack.frame_attack && frame_attack != 1 && !has_attaqued) 
                {
                    is_attacking = true;
                    frame_attack++;
                    has_attaqued = true;
                }

                else
                {
                    frame_attack = 0;
                }

                if (frame_counter <= 20)
                {
                    frame_counter++;
                }
            }

            if (frame_counter >= 20 && frame_counter <= 50)
            {
                frame_counter++;
            }

            else if (frame_counter >= 50 && frame_counter < 80)
            {
                frame_counter ++;
                attack = false;
                has_attaqued = false;
                current_attack = null;
                can_display_attack = false;
                is_attacking = false;
            }

            else if (frame_counter == 80) //blocage des attaques pendant 0,5 seconde
            {
                frame_counter = 0;
                can_attack = true;
            }

            if (current_attack != null && is_attacking && frame_attack == 1)
            {
                Attack.attacking(player_number, current_attack); //gestion des attaques
            }

            //blocage du personnage attaqué pendant 0,5 seconde
            if (is_attacked)
            {
                if (frame_counter_is_attacked < 30)
                {
                    frame_counter_is_attacked++;

                    if (display_caracter)
                    {
                        display_caracter = false;
                    }

                    else
                    {
                        display_caracter = true;
                    }
                }

                else
                {
                    frame_counter_is_attacked = 0;
                    is_attacked = false;
                }
            }
            #endregion

            #region Personnage mort
            if (!win)
            {
                if (vie == 0)
                {
                    is_dead = true;
                }

                if (is_dead)
                {
                    dead_alive_frames_counter++;

                    if (dead_alive_frames_counter % 10 == 0 && dead_alive_frames_counter_display < (texturecaracter.mort.nb_frames - 1))
                    {
                        dead_alive_frames_counter_display++;
                    }
                }
            }
            #endregion

            #region Victoire
            if (win)
            {
                dead_alive_frames_counter++;

                if (dead_alive_frames_counter % 10 == 0 && dead_alive_frames_counter_display == 0)
                {
                    dead_alive_frames_counter_display++;
                }

                else if (dead_alive_frames_counter % 10 == 0 && dead_alive_frames_counter_display == 1)
                {
                    dead_alive_frames_counter_display--;
                }
            }
            #endregion
        }

        //GESTION DU SAUT
        private void Saut()
        {
            if (is_jumping) //est en train de sauter
            {
                if (!jump) //n'a pas atteint la hauteur maximale du saut
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
                    jump_speed = 11; 

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

            if (display_caracter) //permet de faire clignoter le personnage attaqué
            {
                if (frame_counter <= 50 && can_display_attack)
                {
                    if (Effect == SpriteEffects.FlipHorizontally)
                    {
                        //spriteBatch.Draw(BlankTexture, Hitbox, Color.Red);
                        //spriteBatch.Draw(BlankTexture, attaque, Color.Azure);
                        current_attack.draw((Hitbox.Width - current_attack.largeur_image) + Hitbox.X, Hitbox.Y - current_attack.frames.Height + Hitbox.Height, spriteBatch, current_attack.frames, Effect);   
                    }
                    
                    else
                    {
                        //spriteBatch.Draw(BlankTexture, Hitbox, Color.Red);
                        //spriteBatch.Draw(BlankTexture, attaque, Color.Azure);
                        current_attack.draw(Hitbox.X, Hitbox.Y - current_attack.frames.Height + Hitbox.Height, spriteBatch, current_attack.frames, Effect);
                    }
                }

                else
                {
                    if (vie != 0)
                    {
                        if (!win)
                        {
                            //spriteBatch.Draw(BlankTexture, Hitbox, Color.Red);
                            //spriteBatch.Draw(BlankTexture, attaque, Color.Azure);
                            spriteBatch.Draw(Joueur, Hitbox, new Rectangle((Frame - 1) * 95, 0, 95, 200), Color.White, 0f, Vector2.Zero, Effect, 0f);
                        }

                        else
                        {
                            texturecaracter.victoire.Draw(spriteBatch, Effect, Hitbox.X, Hitbox.Y, Hitbox.Width, dead_alive_frames_counter_display);
                        }
                    }

                    else
                    {
                        texturecaracter.mort.Draw(spriteBatch, Effect, Hitbox.X, Hitbox.Y, Hitbox.Width, dead_alive_frames_counter_display);
                    }
                }
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

                if (GameMain.LocalPlayer1.vie < 0)
                {
                    GameMain.LocalPlayer1.vie = 0;
                }
            }

            else if (player_number == 2)
            {
                x = Game1.graphics1.GraphicsDevice.Viewport.Width - BarWidth - 40;

                if (GameMain.LocalPlayer2.vie < 0)
                {
                    GameMain.LocalPlayer2.vie = 0;
                }
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

        public void PrepareAttack()
        {
            attack = true;

            if (Effect == SpriteEffects.FlipHorizontally)
            {
                if (current_attack == texturecaracter.attaque1)
                {
                    attaque.X = Hitbox.X + (Hitbox.Width - current_attack.largeur_image);
                }

                else if (current_attack == texturecaracter.attaque2)
                {
                    attaque.X = Hitbox.X + (Hitbox.Width - current_attack.largeur_image + 23);
                }

                else if (current_attack == texturecaracter.attaque3)
                {
                    attaque.X = Hitbox.X + (Hitbox.Width - current_attack.largeur_image + 10);
                }
            }

            else
                {
                    attaque.X = Hitbox.X + current_attack.x;
                }

            attaque.Y = Hitbox.Y + current_attack.y;
            attaque.Width = current_attack.width;
            attaque.Height = current_attack.height;
            can_attack = false;
            can_display_attack = true;
            current_attack.displayed_picture = 0;
        }
    }


    class Dead_victory
    {
        public Texture2D dead_frames;
        public int nb_frames, largeur_image;

        public Dead_victory(Texture2D dead_frames, int largeur_image, int nb_frames)
        {
            this.dead_frames = dead_frames;
            this.nb_frames = nb_frames;
            this.largeur_image = largeur_image;
        }

        public void Draw(SpriteBatch spriteBatch, SpriteEffects effect, int x, int y, int largeur_hitbox, int frame_displayed)
        {
            spriteBatch.Draw(dead_frames, new Rectangle((largeur_hitbox - largeur_image) + x, (200 - dead_frames.Height) + y, largeur_image, dead_frames.Height), new Rectangle(frame_displayed * largeur_image, 0, largeur_image, dead_frames.Height), Color.White, 0f, Vector2.Zero, effect, 0f);
        }
    }
}
