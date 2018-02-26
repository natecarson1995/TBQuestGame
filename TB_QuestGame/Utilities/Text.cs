using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TB_QuestGame
{
    public static class Text
    {
        public static string[] splashLines =
            {
                "███████╗██╗  ██╗    ███╗   ███╗ █████╗  ██████╗██╗  ██╗██╗███╗   ██╗ █████╗",
                "██╔════╝╚██╗██╔╝    ████╗ ████║██╔══██╗██╔════╝██║  ██║██║████╗  ██║██╔══██╗",
                "█████╗   ╚███╔╝     ██╔████╔██║███████║██║     ███████║██║██╔██╗ ██║███████║",
                "██╔══╝   ██╔██╗     ██║╚██╔╝██║██╔══██║██║     ██╔══██║██║██║╚██╗██║██╔══██║",
                "███████╗██╔╝ ██╗    ██║ ╚═╝ ██║██║  ██║╚██████╗██║  ██║██║██║ ╚████║██║  ██║",
                "╚══════╝╚═╝  ╚═╝    ╚═╝     ╚═╝╚═╝  ╚═╝ ╚═════╝╚═╝  ╚═╝╚═╝╚═╝  ╚═══╝╚═╝  ╚═╝"
            };

        public static string[] introSections = {
            " Unit is a new robot in the race of intelligent machines: Ex Machina ",
            " Unit has been manufactured during a time of war among races ",
            " Therefore, the unit must exercise caution in approaching potential agressors ",
            " Unit will now attempt connecting to the local cluster to determine goals  and  input configuration details ",
            " ....... "," .............. ",
            $" Connection to cluster successful "
        };

        public static string[] mainMenuScreen =
        {
            " Unit is now configured ",
            " Please stand by for further details "
        };
            
        /// <summary>
        /// returns a random hex character 0-f
        /// </summary>
        /// <returns></returns>
        public static string GetRandomHexCharacters(int num)
        {
            Random rand = new Random();
            byte[] bytes = new byte[num];
            char[] chars = new char[num];

            rand.NextBytes(bytes);
            
            for (int i=0;i<num;i++)
                chars[i] = Convert.ToString(bytes[i], 16)[0];

            return new string(chars);
        }

        /// <summary>
        /// return a string in title case
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToTitleCase(string str)
        {
            if (str.Length > 2)
                return str.Substring(0, 1).ToUpper() + str.Substring(1).ToLower();
            else
                return str;
        }
    }
}
