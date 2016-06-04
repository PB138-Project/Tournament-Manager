using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace DAL.Entities
{
    public class Team
    {
        public Team()
        {
            Players = new List<Player>();
        }
        public int Id { get; set; }
        public string TeamName { get; set; }
        [Required]
        public List<Player> Players { get; set; }
        [ForeignKey("Tournament")]
        public int? TournamentId { get; set; }
        public Tournament Tournament { get; set; }

    }
}