using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TB_QuestGame
{
    public class Ability
    {
        #region Fields
        private bool isBattleAbility;
        public event EventHandler OnProc;
        private string name;
        #endregion
        #region Properties
        public bool IsBattleAbility
        {
            get { return isBattleAbility; }
            set { isBattleAbility = value; }
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        #endregion
        #region Methods

        /// <summary>
        /// Processes the ability
        /// </summary>
        /// <param name="universe"></param>
        /// <param name="player"></param>
        public void Proc()
        {
            OnProc?.Invoke(this, EventArgs.Empty);
        }
        #endregion
        #region Constructors
        public Ability(string name)
        {
            this.name = name;
        }
        #endregion
    }
}
