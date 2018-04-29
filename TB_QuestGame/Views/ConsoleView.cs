using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TB_QuestGame
{
    public class ConsoleView
    {
        #region Fields
        private const int DEFAULTSCROLLDELAY = 5;
        private const int DEFAULTSCROLLCHARATATIME = 5;

        private const int SCREENWIDTH = 100;
        private const int SCREENHEIGHT = 40;
        Random random;
        Window mainWindow;
        Window statusWindow;
        Window menuWindow;
        Window inputWindow;
        WindowHandler handler = new WindowHandler();
        #endregion
        #region Properties

        #endregion
        #region Methods

        #region Utility Functions
        /// <summary>
        /// Waits the specified number of ms, useful for animations
        /// </summary>
        /// <param name="ms"></param>
        private void Wait(int ms)
        {
            System.Threading.Thread.Sleep(ms);
        }
        /// <summary>
        /// Displays the continue prompt in the input window
        /// </summary>
        private void DisplayContinuePrompt()
        {
            inputWindow.Clear();
            inputWindow.WriteLine("Press any key to continue.");
            DrawChanges();

            inputWindow.Read();

            inputWindow.Clear();
            DrawChanges();
        }
        /// <summary>
        /// Sets the window sizes based on a specific screen
        /// </summary>
        /// <param name="type"></param>
        private void SetWindowSizes(Screens.ScreenType type, bool delay = true)
        {
            while (Screens.SizeWindowStep(type, mainWindow, statusWindow, menuWindow, inputWindow))
            {
                if (delay)
                    DrawChanges();
                Wait(20);
            }

            if (!delay) DrawChanges();
        }
        /// <summary>
        /// Clears the text from the specified window
        /// </summary>
        /// <param name="window"></param>
        private void ClearWindow(Window window)
        {
            window.Clear();
            DrawChanges();
        }
        /// <summary>
        /// Draws the text to the window in the specified color
        /// </summary>
        /// <param name="window"></param>
        /// <param name="text"></param>
        /// <param name="fg"></param>
        /// <param name="bg"></param>
        private void DrawText(Window window, string text, ConsoleColor fg = ConsoleColor.White, ConsoleColor bg = ConsoleColor.Black)
        {
            window.Write(text, fg, bg);
            DrawChanges();
        }
        /// <summary>
        /// Draws the text to the window in the specified color
        /// </summary>
        /// <param name="window"></param>
        /// <param name="text"></param>
        /// <param name="fg"></param>
        /// <param name="bg"></param>
        private void DrawTextLine(Window window, string text, ConsoleColor fg = ConsoleColor.White, ConsoleColor bg = ConsoleColor.Black)
        {
            DrawText(window,TextUtil.Wrap(text, window.Width - 8), fg, bg);
            window.WriteLine("", fg, bg);
            DrawChanges();
        }
        /// <summary>
        /// Slowly scrolls text on the window
        /// </summary>
        /// <param name="window"></param>
        /// <param name="text"></param>
        /// <param name="delayInMs"></param>
        private void DrawScrollingText(Window window, string text,  ConsoleColor fg = ConsoleColor.White, ConsoleColor bg = ConsoleColor.Black, int delayInMs = DEFAULTSCROLLDELAY, int charAtATime = DEFAULTSCROLLCHARATATIME)
        {
            //
            // write to the window a certain amount of characters at a time, not going over string length
            //
            for (int i = 0; i < text.Length; i += charAtATime)
            {
                DrawText(window,text.Substring(i, Math.Min(charAtATime, (text.Length) - i)), fg, bg);
                Wait(delayInMs);
                DrawChanges();
            }
        }
        /// <summary>
        /// slowly scroll text accross the screen, carriage returning after
        /// </summary>
        /// <param name="window"></param>
        /// <param name="text"></param>
        /// <param name="delayInMs"></param>
        /// <param name="fg"></param>
        /// <param name="bg"></param>
        /// <param name="charAtATime"></param>
        private void DrawScrollingTextLine(Window window, string text, ConsoleColor fg = ConsoleColor.White, ConsoleColor bg = ConsoleColor.Black, int delayInMs = DEFAULTSCROLLDELAY, int charAtATime = DEFAULTSCROLLCHARATATIME)
        {
            foreach (string line in text.Split('\n'))
            {
                DrawScrollingText(window, TextUtil.Wrap(line, window.Width - 8), fg, bg, delayInMs, charAtATime);
                DrawTextLine(window, "\n", fg, bg);
            }

        }
        /// <summary>
        /// Draws only the changes to the screen
        /// </summary>
        private void DrawChanges()
        {
            //
            // save old values so we can restore
            //
            int oldCursorLeft = Console.CursorLeft;
            int oldCursorTop = Console.CursorTop;
            Console.CursorVisible = false;

            ConsoleColor currentForeground = Console.ForegroundColor;
            ConsoleColor currentBackground = Console.BackgroundColor;

            foreach (Window.Point change in handler.GetChanges())
            {

                // get char and color at this coordinate
                //
                Window.CharacterData thisConsoleData = handler.GetConsoleDataAt(change.x,change.y);

                //
                // only set the console color if it has changed
                //
                if (thisConsoleData.foreground != currentForeground)
                {
                    Console.ForegroundColor = thisConsoleData.foreground;
                    currentForeground = thisConsoleData.foreground;
                }
                if (thisConsoleData.background != currentBackground)
                {
                    Console.BackgroundColor = thisConsoleData.background;
                    currentBackground = thisConsoleData.background;
                }

                //
                // write the character
                //
                Console.SetCursorPosition(change.x, change.y);
                Console.Write(thisConsoleData.character);
            }

            //
            // restore old values
            //
            Console.SetCursorPosition(oldCursorLeft, oldCursorTop);
            Console.CursorVisible = true;
        }
        /// <summary>
        /// Writes all of the characters to the screen
        /// </summary>
        private void DrawEntireScreen()
        {
            ConsoleColor currentForeground = ConsoleColor.White;
            ConsoleColor currentBackground = ConsoleColor.Black;

            for (int y = 0; y < SCREENHEIGHT; y++)
            {
                for (int x = 0; x < SCREENWIDTH; x++)
                {
                    //
                    // get char and color at this coordinate
                    //
                    Window.CharacterData thisConsoleData = handler.GetConsoleDataAt(x, y);

                    //
                    // only set the console color if it has changed
                    //
                    if (thisConsoleData.foreground != currentForeground)
                    {
                        Console.ForegroundColor = thisConsoleData.foreground;
                        currentForeground = thisConsoleData.foreground;
                    }
                    if (thisConsoleData.background != currentBackground)
                    {
                        Console.BackgroundColor = thisConsoleData.background;
                        currentBackground = thisConsoleData.background;
                    }

                    //
                    // write the character
                    //
                    Console.Write(thisConsoleData.character);
                }
                Console.WriteLine();
            }
        }
        #endregion

        #region Input Functions
        /// <summary>
        /// Draws the specified menu to the specific window, and returns the index of the selected option
        /// Menus are inspired by the Ubuntu Menuing System
        /// </summary>
        /// <param name="window"></param>
        /// <param name="e"></param>
        private int DrawGetOption(Window window,string[] options)
        {
            ConsoleKey key;
            int selected = 0;
            //
            // save the old values, to restore later
            //
            int oldCursorX = window.CursorX;
            int oldCursorY = window.CursorY;

            ClearWindow(inputWindow);
            inputWindow.WriteLine("Input Menu Option");

            do
            {
                window.SetCursorPos(oldCursorX,oldCursorY);
                //
                // display all of the menu options
                //
                for (int i = 0; i < options.Length; i++)
                {
                    if (i == selected)
                        DrawTextLine(window, options[i], fg: ConsoleColor.Black, bg: ConsoleColor.White);
                    else
                        DrawTextLine(window, options[i]);
                }

                //
                // get the menu option from the user
                //
                key = inputWindow.Read();

                switch (key)
                {
                    case ConsoleKey.UpArrow:

                        selected--;

                        if (selected < 0)
                            selected = options.Length - 1;

                        break;
                    case ConsoleKey.DownArrow:

                        selected++;

                        if (selected > options.Length - 1)
                            selected = 0;
                        break;
                    default:
                        break;
                }

            } while (key != ConsoleKey.Enter);


            return selected;
        }
        /// <summary>
        /// Gets the string from the user
        /// </summary>
        /// <param name="feedback"></param>
        private string GetStringFromUser(string caption)
        {
            string userResponse;

            ClearWindow(inputWindow);

            foreach (string line in caption.Split('\n'))
                DrawText(inputWindow, line);

            userResponse = inputWindow.ReadLine();

            ClearWindow(inputWindow);
            return userResponse;
        }
        /// <summary>
        /// Gets an int from user with validation
        /// </summary>
        /// <param name="caption"></param>
        /// <param name="feedback"></param>
        /// <returns></returns>
        private int GetIntFromUser(string caption, string feedback)
        {
            bool validResponse = true;
            int returnValue;
            string userResponse;

            do
            {
                if (validResponse) userResponse = GetStringFromUser(caption);
                else userResponse = GetStringFromUser(feedback + '\n' + caption);
                validResponse = int.TryParse(userResponse, out returnValue);
            } while (!validResponse);

            return returnValue;
        }
        /// <summary>
        /// Gets the user's race with validation
        /// </summary>
        /// <param name="caption"></param>
        /// <param name="feedback"></param>
        /// <returns></returns> 
        private Player.UnitType GetUnitTypeFromUser(string caption, string feedback, bool skippable = false)
        {
            //
            // Get the possible unit types, skipping "None"
            //
            string[] options = Enum.GetNames(typeof(Player.UnitType)).Skip(1).ToArray();

            //
            // get the chosen unit type, adding one to offset the missing "None"
            //
            int unitIndex = DrawGetOption(mainWindow, options) + 1;

            //
            // do the annoying casting necessary to extract the actual type
            //
            return (Player.UnitType)Enum.GetValues(typeof(Player.UnitType)).GetValue(unitIndex);
        }
        #endregion

        #region Specific Screens
        /// <summary>
        /// Draws the initial screen of the game
        /// </summary>
        public void DrawSplashScreen()
        {
            string[] splashLines = Text.splashLines;

            SetWindowSizes(Screens.ScreenType.SplashScreen, delay: false);

            mainWindow.WindowHeader = "Splash Screen";
            ClearWindow(mainWindow);
            
            //
            // show splash screen
            //
            for (int line = 0; line < splashLines.Length; line++)
            {
                mainWindow.SetCursorPos(12, 15 + line);
                DrawText(mainWindow, splashLines[line], ConsoleColor.White);
            }

            Wait(2000);

            mainWindow.WindowHeader = "Main Window";
            ClearWindow(mainWindow);
        }
        /// <summary>
        /// Draws the intro screen to the game
        /// </summary>
        public void DrawIntroScreen()
        {
            int charAtATime = 4;
            int delayInMs = 1;

            SetWindowSizes(Screens.ScreenType.IntroScreen);
            //
            // clear out the window
            //
            mainWindow.WindowHeader = "BOOT SEQUENCE";
            ClearWindow(mainWindow);
            
            //
            // this sets the initial string to be indented 10 characters
            //
            DrawScrollingText(mainWindow, Text.GetRandomHexCharacters((mainWindow.Width - 4) * 2 + 10), fg:ConsoleColor.DarkGray, delayInMs:delayInMs, charAtATime:charAtATime);

            //
            // display the intro text
            //
            foreach (string section in Text.introSections)
            {
                DrawScrollingText(mainWindow, section, delayInMs: delayInMs, charAtATime: charAtATime);

                //
                // this draws exactly 4 lines of characters minus the text line's width
                //
                DrawScrollingText(mainWindow, Text.GetRandomHexCharacters((mainWindow.Width - 4) * 4 - section.Length), fg:ConsoleColor.DarkGray, delayInMs: delayInMs, charAtATime: charAtATime);
            }


            DisplayContinuePrompt();
        }
        /// <summary>
        /// Draws the character set up screen
        /// </summary>
        public Player DrawSetupScreen()
        {
            string playerName;
            Player.UnitType playerType;
            //
            // clear out the window
            //
            SetWindowSizes(Screens.ScreenType.IntroScreen);
            mainWindow.WindowHeader = "Unit Configuration";
            ClearWindow(mainWindow);

            //
            // get player name
            //
            DrawScrollingTextLine(mainWindow, Text.setupGetName);

            playerName = GetStringFromUser("Insert Unit Alias (name)");

            DrawScrollingTextLine(mainWindow, $"Unit alias is now: [ {playerName} ]");
            DrawText(mainWindow, "");

            //
            // get player unit type
            //
            DrawScrollingTextLine(mainWindow,Text.setupGetType);
            playerType = GetUnitTypeFromUser("Insert Unit Type", "Invalid unit type, try again.");

            DrawScrollingTextLine(mainWindow, $"Unit type is now: [ {playerType} ]");
            DrawScrollingTextLine(mainWindow, Text.syncSuccess);

            mainWindow.WindowHeader = "Main Display";
            DisplayContinuePrompt();

            return new Player(playerName, playerType);
        }
        /// <summary>
        /// Displays the player information
        /// </summary>
        /// <param name="player"></param>
        public void DisplayPlayerInfo(Player player)
        {
            ClearWindow(mainWindow);
            mainWindow.ResetCursorPos();

            foreach (string line in Text.GetPlayerInfoText(player))
                DrawScrollingTextLine(mainWindow, line);

            DisplayContinuePrompt();
        }
        /// <summary>
        /// Displays all locations passed
        /// </summary>
        /// <param name="locations"></param>
        public void DisplayLocations(string title, List<Location> locations)
        {
            mainWindow.Clear();

            DrawScrollingTextLine(mainWindow, title);
            foreach (string line in Text.GetLocationListText(locations))
                DrawScrollingTextLine(mainWindow,line);

            DisplayContinuePrompt();
        }
        /// <summary>
        /// Displays all game objects passed
        /// </summary>
        /// <param name="locations"></param>
        public void DisplayGameObjects(string title, List<GameObject> gameObjects)
        {
            mainWindow.Clear();

            DrawScrollingTextLine(mainWindow, title);

            foreach (string line in Text.GetGameObjectListText(gameObjects))
                DrawScrollingTextLine(mainWindow, line);

            DisplayContinuePrompt();
        }
        /// <summary>
        /// Displays all npcs passed
        /// </summary>
        /// <param name="locations"></param>
        public void DisplayNpcs(string title, List<Npc> npcs)
        {
            mainWindow.Clear();

            DrawScrollingTextLine(mainWindow, title);

            foreach (string line in Text.GetNpcListText(npcs))
                DrawScrollingTextLine(mainWindow, line);

            DisplayContinuePrompt();
        }
        /// <summary>
        /// Displays the editing player information screen
        /// </summary>
        /// <param name="player"></param>
        public Player DisplayEditPlayerInfo(Player player)
        {
            string newAlias;
            Player.UnitType type;

            ClearWindow(mainWindow);

            //
            // get the new character name
            //
            DrawScrollingTextLine(mainWindow, "Current Unit Alias: " + player.Name);
            DrawScrollingTextLine(mainWindow, "Input a new alias, or press enter to skip");

            newAlias = GetStringFromUser("Input a new alias");

            if (newAlias == "")
                newAlias = player.Name;

            DrawText(mainWindow, "");

            //
            // get the new character type
            //
            foreach (string line in Text.GetEditPlayerTypeText(player))
                DrawScrollingTextLine(mainWindow, line);

            type = GetUnitTypeFromUser("Input a new unit type", "Invalid unit type, try again.", skippable: true);

            player = new Player(newAlias, type);

            DrawScrollingTextLine(mainWindow, Text.syncSuccess);

            DisplayContinuePrompt();

            return player;
        }
        /// <summary>
        /// Displays the text for the player looking around
        /// </summary>
        /// <param name="location"></param>
        public void DisplayLocationLookAround(Universe universe, Location location)
        {
            ClearWindow(mainWindow);
            mainWindow.ResetCursorPos();

            DrawScrollingTextLine(mainWindow, "Location Contents\n");

            if (universe.GetObjectsAtLocation(location).Count > 0)
            {
                DrawScrollingTextLine(mainWindow, "Objects");

                foreach (GameObject gameObject in universe.GetObjectsAtLocation(location))
                {
                    DrawScrollingTextLine(mainWindow, "    " + gameObject.Name);
                }

                DrawTextLine(mainWindow, "");
            }
            else
            {
                DrawScrollingTextLine(mainWindow, "No Notable Objects in Area");
                DrawTextLine(mainWindow, "");
            }

            if (universe.GetNpcsAtLocation(location).Count > 0)
            {
                DrawScrollingTextLine(mainWindow, "Entities");

                foreach (Npc npc in universe.GetNpcsAtLocation(location))
                {
                    DrawScrollingTextLine(mainWindow, "    " + npc.Name);
                }

                DrawTextLine(mainWindow, "");
            }
            else
            {
                DrawScrollingTextLine(mainWindow, "No Notable Entities in Area");
                DrawTextLine(mainWindow, "");
            }

            DisplayContinuePrompt();
        }
        /// <summary>
        /// Displays a menu for interacting with abilities
        /// </summary>
        /// <param name="abilities"></param>
        /// <returns></returns>
        public Ability DisplayInteractAbilities(List<Ability> abilities, bool allowCancel=true)
        {
            int abilityIndex = 0;

            //
            // we use an extra list here to make sure there is a consistent mapping of string->ability
            //
            List<string> abilityNames = new List<string>();

            ClearWindow(mainWindow);
            mainWindow.ResetCursorPos();

            if (abilities.Count > 0)
            {
                DrawScrollingTextLine(mainWindow, "User Abilities");

                foreach (Ability ability in abilities)
                    abilityNames.Add(ability.Name);

                if (allowCancel)
                    abilityNames.Add("Back");

                //
                // turn the list of game objects into a menu window to select an object
                //
                abilityIndex = DrawGetOption(mainWindow, abilityNames.ToArray());

                //
                // if they hit back, return null
                //
                if (abilityIndex < abilities.Count)
                    return abilities[abilityIndex];
                else
                    return null;
            }
            else
            {
                //
                // if there are no objects, dont display a menu, just return
                //
                DrawScrollingTextLine(mainWindow, "Analysis: No abilities gained yet");

                DisplayContinuePrompt();

                return null;
            }
        }
        /// <summary>
        /// Draws a screen to select an npc to interact with
        /// </summary>
        /// <param name="universe"></param>
        /// <param name="location"></param>
        /// <returns></returns>
        public Npc DisplayInteractNpc(Universe universe, Location location)
        {
            int npcIndex;

            //
            // we use two lists here to make sure there is a consistent mapping of string->npc
            //
            List<string> npcNames = new List<string>();
            List<Npc> npcs = universe.GetNpcsAtLocation(location);

            ClearWindow(mainWindow);
            mainWindow.ResetCursorPos();

            if (npcs.Count > 0)
            {
                DrawScrollingTextLine(mainWindow, "Location Entities");

                foreach (Npc npc in npcs)
                    npcNames.Add(npc.Name);
                npcNames.Add("Back");
                //
                // turn the list of npcs into a menu window to select an npc
                //
                npcIndex = DrawGetOption(mainWindow, npcNames.ToArray());

                if (npcIndex < npcs.Count)
                    return npcs[npcIndex];
                else
                    return null;
            }
            else
            {
                //
                // if there are no npcs, dont display a menu, just return
                //
                DrawScrollingTextLine(mainWindow, "Analysis: No entities in this area");

                DisplayContinuePrompt();

                return null;
            }
        }
        /// <summary>
        /// Draws a screen to select an object to interact with
        /// </summary>
        /// <param name="universe"></param>
        /// <param name="location"></param>
        /// <returns></returns>
        public GameObject DisplayInteractObject(Universe universe, Location location)
        {
            int gameObjectIndex;

            //
            // we use two lists here to make sure there is a consistent mapping of string->gameobject
            //
            List<string> gameObjectNames = new List<string>();
            List<GameObject> gameObjects = universe.GetObjectsAtLocation(location);

            ClearWindow(mainWindow);
            mainWindow.ResetCursorPos();

            if (gameObjects.Count>0)
            {
                DrawScrollingTextLine(mainWindow, "Location Contents");

                foreach (GameObject gameObject in gameObjects)
                    gameObjectNames.Add(gameObject.Name);
                gameObjectNames.Add("Back");
                //
                // turn the list of game objects into a menu window to select an object
                //
                gameObjectIndex = DrawGetOption(mainWindow, gameObjectNames.ToArray());

                if (gameObjectIndex < gameObjects.Count)
                    return gameObjects[gameObjectIndex];
                else
                    return null;
            }
            else
            {
                //
                // if there are no objects, dont display a menu, just return
                //
                DrawScrollingTextLine(mainWindow, "Analysis: No notable objects in this area");
                
                DisplayContinuePrompt();

                return null;
            }
        }
        /// <summary>
        /// Displays a screen of chat with an npc
        /// </summary>
        /// <param name="gameObject"></param>
        public void DisplayTalkToNpc(Npc npc)
        {
            ClearWindow(mainWindow);
            mainWindow.ResetCursorPos();

            foreach (string line in Text.GetNpcTalkText(npc))
            {
                DrawScrollingTextLine(mainWindow, line);
            }

            DisplayContinuePrompt();
        }
        /// <summary>
        /// Displays a screen of battle with an npc
        /// </summary>
        public void DisplayBattleNpc(Npc npc, string battleText)
        {
            ClearWindow(mainWindow);
            mainWindow.ResetCursorPos();

            foreach (string line in Text.GetNpcBattleText(npc, battleText))
            {
                DrawScrollingTextLine(mainWindow, line);
            }

            DisplayContinuePrompt();
        }
        /// <summary>
        /// Displays the text from the ability proccing
        /// </summary>
        public void DisplayAbilityText(string abilityText)
        {
            ClearWindow(mainWindow);
            mainWindow.ResetCursorPos();
            
            DrawScrollingTextLine(mainWindow, "Observation: " + abilityText);

            DisplayContinuePrompt();
        }
        /// <summary>
        /// Displays an analysis of an object
        /// </summary>
        /// <param name="gameObject"></param>
        public void DisplayObjectAnalysis(GameObject gameObject)
        {
            ClearWindow(mainWindow);
            mainWindow.ResetCursorPos();

            foreach (string line in Text.GetObjectDescriptionText(gameObject))
            {
                DrawScrollingTextLine(mainWindow, line);
            }

            foreach (string line in Text.GetAnalysisText())
            {
                DrawScrollingTextLine(mainWindow, line, charAtATime:1, delayInMs: 50);
            }

            DisplayContinuePrompt();
        }
        /// <summary>
        /// Displays a look-at description of an object
        /// </summary>
        /// <param name="gameObject"></param>
        public void DisplayObjectDescription(GameObject gameObject)
        {
            ClearWindow(mainWindow);
            mainWindow.ResetCursorPos();

            foreach (string line in Text.GetObjectDescriptionText(gameObject))
            {
                DrawScrollingTextLine(mainWindow, line);
            }

            DisplayContinuePrompt();
        }
        /// <summary>
        /// Draws the locations description to the screen
        /// </summary>
        public void DisplayLocationDescription(Location location)
        {
            SetWindowSizes(Screens.ScreenType.MainScreen);
            ClearWindow(mainWindow);
            mainWindow.ResetCursorPos();

            DrawScrollingTextLine(mainWindow, location.Description);
        }
        /// <summary>
        /// Draws the status to the status window
        /// </summary>
        /// <param name="status"></param>
        public void DisplayStatus(string[] status)
        {
            ClearWindow(statusWindow);

            foreach (string line in status)
                DrawTextLine(statusWindow, line);

        }
        /// <summary>
        /// Draws the specified menu to the specific screen, and returns the index of the selected option
        /// Menus are inspired by the Ubuntu Menuing System
        /// </summary>
        /// <param name="window"></param>
        /// <param name="e"></param>
        public int DrawGetMenuOption(string[] options)
        {
            ClearWindow(menuWindow);
            return DrawGetOption(menuWindow, options);
        }

        #endregion

        #endregion
        #region Constructors
        public ConsoleView()
        {
            //
            // set up the console
            //
            Console.WindowWidth = SCREENWIDTH+5;
            Console.WindowHeight = SCREENHEIGHT+3;

            //
            // seed the random number generator
            //
            random = new Random();

            //
            // create the windows
            //
            mainWindow = new Window(1, 1, 99, 40);
            mainWindow.WindowHeader = "Main";

            statusWindow = new Window(71, 1, 29, 20);
            statusWindow.WindowHeader = "Status";


            menuWindow = new Window(71, 21, 29, 20);
            menuWindow.WindowHeader = "Menu";

            inputWindow = new Window(1, 35, 99, 6);
            inputWindow.WindowHeader = "Input";

            //
            // give them to the handler
            //
            handler.AddWindow(mainWindow, inputWindow, statusWindow, menuWindow);
        }
        #endregion
    }
}
