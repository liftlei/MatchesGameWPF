using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchesGameWPF
{
    public class MainViewModel : ViewModelBase
    {
        private string gameRule;
        public string GameRule
        {
            get { return gameRule; }
            set { gameRule = value; Set(ref gameRule, value); }
        }

    }
}
