using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework;

namespace jeu_xna
{
    class Ressources
    {
        // STATIC FIELDS
        public static Texture2D[] fields = new Texture2D[2];
        public static SoundEffect Pas, jump_end;
        public static Song Musique;
        public static SoundEffectInstance jump_end_sound;
        public static TextureCaracter[] caracters = new TextureCaracter[1];

        // LOAD CONTENT
        public static void LoadContent_Sprites(ContentManager Content)
        {
            caracters[0] = new TextureCaracter(Content.Load<Texture2D>(@"Sprites\Personnages\Personnage1\personnage1"), Content.Load<Texture2D>(@"Sprites\Personnages\Personnage1\identité1"), Content.Load<SoundEffect>(@"Sounds\Personnage\Personnage1\jump1"), new Attack(1, 1, 10, Content.Load<Texture2D>(@"Sprites\Personnages\Personnage1\attaque_1"), 114, 114, 5, 5, 117, 1), new Attack(3, 1, 20, Content.Load<Texture2D>(@"Sprites\Personnages\Personnage1\attaque_2"), 125, 45, 5, 5, 167, 3), new Attack(5, 1, 35, Content.Load<Texture2D>(@"Sprites\Personnages\Personnage1\attaque_3"), 125, 190, 5, 5, 223, 4));

            fields[0] = Content.Load<Texture2D>(@"Sprites\Maps\map1");
            fields[1] = Content.Load<Texture2D>(@"Sprites\Maps\map2");
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
        public Texture2D personnage, identity;
        public SoundEffect jump;
        public Attack attaque1, attaque2, attaque3;

        public TextureCaracter(Texture2D personnage, Texture2D identity, SoundEffect jump, Attack attaque1, Attack attaque2, Attack attaque3)
        {
            this.personnage = personnage;
            this.identity = identity;
            this.jump = jump;
            this.attaque1 = attaque1;
            this.attaque2 = attaque2;
            this.attaque3 = attaque3;
        }
    }
}
