using System.Collections.Generic;
using BL.DTO;

namespace Web.Models
{
    public class TournamentModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Size { get; set; }
        public string[] Teams { get; set; }
        public MatchDTO[] Matches { get; set; } 

    }
}