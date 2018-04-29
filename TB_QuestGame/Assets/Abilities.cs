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
            Ability blast = new Ability("Energy Blast")
            {
                ProcText = "Fired Energy Blast"
            };

            blast.OnProc += delegate (object sender, EventArgs e)
            {
                if (e is Ability.TargetArgs)
                {
                    object target = (e as Ability.TargetArgs).Target;
                    if (target is Npc)
                    {
                        (target as Npc).Damage(10);
                        blast.ProcText = $"Damaged [{(target as Npc).Name}] for 10 damage.";
                    }
                }
                else
                {
                    blast.ProcText = $"No effect.";
                }
            };

            player.Abilities.Add(blast);
        }
        public static Ability CreateDamageAbility(Universe universe, Player player, string name, int damage)
        {
            Ability damageAbility = new Ability(name);
            damageAbility.OnProc += delegate (object sender, EventArgs e)
            {
                if (e is Ability.TargetArgs)
                {
                    object target = (e as Ability.TargetArgs).Target;
                    if (target is Npc)
                    {
                        (target as Npc).Damage(damage);
                        damageAbility.ProcText = $"Damaged [{(target as Npc).Name}] for {damage} damage.";
                    }
                }
                else
                {
                    damageAbility.ProcText = $"No effect.";
                }
            };

            return damageAbility;
        }
        public static Ability CreateObjectManufactureAbility(Universe universe, Player player, GameObject gameObject)
        {
            Ability objectManufactureAbility = new Ability("Manufacture " + gameObject.Name)
            {
                ProcText = $"Object [{gameObject.Name}] successfully manufactured."
            };
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
