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
        static Texture2D BlankTexture;
        public static RectangleMaker caracter1, caracter2, caracter3;
        public static int player;
        public static MenuButton terrain, retour;
        public static bool was_cliqued;
        public static SpriteFont name_caracter;

        public static void Initialise()
        {
            player = 1;
            was_cliqued = false;
        }

        public static void LoadContent(ContentManager Content)
        {
            BlankTexture = Content.Load<Texture2D>(@"Sprites\Personnages\BlankTexture");
            caracter1 = new RectangleMaker((MainMenu.graphics.GraphicsDevice.Viewport.Width - (2 * 200) - 100) / 2, ((MainMenu.graphics.GraphicsDevice.Viewport.Height - 100) / 2) - 50, Content.Load<Texture2D>(@"Sprites\Personnages\Personnage1\identité1"), BlankTexture, 100, 100, "Kaktus");
            caracter2 = new RectangleMaker(caracter1.x + 200, caracter1.y, Content.Load<Texture2D>(@"Sprites\Personnages\Personnage2\identité2"), BlankTexture, 100, 100, "Brutus");
            caracter3 = new RectangleMaker(caracter2.x + 200, caracter1.y, Content.Load<Texture2D>(@"Sprites\Personnages\Personnage3\identité3"), BlankTexture, 100, 100, "Ballus");
            terrain = new MenuButton(Content.Load<Texture2D>(@"Sprites\MainMenu\bouton_terrain"), new Vector2(600, 500));
            retour = new MenuButton(Content.Load<Texture2D>(@"Sprites\MainMenu\Options\bouton_retour"), new Vector2(50, 500));
            name_caracter = Content.Load<SpriteFont>("nom_personnage");
        }

        public static void Update()
        {
            if (player == 1)
            {
                if (caracter1.is_clicked && !was_cliqued)
                {
                    GameMain.personnage_choisi1 = 0;
                    player = 2;
                    was_cliqued = true;
                }

                else if (caracter2.is_clicked && !was_cliqued)
                {
                    GameMain.personnage_choisi1 = 1;
                    player = 2;
                    was_cliqued = true;
                }

                else if (caracter3.is_clicked && !was_cliqued)
                {
                    GameMain.personnage_choisi1 = 2;
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
                    was_cliqued = true;
                }

                else if (caracter2.is_clicked && !was_cliqued)
                {
                    GameMain.personnage_choisi2 = 1;
                    player = 3;
                    was_cliqued = true;
                }

                else if (caracter3.is_clicked && !was_cliqued)
                {
                    GameMain.personnage_choisi2 = 2;
                    player = 3;
                    was_cliqued = true;
                }

                else if (retour.isClicked && !was_cliqued)
                {
                    player = 1;
                    was_cliqued = true;
                }
            }

            else if (player == 3)
            {
                if (terrain.isClicked && !was_cliqued)
                {
                    VarTemp.CurrentGameState = GameState.ChoiceMenuBattlefield;
                    was_cliqued = true;
                }

                else if (retour.isClicked && !was_cliqued)
                {
                    player = 2;
                    was_cliqued = true;
                }
            }

            if (MainMenu.mouse.LeftButton == ButtonState.Released && MainMenu.mouse.RightButton == ButtonState.Released)
            {
                was_cliqued = false;
            }
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            Menu.Draw(spriteBatch);
            caracter1.draw(spriteBatch, name_caracter);
            caracter2.draw(spriteBatch, name_caracter);
            caracter3.draw(spriteBatch, name_caracter);

            if (player == 2)
            {
                retour.Draw(spriteBatch);
            }

            if (player < 3)
            {
                spriteBatch.DrawString(Options.options, "Choix du joueur " + player, new Vector2((MainMenu.graphics.GraphicsDevice.Viewport.Width - Options.options.MeasureString("Choix du joueur " + player).Length()) / 2, 0), Color.White);
            }

            else if (player == 3)
            {
                terrain.Draw(spriteBatch);
                retour.Draw(spriteBatch);
            }
        }
    }

    public class RectangleMaker
    {
        int BoarderOffSet;
        public int x, y, width, heigh;
        public bool is_clicked;
        Texture2D caracter, BlankTexture;
        Rectangle rectangle, background;
        public Rectangle RecBoarder;
        Color boarder;
        string name;

        public RectangleMaker(int x, int y, Texture2D caracter, Texture2D BlankTexture, int width, int heigh, string name)
        {
            this.x = x;
            this.y = y;
            is_clicked = false;
            this.caracter = caracter;
            this.BlankTexture = BlankTexture;
            boarder = Color.Gray;
            this.name = name;
            this.width = width;
            this.heigh = heigh;

            BoarderOffSet = 2;

            rectangle = new Rectangle(x, y, width, heigh);
            background = new Rectangle(x, y, width, heigh); 
            RecBoarder = new Rectangle(x - BoarderOffSet, y - BoarderOffSet, width + (BoarderOffSet * 2), heigh + (BoarderOffSet * 2));
        }

        public void Update(MouseState mouse)
        {
            Rectangle mouseRectangle = new Rectangle(MainMenu.mouse.X, MainMenu.mouse.Y, 1, 1);

            if (mouseRectangle.Intersects(RecBoarder)) 
            {
                if (ChoiceMenuCaracter.player == 1) //le bouton devient vert
                {
                    boarder = new Color(0, 255, 0);
                }

                else if (ChoiceMenuCaracter.player == 2)
                {
                    boarder = new Color(255, 0, 0);
                }

                else if (ChoiceMenuCaracter.player == 3)
                {
                    boarder = new Color(185, 122, 87);
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

            else
            {
                boarder = new Color(195, 195, 195);
                is_clicked = false;
            }
        }

        public void draw(SpriteBatch spriteBatch, SpriteFont name_caracter)
        {
            spriteBatch.Draw(BlankTexture, RecBoarder, boarder);
            spriteBatch.Draw(BlankTexture, background, Color.Gray);
            spriteBatch.Draw(caracter, rectangle, Color.White);
            spriteBatch.DrawString(name_caracter, name, new Vector2((Math.Abs((width - name_caracter.MeasureString(name).Length())) / 2) + x, y + heigh + 10), Color.White);
        }
    }
}
