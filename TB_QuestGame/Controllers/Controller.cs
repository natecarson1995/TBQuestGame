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
        Map map;
        Player player;
        #endregion
        #region Properties

        #endregion
        #region Methods
        private void ManageApplicationLoop()
        {
            //view.DrawSplashScreen();
            //view.DrawIntroScreen();
            player = view.DrawSetupScreen();
            view.SetWindowSizesToDefault();

            player.CurrentLocation = Locations.location1;

            ManageMenu();
        }
        private void ManageMenu()
        {
            bool playing = true;
            MenuAction action;

            while (playing)
            {
                view.DisplayLocationDescription(player.CurrentLocation.Description);
                view.DisplayStatus(Text.GetPlayerStatus(player));

                action = view.DrawMenu(MainMenu.actions);

                switch (action)
                {
                    case MenuAction.None:
                        break;
                    case MenuAction.PlayerInfo:
                        view.DisplayPlayerInfo(player);
                        break;
                    case MenuAction.PlayerEdit:
                        Player tempPlayer = view.DisplayEditPlayerInfo(player);
                        EditPlayerInfo(tempPlayer);
                        break;
                    case MenuAction.ListLocations:
                        view.DisplayAllLocations(map.Locations);
                        break;
                    case MenuAction.Travel:
                        Travel();
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
            Location location = view.DisplayGetTravelLocation(TravelMenu.GetLocationActions(map.AdjacentLocations(player.CurrentLocation)));
            player.CurrentLocation = location;
        }
        /// <summary>
        /// Edits the player info, resetting if the unit type is changed
        /// </summary>
        /// <param name="tempPlayer"></param>
        private void EditPlayerInfo(Player tempPlayer)
        {
            if (tempPlayer.Type != Player.UnitType.None)
                player = tempPlayer;
            else
                player.Name = tempPlayer.Name;
        }
        #endregion
        #region Constructors
        public Controller()
        {
            map = new Map();

            //
            // Add sample data
            //
            map.AddLocation(Locations.location1);
            map.AddLocation(Locations.location2);
            map.AddConnection(Locations.location1, Locations.location2);

            view = new ConsoleView();
            ManageApplicationLoop();
        }
        #endregion
    }
}
