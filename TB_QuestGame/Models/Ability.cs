using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TB_QuestGame
{
    public class Ability
    {
        #region EventArgs
        public class TargetArgs :EventArgs
        {
            private object target;

            public object Target
            {
                get { return target; }
                set { target = value; }
            }

            public TargetArgs(object target)
            {
                this.target = target;
            }
        }
        #endregion
        #region Fields
        public event EventHandler OnProc;
        private string procText;
        private string name;
        #endregion
        #region Properties
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string ProcText
        {
            get { return procText; }
            set { procText = value; }
        }
        #endregion
        #region Methods

        /// <summary>
        /// Processes the ability
        /// </summary>
        /// <param name="universe"></param>
        /// <param name="player"></param>
        public void Proc(object target=null)
        {
            if (target==null)
                OnProc?.Invoke(this, EventArgs.Empty);
            else
            {
                OnProc?.Invoke(this, new TargetArgs(target));
            }
        }
        #endregion
        #region Constructors
        public Ability(string name)
        {
            this.name = name;
        }
        #endregion
    }
}
