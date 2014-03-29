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
        public static Texture2D[] fields = new Texture2D[1];
        public static SoundEffect Pas, jump_end;
        public static Song Musique;
        public static SoundEffectInstance jump_end_sound;
        public static TextureCaracter[] caracters = new TextureCaracter[1];

        // LOAD CONTENT
        public static void LoadContent_Sprites(ContentManager Content)
        {
            caracters[0] = new TextureCaracter(Content.Load<Texture2D>(@"Sprites\Personnages\personnage1"), Content.Load<Texture2D>(@"Sprites\Personnages\identité1"), Content.Load<SoundEffect>(@"Sounds\Personnage\jump1"));

            fields[0] = Content.Load<Texture2D>(@"Sprites\Maps\map1");
        }

        public static void LoadContent_Sounds(ContentManager Content)
        {
            SoundEffect.MasterVolume = Options.bruitage_volume;
            Musique = Content.Load<Song>(@"Sounds\Musique\Son Game 1"); //Musique d'ambiance

            Pas = Content.Load<SoundEffect>(@"Sounds\Personnage\step1");
            jump_end = Content.Load<SoundEffect>(@"Sounds\Personnage\jump_end");
            jump_end_sound = jump_end.CreateInstance();
            jump_end_sound.Volume = 0.3f;

            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = Options.mediaplayer_volume;
            MediaPlayer.Play(Ressources.Musique); 
        }
    }


    class TextureCaracter
    {
        public Texture2D personnage, identity;//, attaques
        public SoundEffect jump;

        public TextureCaracter(Texture2D personnage, Texture2D identity, SoundEffect jump)
        {
            this.personnage = personnage;
            this.identity = identity;
            this.jump = jump;
        }
    }
}
