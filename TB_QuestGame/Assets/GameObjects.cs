using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TB_QuestGame
{
    public static class GameObjects
    {
        public static void AddGameObjects(Universe universe, Player player)
        {
            EventHandler destroyObject = new EventHandler(delegate (object sender, EventArgs e)
            {
                if (sender is GameObject && universe.GameObjects.Contains(sender as GameObject))
                    universe.GameObjects.Remove(sender as GameObject);
            });

            EventHandler giveExperience = new EventHandler(delegate (object sender, EventArgs e)
            {
                if (sender is GameObject)
                    player.AddExperience((sender as GameObject).ExperienceReward);
            });

            universe.GameObjects.Add(new GameObject()
            {
                Name = "Unit C132A",
                Description = "Height 1.9m \nColor: #CCCCCC \nTexture: Smooth\nHypothesis: In Sync Mode \nAdding Analysis to Memory Store",
                CurrentLocation = Locations.syncArea,
                ExperienceReward = 15,
                DestroyOnAnalysis = true
            });
            
            universe.GameObjects.Add(new GameObject()
            {
                Name = "Unit C135B",
                Description = "Height 1.9m \nColor: #CCCCCC \nTexture: Smooth\nHypothesis: In Construction \nAdding Analysis to Memory Store",
                CurrentLocation = Locations.starterFactory,
                ExperienceReward = 15,
                DestroyOnAnalysis = true
            });



            GameObject compass = new GameObject()
            {
                Name = "Spirit Matter Compass",
                Description = "Size 10 cm \nColor: #A52A2A \nTexture: Smooth\nAnalysis: Points to the nearest source of spirit matter",
                CurrentLocation = Locations.safeStarterArea,
                ExperienceReward = 10,
                DestroyOnAnalysis = true
            };
            universe.GameObjects.Add(compass);

            GameObject mirror = new GameObject()
            {
                Name = "Reflector Shards",
                Description = "Size 5 cm \nColor: Reflective \nTexture: Very Sharp\nAnalysis: Reflects all energy that hits the surface",
                CurrentLocation = Locations.observationAreaStart,
                ExperienceReward = 10,
                DestroyOnAnalysis = true
            };
            mirror.OnAnalyze += new EventHandler(delegate (object sender, EventArgs e)
            {
                Ability manufactureMirror = Abilities.CreateObjectManufactureAbility(universe, player, mirror);
                manufactureMirror.OnProc += new EventHandler(delegate (object sender2, EventArgs e2)
                {
                    if (Npcs.enemyElf != null  && player.CurrentLocation==Npcs.enemyElf.CurrentLocation)
                    {
                        Npcs.enemyElf.Level=5;
                        Npcs.enemyElf.Damage(Npcs.enemyElf.Health - 10);
                    }

                });
                player.Abilities.Add(manufactureMirror);
            });
            universe.GameObjects.Add(mirror);

            GameObject dwarvenCannon = new GameObject()
            {
                Name = "Explosive Dwarven Cannon",
                Description = "Length: .5m \nCondition: Rusty, made of low-quality metal \nEnergy Output: Very High \n" +
                "Suitable for Approaching the Dwarven Village",
                CurrentLocation = Locations.observationAreaShip,
                DestroyOnAnalysis = true
            };
            dwarvenCannon.OnAnalyze += new EventHandler(delegate (object sender, EventArgs e)
            {
                player.Abilities.Add(Abilities.CreateDamageAbility(universe, player, "Dwarven Cannon", 20));
                player.CurrentQuest = "Enter the Dwarven Village and Neutralize the Threats";
                universe.Map.AddConnection(Locations.observationAreaShip, Locations.observationAreaDwarfOutskirts);
            });
            universe.GameObjects.Add(dwarvenCannon);

            GameObject armorPlating = new GameObject()
            {
                Name = "Spare Armor Plating",
                Description = "Height 2.2m \nColor: #EEEEEE \nTexture: Shiny and Smooth, Very Hard\nEffect: Increases Armor",
                CurrentLocation = Locations.starterFactory,
                DestroyOnAnalysis = true
            };
            armorPlating.OnAnalyze += new EventHandler(delegate (object sender, EventArgs e)
            {
                player.MaxHealth += 10;
                player.Damage(-10);
            });
            universe.GameObjects.Add(armorPlating);

            foreach (GameObject go in universe.GameObjects)
            {
                if (go.DestroyOnAnalysis)
                    go.OnAnalyze += destroyObject;

                if (go.ExperienceReward > 0)
                    go.OnAnalyze += giveExperience;
            }
        }
    }
}
