using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TB_QuestGame
{
    public class MaxHealthIncreaseGameObject : GameObject
    {
        #region Fields
        private int healthGain;
        #endregion
        #region Properties
        public int HealthGain
        {
            get { return healthGain; }
            set { healthGain = value; }
        }
        #endregion
        #region Methods
        /// <summary>
        /// Give the player health (extra armor plating)
        /// </summary>
        /// <param name="universe"></param>
        /// <param name="player"></param>
        public override void Analyze(Universe universe, Player player)
        {
            player.Damage(-healthGain);
            base.Analyze(universe, player);
        }

        public override GameObject Copy()
        {
            return new MaxHealthIncreaseGameObject()
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
        public MaxHealthIncreaseGameObject()
        {
            healthGain = 10;
        }
        #endregion
    }
}
