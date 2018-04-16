using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TB_QuestGame
{
    public class Universe
    {
        #region Fields
        private Map map;
        private List<GameObject> gameObjects;
        private List<Npc> npcs;
        #endregion
        #region Properties
        public Map Map
        {
            get { return map; }
            set { map = value; }
        }
        public List<GameObject> GameObjects
        {
            get { return gameObjects; }
        }
        public List<Npc> Npcs
        {
            get { return npcs; }
        }
        #endregion
        #region Methods
        /// <summary>
        /// Gets all of the game objects at the specified location as a list
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public List<GameObject> GetObjectsAtLocation(Location location)
        {
            List<GameObject> matchingObjects = new List<GameObject>();

            foreach (GameObject gameObject in gameObjects)
                if (gameObject.CurrentLocation == location)
                    matchingObjects.Add(gameObject);

            return matchingObjects;
        }
        /// <summary>
        /// Gets all of the npcs at the specified location as a list
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public List<Npc> GetNpcsAtLocation(Location location)
        {
            List<Npc> matchingNpcs = new List<Npc>();

            foreach (Npc npc in npcs)
                if (npc.CurrentLocation == location)
                    matchingNpcs.Add(npc);

            return matchingNpcs;
        }
        #endregion
        #region Constructors
        public Universe()
        {
            gameObjects = new List<GameObject>();
            npcs = new List<Npc>();
            map = new Map();
        }
        #endregion
    }
}
