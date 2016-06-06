using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Export
{
    public class Tournament
    {
        public int Id { get; set; }
        public string TournamentName { get; set; }
        public int TournamentSize { get; set; }
        public List<Team> Teams { get; set; }
        public List<Match> Matches { get; set; }
    }
}
