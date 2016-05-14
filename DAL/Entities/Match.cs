using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class Match
    {
        public int Id { get; set; }

        [Required]
        public Team TeamA { get; set; }
        [Required]
        public Team TeamB { get; set; }
        public int MatchType { get; set; }
        public bool Winner { get; set; }
        [Required]
        public Tournament Tournament { get; set; }

         
    }
}