using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TB_QuestGame
{
    public class GameObject
    {
        #region Fields
        private bool destroyOnAnalysis;
        public event EventHandler OnAnalyze;
        private int experienceReward;
        private Location currrentLocation;
        private string description;
        private string name;
        #endregion
        #region Properties
        public bool DestroyOnAnalysis
        {
            get { return destroyOnAnalysis; }
            set { destroyOnAnalysis = value; }
        }
        public int ExperienceReward
        {
            get { return experienceReward; }
            set { experienceReward = value; }
        }
        public Location CurrentLocation
        {
            get { return currrentLocation; }
            set { currrentLocation = value; }
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
        public void Analyze()
        {
            OnAnalyze?.Invoke(this, EventArgs.Empty);
        }
        public GameObject Copy()
        {
            return new GameObject()
            {
                Description = this.Description,
                Name = this.Name,
                CurrentLocation = this.CurrentLocation,
                ExperienceReward = this.ExperienceReward
            };
        }
        #endregion
        #region Constructors
        #endregion
    }
}
