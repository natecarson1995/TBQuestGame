using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TB_QuestGame
{
    public class WindowHandler
    {
        #region Fields
        private List<Window> windows;
        #endregion
        #region Properties
        #endregion
        #region Methods
        /// <summary>
        /// Adds the windows to the rendering list
        /// </summary>
        /// <param name="windowsToAdd"></param>
        public void AddWindow(params Window[] windowsToAdd)
        {
            foreach (Window window in windowsToAdd)
            {
                windows.Add(window);
            }
        }
        /// <summary>
        /// Get all of the changed characters in the display since last draw
        /// </summary>
        /// <returns></returns>
        public List<Window.Point> GetChanges()
        {
            List<Window.Point> changes = new List<Window.Point>();

            foreach (Window window in windows)
            {
                changes.AddRange(window.GetChanges());
                window.FlushChanges();
            }

            return changes;
        }
        /// <summary>
        /// Gets the character and color at a specific coordinate
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public Window.CharacterData GetConsoleDataAt(int x, int y)
        {
            foreach (Window window in windows)
            {
                if (x >= window.X && y >= window.Y && x < window.X + window.Width && y < window.Y + window.Height)
                {
                    return window.GetConsoleDataAt(x - window.X, y - window.Y);
                }
            }
            return new Window.CharacterData()
            {
                foreground = ConsoleColor.Black,
                background = ConsoleColor.Black,
                character = ' '
            };
        }
        #endregion
        #region Constructors
        public WindowHandler()
        {
            windows = new List<Window>();
        }
        #endregion
    }
}
