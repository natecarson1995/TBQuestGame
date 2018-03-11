using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TB_QuestGame
{
    static class Locations
    {
        public static Location starterFactory = new Location("Main Proliferatory")
        {
            Description = "Observation: Unit is in a well lit, massive, perfectly square building. The ground is" +
            " covered with a vast array of exactly square tiles, forming a grid.",
            Contents = "Observation: The factory building contains over 100 other units in various stages of being complete.",
            DiscoveryExperience = 0
        };
        public static Location safeStarterArea = new Location("Front Line Sector")
        {
            Description = "Observation: Unit is on a patch of land, oddly grass-free, in between cloud-piercing buildings. \n" +
            "Hypothesis: The grass has been removed through the trods of thousands of other units",
            Contents = "Observation: Other units walk all around, coming incredibly close, but never touching or staggering.",
            DiscoveryExperience = 20
        };
        public static Location syncArea = new Location("Main Sync Zone")
        {
            Description = "Observation: There is a perfectly flat platform implanted in the ground, spanning 20 feet" +
            " in each direction. Sunlight glints off of the hot aluminum, leaving certain areas beyond" +
            " safe observation. As this unit access the core of the cluster, it feel the combined experiences" +
            " and analyses from millions of the race restructuring its data stores.",
            Contents = "Observation: Some members of the race stand motionless, feet away, synchronising to the cluster themselves.",
            DiscoveryExperience = 40
        };
        public static Location observationAreaStart = new Location("Main Observation Sector")
        {
            Description = "Observation: Unit is just outside of the main factory sector. This is the main collection point for" +
            " the meeting of different units to analyze and group-process over new sector observations and artifact collection.",
            Contents = "Observation: One other unit stands still, acting as a sensor for the cluster to keep watch of the area.",
            DiscoveryExperience = 20
        };
        public static Location observationAreaShip = new Location("Sector A - Ship")
        {
            Description = "Observation: Unit is making an observation of a downed Dwarven air-ship. The technology stands rather rudimentary" +
            " save for its military capacity. Energy cannons and explosives fill each hall of the ship.",
            Contents = "Observation: One energy cannon looks to be 90% functional. It is worth further analysis and possible replication.",
            DiscoveryExperience = 20
        };
        public static Location observationAreaVillage = new Location("Sector B - Village")
        {
            Description = "Observation: Unit is making an observation of an abandonded village of the hairless monkeys." +
            " Their continued survival despite their lack of power is a statistical anomaly.",
            Contents = "Observation: Some rudimentary shelters still stand here, but this place has been long abandoned." +
            "Hypothesis: This village has been untouched for over 3 months.",
            DiscoveryExperience = 20
        };
        public static Location observationAreaDwarfOutskirts = new Location("Sector C - Dwarven Outskirts")
        {
            Description = "Observation: Unit is making an observation of the edge of a Dwarven settlement. Large amounts of technology" +
            " stand within 300 feet of the unit, but advanced scanning does not directly show any inhabitants." +
            "Estimation: 50% chance of finding an aggressor in this sector.",
            Contents = "Observation: One white haired creature stands over a metal table in its dwelling, explosive unit ID [ 12EABF ] in hand.",
            DiscoveryExperience = 20
        };
        public static Location dwarfAreaVillage = new Location("Sector D - Dwarven Village")
        {
            Description = "Observation: Unit is making an observation of the center of a Dwarven settlement. " +
            " The risk associated with this area is incredibly high, and poses significant danger to this unit.",
            Contents = "Observation: Scan shows a large number of inhabitants, aware of the units presence.",
            DiscoveryExperience = 20
        };
    }
}
