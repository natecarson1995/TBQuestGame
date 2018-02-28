using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TB_QuestGame
{
    public class Map
    {
        #region Fields
        private List<Location> locations;
        List<List<int>> adjacentLocations;
        Dictionary<Location, int> inverseIndex;
        #endregion
        #region Properties
        public List<Location> Locations
        {
            get { return locations; }
        }
        #endregion
        #region Methods

        /// <summary>
        /// Adds a new location to the map
        /// </summary>
        /// <param name="location"></param>
        public void AddLocation(Location location)
        {
            int index;

            locations.Add(location);
            index = locations.LastIndexOf(location);

            adjacentLocations.Insert(index, new List<int>());
            inverseIndex.Add(location,index);
        }

        /// <summary>
        /// Adds a connection between two locations
        /// </summary>
        /// <param name="location1"></param>
        /// <param name="location2"></param>
        public void AddConnection(Location location1, Location location2)
        {
            if (inverseIndex.ContainsKey(location1) && inverseIndex.ContainsKey(location2))
            {
                int index1 = inverseIndex[location1];
                int index2 = inverseIndex[location2];

                adjacentLocations[index1].Add(index2);
                adjacentLocations[index2].Add(index1);
            }
        }
        /// <summary>
        /// Gets locations adjacent to this one
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public List<Location> AdjacentLocations(Location location)
        {
            List<Location> list = new List<Location>();

            foreach (int index in adjacentLocations[inverseIndex[location]])
                list.Add(locations[index]);

            return list;
        }
        #endregion
        #region Constructors
        public Map()
        {
            locations = new List<Location>();
            adjacentLocations = new List<List<int>>();
            inverseIndex = new Dictionary<Location, int>();
        }
        #endregion
    }
}
