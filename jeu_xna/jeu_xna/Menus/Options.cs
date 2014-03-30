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
        public static MenuButton plus_musique, moins_musique, plus_bruitages, moins_bruitages, bouton_retour;
        public static bool was_cliqued, is_mainmenu;
        public static SoundEffect test_volume_bruitage;
        public static float mediaplayer_volume, bruitage_volume;
        public static Texture2D background, volume, volume_musique, volume_bruitage;
        public static SpriteFont volume_musique_affichage;

        public static void Initialise()
        {
            was_cliqued = false;
        }

        public static void LoadContent(ContentManager Content)
        {
            plus_musique = new MenuButton(Content.Load<Texture2D>(@"Sprites\MainMenu\Options\+"), new Vector2(400, 100));
            moins_musique = new MenuButton(Content.Load<Texture2D>(@"Sprites\MainMenu\Options\-"), new Vector2(250, 100));
            plus_bruitages = new MenuButton(Content.Load<Texture2D>(@"Sprites\MainMenu\Options\+"), new Vector2(400, 170));
            moins_bruitages = new MenuButton(Content.Load<Texture2D>(@"Sprites\MainMenu\Options\-"), new Vector2(250, 170));
            bouton_retour = new MenuButton(Content.Load<Texture2D>(@"Sprites\MainMenu\Options\bouton_retour"), new Vector2(20, 520));
            test_volume_bruitage = Content.Load<SoundEffect>(@"Sounds\Personnage\jump1");
            background = Content.Load<Texture2D>(@"Sprites\MainMenu\Options\background");
            volume = Content.Load<Texture2D>(@"Sprites\MainMenu\Options\volume");
            volume_musique = Content.Load<Texture2D>(@"Sprites\MainMenu\Options\volume_musique");
            volume_bruitage = Content.Load<Texture2D>(@"Sprites\MainMenu\Options\volume_bruitages");
            volume_musique_affichage = Content.Load<SpriteFont>(@"Sprites\MainMenu\Options\Font\volume_musique");
        }

        public static void Update()
        {
            mediaplayer_volume = MediaPlayer.Volume;
            bruitage_volume = SoundEffect.MasterVolume;

            if (bouton_retour.isClicked)
            {
                if (is_mainmenu)
                {
                    MainMenu.CurrentGameState = GameState.MainMenu;
                }

                else   
                {
                    MainMenu.CurrentGameState = GameState.Pause;
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
            spriteBatch.Draw(volume, new Vector2(30, 30), Color.White);
            spriteBatch.Draw(volume_musique, new Vector2(18, 100), Color.White);
            spriteBatch.Draw(volume_bruitage, new Vector2(22, 170), Color.White);
            spriteBatch.DrawString(volume_musique_affichage, string.Format(Convert.ToString(display_mediaplayer_volume)), new Vector2(320, 95), Color.Black);
            spriteBatch.DrawString(volume_musique_affichage, string.Format(Convert.ToString(display_bruitages_volume)), new Vector2(320, 165), Color.Black);
            plus_musique.Draw(spriteBatch);
            moins_musique.Draw(spriteBatch);
            plus_bruitages.Draw(spriteBatch);
            moins_bruitages.Draw(spriteBatch);
            bouton_retour.Draw(spriteBatch);
        }
    }
}
