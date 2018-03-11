using System;
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
                "Current Unit Type: " + player.Type,
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
                "Current Location:\n" +
                player.CurrentLocation.Name};
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
