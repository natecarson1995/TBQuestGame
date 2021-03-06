﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TB_QuestGame
{
    public static class Text
    {
        public static string[] splashLines =
            {
                "███████╗██╗  ██╗    ███╗   ███╗ █████╗  ██████╗██╗  ██╗██╗███╗   ██╗ █████╗",
                "██╔════╝╚██╗██╔╝    ████╗ ████║██╔══██╗██╔════╝██║  ██║██║████╗  ██║██╔══██╗",
                "█████╗   ╚███╔╝     ██╔████╔██║███████║██║     ███████║██║██╔██╗ ██║███████║",
                "██╔══╝   ██╔██╗     ██║╚██╔╝██║██╔══██║██║     ██╔══██║██║██║╚██╗██║██╔══██║",
                "███████╗██╔╝ ██╗    ██║ ╚═╝ ██║██║  ██║╚██████╗██║  ██║██║██║ ╚████║██║  ██║",
                "╚══════╝╚═╝  ╚═╝    ╚═╝     ╚═╝╚═╝  ╚═╝ ╚═════╝╚═╝  ╚═╝╚═╝╚═╝  ╚═══╝╚═╝  ╚═╝"
            };

        public static string[] introSections = {
            " Unit is a new robot in the race of intelligent machines: Ex Machina ",
            " Unit has been manufactured during a time of war among races ",
            " Therefore, the unit must exercise caution in approaching potential agressors ",
            " Unit will now attempt connecting to the local cluster to determine goals  and  input configuration details ",
            " ....... "," .............. ",
            $" Connection to cluster successful "
        };

        public static string[] initialScreen =
        {
            " Unit is now configured ",
            " Please stand by for further details "
        };
        public static string setupGetName =
            "Unit's hardware number is " + Text.GetRandomHexCharacters(8) +
            "\nAn alias is recommended for quick recall, please insert this now.";
        public static string setupGetType = "\nUnit may be configured in one specialty.\n" +
            "These specialties include:\n";
        public static string syncSuccess = "\nNew configuration settings have been synchronized with the cluster.";

        public static string[] GetPlayerInfoText(Player player)
        {
            return new string[]
            {
                "Unit Alias: " + player.Name,
                "Unit Level: " + player.Level,
                "Unit Cluster: " + player.ClusterId,
                "",
                "Unit Combat Ability: " + player.CombatCapability,
                "Unit Average Manufacturing Time: " + player.ManufacturingTime,
                "Unit Average Processing Time: " + player.ProcessingTime
            };
        }

        /// <summary>
        /// Gets the edit player type screen text
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public static string[] GetEditPlayerTypeText(Player player)
        {
            return new string[] {
                "\nCurrent Unit Type: " + player.Type,
                "Input a new unit type, or press enter to skip",
                "WARNING : THIS WILL RESET UNIT'S WORKING MEMORY",
                "\nThese unit types include: ",
                "Processing\nManufacturing\nCombat",

            };
        }
        /// <summary>
        /// Gets the location list text
        /// </summary>
        /// <param name="locations"></param>
        /// <returns></returns>
        public static string[] GetLocationListText(List<Location> locations)
        {
            List<string> locationText = new List<string>();
            
            foreach (Location location in locations)
                locationText.Add(location.Name);

            return locationText.ToArray();
        }
        /// <summary>
        /// Gets the game object list text
        /// </summary>
        /// <param name="locations"></param>
        /// <returns></returns>
        public static string[] GetGameObjectListText(List<GameObject> gameObjects)
        {
            List<string> objectText = new List<string>();

            foreach (GameObject gameObject in gameObjects)
                objectText.Add(gameObject.Name);

            return objectText.ToArray();
        }
        /// <summary>
        /// Gets the npc list text
        /// </summary>
        /// <param name="locations"></param>
        /// <returns></returns>
        public static string[] GetNpcListText(List<Npc> npcs)
        {
            List<string> npcText = new List<string>();

            foreach (Npc npc in npcs)
                npcText.Add(npc.Name);

            return npcText.ToArray();
        }
        /// <summary>
        /// Gets the object description for an object
        /// </summary>
        /// <param name="gameObject"></param>
        /// <returns></returns>
        public static string[] GetObjectDescriptionText(GameObject gameObject)
        {
            return new string[]
            {
                "Object Analysis",
                $"Object Name: {gameObject.Name}",
                "Object Observations:",
                gameObject.Description
            };
        }
        public static string[] GetNpcTalkText(Npc npc)
        {
            if (npc is ISpeak)
            {
                return new string[]
                {
                    npc.Name,
                    (npc as ISpeak).Speak()
                };
            }
            else
            {
                return new string[]
                {
                    $"Observation: {npc.Name} remains silent."
                };
            }
        }
        public static string[] GetNpcBattleText(Npc npc, string battleText)
        {
            if (npc is IBattle)
            {
                return new string[]
                {
                    battleText
                };
            }
            else
            {
                return new string[]
                {
                    $"Observation: {npc.Name} is not a threat."
                };
            }
        }
        public static string[] GetBattleStatus(Player player, Npc npc)
        {
            return new string[] {
                "Player Health: " +
                player.Health,
                "Level: " +
                player.Level,
                "Experience: " +
                player.Experience +
                "/" +
                player.ExperienceToNextLevel(),
                "Current Enemy:",
                npc.Name,
                "Enemy Health: " +
                npc.Health
            };
        }
        public static string[] GetPlayerStatus(Player player)
        {
            return new string[] {
                "Player Health: " +
                player.Health,
                "Level: " +
                player.Level,
                "Experience: " +
                player.Experience +
                "/" +
                player.ExperienceToNextLevel(),
                "Current Location:",
                player.CurrentLocation.Name,
                "Current Quest:",
                player.CurrentQuest};
        }
        public static string[] GetAnalysisText()
        {
            return new string[]
            {
                "",
                "",
                "████████████████████████████████",
                "",
                "Analysis: Complete"
            };
        }
        /// <summary>
        /// returns a random hex character 0-f
        /// </summary>
        /// <returns></returns>
        public static string GetRandomHexCharacters(int num)
        {
            Random rand = new Random();
            byte[] bytes = new byte[num];
            char[] chars = new char[num];

            rand.NextBytes(bytes);
            
            for (int i=0;i<num;i++)
                chars[i] = Convert.ToString(bytes[i], 16)[0];

            return new string(chars);
        }

        /// <summary>
        /// return a string in title case
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToTitleCase(string str)
        {
            if (str.Length > 2)
                return str.Substring(0, 1).ToUpper() + str.Substring(1).ToLower();
            else
                return str;
        }
    }
}
