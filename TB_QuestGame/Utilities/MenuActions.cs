using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TB_QuestGame
{
    public enum MenuAction
    {
        LookAround,
        Interact,
        Abilities,
        Travel,
        LocationsVisited,
        AdminMenu,
        Exit
    }
    public enum InteractionMenuAction
    {
        LookAt,
        Analyze,
        Back
    }
    public enum InventoryMenuAction
    {
        LookAt,
        PutDown,
        Back
    }
    public enum AdminMenuAction
    {
        PlayerInfo,
        ListLocations,
        ListGameObjects,
        PlayerEdit,
        Back
    }
    public static class TravelMenu
    {
        public static string[] GetLocationMenu(List<Location> locations)
        {
            List<string> actions = new List<string>();

            foreach (Location location in locations)
            {
                actions.Add(location.Name);
            }
            actions.Add("Back");

            return actions.ToArray();
        }
    }
    public static class ActionMenu
    {
        /// <summary>
        /// Splits up the enum name from title case into multiple words
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private static string ProcessEnumName(string name)
        {
            bool lastCharUpper = true;
            StringBuilder sb = new StringBuilder();

            foreach (char c in name)
            {
                if (char.IsUpper(c) && !lastCharUpper)
                    sb.Append(' ');

                sb.Append(c);

                lastCharUpper = char.IsUpper(c);
            }

            return sb.ToString();
        }
        /// <summary>
        /// Processes enum names into more presentable forms in menu
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static string[] GetEnumMenu(Type t)
        {
            List<string> actions = new List<string>();

            foreach (string name in Enum.GetNames(t))
            {
                actions.Add(ProcessEnumName(name));
            }

            return actions.ToArray();
        }
    }
}
