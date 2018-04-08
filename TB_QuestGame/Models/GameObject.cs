using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TB_QuestGame
{
    public abstract class GameObject
    {
        #region Fields
        private bool destroyOnAnalysis;
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
        public virtual void Analyze(Universe universe, Player player)
        {
            player.AddExperience(ExperienceReward);
        }
        public abstract GameObject Copy();
        #endregion
        #region Constructors
        #endregion
    }
}
