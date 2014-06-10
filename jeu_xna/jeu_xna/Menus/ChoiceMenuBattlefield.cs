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
        static Texture2D blanck;
        public static RectangleMaker terrain1, terrain2, terrain3, terrain4, terrain5;
        public static MenuButton jouer;
        public static bool choisi = false;

        public static void Initialise() 
        {
            ChoiceMenuCaracter.was_cliqued = false;
        }

        public static void LoadContent(ContentManager Content)
        {
            blanck = Content.Load<Texture2D>(@"Sprites\Personnages\BlankTexture");
            terrain1 = new RectangleMaker((MainMenu.graphics.GraphicsDevice.Viewport.Width - 640) / 2, 100, Content.Load<Texture2D>(@"Sprites\Maps\map1"), blanck, 200, 120);
            terrain2 = new RectangleMaker(terrain1.x + 220, 100, Content.Load<Texture2D>(@"Sprites\Maps\map2"), blanck, 200, 120);
            terrain3 = new RectangleMaker(terrain2.x + 220, 100, Content.Load<Texture2D>(@"Sprites\Maps\map3"), blanck, 200, 120);
            terrain4 = new RectangleMaker((MainMenu.graphics.GraphicsDevice.Viewport.Width - 420) / 2, terrain1.RecBoarder.Width + 40, Content.Load<Texture2D>(@"Sprites\Maps\map4"), blanck, 200, 120);
            terrain5 = new RectangleMaker(terrain4.x + 220, terrain1.RecBoarder.Width + 40, Content.Load<Texture2D>(@"Sprites\Maps\map5"), blanck, 200, 120);
            jouer = new MenuButton(Content.Load<Texture2D>(@"Sprites\MainMenu\bouton_jouer"), new Vector2(600, 500));
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

            else if (terrain3.is_clicked && !ChoiceMenuCaracter.was_cliqued)
            {
                GameMain.terrain_choisi = 2;
                choisi = true;
                ChoiceMenuCaracter.was_cliqued = true;
            }

            else if (terrain4.is_clicked && !ChoiceMenuCaracter.was_cliqued)
            {
                GameMain.terrain_choisi = 3;
                choisi = true;
                ChoiceMenuCaracter.was_cliqued = true;
            }

            else if (terrain5.is_clicked && !ChoiceMenuCaracter.was_cliqued)
            {
                GameMain.terrain_choisi = 4;
                choisi = true;
                ChoiceMenuCaracter.was_cliqued = true;
            }

            if (ChoiceMenuCaracter.retour.isClicked && !ChoiceMenuCaracter.was_cliqued)
            {
                choisi = false;
                ChoiceMenuCaracter.was_cliqued = true;
                ChoiceMenuCaracter.player = 2;
                VarTemp.CurrentGameState = GameState.ChoiceMenuCaracter;
            }

            if (MainMenu.mouse.LeftButton == ButtonState.Released && MainMenu.mouse.RightButton == ButtonState.Released)
            {
                ChoiceMenuCaracter.was_cliqued = false;
            }

            if (choisi)
            {
                if (jouer.isClicked)
                {
                    VarTemp.CurrentGameState = GameState.Playing;
                    choisi = false;
                }

            }  
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            Menu.Draw(spriteBatch);
            spriteBatch.DrawString(Options.options, "Choix du terrain de combat", new Vector2((MainMenu.graphics.GraphicsDevice.Viewport.Width - Options.options.MeasureString("Choix du terrain de combat").Length()) / 2, 0), Color.White);
            terrain1.draw(spriteBatch);
            terrain2.draw(spriteBatch);
            terrain3.draw(spriteBatch);
            terrain4.draw(spriteBatch);
            terrain5.draw(spriteBatch);
            ChoiceMenuCaracter.retour.Draw(spriteBatch);

            if (choisi)
            {
                jouer.Draw(spriteBatch);
            }
        }
    }
}
