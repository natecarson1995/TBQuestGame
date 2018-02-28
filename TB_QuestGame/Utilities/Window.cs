using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TB_QuestGame
{
    public class Window
    {
        #region Structs
        public struct ConsolePoint
        {
            public int x;
            public int y;
            public ConsoleColor foreground;
            public ConsoleColor background;
            public char character;
        }
        #endregion
        #region Fields

        private ConsolePoint[][] display;
        private List<Tuple<int, int>> changes;
        private int x;
        private int y;
        private int width;
        private int height;
        private int cursorX;
        private int cursorY;
        private string windowHeader;

        #endregion
        #region Properties
        public int X
        {
            get { return x; }
        }
        public int Y
        {
            get { return y; }
        }
        public int Width
        {
            get { return width; }
        }
        public int Height
        {
            get { return height; }
        }
        public string WindowHeader
        {
            get { return windowHeader; }
            set {
                windowHeader = value;
                RedrawWindow();
            }
        }
        #endregion
        #region Methods
        /// <summary>
        /// Get all of the changed characters in the window since last draw
        /// </summary>
        /// <returns></returns>
        public List<Tuple<int,int>> GetChanges()
        {
            return changes;
        }
        /// <summary>
        /// Clear all of the elements in the change list
        /// </summary>
        public void FlushChanges()
        {
            changes.Clear();
        }
        /// <summary>
        /// Gets the character and color at a coordinate
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public ConsolePoint GetConsoleDataAt(int x, int y)
        {
            return display[y][x];
        }
        /// <summary>
        /// Sets the character at the coordinate to the one specified
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="c"></param>
        public void SetCharAt(int x, int y, char c, ConsoleColor fg = ConsoleColor.White, ConsoleColor bg=ConsoleColor.Black)
        {
            display[y][x].character = c;
            display[y][x].foreground = fg;
            display[y][x].background = bg;
            changes.Add(new Tuple<int, int>(this.x+x,this.y+y));
        }
        /// <summary>
        /// Sets the windows cursor to the coordinate
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void SetCursorPos(int x, int y)
        {
            cursorX = x;
            cursorY = y;
        }

        /// <summary>
        /// Reads a line from the input on this window
        /// </summary>
        /// <returns></returns>
        public string ReadLine()
        {
            //
            // initialize variable and make sure cursor is visible
            //
            string userResponse;
            Console.CursorVisible = true;

            //
            // set cursor to input window space
            //
            Console.SetCursorPosition(x+2, y+height-2);
            SetCursorPos(2, height - 2);
            userResponse= Console.ReadLine();

            //
            // return to original state
            //
            Console.CursorVisible = false;
            return userResponse;
        }

        /// <summary>
        /// Reads a character from the input on this window
        /// </summary>
        /// <returns></returns>
        public ConsoleKey Read()
        {
            //
            // initialize variable and make sure cursor is visible
            //
            System.ConsoleKeyInfo userResponse;
            Console.CursorVisible = true;

            //
            // set cursor to input window space
            //
            Console.SetCursorPosition(x + 2, y + height - 2);
            SetCursorPos(2, height - 2);
            userResponse = Console.ReadKey();

            //
            // return to original state
            //
            Console.CursorVisible = false;
            return userResponse.Key;
        }

        /// <summary>
        /// Writes the string to the window, incrementing the cursor as it goes
        /// (Will Wrap)
        /// </summary>
        /// <param name="str"></param>
        public void Write(string str, ConsoleColor fg=ConsoleColor.White, ConsoleColor bg = ConsoleColor.Black)
        {
            foreach (char c in str)
            {
                SetCharAt(cursorX, cursorY, c,fg, bg);
                IncrementCursor();
            }
        }
        /// <summary>
        /// Writes the string to the window, incrementing the cursor as it goes
        /// and leaving a new line at the end
        /// (Will Wrap)
        /// </summary>
        /// <param name="str"></param>
        public void WriteLine(string str, ConsoleColor fg = ConsoleColor.White, ConsoleColor bg = ConsoleColor.Black)
        {
            Write(str, fg,bg);
            CarriageReturnCursor();
        }

        /// <summary>
        /// Update the window position
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void SetWindowPosition(int x, int y)
        {
            this.x = x;
            this.y = y;
            RedrawWindow();
        }

        /// <summary>
        /// Update the window size
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public void SetWindowSize(int width, int height)
        {
            //
            // redraw the characters in this size change
            //

            // TODO: FIGURE OUT THE PROBLEM WITH THIS
            //for (int y = Math.Min(height, this.height); y <= Math.Max(height, this.height); y++)
            //    for (int x = Math.Min(width, this.width); x <= Math.Max(width, this.width); x++)
            //        RedrawCharacter(x, y);
            RedrawWindow();

            //
            // set the variables
            //
            this.width = width;
            this.height = height;
            InitializeArrays();
        }
        /// <summary>
        /// Clears the entire window
        /// </summary>
        public void Clear()
        {
            for (int y=1;y<height-1;y++)
            {
                for (int x=1;x<width-1;x++)
                {
                    display[y][x].character = ' ';
                    display[y][x].foreground = ConsoleColor.Black;
                    display[y][x].background = ConsoleColor.Black;
                }
            }

            SetCursorPos(2, 2);
            RedrawWindow();
        }

        /// <summary>
        /// Puts the entire window space to be redrawn
        /// </summary>
        private void RedrawWindow()
        {
            DisplayBorderCharacters();

            for (int y=0;y<height;y++)
                for (int x=0;x<width;x++)
                    RedrawCharacter(x, y);
        }
        /// <summary>
        /// Redraws a character at one point
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        private void RedrawCharacter(int x, int y)
        {
            changes.Add(new Tuple<int, int>(this.x + x, this.y + y));
        }
        /// <summary>
        /// Increments the cursor by the number of characters specified
        /// </summary>
        /// <param name="numTimes"></param>
        private void IncrementCursor(int numTimes=1)
        {
            for (int i = 0; i < numTimes; i++)
            {
                cursorX++;
                if (cursorX >= width-2) CarriageReturnCursor();
            }
        }
        /// <summary>
        /// Sets the cursor to a new line
        /// </summary>
        private void CarriageReturnCursor()
        {
            cursorX = 2;
            cursorY++;
            if (cursorY >= height-2) cursorY = 2;
        }
        /// <summary>
        /// Sets the characters on the borders of the display
        /// </summary>
        private void DisplayBorderCharacters()
        {
            for (int row=0;row<height;row++)
            {
                display[row][0].character = '│';
                display[row][0].foreground = ConsoleColor.White;
                display[row][width-1].character = '│';
                display[row][width-1].foreground = ConsoleColor.White;
            }
            for (int column = 1; column < width-1; column++)
            {
                //
                // put the text for the header on top
                //
                display[0][column].character=(column-1<windowHeader.Length)? windowHeader[column-1]:' ';
                display[0][column].foreground = ConsoleColor.Black;
                display[0][column].background = ConsoleColor.White;

                display[height-1][column].character = '─';
                display[height-1][column].foreground = ConsoleColor.White;
            }
            display[0][0].character = '█';
            display[0][0].foreground = ConsoleColor.White;
            display[0][width-1].character = '█';
            display[0][width-1].foreground = ConsoleColor.White;
            display[height-1][0].character = '└';
            display[height - 1][0].foreground = ConsoleColor.White;
            display[height-1][width-1].character = '┘';
            display[height - 1][width - 1].foreground = ConsoleColor.White;
        }
        /// <summary>
        /// Initializes the display arrays and adds the borders
        /// </summary>
        private void InitializeArrays()
        {
            display = new ConsolePoint[height][];
            for (int row = 0; row < height; row++)
                display[row] = new ConsolePoint[width];

            DisplayBorderCharacters();
        }
        #endregion
        #region Constructors
        public Window(int x, int y, int width, int height)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
            windowHeader = "";
            changes = new List<Tuple<int, int>>();
            InitializeArrays();

            SetCursorPos(2, 2);
        }
        #endregion
    }
}
