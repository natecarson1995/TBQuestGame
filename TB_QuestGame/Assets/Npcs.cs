using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TB_QuestGame
{
    public static class Npcs
    {
        public static void AddNpcs(Universe universe, Player player)
        {
            EventHandler rewardExperience = new EventHandler(delegate (object sender, EventArgs e)
            {
                if (sender is Npc)
                    player.AddExperience((sender as Npc).ExperienceReward);
            });

            RegularNpc guideSentinel = 
                new RegularNpc("Guide Sentinel", Character.RaceType.ExMachina, 100)
                    {
                        CurrentLocation = Locations.starterFactory,
                        ExperienceReward = 10,
                        Messages = new List<string>()
                            {
                                $"Observation: Unit Alias [{player.Name}] has completed manufacturing\n" +
                                "Recommended Action: Travel through Front Line Sector to Main Sync Zone\n" +
                                "Recommended: Interact and observe with environment at all times"
                            }
                    };
            guideSentinel.OnSpeak += new EventHandler(delegate (object sender, EventArgs e)
            {
                universe.Map.AddConnection(Locations.starterFactory, Locations.safeStarterArea);
            });
            universe.Npcs.Add(guideSentinel);


            CombatNpc combatSentinel  =
                new CombatNpc("Combat Sentinel", Character.RaceType.ExMachina, 1)
                {
                    CurrentLocation = Locations.safeStarterArea,
                    ExperienceReward = 10
                };
            combatSentinel.OnLoss += new EventHandler(delegate (object sender, EventArgs e)
            {
                player.AddExperience(50);
            });
            universe.Npcs.Add(combatSentinel);

            foreach (Npc npc in universe.Npcs)
            {
                if (npc is ISpeak)
                    (npc as ISpeak).OnSpeak += rewardExperience;
            }
        }
    }
}
