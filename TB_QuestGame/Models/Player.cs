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
        private int experience;
        private List<Ability> abilities;
        private List<Location> locationsVisited;
        private string clusterId;
        private string currentQuest;
        private UnitType type;
        #endregion
        #region Properties
        public int CombatCapability
        {
            get { return combatCapability; }
            set { combatCapability = value; }
        }
        public int ManufacturingTime
        {
            get { return manufacturingTime; }
            set { manufacturingTime = value; }
        }
        public int ProcessingTime
        {
            get { return processingTime; }
            set { processingTime = value; }
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
        public string CurrentQuest
        {
            get { return currentQuest; }
            set { currentQuest = value; }
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
                MaxHealth -= 2;
                experience += ExperienceToNextLevel(); 
            }

            while (experience > ExperienceToNextLevel())
            {
                experience -= ExperienceToNextLevel();
                Level++;
                MaxHealth += 2;
                Damage(Health - MaxHealth);
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

        /// <summary>
        /// Calculates the players battle index for comparison during fighting
        /// </summary>
        /// <returns></returns>
        public int CalculateBattleIndex()
        {
            return (int)(ManufacturingTime + ProcessingTime + 1.2 * CombatCapability);
        }
        #endregion
        #region Constructors
        public Player(string name, UnitType type) : base(name,RaceType.ExMachina)
        {
            Damage(-19);
            MaxHealth = 20;
            this.type = type;
            abilities = new List<Ability>();
            locationsVisited = new List<Location>();

            clusterId = Text.GetRandomHexCharacters(8);

            //
            // set stat levels based upon unit type
            //
            switch (type)
            {
                case UnitType.None:
                    break;
                case UnitType.Processing:
                    combatCapability = 1;
                    manufacturingTime = 3;
                    processingTime = 1;
                    break;
                case UnitType.Manufacturing:
                    combatCapability = 1;
                    manufacturingTime = 1;
                    processingTime = 3;
                    break;
                case UnitType.Combat:
                    combatCapability = 3;
                    manufacturingTime = 3;
                    processingTime = 3;
                    break;
                default:
                    break;
            }
        }
        #endregion
    }
}
