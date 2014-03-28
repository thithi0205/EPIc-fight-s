﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace jeu_xna
{
    public class ChoiceMenuCaracter
    {
        static Texture2D BlankTexture, background;
        public static RectangleMaker caracter1;
        public static int player;
        static SpriteFont choix;
        static MenuButton jouer;
        static bool was_cliqued;

        public static void Initialise()
        {
            player = 1;
            was_cliqued = false;
        }

        public static void LoadContent(ContentManager Content)
        {
            BlankTexture = Content.Load<Texture2D>(@"Sprites\Personnages\BlankTexture");
            caracter1 = new RectangleMaker(50, 100, Content.Load<Texture2D>(@"Sprites\Personnages\identité1"), BlankTexture);
            background = Content.Load<Texture2D>(@"Sprites\MainMenu\Options\background");
            choix = Content.Load<SpriteFont>("choix");
            jouer = new MenuButton(Content.Load<Texture2D>(@"Sprites\MainMenu\button_jouer"), new Vector2(500, 500));
        }

        public static void Update()
        {
            Console.WriteLine("player : " + player);
            //mouse = Mouse.GetState();
            caracter1.Update(MainMenu.mouse);

            if (player == 1)
            {
                if (caracter1.is_clicked && !was_cliqued)
                {
                    GameMain.personnage_choisi1 = 0;
                    player = 2;
                    was_cliqued = true;
                }
            }

            else if (player == 2)
            {
                if (caracter1.is_clicked && !was_cliqued)
                {
                    GameMain.personnage_choisi2 = 0;
                    player = 3;
                }

                else if (MainMenu.mouse.LeftButton == ButtonState.Released && MainMenu.mouse.RightButton == ButtonState.Released)
                {
                    was_cliqued = false;
                }
            }

            else if (player == 3)
            {
                jouer.Update(MainMenu.mouse);

                if (jouer.isClicked)
                {
                    MainMenu.CurrentGameState = GameState.Playing;
                }
            }
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background, Vector2.Zero, Color.White);
            caracter1.draw(spriteBatch);
            jouer.Draw(spriteBatch);
            if (player < 3)
            {
                spriteBatch.DrawString(choix, "Choix du joueur " + player, new Vector2(300, 30), Color.Black);
            }

            else if (player == 3)
            {
                jouer.Draw(spriteBatch);
            }
        }
    }

    public class RectangleMaker
    {
        int x, y, BoarderOffSet;
        public bool is_clicked;
        Texture2D caracter, BlankTexture;
        Rectangle rectangle, RecBoarder, background;
        Color boarder;
        //MouseState mouse;

        public RectangleMaker(int x, int y, Texture2D caracter, Texture2D BlankTexture)
        {
            this.x = x;
            this.y = y;
            is_clicked = false;
            this.caracter = caracter;
            this.BlankTexture = BlankTexture;
            boarder = Color.Gray;

            BoarderOffSet = 2;

            rectangle = new Rectangle(x, y, 100, 100);
            background = new Rectangle(x, y, 100, 100); 
            RecBoarder = new Rectangle(x - BoarderOffSet, y - BoarderOffSet, 100 + (BoarderOffSet * 2), 100 + (BoarderOffSet * 2));
        }

        public void Update(MouseState mouse)
        {
            //mouse = Mouse.GetState();
            Rectangle mouseRectangle = new Rectangle(MainMenu.mouse.X, MainMenu.mouse.Y, 1, 1);

            if (mouseRectangle.Intersects(RecBoarder)) 
            {
                if (ChoiceMenuCaracter.player == 1) //le bouton devient vert
                {
                    boarder = new Color(0, 255, 0);
                    Console.WriteLine("blablabla\n");
                }

                else if (ChoiceMenuCaracter.player == 2)
                {
                    boarder = new Color(255, 0, 0);
                }

                if (mouse.LeftButton == ButtonState.Pressed)
                {
                    is_clicked = true;
                }

                else
                {
                    is_clicked = false;
                }
            }

            else //le button devient gris
            {
                boarder = new Color(195, 195, 195);
                is_clicked = false;
            }
        }

        public void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(BlankTexture, RecBoarder, boarder);
            spriteBatch.Draw(BlankTexture, background, Color.Gray);
            spriteBatch.Draw(caracter, rectangle, Color.White);
        }
    }
}
