using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TB_QuestGame
{
    public class Universe
    {
        #region Fields
        private Map map;
        #endregion
        #region Properties
        public Map Map
        {
            get { return map; }
            set { map = value; }
        }

        #endregion
        #region Methods

        #endregion
        #region Constructors
        public Universe()
        {
            map = new Map();
        }
        #endregion
    }
}
