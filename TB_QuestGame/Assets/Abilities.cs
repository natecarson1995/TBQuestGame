using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TB_QuestGame
{
    public static class Abilities
    {
        public static void AddAbilities(Universe universe, Player player)
        {

        }
        public static Ability CreateObjectManufactureAbility(Universe universe, Player player, GameObject gameObject)
        {
            Ability objectManufactureAbility = new Ability("Manufacture " + gameObject.Name);
            objectManufactureAbility.IsBattleAbility = false;

            objectManufactureAbility.OnProc += delegate (object sender, EventArgs e)
            {
                universe.GameObjects.Add(
                    new GameObject()
                    {
                        Description = gameObject.Description,
                        Name = gameObject.Name,
                        ExperienceReward = gameObject.ExperienceReward,
                        CurrentLocation = player.CurrentLocation,
                        DestroyOnAnalysis = true,
                    }
                );

            };

            return objectManufactureAbility;
        }
    }
}
