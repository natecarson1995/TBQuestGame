using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TB_QuestGame
{
    public static class Npcs
    {
        public static RegularNpc guideSentinel;
        public static RegularNpc dwarfNonthreat;
        public static CombatNpc combatSentinel;
        public static CombatNpc enemyDwarf;
        public static CombatNpc enemyDwarfBoss;
        public static CombatNpc enemyElf;
        public static void AddNpcs(Universe universe, Player player)
        {
            EventHandler rewardExperience = new EventHandler(delegate (object sender, EventArgs e)
            {
                if (sender is Npc)
                    player.AddExperience((sender as Npc).ExperienceReward);
            });

            EventHandler fullHeal = new EventHandler(delegate (object sender, EventArgs e)
            {
                if (sender is Npc)
                {
                    Npc npc = sender as Npc;
                    npc.Damage(npc.Health - npc.Level);
                }
            });
            EventHandler removeNpc = new EventHandler(delegate (object sender, EventArgs e)
            {
                if (sender is Npc)
                    universe.Npcs.Remove(sender as Npc);
            });

            guideSentinel = 
                new RegularNpc("Guide Sentinel", Character.RaceType.ExMachina, 100)
                    {
                        CurrentLocation = Locations.starterFactory,
                        ExperienceReward = 10,
                        Messages = new List<string>()
                            {
                                $"Observation: Unit Alias [{player.Name}] has completed manufacturing\n" +
                                "Recommended Action: Travel through Front Line Sector to Main Sync Zone\n" +
                                "Recommended: Interact and observe with the environment at all times"
                            }
                    };
            guideSentinel.OnSpeak += new EventHandler(delegate (object sender, EventArgs e)
            {
                player.CurrentQuest = "Travel to the Sync Zone";
                universe.Map.AddConnection(Locations.starterFactory, Locations.safeStarterArea);
            });
            universe.Npcs.Add(guideSentinel);

            enemyDwarf =
                new CombatNpc($"Dwarf: ID [{Text.GetRandomHexCharacters(4)}]", Character.RaceType.Dwarf, 20)
                {
                    CurrentLocation = Locations.observationAreaDwarfOutskirts,
                    ExperienceReward = 10,
                    VictoryText = "Analysis: Valuable combat data gained.\n" +
                    "Recommended Action: Move to find and neutralize Dwarven leader"
                };
            enemyDwarf.OnLoss += new EventHandler(delegate (object sender, EventArgs e)
            {
                player.AddExperience(100);
            });
            universe.Npcs.Add(enemyDwarf);


            dwarfNonthreat =
                new RegularNpc($"Dwarf Child: ID [{Text.GetRandomHexCharacters(4)}]", Character.RaceType.Dwarf, 2)
                {
                    CurrentLocation = Locations.dwarfAreaVillage,
                    Messages = new List<string>
                    {
                        "Please don't hurt me",
                        "What are you?"
                    }
                };
            universe.Npcs.Add(dwarfNonthreat);

            enemyDwarfBoss =
                new CombatNpc($"Dwarf Chieftan: ID [{Text.GetRandomHexCharacters(4)}]", Character.RaceType.Dwarf, 30)
                {
                    CurrentLocation = Locations.dwarfAreaVillage,
                    ExperienceReward = 200,
                    VictoryText = "Analysis: Valuable combat data gained.\n" +
                    "Observation: Dwarven threat in this sector has been fully neutralized"
                };
            universe.Npcs.Add(enemyDwarfBoss);

            enemyElf =
                new CombatNpc($"Elf: ID [{Text.GetRandomHexCharacters(4)}]", Character.RaceType.Elf, 40)
                {
                    CurrentLocation = Locations.observationAreaVillage,
                    ExperienceReward = 10,
                    VictoryText = "Analysis: Valuable combat data gained.\n" +
                    "Recommended Action: Attempt placing reflectors on the field to reflect magic."
                };
            enemyElf.OnLoss += new EventHandler(delegate (object sender, EventArgs e)
            {

                player.AddExperience(100);
            });
            universe.Npcs.Add(enemyElf);

            combatSentinel  =
                new CombatNpc("Combat Sentinel", Character.RaceType.ExMachina, 5)
                {
                    CurrentLocation = Locations.safeStarterArea,
                    ExperienceReward = 10,
                    VictoryText = "Analysis: Valuable combat data gained.\n" +
                    "Recommended Action: Attempt battle again."
                };
            combatSentinel.OnVictory += new EventHandler(delegate (object sender, EventArgs e)
            {
                player.CombatCapability += 10;
            });
            combatSentinel.OnLoss += new EventHandler(delegate (object sender, EventArgs e)
            {
                player.AddExperience(50);
            });
            universe.Npcs.Add(combatSentinel);

            foreach (Npc npc in universe.Npcs)
            {
                if (npc is ISpeak)
                    (npc as ISpeak).OnSpeak += rewardExperience;

                if (npc is CombatNpc)
                {
                    (npc as CombatNpc).OnLoss += removeNpc;
                    (npc as CombatNpc).OnVictory += fullHeal;
                }
            }
        }
    }
}
