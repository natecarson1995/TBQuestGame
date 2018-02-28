using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TB_QuestGame
{
    public enum MenuAction
    {
        None,
        PlayerInfo,
        LookAround,
        Travel,
        LocationsVisited,
        ListLocations,
        PlayerEdit,
        Exit
    }
    public static class MainMenu
    {
        public static Dictionary<ConsoleKey, MenuAction> actions = new Dictionary<ConsoleKey, MenuAction>()
        {
            { ConsoleKey.D1, MenuAction.PlayerInfo },
            { ConsoleKey.D2, MenuAction.PlayerEdit },
            { ConsoleKey.D3, MenuAction.LookAround },
            { ConsoleKey.D4, MenuAction.Travel },
            { ConsoleKey.D5, MenuAction.LocationsVisited },
            { ConsoleKey.D6, MenuAction.ListLocations },
            { ConsoleKey.Escape, MenuAction.Exit }
        };
    }
    public static class TravelMenu
    {
        public static Dictionary<string, Location> GetLocationActions(List<Location> locations)
        {
            Dictionary<string, Location> actions = new Dictionary<string, Location>();

            foreach (Location location in locations)
            {
                actions.Add(location.Id, location);
            }

            return actions;
        }
    }
}
