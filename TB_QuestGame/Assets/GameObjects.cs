using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TB_QuestGame
{
    public static class GameObjects
    {
        public static void AddGameObjects(Universe universe)
        {
            universe.GameObjects.Add(new RegularGameObject()
            {
                Name = "Spirit Matter Compass",
                Description = "Size 10 cm \nColor: #A52A2A \nTexture: Smooth\nAnalysis: Points to the nearest source of spirit matter",
                CurrentLocation = Locations.safeStarterArea,
                ExperienceReward = 10
            });
            universe.GameObjects.Add(new SceneryGameObject()
            {
                Name = "Unit C132A",
                Description = "Height 1.9m \nColor: #CCCCCC \nTexture: Smooth\nHypothesis: In Sync Mode \nAdding Analysis to Memory Store",
                CurrentLocation = Locations.syncArea,
                ExperienceReward = 15
            });
            universe.GameObjects.Add(new MaxHealthIncreaseGameObject()
            {
                Name = "Spare Armor Plating",
                Description = "Height 2.2m \nColor: #EEEEEE \nTexture: Shiny and Smooth, Very Hard\nEffect: Increases Armor",
                CurrentLocation = Locations.starterFactory,
                HealthGain = 10,
                DestroyOnAnalysis=true
            });
            universe.GameObjects.Add(new SceneryGameObject()
            {
                Name = "Unit C135B",
                Description = "Height 1.9m \nColor: #CCCCCC \nTexture: Smooth\nHypothesis: In Construction \nAdding Analysis to Memory Store",
                CurrentLocation = Locations.starterFactory,
                ExperienceReward = 15
            });
            universe.GameObjects.Add(new KeyGameObject()
            {
                Name = "Explosive Dwarven Cannon",
                Description = "Length: .5m \nCondition: Rusty, made of low-quality metal \nEnergy Output: Very High \n" +
                "Suitable for Approaching the Dwarven Village",
                CurrentLocation = Locations.observationAreaShip,
                NodeFrom = Locations.observationAreaShip,
                NodeTo = Locations.observationAreaDwarfOutskirts,
                DestroyOnAnalysis = true
            });
        }
    }
}
