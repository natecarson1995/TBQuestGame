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
        private int discoveryExperience;
        public event EventHandler OnTravel;
        private string contents;
        private string description;
        private string name;
        #endregion
        #region Properties
        public int DiscoveryExperience
        {
            get { return discoveryExperience; }
            set { discoveryExperience = value; }
        }
        public string Contents
        {
            get { return contents; }
            set { contents = value; }
        }
        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        #endregion
        #region Methods
        public void TravelTo()
        {
            OnTravel?.Invoke(this, EventArgs.Empty);
        }
        #endregion
        #region Constructors
        public Location(string name)
        {
            this.name = name;
            contents = "";
        }
        #endregion
    }
}
