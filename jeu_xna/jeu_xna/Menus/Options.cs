using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace jeu_xna
{
    public class Options
    {
        public static MenuButton plus_musique, moins_musique, plus_bruitages, moins_bruitages, bouton_retour, bouton_commande;
        public static bool was_cliqued, is_mainmenu;
        public static SoundEffect test_volume_bruitage;
        public static float mediaplayer_volume, bruitage_volume;
        public static Texture2D background, volume, volume_musique, volume_bruitage;
        public static SpriteFont volume_musique_affichage, options;

        public static void Initialise()
        {
            was_cliqued = false;
        }

        public static void LoadContent(ContentManager Content)
        {
            plus_musique = new MenuButton(Content.Load<Texture2D>(@"Sprites\MainMenu\Options\+"), new Vector2(400, 230));
            moins_musique = new MenuButton(Content.Load<Texture2D>(@"Sprites\MainMenu\Options\-"), new Vector2(250, 230));
            plus_bruitages = new MenuButton(Content.Load<Texture2D>(@"Sprites\MainMenu\Options\+"), new Vector2(400, 300));
            moins_bruitages = new MenuButton(Content.Load<Texture2D>(@"Sprites\MainMenu\Options\-"), new Vector2(250, 300));
            bouton_retour = new MenuButton(Content.Load<Texture2D>(@"Sprites\MainMenu\Options\bouton_retour"), new Vector2(20, 520));
            bouton_commande = new MenuButton(Content.Load<Texture2D>(@"Sprites\MainMenu\Options\bouton_commandes"), new Vector2(20, 380));
            test_volume_bruitage = Content.Load<SoundEffect>(@"Sounds\Personnage\Personnage1\jump1");
            background = Content.Load<Texture2D>(@"Sprites\MainMenu\Options\background");
            volume = Content.Load<Texture2D>(@"Sprites\MainMenu\Options\volume");
            volume_musique = Content.Load<Texture2D>(@"Sprites\MainMenu\Options\volume_musique");
            volume_bruitage = Content.Load<Texture2D>(@"Sprites\MainMenu\Options\volume_bruitages");
            volume_musique_affichage = Content.Load<SpriteFont>(@"Sprites\MainMenu\Options\Font\volume_musique");
            options = Content.Load<SpriteFont>(@"Sprites\MainMenu\Options\Font\pause");
        }

        public static void Update()
        {
            mediaplayer_volume = MediaPlayer.Volume;
            bruitage_volume = SoundEffect.MasterVolume;

            if (bouton_retour.isClicked && !was_cliqued)
            {
                if (is_mainmenu)
                {
                    VarTemp.CurrentGameState = GameState.MainMenu;
                }

                else   
                {
                    VarTemp.CurrentGameState = GameState.Pause;
                }
            }

            else if (plus_musique.isClicked && !was_cliqued)
            {
                MediaPlayer.Volume = MediaPlayer.Volume + 0.01f;
                was_cliqued = true;
            }

            else if (moins_musique.isClicked && !was_cliqued)
            {
                MediaPlayer.Volume = MediaPlayer.Volume - 0.01f;
                was_cliqued = true;
            }

            else if (plus_bruitages.isClicked && !was_cliqued)
            {
                if (SoundEffect.MasterVolume + 0.01f > 1)
                {
                    SoundEffect.MasterVolume = 1;
                }

                else
                {
                    SoundEffect.MasterVolume = SoundEffect.MasterVolume + 0.01f;
                }
                test_volume_bruitage.Play();
                was_cliqued = true;
            }

            else if (moins_bruitages.isClicked && !was_cliqued)
            {
                if (SoundEffect.MasterVolume - 0.01f < 0)
                {
                    SoundEffect.MasterVolume = 0;
                }

                else
                {
                    SoundEffect.MasterVolume = SoundEffect.MasterVolume - 0.01f;
                }
                test_volume_bruitage.Play();
                was_cliqued = true;
            }

            else if (bouton_commande.isClicked)
            {
                VarTemp.CurrentGameState = GameState.Commandes;
            }

            else if (MainMenu.mouse.LeftButton == ButtonState.Released && MainMenu.mouse.RightButton == ButtonState.Released)
            {
                was_cliqued = false;
            }
        }

        public static void Draw(SpriteBatch spriteBatch, ContentManager Content)
        {
            int display_mediaplayer_volume = (int)(mediaplayer_volume * 100);
            int display_bruitages_volume = (int)(bruitage_volume * 100);
            spriteBatch.Draw(background, Vector2.Zero, Color.White);
            spriteBatch.Draw(volume, new Vector2(30, 160), Color.White);
            spriteBatch.Draw(volume_musique, new Vector2(18, 230), Color.White);
            spriteBatch.Draw(volume_bruitage, new Vector2(22, 290), Color.White);
            spriteBatch.DrawString(volume_musique_affichage, string.Format(Convert.ToString(display_mediaplayer_volume)), new Vector2(320, 225), Color.Black);
            spriteBatch.DrawString(volume_musique_affichage, string.Format(Convert.ToString(display_bruitages_volume)), new Vector2(320, 295), Color.Black);
            spriteBatch.DrawString(options, "Options", new Vector2(320, 20), Color.Black);
            plus_musique.Draw(spriteBatch);
            moins_musique.Draw(spriteBatch);
            plus_bruitages.Draw(spriteBatch);
            moins_bruitages.Draw(spriteBatch);
            bouton_retour.Draw(spriteBatch);
            bouton_commande.Draw(spriteBatch);
        }
    }
}
