namespace BL.DTO
{
    public class PlayerDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public TeamDTO Team { get; set; }
    }
}