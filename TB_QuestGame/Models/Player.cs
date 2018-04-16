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
        private bool isProcessing; //if this is true, player is in a processing mode, if false, they are in manufacturing mode
        private int combatCapability;
        private int manufacturingTime;
        private int processingTime;
        private int experience;
        private List<Ability> abilities;
        private List<Location> locationsVisited;
        private string clusterId;
        private UnitType type;
        #endregion
        #region Properties
        public bool IsProcessing
        {
            get { return isProcessing; }
            set { isProcessing=value; }
        }
        public bool IsManufacturing
        {
            get { return !isProcessing; }
        }
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
        public int Experience
        {
            get { return experience; }
        }
        public List<Ability> Abilities
        {
            get { return abilities; }
            set { abilities = value; }
        }
        public List<Location> LocationsVisited
        {
            get { return locationsVisited; }
            set { locationsVisited = value; }
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
        /// Damages the player, based on the sources level (in a combat setting)
        /// </summary>
        /// <param name="source"></param>
        /// <param name="damage"></param>
        public override void Damage(Character source, int damage)
        {
            double modifier = source.Level / Math.Max(Level,1);
            int newDamage = (int)(damage * modifier) - combatCapability;
            
            base.Damage(source, newDamage);
        }
        /// <summary>
        /// Adds (or subtracts if negative) the specified amount of experience to the player, leveling up if necessary
        /// </summary>
        /// <param name="experience"></param>
        public void AddExperience(int addedExperience)
        {
            experience += addedExperience;

            //
            // accomodate removal of experience
            //
            if (experience < 0)
            {
                Level--;
                experience += ExperienceToNextLevel(); 
            }

            while (experience > ExperienceToNextLevel())
            {
                experience -= ExperienceToNextLevel();
                Level++;
            }
        }

        /// <summary>
        /// Gets the experience required for the player to hit the next level
        /// </summary>
        /// <returns></returns>
        public int ExperienceToNextLevel()
        {
            return 10 + (int)Math.Pow(2, Level);
        }
        #endregion
        #region Constructors
        public Player(string name, UnitType type) : base(name,RaceType.ExMachina)
        {
            this.type = type;
            abilities = new List<Ability>();
            locationsVisited = new List<Location>();

            clusterId = Text.GetRandomHexCharacters(8);
            isProcessing = true;

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
