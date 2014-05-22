using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace jeu_xna
{
    class LocalNetworkChoice
    {
        public static MenuButton local, network, retour;
        public static SpriteFont font;

        public static void LoadContent(ContentManager content)
        {
            local = new MenuButton(content.Load<Texture2D>(@"Sprites\MainMenu\localnetwork\bouton_local"), new Vector2(300, 200));
            network = new MenuButton(content.Load<Texture2D>(@"Sprites\MainMenu\localnetwork\bouton_reseau"), new Vector2(300, 300));
            font = content.Load<SpriteFont>(@"Sprites\MainMenu\Options\Font\pause");
            retour = new MenuButton(content.Load<Texture2D>(@"Sprites\MainMenu\Options\bouton_retour"), new Vector2(50, 500));
        }

        public static void Update()
        {
            MainMenu.mouse = Mouse.GetState();

            if (local.isClicked && !ChoiceMenuCaracter.was_cliqued)
            {
                VarTemp.CurrentGameState = GameState.ChoiceMenuCaracter;
                ChoiceMenuCaracter.was_cliqued = true;
            }

            else if (retour.isClicked && !ChoiceMenuCaracter.was_cliqued)
            {
                VarTemp.CurrentGameState = GameState.MainMenu;
                ChoiceMenuCaracter.was_cliqued = true;
            }

            if (MainMenu.mouse.LeftButton == ButtonState.Released && MainMenu.mouse.RightButton == ButtonState.Released)
            {
                ChoiceMenuCaracter.was_cliqued = false;
            }
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(MainMenu.background, Vector2.Zero, Color.White);
            local.Draw(spriteBatch);
            network.Draw(spriteBatch);
            retour.Draw(spriteBatch);
            spriteBatch.DrawString(font, "Choix du mode de jeu", new Vector2(170, 20), Color.Black);
        }
    }
}
