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
        /// Sets all window sizes to their defaults for the game
        /// </summary>
        public void SetWindowSizesToDefault()
        {
            //
            // this will make the initial resize smoother
            //
            if (mainWindow.Width > 69)
            {
                for (int width = mainWindow.Width; width >= 69; width -= 2)
                {
                    mainWindow.SetWindowSize(width, 34);
                    inputWindow.SetWindowSize(width, 6);
                    DrawChanges();
                }
            }
            else
            {
                mainWindow.SetWindowSize(69, 34);
                inputWindow.SetWindowSize(69, 6);
            }
            menuWindow.SetWindowSize(29, 20);
            statusWindow.SetWindowSize(29, 20);
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
        private void DrawScrollingText(Window window, string text,  ConsoleColor fg = ConsoleColor.White, ConsoleColor bg = ConsoleColor.Black, int delayInMs = DEFAULTSCROLLDELAY, int charAtATime = DEFAULTSCROLLCHARATATIME)
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
        private void DrawScrollingTextLine(Window window, string text, ConsoleColor fg = ConsoleColor.White, ConsoleColor bg = ConsoleColor.Black, int delayInMs = DEFAULTSCROLLDELAY, int charAtATime = DEFAULTSCROLLCHARATATIME)
        {
            foreach (string line in text.Split('\n'))
            {
                DrawScrollingText(window, line, fg, bg, charAtATime, delayInMs);
                DrawTextLine(window, "");
            }

            DrawTextLine(window, "");
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

            Wait(2000);

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
            int charAtATime = 4;
            int delayInMs = 1;
            //
            // clear out the window
            //
            mainWindow.WindowHeader = "BOOT SEQUENCE";
            mainWindow.Clear();
            DrawChanges();

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
        /// Displays the editing player information screen
        /// </summary>
        /// <param name="player"></param>
        public Player DisplayEditPlayerInfo(Player player)
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

            if (newAlias == "")
                newAlias = player.Name;

            DrawTextLine(mainWindow, "");

            //
            // get the new character type
            //
            foreach (string line in Text.GetEditPlayerTypeText(player))
                DrawScrollingTextLine(mainWindow, line);

            type = GetUnitTypeFromUser("Input a new unit type", "Invalid unit type, try again.", skippable: true);

            player = new Player(newAlias, type);

            DrawScrollingTextLine(mainWindow, "\nNew configuration settings have been synchronized\n with the cluster.");

            DisplayContinuePrompt();

            return player;
        }
        
        /// <summary>
        /// Displays the text for the player looking around
        /// </summary>
        /// <param name="location"></param>
        public void DisplayLocationLookAround(Location location)
        {
            ClearWindow(mainWindow);
            mainWindow.ResetCursorPos();

            DrawScrollingTextLine(mainWindow, "Location Contents");
            DrawScrollingTextLine(mainWindow, location.Contents);

            DisplayContinuePrompt();
        }
        /// <summary>
        /// Draws the locations description to the screen
        /// </summary>
        public void DisplayLocationDescription(Location location)
        {
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
        /// Draws the specified menu to the menu screen, and returns the index of the selected option
        /// Menus are inspired by the Ubuntu Menuing System
        /// </summary>
        /// <param name="window"></param>
        /// <param name="e"></param>
        public int DrawGetMenuOption(string[] options)
        {
            ConsoleKey key;
            int selected = 0;

            ClearWindow(inputWindow);
            ClearWindow(menuWindow);

            inputWindow.WriteLine("Input Menu Option");

            do
            {
                menuWindow.ResetCursorPos();
                //
                // display all of the menu options
                //
                for (int i = 0; i < options.Length; i++)
                {
                    if (i == selected)
                        DrawTextLine(menuWindow, options[i], fg: ConsoleColor.Black, bg: ConsoleColor.White);
                    else
                        DrawTextLine(menuWindow, options[i]);
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
            mainWindow = new Window(1, 1, 99, 34);
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
