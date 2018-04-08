using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TB_QuestGame
{
    public abstract class Ability
    {
        #region Fields
        private string name;
        #endregion
        #region Properties
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
        public abstract void Proc(Universe universe, Player player);
        #endregion
        #region Constructors
        public Ability(string name)
        {
            this.name = name;
        }
        #endregion
    }
}
