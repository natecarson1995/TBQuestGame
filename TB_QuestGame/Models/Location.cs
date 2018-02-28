using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TB_QuestGame
{
    public class Location
    {
        #region Fields
        private string description;
        private string id;
        private string name;
        #endregion
        #region Properties
        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        public string Id
        {
            get { return id; }
            set { id = value; }
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        #endregion
        #region Methods

        #endregion
        #region Constructors
        public Location(string id, string name, string description)
        {
            this.id = id;
            this.name = name;
            this.description = description;
        }
        #endregion
    }
}
