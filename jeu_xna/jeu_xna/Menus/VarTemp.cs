using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace jeu_xna
{
    public enum GameState
    {
        MainMenu,
        ChoiceMenuCaracter,
        ChoiceMenuBattlefield,
        Options,
        Commandes,
        Playing,
        Pause,
        LocalNetworkChoice,
        EndGame,
        ProfilPlayer
    }

    class VarTemp
    {
        public static GameState CurrentGameState, temp;
        public static bool is_connected;
        public static Connexion connexion, victory;
        public static string player_caracteristic, player, victoire_defaite, mail, date_inscription;
        public static string[] string_board, string_board_bis;
        public static int nb_victory, nb_defaites;

        public static Keys left1 = Keys.Q, right1 = Keys.D, up1 = Keys.Z, attack1_1 = Keys.F, attack1_2 = Keys.G, attack1_3 = Keys.B, attack1_4 = Keys.V, accroupi_1 = Keys.S;
        public static Keys left2 = Keys.Left, right2 = Keys.Right, up2 = Keys.Up, attack2_1 = Keys.OemPeriod, attack2_2 = Keys.OemQuestion, attack2_3 = Keys.RightShift, attack2_4 = Keys.Oem8, accroupi_2 = Keys.Down;
    }
}
