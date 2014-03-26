using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace jeu_xna.Menus
{
    class ChoiceMenuCaracter
    {
        static Texture2D BlankTexture;
        Texture2D personnage1;
        static RectangleMaker caracter1;
        static int player;

        public static void Initialise()
        {
            player = 1;
        }

        public static void LoadContent(ContentManager Content)
        {
            BlankTexture = Content.Load<Texture2D>(@"Sprites\Personnages\BlankTexture");
            caracter1 = new RectangleMaker(50, 100, Content.Load<Texture2D>(@"Sprites\Personnages\identité1"), BlankTexture);
        }

        public static void Update()
        {
            if (caracter1.is_clicked)
            {
                MainMenu.personnage_choisi = 1;
            }
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            caracter1.draw(spriteBatch);
        }
    }

    class RectangleMaker
    {
        int x, y, BoarderOffSet;
        public bool is_clicked;
        Texture2D caracter, BlankTexture;
        Rectangle rectangle, RecBoarder, background;
        Color boarder;

        public RectangleMaker(int x, int y, Texture2D caracter, Texture2D BlankTexture)
        {
            this.x = x;
            this.y = y;
            is_clicked = false;
            this.caracter = caracter;
            this.BlankTexture = BlankTexture;

            BoarderOffSet = 2;

            rectangle = new Rectangle(x, y, 100, 100);
            background = new Rectangle(x, y, 100, 100); 
            RecBoarder = new Rectangle(x, y, 100 + (BoarderOffSet * 2), 100 + (BoarderOffSet * 2));
        }

        public void Update(MouseState mouse, int player)
        {
            Rectangle mouseRectangle = new Rectangle(mouse.X, mouse.Y, 1, 1);

            if (mouseRectangle.Intersects(RecBoarder)) 
            {
                if (player == 1) //le button devient vert
                {
                    boarder.R = 0;
                    boarder.G = 255;
                    boarder.B = 0;
                }

                else if (player == 2)
                {
                    boarder.R = 255;
                    boarder.G = 0;
                    boarder.B = 0;
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
                boarder.R = 195;
                boarder.G = 195;
                boarder.B = 195;
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
