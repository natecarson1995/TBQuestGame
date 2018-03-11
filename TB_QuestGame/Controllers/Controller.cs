using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TB_QuestGame
{
    public class Controller
    {
        #region Fields
        ConsoleView view;
        Universe universe;
        Player player;
        #endregion
        #region Properties

        #endregion
        #region Methods
        /// <summary>
        /// Initializes all of the game data
        /// </summary>
        private void Initialize()
        {
            InitializeMap();
            //view.DrawSplashScreen();
            //view.DrawIntroScreen();
            player = view.DrawSetupScreen();
            view.SetWindowSizesToDefault();

            player.CurrentLocation = Locations.starterFactory;

            ManageGameLoop();
        }
        /// <summary>
        /// Updates game status
        /// </summary>
        private void UpdateGameStatus()
        {
            //
            // give the player experience for visiting new locations
            //
            if (!player.LocationsVisited.Contains(player.CurrentLocation))
            {
                player.LocationsVisited.Add(player.CurrentLocation);
                player.AddExperience(player.CurrentLocation.DiscoveryExperience);
                view.DisplayStatus(Text.GetPlayerStatus(player));
            }
            //
            // unlock the dwarven area line after the player has hit level 6 (requires travelling to almost all prior areas)
            //
            if (player.Level>=6)
            {
                if (!universe.Map.AdjacentLocations(Locations.observationAreaShip).Contains(Locations.observationAreaDwarfOutskirts))
                    universe.Map.AddConnection(Locations.observationAreaShip, Locations.observationAreaDwarfOutskirts);
            }
        }
        /// <summary>
        /// Does the core game loop
        /// </summary>
        private void ManageGameLoop()
        {
            bool playing = true;
            int index = 0;
            MenuAction action;

            while (playing)
            {
                UpdateGameStatus();

                view.DisplayLocationDescription(player.CurrentLocation);
                view.DisplayStatus(Text.GetPlayerStatus(player));

                index = view.DrawGetMenuOption(ActionMenu.GetEnumMenu(typeof(MenuAction)));
                action = (MenuAction)Enum.GetValues(typeof(MenuAction)).GetValue(index);

                switch (action)
                {
                    case MenuAction.PlayerInfo:
                        view.DisplayPlayerInfo(player);
                        break;
                    case MenuAction.PlayerEdit:
                        Player tempPlayer = view.DisplayEditPlayerInfo(player);
                        EditPlayerInfo(tempPlayer);
                        break;
                    case MenuAction.ListLocations:
                        view.DisplayLocations("All Locations: ",universe.Map.Locations);
                        break;
                    case MenuAction.LocationsVisited:
                        view.DisplayLocations("Locations Visited: ", player.LocationsVisited);
                        break;
                    case MenuAction.Travel:
                        Travel();
                        break;
                    case MenuAction.LookAround:
                        view.DisplayLocationLookAround(player.CurrentLocation);
                        break;
                    case MenuAction.Exit:
                        playing = false;
                        break;
                    default:
                        break;
                }
            }
        }
        /// <summary>
        /// Pulls up the travel menu and travels the player
        /// </summary>
        private void Travel()
        {
            List<Location> adjacent = universe.Map.AdjacentLocations(player.CurrentLocation);
            string[] locationMenu = TravelMenu.GetLocationMenu(adjacent);

            int locationIndex = view.DrawGetMenuOption(locationMenu);
            if (locationIndex<adjacent.Count)
                player.CurrentLocation = adjacent[locationIndex];
        }
        /// <summary>
        /// Edits the player info, resetting if the unit type is changed
        /// </summary>
        /// <param name="tempPlayer"></param>
        private void EditPlayerInfo(Player tempPlayer)
        {
            if (tempPlayer.Type != Player.UnitType.None)
            {
                tempPlayer.CurrentLocation = player.CurrentLocation;
                player = tempPlayer;
            }
            else
                player.Name = tempPlayer.Name;
        }

        /// <summary>
        /// Initializes the map with starter data
        /// </summary>
        private void InitializeMap()
        {
            universe.Map.AddLocation(Locations.starterFactory);
            universe.Map.AddLocation(Locations.safeStarterArea);
            universe.Map.AddLocation(Locations.syncArea);
            universe.Map.AddLocation(Locations.observationAreaStart);
            universe.Map.AddLocation(Locations.observationAreaShip);
            universe.Map.AddLocation(Locations.observationAreaVillage);
            universe.Map.AddLocation(Locations.observationAreaDwarfOutskirts);

            universe.Map.AddConnection(Locations.starterFactory, Locations.safeStarterArea);
            universe.Map.AddConnection(Locations.safeStarterArea, Locations.syncArea);
            universe.Map.AddConnection(Locations.safeStarterArea, Locations.observationAreaStart);
            universe.Map.AddConnection(Locations.observationAreaStart, Locations.observationAreaShip);
            universe.Map.AddConnection(Locations.observationAreaStart, Locations.observationAreaVillage);
            universe.Map.AddConnection(Locations.observationAreaDwarfOutskirts, Locations.dwarfAreaVillage);

        }
        #endregion
        #region Constructors
        public Controller()
        {
            universe = new Universe();
            view = new ConsoleView();
            Initialize();
        }
        #endregion
    }
}
