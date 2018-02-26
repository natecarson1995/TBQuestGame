using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TB_QuestGame
{
    public class Player : Character
    {
        #region Enums
        public enum UnitType
        {
            None,
            Processing,
            Manufacturing,
            Combat
        }
        #endregion
        #region Fields
        private int combatCapability;
        private int manufacturingTime;
        private int processingTime;
        private string clusterId;
        private UnitType type;
        #endregion
        #region Properties
        public int CombatCapability
        {
            get { return combatCapability; }
        }
        public int ManufacturingTime
        {
            get { return manufacturingTime; }
        }
        public int ProcessingTime
        {
            get { return processingTime; }
        }
        public string ClusterId
        {
            get { return clusterId; }
            set { clusterId = value; }
        }
        public UnitType Type
        {
            get { return type; }
        }
        #endregion
        #region Methods

        /// <summary>
        /// Damages the player, based on the sources level
        /// </summary>
        /// <param name="source"></param>
        /// <param name="damage"></param>
        public override void Damage(Character source, int damage)
        {
            double modifier = source.Level / Math.Max(Level,1);
            int newDamage = (int)(damage * modifier) - combatCapability;
            
            base.Damage(source, newDamage);
        }

        #endregion
        #region Constructors
        public Player(string name, UnitType type) : base(name,RaceType.ExMachina)
        {
            this.type = type;
            clusterId = Text.GetRandomHexCharacters(8);

            //
            // set stat levels based upon unit type
            //
            switch (type)
            {
                case UnitType.None:
                    break;
                case UnitType.Processing:
                    combatCapability = 2;
                    manufacturingTime = 7;
                    processingTime = 3;
                    break;
                case UnitType.Manufacturing:
                    combatCapability = 2;
                    manufacturingTime = 3;
                    processingTime = 7;
                    break;
                case UnitType.Combat:
                    combatCapability = 5;
                    manufacturingTime = 7;
                    processingTime = 7;
                    break;
                default:
                    break;
            }
        }
        #endregion
    }
}
