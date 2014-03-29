﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace jeu_xna
{
    class ChoiceMenuBattlefield
    {
        static Texture2D blanck, background;
        static RectangleMaker terrain1;
        static MenuButton jouer;
        static bool choisi = false;
        static SpriteFont display;

        public static void Initialise() 
        {
            ChoiceMenuCaracter.was_cliqued = false;
        }

        public static void LoadContent(ContentManager Content)
        {
            blanck = Content.Load<Texture2D>(@"Sprites\Personnages\BlankTexture");
            terrain1 = new RectangleMaker(50, 100, Content.Load<Texture2D>(@"Sprites\Maps\map1"), blanck, 200, 120);
            jouer = new MenuButton(Content.Load<Texture2D>(@"Sprites\MainMenu\button_jouer"), new Vector2(500, 500));
            background = Content.Load<Texture2D>(@"Sprites\MainMenu\Options\background");
            display = Content.Load<SpriteFont>("choix");
        }

        public static void Update()
        {
            if (terrain1.is_clicked)
            {
                GameMain.terrain_choisi = 0;
                choisi = true;
            }

            if (choisi)
            {
                if (jouer.isClicked)
                {
                    MainMenu.CurrentGameState = GameState.Playing;
                }

                jouer.Update(MainMenu.mouse);
            }

            terrain1.Update(MainMenu.mouse);
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background, Vector2.Zero, Color.White);
            spriteBatch.DrawString(display, "Choix du terrain de combat", new Vector2(230, 30), Color.Black);
            terrain1.draw(spriteBatch);
            jouer.Draw(spriteBatch);
        }
    }
}
