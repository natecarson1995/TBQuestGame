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
        private bool playing;
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
            playing = true;
            view.DrawSplashScreen();
            view.DrawIntroScreen();
            player = view.DrawSetupScreen();
            player.CurrentQuest = "Interface with the Guide Sentinel";
            player.CurrentLocation = Locations.starterFactory;

            Locations.AddLocations(universe, player);
            Npcs.AddNpcs(universe, player);
            GameObjects.AddGameObjects(universe, player);
            Abilities.AddAbilities(universe, player);

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
        }
        /// <summary>
        /// Does the core game loop
        /// </summary>
        private void ManageGameLoop()
        {
            while (playing)
            {
                UpdateGameStatus();

                view.DisplayLocationDescription(player.CurrentLocation);
                view.DisplayStatus(Text.GetPlayerStatus(player));

                HandleMainMenu();
            }
        }
        /// <summary>
        /// Does everything for the main menu
        /// </summary>
        private void HandleMainMenu()
        {
            int index = view.DrawGetMenuOption(ActionMenu.GetEnumMenu(typeof(MenuAction)));
            MenuAction action = (MenuAction)Enum.GetValues(typeof(MenuAction)).GetValue(index);

            switch (action)
            {
                case MenuAction.LocationsVisited:
                    view.DisplayLocations("Locations Visited: ", player.LocationsVisited);
                    break;
                case MenuAction.Travel:
                    Travel();
                    break;
                case MenuAction.LookAround:
                    view.DisplayLocationLookAround(universe,player.CurrentLocation);
                    break;
                case MenuAction.Interact:
                    HandleInteractionMenu();
                    break;
                case MenuAction.InterfaceWith:
                    HandleEntityMenu();
                    break;
                case MenuAction.Abilities:
                    HandleAbilityMenu();
                    break;
                case MenuAction.AdminMenu:
                    HandleAdminMenu();
                    break;
                case MenuAction.Exit:
                    playing = false;
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// Does everything for the admin submenu
        /// </summary>
        private void HandleAdminMenu()
        {
            int index = view.DrawGetMenuOption(ActionMenu.GetEnumMenu(typeof(AdminMenuAction)));
            AdminMenuAction action = (AdminMenuAction)Enum.GetValues(typeof(AdminMenuAction)).GetValue(index);

            switch (action)
            {
                case AdminMenuAction.PlayerInfo:
                    view.DisplayPlayerInfo(player);
                    break;
                case AdminMenuAction.PlayerEdit:
                    Player tempPlayer = view.DisplayEditPlayerInfo(player);
                    EditPlayerInfo(tempPlayer);
                    break;
                case AdminMenuAction.ListGameObjects:
                    view.DisplayGameObjects("All Game Objects: ", universe.GameObjects);
                    break;
                case AdminMenuAction.ListNpcs:
                    view.DisplayNpcs("All Npcs: ", universe.Npcs);
                    break;
                case AdminMenuAction.ListLocations:
                    view.DisplayLocations("All Locations: ", universe.Map.Locations);
                    break;
                case AdminMenuAction.Back:
                    return;
            }
        }
        /// <summary>
        /// Pulls up the ability menu, and handles that interaction
        /// </summary>
        private void HandleAbilityMenu()
        {
            Ability ability = view.DisplayInteractAbilities(player.Abilities);

            //
            // if not null, procs the ability
            //
            ability?.Proc();

            if (ability != null && ability.ProcText != "")
                view.DisplayAbilityText(ability.ProcText);

        }
        /// <summary>
        /// Pulls up the entity menu, and handles that interaction
        /// </summary>
        private void HandleEntityMenu()
        {
            int entityIndex = 0;
            Npc entity = view.DisplayInteractNpc(universe, player.CurrentLocation);
            //
            // in case the location is empty
            //
            if (entity != null)
            {
                entityIndex = view.DrawGetMenuOption(ActionMenu.GetEnumMenu(typeof(NpcMenuAction)));
                NpcMenuAction action = (NpcMenuAction)Enum.GetValues(typeof(NpcMenuAction)).GetValue(entityIndex);
                HandleNpcInteraction(entity, action);
            }
        }
        /// <summary>
        /// Processes a specific interaction on a specific npc
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="action"></param>
        private void HandleNpcInteraction(Npc npc, NpcMenuAction action)
        {
            switch (action)
            {
                case NpcMenuAction.TalkTo:
                    view.DisplayTalkToNpc(npc);
                    break;
                case NpcMenuAction.Battle:
                    if (npc is IBattle)
                        HandleNpcBattle(npc);
                    else
                        view.DisplayBattleNpc(npc, "");
                    break;
                case NpcMenuAction.Back:
                    break;
                default:
                    break;
            }
        }
        private void HandleNpcBattle(Npc npc)
        {
            IBattle battleInterface = npc as IBattle;

            while (player.Health > 0 && npc.Health > 0)
            {
                view.DisplayStatus(Text.GetBattleStatus(player, npc));

                Ability ability = view.DisplayInteractAbilities(player.Abilities, allowCancel: false);
                //
                // if not null, procs the ability
                //
                ability?.Proc(npc);
                view.DisplayStatus(Text.GetBattleStatus(player, npc));

                if (ability != null && ability.ProcText!=null && ability.ProcText != "")
                    view.DisplayAbilityText(ability.ProcText);

                if (npc.Health>0)
                    battleInterface.Battle(player);

                view.DisplayStatus(Text.GetBattleStatus(player, npc));
            }

            if (player.Health<=0)
            {
                player.Damage(player.Health);
                view.DisplayBattleNpc(npc, "Observation: Unit critically damaged.\nRecommended Action: " +
                    "Return to sync zone for repair.");
            }
            else if (npc.Health<=0)
            {
                view.DisplayBattleNpc(npc, battleInterface.GetLossText());
            }
        }
        /// <summary>
        /// Pulls up the interaction menu, and handles that interaction
        /// </summary>
        private void HandleInteractionMenu()
        {
            int interactionIndex = 0;
            GameObject gameObject = view.DisplayInteractObject(universe, player.CurrentLocation);
            
            //
            // in case the location is empty
            //
            if (gameObject!=null)
            {
                interactionIndex = view.DrawGetMenuOption(ActionMenu.GetEnumMenu(typeof(InteractionMenuAction)));
                InteractionMenuAction action = (InteractionMenuAction)Enum.GetValues(typeof(InteractionMenuAction)).GetValue(interactionIndex);
                HandleObjectInteraction(gameObject, action);
            }
        }
        /// <summary>
        /// Processes a specific interaction on a specific object
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="action"></param>
        private void HandleObjectInteraction(GameObject gameObject, InteractionMenuAction action)
        {
            switch (action)
            {
                case InteractionMenuAction.LookAt:
                    view.DisplayObjectDescription(gameObject);
                    break;
                case InteractionMenuAction.Analyze:
                    view.DisplayObjectAnalysis(gameObject);
                    gameObject.Analyze();
                    break;
                case InteractionMenuAction.Back:
                    break;
                default:
                    break;
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
            player.CurrentLocation.TravelTo();
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
