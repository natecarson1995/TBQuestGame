using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TB_QuestGame
{
    public static class Npcs
    {
        public static void AddNpcs(Universe universe)
        {
            universe.Npcs.Add(
                new RegularNpc("Guide Sentinel", Character.RaceType.ExMachina, 100)
                {
                    CurrentLocation = Locations.starterFactory,
                    Messages = new List<string>()
                    {
                        "Test Message 1",
                        "Test Message 2"
                    }
                }
            );
        }
    }
}
