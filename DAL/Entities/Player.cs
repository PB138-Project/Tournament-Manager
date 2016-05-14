using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    public class Player
    {
        public int Id { get; set; }
        public string Name { get; set; } 
        public string Surname { get; set; }
        public int Age { get; set; }
        [Required]
        public Team Team { get; set; }

    }
}