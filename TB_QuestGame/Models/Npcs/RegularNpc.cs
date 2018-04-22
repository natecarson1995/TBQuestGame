using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TB_QuestGame
{
    public class RegularNpc : Npc, ISpeak
    {
        #region Fields
        private Random random;
        public event EventHandler OnSpeak;
        #endregion
        #region Properties
        public List<string> Messages { get; set; }
        #endregion
        #region Methods
        /// <summary>
        /// Returns a random element from the pool of npc responses
        /// </summary>
        /// <returns></returns>
        public string Speak()
        {
            OnSpeak?.Invoke(this, EventArgs.Empty);

            if (Messages!=null && Messages.Count() > 0)
            {
                int randomIndex = random.Next(0, Messages.Count());
                return Messages[randomIndex];
            }
            else
            {
                return "";
            }
        }

        #endregion
        #region Constructors
        public RegularNpc(string name, RaceType race, int level = 1) : base(name, race, level)
        {
            random = new Random();
        }
        #endregion
    }
}
