namespace BL.DTO
{
    public class MatchDTO
    {
        public int Id { get; set; }

        public TeamDTO TeamA { get; set; }
        public TeamDTO TeamB { get; set; }
        public int MatchType { get; set; }
        public bool Winner { get; set; }
        public TournamentDTO Tournament { get; set; }
    }
}