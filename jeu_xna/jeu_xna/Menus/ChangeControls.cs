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
    class ChangeControls
    {
        static SpriteFont player, commandes, touche, commande_display;
        static Texture2D background;
        public static Keys[] cles;
        public static Texture2D blank;
        public static ControlsButton left1, right1, up1, attack1_1, attack1_2, attack1_3, left2, right2, up2, attack2_1, attack2_2, attack2_3;
        public static SpriteFont font;

        public static void LoadContent(ContentManager Content)
        {
            player = Content.Load<SpriteFont>(@"Sprites\MainMenu\Options\Font\volume_musique");
            commandes = Content.Load<SpriteFont>(@"Sprites\MainMenu\Options\Font\pause");
            background = Content.Load<Texture2D>(@"Sprites\MainMenu\Options\background");

            blank = Content.Load<Texture2D>(@"Sprites\Personnages\BlankTexture");

            left1 = new ControlsButton(GameMain.LocalPlayer1.gauche, 200, 210, "left", 1);
            right1 = new ControlsButton(GameMain.LocalPlayer1.droite, 200, 260, "right", 1);
            up1 = new ControlsButton(GameMain.LocalPlayer1.saut, 200, 310, "up", 1);
            attack1_1 = new ControlsButton(GameMain.LocalPlayer1.attaque1, 200, 360, "attack1", 1);
            attack1_2 = new ControlsButton(GameMain.LocalPlayer1.attaque2, 200, 410, "attack2", 1);
            attack1_3 = new ControlsButton(GameMain.LocalPlayer1.attaque3, 200, 460, "attack3", 1);

            left2 = new ControlsButton(GameMain.LocalPlayer2.gauche, 670, 210, "left", 2);
            right2 = new ControlsButton(GameMain.LocalPlayer2.droite, 670, 260, "right", 2);
            up2 = new ControlsButton(GameMain.LocalPlayer2.saut, 670, 310, "up", 2);
            attack2_1 = new ControlsButton(GameMain.LocalPlayer2.attaque1, 670, 360, "attack1", 2);
            attack2_2 = new ControlsButton(GameMain.LocalPlayer2.attaque2, 670, 410, "attack2", 2);
            attack2_3 = new ControlsButton(GameMain.LocalPlayer2.attaque3, 670, 460, "attack3", 2);

            font = Content.Load<SpriteFont>("Controls");
            touche = Content.Load<SpriteFont>(@"Sprites\MainMenu\Options\Font\touches");
            commande_display = Content.Load<SpriteFont>(@"Sprites\MainMenu\Options\Font\pause");
            cles = new Keys[12];

            Initialise_keys_board(cles);
        }

        public static void Update(KeyboardState keyboard)
        {
            if (Options.bouton_retour.isClicked)
            {
                VarTemp.CurrentGameState = GameState.Options;
                Options.was_cliqued = true;
            }
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background, Vector2.Zero, Color.White);
            spriteBatch.DrawString(player, GameMain.LocalPlayer1.name + " :", new Vector2(130, 130), Color.Black);
            spriteBatch.DrawString(player, GameMain.LocalPlayer2.name + " :", new Vector2(600, 130), Color.Black);
            spriteBatch.DrawString(touche, "gauche :", new Vector2(20, 200), Color.Black);
            spriteBatch.DrawString(touche, "droite :", new Vector2(20, 250), Color.Black);
            spriteBatch.DrawString(touche, "saut :", new Vector2(20, 300), Color.Black);
            spriteBatch.DrawString(touche, "attaque 1 :", new Vector2(20, 350), Color.Black);
            spriteBatch.DrawString(touche, "attaque 2 :", new Vector2(20, 400), Color.Black);
            spriteBatch.DrawString(touche, "attaque 3 :", new Vector2(20, 450), Color.Black);
            spriteBatch.DrawString(commande_display, "Commandes", new Vector2(300, 20), Color.Black);

            left1.Draw(spriteBatch);
            right1.Draw(spriteBatch);
            up1.Draw(spriteBatch);
            attack1_1.Draw(spriteBatch);
            attack1_2.Draw(spriteBatch);
            attack1_3.Draw(spriteBatch);

            left2.Draw(spriteBatch);
            right2.Draw(spriteBatch);
            up2.Draw(spriteBatch);
            attack2_1.Draw(spriteBatch);
            attack2_2.Draw(spriteBatch);
            attack2_3.Draw(spriteBatch);

            Options.bouton_retour.Draw(spriteBatch);
        }

        public static void Initialise_keys_board(Keys[] keys)
        {
            //JOUEUR 1
            keys[0] = left1.key_bis;
            keys[1] = up1.key_bis;
            keys[2] = right1.key_bis;
            keys[3] = attack1_1.key_bis;
            keys[4] = attack1_2.key_bis;
            keys[5] = attack1_3.key_bis;

            //JOUEUR 2
            keys[6] = left2.key_bis;
            keys[7] = up2.key_bis;
            keys[8] = right2.key_bis;
            keys[9] = attack2_1.key_bis;
            keys[10] = attack2_2.key_bis;
            keys[11] = attack2_3.key_bis;
        }
    }


    class ControlsButton
    {
        Rectangle boarders, souris;
        public string clé;
        public Keys key_bis;
        Color color;
        Keys[] board;
        string touche_bis;
        int player_number_bis;
        int x, y;
        bool can_modify;

        public ControlsButton(Keys key, int x, int y, string touche, int player_number)
        {
            boarders = new Rectangle(x, y, 10, 20);
            key_bis = key;
            souris = new Rectangle(1, 1, 1, 1);
            color = Color.Blue;
            touche_bis = touche;
            player_number_bis = player_number;
            this.x = x;
            this.y = y;
            MakeControl(key_bis);
            can_modify = false;
        }

        public void Update(KeyboardState keyboard)
        {
            MainMenu.mouse = Mouse.GetState();
            souris.X = MainMenu.mouse.X;
            souris.Y = MainMenu.mouse.Y;
            keyboard = Keyboard.GetState();

            if (boarders.Intersects(souris))
            {
                color = Color.Purple;

                if (MainMenu.mouse.LeftButton == ButtonState.Pressed)
                {
                    can_modify = true;
                }
            }

            else
            {
                if (MainMenu.mouse.LeftButton == ButtonState.Pressed)
                {
                    can_modify = false;
                }
            }

            if (can_modify)
            {
                board = keyboard.GetPressedKeys();
                color = Color.Green;

                if (board.Length >= 1)
                {
                    can_modify = false;
                    key_bis = board[0];

                    if (player_number_bis == 1)
                    {
                        if (!is_present(key_bis, ChangeControls.cles) && touche_bis == "left")
                        {
                            GameMain.LocalPlayer1.gauche = key_bis;
                            VarTemp.left1 = key_bis;
                            ChangeControls.cles[0] = key_bis; //left1
                            MakeControl(key_bis);
                        }

                        else if (!is_present(key_bis, ChangeControls.cles) && touche_bis == "right")
                        {
                            GameMain.LocalPlayer1.droite = key_bis;
                            VarTemp.right1 = key_bis;
                            ChangeControls.cles[1] = key_bis; //right1
                            MakeControl(key_bis);
                        }

                        else if (!is_present(key_bis, ChangeControls.cles) && touche_bis == "up")
                        {
                            GameMain.LocalPlayer1.saut = key_bis;
                            VarTemp.up1 = key_bis;
                            ChangeControls.cles[2] = key_bis; //up1
                            MakeControl(key_bis);
                        }

                        else if (!is_present(key_bis, ChangeControls.cles) && touche_bis == "attack1")
                        {
                            GameMain.LocalPlayer1.attaque1 = key_bis;
                            VarTemp.attack1_1 = key_bis;
                            ChangeControls.cles[3] = key_bis; //attack1_1
                            MakeControl(key_bis);
                        }

                        else if (!is_present(key_bis, ChangeControls.cles) && touche_bis == "attack2")
                        {
                            GameMain.LocalPlayer1.attaque2 = key_bis;
                            VarTemp.attack1_2 = key_bis;
                            ChangeControls.cles[4] = key_bis; //attack1_2
                            MakeControl(key_bis);
                        }

                        else if (!is_present(key_bis, ChangeControls.cles) && touche_bis == "attack3")
                        {
                            GameMain.LocalPlayer1.attaque3 = key_bis;
                            VarTemp.attack1_3 = key_bis;
                            ChangeControls.cles[5] = key_bis; //attack1_3
                            MakeControl(key_bis);
                        }
                    }

                    if (player_number_bis == 2)
                    {
                        if (!is_present(key_bis, ChangeControls.cles) && touche_bis == "left")
                        {
                            GameMain.LocalPlayer2.gauche = key_bis;
                            VarTemp.left2 = key_bis;
                            ChangeControls.cles[6] = key_bis; //left2
                            MakeControl(key_bis);
                        }

                        else if (!is_present(key_bis, ChangeControls.cles) && touche_bis == "right")
                        {
                            GameMain.LocalPlayer2.droite = key_bis;
                            VarTemp.right2 = key_bis;
                            ChangeControls.cles[7] = key_bis; //right2
                            MakeControl(key_bis);
                        }

                        else if (!is_present(key_bis, ChangeControls.cles) && touche_bis == "up")
                        {
                            GameMain.LocalPlayer2.saut = key_bis;
                            VarTemp.up2 = key_bis;
                            ChangeControls.cles[8] = key_bis; //up2
                            MakeControl(key_bis);
                        }

                        else if (!is_present(key_bis, ChangeControls.cles) && touche_bis == "attack1")
                        {
                            GameMain.LocalPlayer2.attaque1 = key_bis;
                            VarTemp.attack2_1 = key_bis;
                            ChangeControls.cles[9] = key_bis; //attack2_1
                            MakeControl(key_bis);
                        }

                        else if (!is_present(key_bis, ChangeControls.cles) && touche_bis == "attack2")
                        {
                            GameMain.LocalPlayer2.attaque2 = key_bis;
                            VarTemp.attack2_2 = key_bis;
                            ChangeControls.cles[10] = key_bis; //attack2_2
                            MakeControl(key_bis);
                        }

                        else if (!is_present(key_bis, ChangeControls.cles) && touche_bis == "attack3")
                        {
                            GameMain.LocalPlayer2.attaque3 = key_bis;
                            VarTemp.attack2_3 = key_bis;
                            ChangeControls.cles[11] = key_bis; //attack2_3
                            MakeControl(key_bis);
                        }
                    }
                }
            }

            else
            {
                color = Color.Blue;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(ChangeControls.blank, boarders, color);
            spriteBatch.DrawString(ChangeControls.font, clé, new Vector2(x + 10, y + 5), Color.Black); 
        }

        public bool is_present(Keys key, Keys[] clés)
        {
            bool present = false;
            int i = 0;

            while(i < clés.Length && !present)
            {
                if (clés[i] == key)
                {
                    present = true;
                }

                i++;
            }

            return present;
        }

        public void MakeControl(Keys key)
        {
            switch (key)
            {
                case Keys.A :
                    clé = "A";
                    break;

                case Keys.Add:
                    clé = "Add";
                    break;

                case Keys.Apps:
                    clé = "Apps";
                    break;

                case Keys.Attn:
                    clé = "Attn";
                    break;

                case Keys.B:
                    clé = "B";
                    break;

                case Keys.Back:
                    clé = "Back";
                    break;

                case Keys.BrowserBack:
                    clé = "BrowserBack";
                    break;

                case Keys.BrowserFavorites:
                    clé = "BrowserFavorites";
                    break;

                case Keys.BrowserForward:
                    clé = "BrowserForward";
                    break;

                case Keys.BrowserHome:
                    clé = "BrowserHome";
                    break;

                case Keys.BrowserRefresh:
                    clé = "BrowserRefresh";
                    break;

                case Keys.BrowserSearch:
                    clé = "BrowserSearch";
                    break;

                case Keys.BrowserStop:
                    clé = "BrowserStop";
                    break;

                case Keys.C:
                    clé = "C";
                    break;

                case Keys.CapsLock:
                    clé = "CapsLock";
                    break;

                case Keys.ChatPadGreen:
                    clé = "ChatPadGreen";
                    break;

                case Keys.ChatPadOrange:
                    clé = "ChatPadOrange";
                    break;

                case Keys.Crsel:
                    clé = "Crsel";
                    break;

                case Keys.D:
                    clé = "D";
                    break;

                case Keys.D0:
                    clé = "D0";
                    break;

                case Keys.D1:
                    clé = "D1";
                    break;

                case Keys.D2:
                    clé = "D2";
                    break;

                case Keys.D3:
                    clé = "D3";
                    break;

                case Keys.D4:
                    clé = "D4";
                    break;

                case Keys.D5:
                    clé = "D5";
                    break;

                case Keys.D6:
                    clé = "D6";
                    break;

                case Keys.D7:
                    clé = "D7";
                    break;

                case Keys.D8:
                    clé = "D9";
                    break;

                case Keys.D9:
                    clé = "D9";
                    break;

                case Keys.Decimal:
                    clé = "Decimal";
                    break;

                case Keys.Delete:
                    clé = "Delete";
                    break;

                case Keys.Divide:
                    clé = "Divide";
                    break;

                case Keys.Down:
                    clé = "Down";
                    break;

                case Keys.E:
                    clé = "E";
                    break;

                case Keys.End:
                    clé = "End";
                    break;

                case Keys.Enter:
                    clé = "Enter";
                    break;

                case Keys.EraseEof:
                    clé = "EraseEof";
                    break;

                case Keys.Escape:
                    clé = "Escape";
                    break;

                case Keys.Execute:
                    clé = "Execute";
                    break;

                case Keys.Exsel:
                    clé = "Exsel";
                    break;

                case Keys.F:
                    clé = "F";
                    break;

                case Keys.F1:
                    clé = "F1";
                    break;

                case Keys.F2:
                    clé = "F2";
                    break;

                case Keys.F3:
                    clé = "F3";
                    break;

                case Keys.F4:
                    clé = "F4";
                    break;

                case Keys.F5:
                    clé = "F5";
                    break;

                case Keys.F6:
                    clé = "F6";
                    break;

                case Keys.F7:
                    clé = "F7";
                    break;

                case Keys.F8:
                    clé = "F8";
                    break;

                case Keys.F9:
                    clé = "F9";
                    break;

                case Keys.F10:
                    clé = "F10";
                    break;

                case Keys.F11:
                    clé = "F11";
                    break;

                case Keys.F12:
                    clé = "F12";
                    break;

                case Keys.F13:
                    clé = "F13";
                    break;

                case Keys.F14:
                    clé = "F14";
                    break;

                case Keys.F15:
                    clé = "F15";
                    break;

                case Keys.F16:
                    clé = "F16";
                    break;

                case Keys.F17:
                    clé = "F17";
                    break;

                case Keys.F18:
                    clé = "F18";
                    break;

                case Keys.F19:
                    clé = "F19";
                    break;

                case Keys.F20:
                    clé = "F20";
                    break;

                case Keys.F21:
                    clé = "F21";
                    break;

                case Keys.F22:
                    clé = "F22";
                    break;

                case Keys.F23:
                    clé = "F23";
                    break;

                case Keys.F24:
                    clé = "F24";
                    break;

                case Keys.G:
                    clé = "G";
                    break;

                case Keys.H:
                    clé = "H";
                    break;

                case Keys.Help:
                    clé = "Help";
                    break;

                case Keys.Home:
                    clé = "Home";
                    break;

                case Keys.I:
                    clé = "I";
                    break;

                case Keys.ImeConvert:
                    clé = "ImeConvert";
                    break;

                case Keys.ImeNoConvert:
                    clé = "ImeNoConvert";
                    break;

                case Keys.Insert:
                    clé = "Insert";
                    break;

                case Keys.J:
                    clé = "J";
                    break;

                case Keys.K:
                    clé = "K";
                    break;

                case Keys.Kana:
                    clé = "Kana";
                    break;

                case Keys.Kanji:
                    clé = "Kanji";
                    break;

                case Keys.L:
                    clé = "L";
                    break;

                case Keys.LaunchApplication1:
                    clé = "LaunchApplication1";
                    break;

                case Keys.LaunchApplication2:
                    clé = "LaunchApplication2";
                    break;

                case Keys.LaunchMail:
                    clé = "LaunchMail";
                    break;

                case Keys.Left:
                    clé = "Left";
                    break;

                case Keys.LeftControl:
                    clé = "LeftControl";
                    break;

                case Keys.LeftShift:
                    clé = "LeftShift";
                    break;

                case Keys.LeftWindows:
                    clé = "LeftWindows";
                    break;

                case Keys.M:
                    clé = "M";
                    break;

                case Keys.MediaNextTrack:
                    clé = "MediaNextTrack";
                    break;

                case Keys.MediaPlayPause:
                    clé = "MediaPlayPause";
                    break;

                case Keys.MediaPreviousTrack:
                    clé = "MediaPreviousTrack";
                    break;

                case Keys.MediaStop:
                    clé = "MediaStop";
                    break;

                case Keys.Multiply:
                    clé = "Multiply";
                    break;

                case Keys.N:
                    clé = "N";
                    break;

                case Keys.None:
                    clé = "None";
                    break;

                case Keys.NumLock:
                    clé = "NumLock";
                    break;

                case Keys.NumPad0:
                    clé = "NumPad0";
                    break;

                case Keys.NumPad1:
                    clé = "NumPad1";
                    break;

                case Keys.NumPad2:
                    clé = "NumPad2";
                    break;

                case Keys.NumPad3:
                    clé = "NumPad3";
                    break;

                case Keys.NumPad4:
                    clé = "NumPad4";
                    break;

                case Keys.NumPad5:
                    clé = "NumPad5";
                    break;

                case Keys.NumPad6:
                    clé = "NumPad6";
                    break;

                case Keys.NumPad7:
                    clé = "NumPad7";
                    break;

                case Keys.NumPad8:
                    clé = "NumPad8";
                    break;

                case Keys.NumPad9:
                    clé = "NumPad9";
                    break;

                case Keys.O:
                    clé = "O";
                    break;

                case Keys.Oem8:
                    clé = "Oem8";
                    break;

                case Keys.OemAuto:
                    clé = "OemAuto";
                    break;

                case Keys.OemBackslash:
                    clé = "OemBackslash";
                    break;

                case Keys.OemClear:
                    clé = "OemClear";
                    break;

                case Keys.OemCloseBrackets:
                    clé = "OemCloseBrackets";
                    break;

                case Keys.OemComma:
                    clé = "OemComma";
                    break;

                case Keys.OemCopy:
                    clé = "OemCopy";
                    break;

                case Keys.OemEnlW:
                    clé = "OemEnlW";
                    break;

                case Keys.OemMinus:
                    clé = "OemMinus";
                    break;

                case Keys.OemOpenBrackets:
                    clé = "OemOpenBrackets";
                    break;

                case Keys.OemPeriod:
                    clé = "OemPeriod";
                    break;

                case Keys.OemPipe:
                    clé = "OemPipe";
                    break;

                case Keys.OemPlus:
                    clé = "OemPlus";
                    break;

                case Keys.OemQuestion:
                    clé = "OemQuestion";
                    break;

                case Keys.OemQuotes:
                    clé = "OemQuotes";
                    break;

                case Keys.OemSemicolon:
                    clé = "OemSemicolon";
                    break;

                case Keys.OemTilde:
                    clé = "OemTilde";
                    break;

                case Keys.P:
                    clé = "P";
                    break;

                case Keys.Pa1:
                    clé = "Pa1";
                    break;

                case Keys.PageDown:
                    clé = "PageDown";
                    break;

                case Keys.PageUp:
                    clé = "PageUp";
                    break;

                case Keys.Pause:
                    clé = "Pause";
                    break;

                case Keys.Play:
                    clé = "Play";
                    break;

                case Keys.Print:
                    clé = "Print";
                    break;

                case Keys.PrintScreen:
                    clé = "PrintScreen";
                    break;

                case Keys.ProcessKey:
                    clé = "ProcessKey";
                    break;

                case Keys.Q:
                    clé = "Q";
                    break;

                case Keys.R:
                    clé = "R";
                    break;

                case Keys.Right:
                    clé = "Right";
                    break;

                case Keys.RightAlt:
                    clé = "RightAlt";
                    break;

                case Keys.RightControl:
                    clé = "RightControl";
                    break;

                case Keys.RightShift:
                    clé = "RightShift";
                    break;

                case Keys.RightWindows:
                    clé = "RightWindows";
                    break;

                case Keys.S:
                    clé = "S";
                    break;

                case Keys.Scroll:
                    clé = "Scroll";
                    break;

                case Keys.Select:
                    clé = "Select";
                    break;

                case Keys.SelectMedia:
                    clé = "SelectMedia";
                    break;

                case Keys.Separator:
                    clé = "Separator";
                    break;

                case Keys.Sleep:
                    clé = "Sleep";
                    break;

                case Keys.Space:
                    clé = "Space";
                    break;

                case Keys.Subtract:
                    clé = "Subtract";
                    break;

                case Keys.T:
                    clé = "T";
                    break;

                case Keys.Tab:
                    clé = "Tab";
                    break;

                case Keys.U:
                    clé = "U";
                    break;

                case Keys.Up:
                    clé = "Up";
                    break;

                case Keys.V:
                    clé = "V";
                    break;

                case Keys.VolumeDown:
                    clé = "VolumeDown";
                    break;

                case Keys.VolumeMute:
                    clé = "VolumeMute";
                    break;

                case Keys.VolumeUp:
                    clé = "VolumeUp";
                    break;

                case Keys.W:
                    clé = "W";
                    break;

                case Keys.X:
                    clé = "X";
                    break;

                case Keys.Y:
                    clé = "Y";
                    break;

                case Keys.Z:
                    clé = "Z";
                    break;

                case Keys.Zoom:
                    clé = "Zoom";
                    break;
            }
        }
    }
}
