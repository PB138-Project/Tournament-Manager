using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    public class Tournament
    {

        public int Id { get; set; }
        public string TournamentName { get; set; }
        public int TournamentSize { get; set; }
    }
}