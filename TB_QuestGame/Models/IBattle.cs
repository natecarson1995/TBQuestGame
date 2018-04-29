using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TB_QuestGame
{
    interface IBattle
    {
        event EventHandler OnVictory;
        event EventHandler OnLoss;

        int CalculateBattleIndex();
        string GetLossText();
        string GetVictoryText();
        void Battle(Player player);
    }
}
