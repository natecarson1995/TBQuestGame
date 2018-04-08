using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TB_QuestGame
{
    public class ObjectManufactureAbility : Ability
    {
        #region Fields
        private GameObject gameObject;
        #endregion
        #region Methods
        public override void Proc(Universe universe, Player player)
        {
            GameObject newObject = gameObject.Copy();
            newObject.CurrentLocation = player.CurrentLocation;
            universe.GameObjects.Add(newObject);
        }
        #endregion
        #region Constructors
        public ObjectManufactureAbility(GameObject objectTemplate) : base("Manufacture: " + objectTemplate.Name)
        {
            gameObject = objectTemplate.Copy();
        }
        #endregion
    }
}
