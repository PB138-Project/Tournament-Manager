using System.Collections.Generic;

namespace BL.DTO
{
    public class TournamentDTO
    {
        public int Id { get; set; }
        public List<TeamDTO> Teams { get; set; }
        public List<MatchDTO> Matches { get; set; }
    }
}