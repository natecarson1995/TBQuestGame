using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TB_QuestGame
{
    class CombatNpc : Npc, IBattle
    {
        #region Fields
        public event EventHandler OnVictory;
        public event EventHandler OnLoss;
        #endregion
        #region Properties
        #endregion
        #region Methods

        /// <summary>
        /// Runs a battle between the player and npc, returning "true" if the npc won, "false" if they lost
        /// </summary>
        /// <param name="playerBattleIndex"></param>
        /// <returns></returns>
        public bool Battle(int playerBattleIndex)
        {
            if (CalculateBattleIndex() > playerBattleIndex)
            {
                OnVictory?.Invoke(this, EventArgs.Empty);
                return true;
            }
            else
            {
                OnLoss?.Invoke(this, EventArgs.Empty);
                return false;
            }
        }

        public int CalculateBattleIndex()
        {
            return Level;
        }

        public string GetLossText()
        {
            return $"Observation: Entity [{Name}] successfully neutralized.";
        }

        public string GetVictoryText()
        {
            return $"Observation: Entity [{Name}] 's power level exceeds safe threshold, Unit is exiting situation cautiously.";
        }
        #endregion
        #region Constructors
        public CombatNpc(string name, RaceType race, int level) : base(name, race, level)
        {
        }
        #endregion


    }
}
