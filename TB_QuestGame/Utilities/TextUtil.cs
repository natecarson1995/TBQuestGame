using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TB_QuestGame
{
    static class TextUtil
    {
        /// <summary>
        /// Joins all of the words in the list, using up all of the spaces
        /// </summary>
        /// <param name="words"></param>
        /// <param name="spaces"></param>
        /// <returns></returns>
        static string JoinWithVariableSpaces(List<string> words, int spaces)
        {
            StringBuilder sb = new StringBuilder();
            if (words.Count <= 1) return words[0];
            //
            // initialize variables, to tell us how many spaces per each word
            // as well as how many spaces would be left over
            //
            int spaceAmounts = words.Count - 1;
            int normalAmount = (int)Math.Floor((double)spaces / spaceAmounts);
            int remainder = spaces % spaceAmounts;

            //
            // go through each of the spaces between words
            //
            for (int i = 0; i < spaceAmounts; i++)
            {
                //
                // add the word, and then the variable amount of spaces
                //
                sb.Append(words.ElementAt(i));

                for (int j = 0; j < normalAmount; j++)
                    sb.Append(" ");

                if (i <= remainder)
                    sb.Append(" ");
            }
            //
            // add the final word to the string
            //
            sb.Append(words.Last());

            return sb.ToString();
        }
        /// <summary>
        /// Wraps the text, splitting new lines to fit the max length
        /// </summary>
        /// <param name="text"></param>
        /// <param name="maxLength"></param>
        /// <returns></returns>
        public static string Wrap(string text, int maxLength, bool addNewLine = false)
        {
            StringBuilder sb = new StringBuilder();
            List<string> wordsThisLine = new List<string>();
            string temp;

            int thisLineLength = 0;
            //
            // go through the individual lines
            //
            foreach (string line in text.Split('\n'))
            {
                //
                // reset the line "cursor" on each new line
                //
                thisLineLength = 0;
                wordsThisLine.Clear();
                
                //
                // go through each word in the line
                //
                foreach (string word in line.Split(' '))
                {
                    if (thisLineLength ==0 && word.Length+1 > maxLength)
                    {
                        //
                        // split up single words too long for line length
                        //
                        temp = word;
                        while (temp.Length + 1 > maxLength)
                        {
                            sb.AppendLine(temp.Substring(0, maxLength));
                            temp = temp.Substring(maxLength);
                        }

                        //
                        // add the remainder letters to the words list
                        //
                        thisLineLength += temp.Length + 1;
                        wordsThisLine.Add(temp);
                    }
                    else
                    {
                        //
                        // if the word would make it go over the max length, send that word length to the
                        // join with variable spaces function, and append to the final string
                        // finally, clear the words list
                        //
                        if (thisLineLength + word.Length + 1 > maxLength)
                        {
                            sb.AppendLine(JoinWithVariableSpaces(wordsThisLine, maxLength - (thisLineLength - wordsThisLine.Count - 1)));
                            thisLineLength = 0;
                            wordsThisLine.Clear();
                        }
                        //
                        // move the "cursor", and add the word to the words list
                        //
                        thisLineLength += word.Length + 1;
                        wordsThisLine.Add(word);
                    }
                }
            }
            //
            // if there are any words left over, add them to the final line
            //
            if (wordsThisLine.Count > 0)
                sb.AppendLine(String.Join(" ", wordsThisLine));


            return sb.ToString();
        }
    }
}
