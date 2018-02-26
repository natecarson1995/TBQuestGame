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
            ManageMenu();
        }
        private void ManageMenu()
        {
            bool playing = true;

            while (playing)
            {
                MainMenuAction action = view.DrawMainMenu();
            
                switch (action)
                {
                    case MainMenuAction.None:
                        break;
                    case MainMenuAction.PlayerInfo:
                        view.DisplayPlayerInfo(player);
                        break;
                    case MainMenuAction.PlayerEdit:
                        view.DisplayEditPlayerInfo(ref player);
                        break;
                    case MainMenuAction.Exit:
                        playing = false;
                        break;
                    default:
                        break;
                }
            }
        }
        #endregion
        #region Constructors
        public Controller()
        {
            view = new ConsoleView();
            ManageApplicationLoop();
        }
        #endregion
    }
}
