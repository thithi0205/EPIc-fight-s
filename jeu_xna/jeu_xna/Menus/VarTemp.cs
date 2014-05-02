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
        Pause
    }

    class VarTemp
    {
        public static GameState CurrentGameState;

        public static Keys left1 = Keys.Q, right1 = Keys.D, up1 = Keys.Z, attack1_1 = Keys.F, attack1_2 = Keys.G;
        public static Keys left2 = Keys.Left, right2 = Keys.Right, up2 = Keys.Up, attack2_1 = Keys.Oem8, attack2_2 = Keys.RightShift;
    }
}
