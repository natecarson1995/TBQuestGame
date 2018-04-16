using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TB_QuestGame
{
    public class SceneryGameObject : GameObject
    {
        #region Fields
        private bool beenAnalyzed;
        #endregion
        #region Properties
        public bool BeenAnalyzed
        {
            get { return beenAnalyzed; }
            set { beenAnalyzed = value; }
        }
        #endregion
        #region Methods
        public override void Analyze(Universe universe, Player player)
        {
            base.Analyze(universe, player);

            //
            // remove experience to offset player analyzing scenery multiple times
            //
            if (beenAnalyzed)
                player.AddExperience(-ExperienceReward);

            beenAnalyzed = true;
        }

        public override GameObject Copy()
        {
            return new SceneryGameObject()
            {
                Description = this.Description,
                Name = this.Name,
                CurrentLocation = this.CurrentLocation,
                ExperienceReward=0
            };
        }
        #endregion
        #region Constructors
        public SceneryGameObject()
        {
            beenAnalyzed = false;
            DestroyOnAnalysis = false;
            ExperienceReward = 5;
            Description = "";
            Name = "";
            CurrentLocation = null;
        }
        public SceneryGameObject(string name, Location currentLocation, string description="")
        {
            beenAnalyzed = false;
            DestroyOnAnalysis = false;
            ExperienceReward = 5;
            Name = name;
            CurrentLocation = currentLocation;
            Description = description;
        }
        #endregion
    }
}
