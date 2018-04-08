using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TB_QuestGame
{
    public static class Screens
    {
        const int RESIZESPEED = 2;
        public enum ScreenType
        {
            SplashScreen,
            IntroScreen,
            MainScreen
        }

        /// <summary>
        /// Gives one step of resize for a window, returning a bool if it has changed
        /// </summary>
        /// <param name="screens"></param>
        /// <param name="main"></param>
        /// <param name="status"></param>
        /// <param name="menu"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool SizeWindowStep(ScreenType screens, Window main, Window status, Window menu, Window input)
        {
            //
            // initialize variables
            //
            int changes = 0;
            int mainWidth = 0;
            int mainHeight = 0;
            int statusWidth = 0;
            int statusHeight = 0;
            int menuWidth = 0;
            int menuHeight = 0;
            int inputWidth = 0;
            int inputHeight = 0;

            //
            // set values for screen
            //
            switch (screens)
            {
                case ScreenType.SplashScreen:
                    mainWidth = 99;
                    mainHeight = 40;
                    statusWidth = 29;
                    statusHeight = 20;
                    menuWidth = 29;
                    menuHeight = 20;
                    inputWidth = 99;
                    inputHeight = 6;
                    break;
                case ScreenType.IntroScreen:
                    mainWidth = 99;
                    mainHeight = 34;
                    statusWidth = 29;
                    statusHeight = 20;
                    menuWidth = 29;
                    menuHeight = 20;
                    inputWidth = 99;
                    inputHeight = 6;
                    break;
                case ScreenType.MainScreen:
                    mainWidth = 69;
                    mainHeight = 34;
                    statusWidth = 29;
                    statusHeight = 20;
                    menuWidth = 29;
                    menuHeight = 20;
                    inputWidth = 69;
                    inputHeight = 6;
                    break;
            }

            //
            // check for changes
            //
            changes += Math.Abs(GetWidthOrHeightDelta(input.Width, inputWidth));
            changes += Math.Abs(GetWidthOrHeightDelta(input.Height, inputHeight));
            changes += Math.Abs(GetWidthOrHeightDelta(menu.Width, menuWidth));
            changes += Math.Abs(GetWidthOrHeightDelta(menu.Height, menuHeight));
            changes += Math.Abs(GetWidthOrHeightDelta(status.Width, statusWidth));
            changes += Math.Abs(GetWidthOrHeightDelta(status.Height, statusHeight));
            changes += Math.Abs(GetWidthOrHeightDelta(main.Width, mainWidth));
            changes += Math.Abs(GetWidthOrHeightDelta(main.Height, mainHeight));

            //
            // resize windows
            //
            input.SetWindowSize(input.Width + GetWidthOrHeightDelta(input.Width, inputWidth),
                input.Height + GetWidthOrHeightDelta(input.Height, inputHeight));
            status.SetWindowSize(status.Width + GetWidthOrHeightDelta(status.Width, statusWidth),
                status.Height + GetWidthOrHeightDelta(status.Height, statusHeight));
            main.SetWindowSize(main.Width + GetWidthOrHeightDelta(main.Width, mainWidth),
                main.Height + GetWidthOrHeightDelta(main.Height, mainHeight));
            menu.SetWindowSize(menu.Width + GetWidthOrHeightDelta(menu.Width, menuWidth),
                menu.Height + GetWidthOrHeightDelta(menu.Height, menuHeight));

            return changes != 0;
        }

        public static int GetWidthOrHeightDelta(int current, int target)
        {
            if (target-current > 0)
                return Math.Min(target - current, RESIZESPEED);
            else
                return Math.Max(target - current, -RESIZESPEED);
        }
    }
}
