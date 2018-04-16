using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TB_QuestGame
{
    public class KeyGameObject : GameObject
    {
        #region Fields
        private Location nodeFrom;
        private Location nodeTo;
        #endregion
        #region Properties
        public Location NodeFrom
        {
            get { return nodeFrom; }
            set { nodeFrom = value; }
        }
        public Location NodeTo
        {
            get { return nodeTo; }
            set { nodeTo = value; }
        }
        #endregion
        #region Methods
        public override void Analyze(Universe universe, Player player)
        {
            if (nodeFrom!=null && nodeTo!=null)
            {
                universe.Map.AddConnection(nodeFrom, nodeTo);
            }

            base.Analyze(universe, player);
        }
        public override GameObject Copy()
        {
            return new KeyGameObject()
            {
                Description = this.Description,
                Name = this.Name,
                CurrentLocation = this.CurrentLocation,
                ExperienceReward = this.ExperienceReward,
                DestroyOnAnalysis = this.DestroyOnAnalysis,
                NodeFrom = nodeFrom,
                NodeTo = nodeTo
            };
        }
        #endregion
    }
}
