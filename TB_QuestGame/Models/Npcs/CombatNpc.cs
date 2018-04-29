using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TB_QuestGame
{
    public class CombatNpc : Npc, IBattle
    {
        #region Fields
        public event EventHandler OnVictory;
        public event EventHandler OnLoss;
        private string lossText;
        private string victoryText;
        #endregion
        #region Properties
        public string LossText
        {
            get { return lossText; }
            set { lossText = value; }
        }
        public string VictoryText
        {
            get { return victoryText; }
            set { victoryText = value; }
        }
        #endregion
        #region Methods

        /// <summary>
        /// Runs a battle between the player and npc
        /// </summary>
        /// <returns></returns>
        public void Battle(Player player)
        {
            if (Health<=0)
                OnLoss?.Invoke(this, EventArgs.Empty);

            player.Damage(Level);

            if (player.Health<=0)
                OnVictory?.Invoke(this, EventArgs.Empty);
        }

        public int CalculateBattleIndex()
        {
            return (int)(Level*1.5);
        }

        public string GetLossText()
        {
            return $"Observation: Entity [{Name}] successfully neutralized.\n" + 
                lossText;
        }

        public string GetVictoryText()
        {
            return $"Observation: Entity [{Name}] 's power level exceeds safe threshold, " +
                $"Unit is exiting situation immediately.\n" + victoryText;
        }
        #endregion
        #region Constructors
        public CombatNpc(string name, RaceType race, int level) : base(name, race, level)
        {
        }
        #endregion


    }
}
