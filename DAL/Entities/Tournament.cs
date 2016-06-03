using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    public class Tournament
    {
        public Tournament()
        {
            Teams = new List<Team>();
            Matches = new List<Match>();
        }
        public int Id { get; set; }
        public string TournamentName { get; set; }
        public int TournamentSize { get; set; }
        public List<Team> Teams { get; set; }
        public List<Match> Matches { get; set; }
        
         

    }
}