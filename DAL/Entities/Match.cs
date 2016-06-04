using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class Match
    {
        public int Id { get; set; }
        [ForeignKey("TeamA")]
        public int TeamAId { get; set; }
        public Team TeamA { get; set; }
        [ForeignKey("TeamB")]
        public int TeamBId { get; set; }
        public Team TeamB { get; set; }
        public int? WinnerId { get; set; }
        [ForeignKey("Tournament")]
        public int TournamentId { get; set; }
        public Tournament Tournament { get; set; }


         
    }
}