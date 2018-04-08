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

        #endregion
        #region Properties

        #endregion
        #region Methods
        public override void Analyze(Universe universe, Player player)
        {
            base.Analyze(universe, player);
        }

        public override GameObject Copy()
        {
            return new SceneryGameObject()
            {
                Description = this.Description,
                Name = this.Name,
                CurrentLocation = this.CurrentLocation
            };
        }
        #endregion
        #region Constructors
        public SceneryGameObject()
        {
            DestroyOnAnalysis = false;
            ExperienceReward = 5;
            Description = "";
            Name = "";
            CurrentLocation = null;
        }
        public SceneryGameObject(string name, Location currentLocation, string description="")
        {
            DestroyOnAnalysis = false;
            ExperienceReward = 5;
            Name = name;
            CurrentLocation = currentLocation;
            Description = description;
        }
        #endregion
    }
}
