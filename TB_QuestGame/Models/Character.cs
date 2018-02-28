using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TB_QuestGame
{
    public class Character
    {
        #region Enums
        public enum RaceType
        {
            None,
            Human,
            ExMachina,
            Flugel,
            Werebeast
        }
        #endregion
        #region Fields
        private bool isAlive;
        private int health;
        private int level;
        private Location currentLocation;
        private RaceType race;
        private string name;
        #endregion
        #region Properties
        public bool IsAlive
        {
            get { return isAlive; }
        }
        public int Health
        {
            get { return health; }
        }
        public int Level
        {
            get { return level; }
        }
        public Location CurrentLocation
        {
            get { return currentLocation; }
            set { currentLocation = value; }
        }
        public RaceType Race
        {
            get { return race; }
        }
        public string Name
        {
            set { name = value; }
            get { return name; }
        }
        #endregion
        #region Methods

        /// <summary>
        /// Damages the character for the specified amount of health
        /// </summary>
        /// <param name="source"></param>
        /// <param name="damage"></param>
        public virtual void Damage(Character source, int damage)
        {
            health -= damage;
            if (health <= 0) isAlive = false;
        }

        #endregion
        #region Constructors
        public Character(string name, RaceType race,  int level=1)
        {
            this.name = name;
            this.race = race;
            this.level = level;
            health = level*20;
            isAlive = true;
        }
        #endregion
    }
}
