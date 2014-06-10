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
        public static Texture2D[] fields = new Texture2D[5];
        public static SoundEffect Pas, jump_end;
        public static Song Musique;
        public static SoundEffectInstance jump_end_sound;
        public static TextureCaracter[] caracters = new TextureCaracter[2];

        // LOAD CONTENT
        public static void LoadContent_Sprites(ContentManager Content)
        {
            caracters[0] = new TextureCaracter(95, 4, Content.Load<Texture2D>(@"Sprites\Personnages\Personnage1\personnage1"), Content.Load<Texture2D>(@"Sprites\Personnages\Personnage1\identité1"), Content.Load<SoundEffect>(@"Sounds\Personnage\Personnage1\jump1"), new Attack(1, 1, 10, Content.Load<Texture2D>(@"Sprites\Personnages\Personnage1\attaque_1"), 45, 114, 70, 100, 117, 1, Content.Load<SoundEffect>(@"Sounds\Personnage\Personnage1\attaque1")), new Attack(3, 1, 20, Content.Load<Texture2D>(@"Sprites\Personnages\Personnage1\attaque_2"), 50, 45, 90, 30, 167, 3, Content.Load<SoundEffect>(@"Sounds\Personnage\Personnage1\attaque2")), new Attack(5, 1, 35, Content.Load<Texture2D>(@"Sprites\Personnages\Personnage1\attaque_3"), 115, 100, 100, 100, 223, 4, Content.Load<SoundEffect>(@"Sounds\Personnage\Personnage1\attaque3")), new Attack(3, 10, 20, Content.Load<Texture2D>(@"Sprites\Personnages\Personnage1\attaque_4"), 45, 114, 70, 10, 166, 2, Content.Load<SoundEffect>(@"Sounds\Personnage\Personnage1\attaque4")), new Dead_victory(Content.Load<Texture2D>(@"Sprites\Personnages\Personnage1\mort"), 228, 3, Content.Load<SoundEffect>(@"Sounds\Personnage\Personnage1\death1")), new Dead_victory(Content.Load<Texture2D>(@"Sprites\Personnages\Personnage1\victoire"), 93, 2, Content.Load<SoundEffect>(@"Sounds\Personnage\Personnage1\death1")), Content.Load<Texture2D>(@"Sprites\Personnages\Personnage1\accroupi"), 148, Content.Load<SoundEffect>(@"Sounds\Personnage\Personnage1\pain"));
            caracters[1] = new TextureCaracter(105, 4, Content.Load<Texture2D>(@"Sprites\Personnages\Personnage2\personnage_2"), Content.Load<Texture2D>(@"Sprites\Personnages\Personnage2\identité2"), Content.Load<SoundEffect>(@"Sounds\Personnage\Personnage2\jump"), new Attack(2, 1, 10, Content.Load<Texture2D>(@"Sprites\Personnages\Personnage2\attaque_1"), 1, 1, 1, 1, 183, 2, Content.Load<SoundEffect>(@"Sounds\Personnage\Personnage2\attaque1")), new Attack(2, 1, 20, Content.Load<Texture2D>(@"Sprites\Personnages\Personnage2\attaque_2"), 1, 1, 1, 1, 191, 1, Content.Load<SoundEffect>(@"Sounds\Personnage\Personnage2\attaque2")), new Attack(3, 1, 35, Content.Load<Texture2D>(@"Sprites\Personnages\Personnage2\attaque_3"), 1, 1, 1, 1, 200, 2, Content.Load<SoundEffect>(@"Sounds\Personnage\Personnage2\attaque3")), new Attack(4, 10, 20, Content.Load<Texture2D>(@"Sprites\Personnages\Personnage2\attaque_4"), 1, 1, 1, 1, 194, 4, Content.Load<SoundEffect>(@"Sounds\Personnage\Personnage2\attaque4")), new Dead_victory(Content.Load<Texture2D>(@"Sprites\Personnages\Personnage2\mort"), 176, 4, Content.Load<SoundEffect>(@"Sounds\Personnage\Personnage2\death")), new Dead_victory(Content.Load<Texture2D>(@"Sprites\Personnages\Personnage2\victoire"), 107, 2, Content.Load<SoundEffect>(@"Sounds\Personnage\Personnage2\death")), Content.Load<Texture2D>(@"Sprites\Personnages\Personnage2\accroupi"), 141, Content.Load<SoundEffect>(@"Sounds\Personnage\Personnage2\pain"));    
            //attaque3 = attaque qui a besoin de toute l'énergie pour s'activer 

            fields[0] = Content.Load<Texture2D>(@"Sprites\Maps\map1");
            fields[1] = Content.Load<Texture2D>(@"Sprites\Maps\map2");
            fields[2] = Content.Load<Texture2D>(@"Sprites\Maps\map3");
            fields[3] = Content.Load<Texture2D>(@"Sprites\Maps\map4");
            fields[4] = Content.Load<Texture2D>(@"Sprites\Maps\map5");
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
        public Texture2D personnage, identity, accroupi;
        public SoundEffect jump, pain;
        public Attack attaque1, attaque2, attaque3, attaque4;
        public Dead_victory mort, victoire;
        public int accroupi_hauteur, largeur, nb_images;

        public TextureCaracter(int largeur, int nb_images, Texture2D personnage, Texture2D identity, SoundEffect jump, Attack attaque1, Attack attaque2, Attack attaque3, Attack attaque4, Dead_victory mort, Dead_victory victoire, Texture2D accroupi, int accroupi_hauteur, SoundEffect pain)
        {
            this.personnage = personnage;
            this.identity = identity;
            this.jump = jump;
            this.attaque1 = attaque1;
            this.attaque2 = attaque2;
            this.attaque3 = attaque3;
            this.attaque4 = attaque4;
            this.mort = mort;
            this.victoire = victoire;
            this.accroupi = accroupi;
            this.accroupi_hauteur = accroupi_hauteur;
            this.largeur = largeur;
            this.nb_images = nb_images;
            this.pain = pain;
        }
    }
}
