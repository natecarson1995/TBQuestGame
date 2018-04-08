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
        #endregion
        #region Constructors
        public Universe()
        {
            gameObjects = new List<GameObject>();
            map = new Map();
        }
        #endregion
    }
}
