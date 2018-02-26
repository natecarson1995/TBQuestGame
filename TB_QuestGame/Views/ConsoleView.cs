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
        /// Sets all window sizes to their defaults for the game
        /// </summary>
        public void SetWindowSizesToDefault()
        {
            for (int width = mainWindow.Width; width >= 79; width -= 2)
            {
                mainWindow.SetWindowSize(width, 34);
                inputWindow.SetWindowSize(width, 6);
                DrawChanges();
            }
            menuWindow.SetWindowSize(19, 20);
            statusWindow.SetWindowSize(19, 20);
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

            ConsoleColor currentForeground = ConsoleColor.Black;
            ConsoleColor currentBackground = ConsoleColor.Black;

            foreach (var change in handler.GetChanges())
            {

                // get char and color at this coordinate
                //
                Window.ConsolePoint thisConsoleData = handler.GetConsoleDataAt(change.Item1, change.Item2);

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
                Console.SetCursorPosition(change.Item1, change.Item2);
                Console.Write(thisConsoleData.character);
            }

            //
            // restore old values
            //
            Console.SetCursorPosition(oldCursorLeft, oldCursorTop);
            Console.CursorVisible = true;
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
            foreach (string line in text.Split('\n'))
                window.WriteLine(line, fg, bg);
            DrawChanges();
        }
        /// <summary>
        /// Draws the text to the window in the specified color, and carriage returns
        /// </summary>
        /// <param name="window"></param>
        /// <param name="text"></param>
        /// <param name="fg"></param>
        /// <param name="bg"></param>
        private void DrawTextLine(Window window, string text, ConsoleColor fg = ConsoleColor.White, ConsoleColor bg = ConsoleColor.Black)
        {
            foreach (string line in text.Split('\n'))
                window.WriteLine(line, fg, bg);
            DrawChanges();
        }
        /// <summary>
        /// Slowly scrolls text on the window
        /// </summary>
        /// <param name="window"></param>
        /// <param name="text"></param>
        /// <param name="delayInMs"></param>
        private void DrawScrollingText(Window window, string text, int delayInMs = 5, ConsoleColor fg = ConsoleColor.White, ConsoleColor bg = ConsoleColor.Black, int charAtATime = 1)
        {
            //
            // write to the window a certain amount of characters at a time, not going over string length
            //
            for (int i = 0; i < text.Length; i += charAtATime)
            {
                window.Write(text.Substring(i, Math.Min(charAtATime, (text.Length) - i)), fg, bg);
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
        private void DrawScrollingTextLine(Window window, string text, int delayInMs = 5, ConsoleColor fg = ConsoleColor.White, ConsoleColor bg = ConsoleColor.Black, int charAtATime = 1)
        {
            foreach (string line in text.Split('\n'))
            {
                DrawScrollingText(window, line, delayInMs, fg, bg, charAtATime);
                DrawTextLine(window, "");
            }

            DrawTextLine(window, "");
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
                    Window.ConsolePoint thisConsoleData = handler.GetConsoleDataAt(x, y);

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
        /// <summary>
        /// Draws the specified menu to the specified window
        /// </summary>
        /// <param name="window"></param>
        /// <param name="e"></param>
        private Enum DrawGetMenuOption(Type e)
        {
            int counter = 1;
            int menuChoiceInt = 0;

            ClearWindow(menuWindow);

            //
            // display all of the menu options
            //
            foreach (string value in e.GetEnumNames())
            {
                if (value == "None") continue;

                DrawTextLine(menuWindow, $"{counter}. {value}");
                counter++;
            }

            //
            // get the menu option from the user
            //
            while (menuChoiceInt < 1 || menuChoiceInt > counter)
                menuChoiceInt = GetIntFromUser("Input menu option", "Must input an integer for the menu option");

            ClearWindow(menuWindow);
            ClearWindow(inputWindow);

            return (Enum)e.GetEnumValues().GetValue(menuChoiceInt);
        }
        #endregion

        #region Input Functions
        /// <summary>
        /// Gets the string from the user
        /// </summary>
        /// <param name="feedback"></param>
        private string GetStringFromUser(string caption)
        {
            string userResponse;

            ClearWindow(inputWindow);

            foreach (string line in caption.Split('\n'))
                DrawTextLine(inputWindow, line);

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
            bool validInput = true;
            Player.UnitType returnValue;
            string userResponse;

            do
            {
                if (validInput)
                    userResponse = GetStringFromUser(caption);
                else
                    userResponse = GetStringFromUser(caption + "\n" + feedback);

                if (skippable && userResponse == "")
                    return Player.UnitType.None;

                validInput = Enum.TryParse<Player.UnitType>(Text.ToTitleCase(userResponse), out returnValue);
            } while (!validInput);

            return returnValue;
        }
        #endregion

        #region Specific Screens
        /// <summary>
        /// Draws the initial screen of the game
        /// </summary>
        public void DrawSplashScreen()
        {
            mainWindow.SetWindowSize(99, 40);
            inputWindow.SetWindowSize(99, 6);

            mainWindow.WindowHeader = "Splash Screen";
            DrawChanges();

            string[] splashLines = Text.splashLines;

            //
            // show splash screen
            //
            for (int line = 0; line < splashLines.Length; line++)
            {
                mainWindow.SetCursorPos(12, 15 + line);
                DrawTextLine(mainWindow, splashLines[line], ConsoleColor.White);
            }

            Wait(2500);

            mainWindow.WindowHeader = "Main Window";
            ClearWindow(mainWindow);

            //
            // do closing animation
            //
            for (int i = 5; i >= 0; i--)
            {
                mainWindow.SetWindowSize(99, 34 + i);
                DrawChanges();
            }
        }
        /// <summary>
        /// Draws the intro screen to the game
        /// </summary>
        public void DrawIntroScreen()
        {
            int textSpeed = 15;
            //
            // clear out the window
            //
            mainWindow.Clear();
            DrawChanges();

            //
            // this sets the initial string to be indented 10 characters
            //
            DrawScrollingText(mainWindow, Text.GetRandomHexCharacters((mainWindow.Width - 4) * 2 + 10), 15, ConsoleColor.DarkGray, charAtATime: textSpeed);

            //
            // display the intro text
            //
            foreach (string section in Text.introSections)
            {
                DrawScrollingText(mainWindow, section, 25, charAtATime: textSpeed / 3);

                //
                // this draws exactly 4 lines of characters minus the text line's width
                //
                DrawScrollingText(mainWindow, Text.GetRandomHexCharacters((mainWindow.Width - 4) * 4 - section.Length), 15, ConsoleColor.DarkGray, charAtATime: textSpeed);
            }


            DisplayContinuePrompt();
        }
        /// <summary>
        /// Draws the character set up screen
        /// </summary>
        public void DrawSetupScreen(out Player player)
        {
            string playerName;
            Player.UnitType playerType;
            //
            // clear out the window
            //
            mainWindow.WindowHeader = "Unit Configuration";
            ClearWindow(mainWindow);

            //
            // get player name
            //
            mainWindow.SetCursorPos(2, 5);
            DrawScrollingTextLine(mainWindow, "Unit's hardware number is " + Text.GetRandomHexCharacters(8));
            DrawScrollingTextLine(mainWindow, "An alias is recommended for quick recall, please insert this now.");

            playerName = GetStringFromUser("Insert Unit Alias (name)");

            DrawScrollingTextLine(mainWindow, $"Unit alias is now: [ {playerName} ]");
            DrawTextLine(mainWindow, "");

            //
            // get player unit type
            //
            DrawTextLine(mainWindow, "");
            DrawScrollingTextLine(mainWindow, "Unit may be configured in one specialty.\n");
            DrawScrollingTextLine(mainWindow, "These specialties include: ");
            DrawScrollingTextLine(mainWindow, "Processing\nManufacturing\nCombat");
            DrawTextLine(mainWindow, "");
            playerType = GetUnitTypeFromUser("Insert Unit Type", "Invalid unit type, try again.");

            DrawScrollingTextLine(mainWindow, $"Unit type is now: [ {playerType} ]");
            DrawScrollingTextLine(mainWindow, "\nNew configuration settings have been synchronized with the cluster.");

            player = new Player(playerName, playerType);
            DisplayContinuePrompt();

        }
        /// <summary>
        /// Displays the player information
        /// </summary>
        /// <param name="player"></param>
        public void DisplayPlayerInfo(Player player)
        {
            ClearWindow(mainWindow);
            mainWindow.SetCursorPos(2, 5);

            DrawScrollingTextLine(mainWindow, "Unit Alias: " + player.Name);
            DrawScrollingTextLine(mainWindow, "Unit Level: " + player.Level);
            DrawScrollingTextLine(mainWindow, "Unit Cluster: " + player.ClusterId);
            DrawTextLine(mainWindow, "");
            DrawScrollingTextLine(mainWindow, "Unit Combat Ability: " + player.CombatCapability);
            DrawScrollingTextLine(mainWindow, "Unit Average Manufacturing Time: " + player.ManufacturingTime);
            DrawScrollingTextLine(mainWindow, "Unit Average Processing Time: " + player.ProcessingTime);

            DisplayContinuePrompt();
        }
        /// <summary>
        /// Displays the editing player information screen
        /// </summary>
        /// <param name="player"></param>
        public void DisplayEditPlayerInfo(ref Player player)
        {
            string newAlias;
            Player.UnitType type;

            ClearWindow(mainWindow);
            mainWindow.SetCursorPos(2, 5);

            //
            // get the new character name
            //
            DrawScrollingTextLine(mainWindow, "Current Unit Alias: " + player.Name);
            DrawScrollingTextLine(mainWindow, "Input a new alias, or press enter to skip");
            newAlias = GetStringFromUser("Input a new alias");

            if (newAlias != "")
                newAlias = player.Name;

            DrawTextLine(mainWindow, "");

            //
            // get the new character type
            //
            DrawScrollingTextLine(mainWindow, "Current Unit Type: " + player.Type);
            DrawScrollingTextLine(mainWindow, "Input a new unit type, or press enter to skip");
            DrawScrollingTextLine(mainWindow, "\nThese unit types include: ");
            DrawScrollingTextLine(mainWindow, "Processing\nManufacturing\nCombat");
            type = GetUnitTypeFromUser("Input a new unit type", "Invalid unit type, try again.", skippable: true);

            if (type != Player.UnitType.None)
                player = new Player(newAlias, type);
            else
                player = new Player(newAlias, player.Type);

            DrawScrollingTextLine(mainWindow, "\nNew configuration settings have been synchronized with the cluster.");

            DisplayContinuePrompt();
        }
        /// <summary>
        /// Draws the main menu screen
        /// </summary>
        public MainMenuAction DrawMainMenu()
        {
            mainWindow.WindowHeader = "Main Window";
            ClearWindow(mainWindow);
            SetWindowSizesToDefault();

            foreach (string line in Text.mainMenuScreen)
                DrawScrollingTextLine(mainWindow, line);

            MainMenuAction m = (MainMenuAction)DrawGetMenuOption(typeof(MainMenuAction));

            return m;
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
            mainWindow = new Window(1, 1, 80, 34);
            mainWindow.WindowHeader = "Main";

            statusWindow = new Window(81, 1, 19, 20);
            statusWindow.WindowHeader = "Status";


            menuWindow = new Window(81, 21, 19, 20);
            menuWindow.WindowHeader = "Menu";

            inputWindow = new Window(1, 35, 80, 6);
            inputWindow.WindowHeader = "Input";

            //
            // give them to the handler
            //
            handler.AddWindow(mainWindow, inputWindow, statusWindow, menuWindow);
        }
        #endregion
    }
}
