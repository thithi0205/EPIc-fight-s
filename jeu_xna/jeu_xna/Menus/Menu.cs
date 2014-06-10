using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace jeu_xna
{
    public class Menu 
    {
        static Texture2D background, banniere_couleur;

        public static void LoadContent(ContentManager Content)
        {
            background = Content.Load<Texture2D>(@"Sprites\Personnages\BlankTexture");
            banniere_couleur = Content.Load<Texture2D>(@"Sprites\MainMenu\banniere_couleur");
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            if (VarTemp.temp == GameState.MainMenu)
            {
                spriteBatch.Draw(background, new Rectangle(0, 0, MainMenu.graphics.GraphicsDevice.Viewport.Width, MainMenu.graphics.GraphicsDevice.Viewport.Height), new Color(146, 22, 22));
                spriteBatch.Draw(banniere_couleur, new Rectangle(0, 0, MainMenu.graphics.GraphicsDevice.Viewport.Width, MainMenu.epic_fight_s.Height), Color.White);
            }

            else if (VarTemp.temp == GameState.Playing)
            {
                spriteBatch.Draw(background, new Rectangle(0, 0, Game1.graphics1.GraphicsDevice.Viewport.Width, Game1.graphics1.GraphicsDevice.Viewport.Height), new Color(146, 22, 22));
                spriteBatch.Draw(banniere_couleur, new Rectangle(0, 0, Game1.graphics1.GraphicsDevice.Viewport.Width, MainMenu.epic_fight_s.Height), Color.White);
            }
        }
    }
}
