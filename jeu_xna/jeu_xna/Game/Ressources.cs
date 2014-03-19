using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace jeu_xna
{
    class Ressources
    {
        // STATIC FIELDS
        public static Texture2D Fond, personnage;
        public static SoundEffect Pas, Jump, jump_end;
        public static Song Musique;
        public static SoundEffectInstance jump_end_sound;

        // LOAD CONTENT
        public static void LoadContent_Sprites(ContentManager Content)
        {
            Fond = Content.Load<Texture2D>(@"Sprites\Maps\map1");
            personnage = Content.Load<Texture2D>(@"Sprites\Personnages\personnage1");
        }

        public static void LoadContent_Sounds(ContentManager Content)
        {
            SoundEffect.MasterVolume = Options.bruitage_volume;
            Musique = Content.Load<Song>(@"Sounds\Musique\Son Game 1"); //Musique d'ambiance

            Pas = Content.Load<SoundEffect>(@"Sounds\Personnage\step1");
            Jump = Content.Load<SoundEffect>(@"Sounds\Personnage\jump1");
            jump_end = Content.Load<SoundEffect>(@"Sounds\Personnage\jump_end");
            jump_end_sound = jump_end.CreateInstance();
            jump_end_sound.Volume = 0.3f;

            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = Options.mediaplayer_volume;
            MediaPlayer.Play(Ressources.Musique); 
        }
    }
}
