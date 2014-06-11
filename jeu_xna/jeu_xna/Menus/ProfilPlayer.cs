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
        public static SpriteFont profil_writer, profil_title;

        public static void LoadContent(ContentManager Content)
        {
            retour = new MenuButton(Content.Load<Texture2D>(@"Sprites\MainMenu\Options\bouton_retour"), new Vector2(50, 500));
            profil_writer = Content.Load<SpriteFont>("profil_writer");
            profil_title = Content.Load<SpriteFont>(@"Sprites\MainMenu\Options\Font\pause");
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
            Menu.Draw(spriteBatch);
            retour.Draw(spriteBatch);
            spriteBatch.DrawString(profil_title, "Profil", new Vector2((MainMenu.graphics.GraphicsDevice.Viewport.Width - profil_title.MeasureString("Profil").Length()) / 2, 0), Color.White);
            spriteBatch.DrawString(profil_writer, "PSEUDO DU JOUEUR : " + VarTemp.player + "\n\nADRESSE MAIL : " + VarTemp.mail + "\n\nNOMBRE DE VICTOIRES : " + VarTemp.nb_victory + "\n\nNOMBRE DE DEFAITES : " + VarTemp.nb_defaites, new Vector2(50, 100), Color.White);
        }
    }
}
