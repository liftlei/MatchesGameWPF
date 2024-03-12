using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchesGameWPF
{
    public class Player
    {
        public Player(int id)
        {
            Id = id;
            SelectingMatches = new List<Match>();
            SelectedMatches = new List<Match>();
        }

        public int Id { get; set; }
        public string Name => $"玩家 {Id}";

        public List<Match> SelectingMatches { get; set; }
        public List<Match> SelectedMatches { get; set; }
    }
}
