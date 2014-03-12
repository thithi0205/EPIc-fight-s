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
            Fond = Content.Load<Texture2D>(@"Sprites\image 142");
            personnage = Content.Load<Texture2D>(@"Sprites\personnage_jeu");
        }

        public static void LoadContent_Sounds(ContentManager Content)
        {
            SoundEffect.MasterVolume = 0.25f;
            Musique = Content.Load<Song>(@"Sounds\Son Game 1"); //Musique d'ambiance

            Pas = Content.Load<SoundEffect>(@"Sounds\step1");
            Jump = Content.Load<SoundEffect>(@"Sounds\jump1");
            jump_end = Content.Load<SoundEffect>(@"Sounds\jump_end");
            jump_end_sound = jump_end.CreateInstance();
            jump_end_sound.Volume = 0.3f;

            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = 50;
            MediaPlayer.Play(Ressources.Musique); 
        }
    }
}
