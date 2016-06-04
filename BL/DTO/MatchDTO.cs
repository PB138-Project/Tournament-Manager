namespace BL.DTO
{
    public class MatchDTO
    {
        public int Id { get; set; }
        public int TeamAId { get; set; }
        public TeamDTO TeamA { get; set; }
        public int TeamBId { get; set; }
        public TeamDTO TeamB { get; set; }
        public int? WinnerId { get; set; }
        public int TournamentId { get; set; }
        public TournamentDTO Tournament { get; set; }
    }
}