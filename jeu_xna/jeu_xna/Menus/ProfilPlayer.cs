using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace jeu_xna
{
    class ProfilPlayer
    {
        public static MenuButton retour;
        public static SpriteFont profil_writer;

        public static void LoadContent(ContentManager Content)
        {
            retour = new MenuButton(Content.Load<Texture2D>(@"Sprites\MainMenu\Options\bouton_retour"), new Vector2(50, 500));
        }

        public static void Update(MouseState mouse)
        {
            if (retour.isClicked && !ChoiceMenuCaracter.was_cliqued)
            {
                VarTemp.CurrentGameState = GameState.MainMenu;
                ChoiceMenuCaracter.was_cliqued = true;
            }

            else if (mouse.LeftButton == ButtonState.Released)
            {
                ChoiceMenuCaracter.was_cliqued = false;
            }
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            retour.Draw(spriteBatch);
        }
    }
}
