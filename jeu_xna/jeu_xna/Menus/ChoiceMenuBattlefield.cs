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
    class ChoiceMenuBattlefield
    {
        static Texture2D blanck, background;
        public static RectangleMaker terrain1, terrain2;
        public static MenuButton jouer, retour;
        public static bool choisi = false;
        static SpriteFont display;

        public static void Initialise() 
        {
            ChoiceMenuCaracter.was_cliqued = false;
        }

        public static void LoadContent(ContentManager Content)
        {
            blanck = Content.Load<Texture2D>(@"Sprites\Personnages\BlankTexture");
            terrain1 = new RectangleMaker(50, 100, Content.Load<Texture2D>(@"Sprites\Maps\map1"), blanck, 200, 120);
            terrain2 = new RectangleMaker(270, 100, Content.Load<Texture2D>(@"Sprites\Maps\map2"), blanck, 200, 120);
            jouer = new MenuButton(Content.Load<Texture2D>(@"Sprites\MainMenu\button_jouer"), new Vector2(600, 500));
            background = Content.Load<Texture2D>(@"Sprites\MainMenu\Options\background");
            display = Content.Load<SpriteFont>("choix");
            retour = new MenuButton(Content.Load<Texture2D>(@"Sprites\MainMenu\Options\bouton_retour"), new Vector2(50, 500));
        }

        public static void Update()
        {
            if (terrain1.is_clicked && !ChoiceMenuCaracter.was_cliqued)
            {
                GameMain.terrain_choisi = 0;
                choisi = true;
                ChoiceMenuCaracter.was_cliqued = true;
            }

            else if (terrain2.is_clicked && !ChoiceMenuCaracter.was_cliqued)
            {
                GameMain.terrain_choisi = 1;
                choisi = true;
                ChoiceMenuCaracter.was_cliqued = true;
            }

            if (retour.isClicked && !ChoiceMenuCaracter.was_cliqued)
            {
                choisi = false;
                ChoiceMenuCaracter.was_cliqued = true;
                ChoiceMenuCaracter.player = 2;
                State.CurrentGameState = GameState.ChoiceMenuCaracter;
            }

            if (MainMenu.mouse.LeftButton == ButtonState.Released && MainMenu.mouse.RightButton == ButtonState.Released)
            {
                ChoiceMenuCaracter.was_cliqued = false;
            }

            if (choisi)
            {
                if (jouer.isClicked)
                {
                    State.CurrentGameState = GameState.Playing;
                    choisi = false;
                }

            }  
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background, Vector2.Zero, Color.White);
            spriteBatch.DrawString(display, "Choix du terrain de combat", new Vector2(230, 30), Color.Black);
            terrain1.draw(spriteBatch);
            terrain2.draw(spriteBatch);
            retour.Draw(spriteBatch);

            if (choisi)
            {
                jouer.Draw(spriteBatch);
            }
        }
    }
}
