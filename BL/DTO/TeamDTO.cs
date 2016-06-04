using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace BL.DTO
{
    public class TeamDTO
    {
        public int Id { get; set; }
        public string TeamName { get; set; }
        public List<PlayerDTO> Players { get; set; }
        public int? TournamentId { get; set; }
        public TournamentDTO Tournament { get; set; }

    }
}