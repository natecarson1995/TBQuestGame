using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TB_QuestGame
{
    public class RegularGameObject : GameObject
    {
        #region Fields

        #endregion
        #region Properties

        #endregion
        #region Methods
        public override void Analyze(Universe universe, Player player)
        {
            player.Abilities.Add(new ObjectManufactureAbility(Copy()));
            base.Analyze(universe, player);
        }
        public override GameObject Copy()
        {
            return new RegularGameObject()
            {
                Description = this.Description,
                Name = this.Name,
                CurrentLocation = this.CurrentLocation,
                ExperienceReward = this.ExperienceReward,
                DestroyOnAnalysis = this.DestroyOnAnalysis
            };
        }
        #endregion
        #region Constructors
        public RegularGameObject()
        {
            DestroyOnAnalysis = true;
            ExperienceReward = 5;
            Description = "";
            Name = "";
            CurrentLocation = null;
        }
        public RegularGameObject(string name, Location currentLocation, string description = "")
        {
            DestroyOnAnalysis = true;
            ExperienceReward = 5;
            Name = name;
            CurrentLocation = currentLocation;
            Description = description;
        }
        #endregion
    }
}
