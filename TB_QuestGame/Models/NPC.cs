using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TB_QuestGame
{
    public abstract class Npc : Character
    {
        #region Constructors
        public Npc(string name, RaceType race, int level = 1) : base(name, race, level)
        {

        }
        #endregion
    }
}
