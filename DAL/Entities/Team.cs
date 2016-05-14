using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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

        //Removed KD (Kill-death ratio).
        [Required]
        public List<Player> Players { get; set; }
    }
}