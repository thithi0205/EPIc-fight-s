using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

    class State
    {
        public static GameState CurrentGameState;
    }
}
