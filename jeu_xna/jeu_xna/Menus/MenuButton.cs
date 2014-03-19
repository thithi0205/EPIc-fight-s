using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace jeu_xna
{
    public class MenuButton
    {
        Texture2D texture;
        public Vector2 position; //position du boutton
        Rectangle rectangle; //rectangle contenant le boutton

        Color couleur = new Color(195, 195, 195);

        public MenuButton(Texture2D newTexture, Vector2 newPosition)
        {
            texture = newTexture;
            position = newPosition;
        }

        public bool isClicked;

        public void Update(MouseState mouse)
        {
            rectangle = new Rectangle((int)position.X, (int)position.Y, (int)texture.Width, (int)texture.Height);

            Rectangle mouseRectangle = new Rectangle(mouse.X, mouse.Y, 1, 1);

            if (mouseRectangle.Intersects(rectangle)) //le button devient vert
            {
                couleur.R = 0;
                couleur.G = 255;
                couleur.B = 0;

                if (mouse.LeftButton == ButtonState.Pressed)
                {
                    isClicked = true;
                }

                else
                {
                    isClicked = false;
                }
            }

            else //le button devient gris
            {
                couleur.R = 195;
                couleur.G = 195;
                couleur.B = 195;
                //isClicked = false;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle, couleur);
        }
    }
}
