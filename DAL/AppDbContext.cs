using System.Data.Entity;
using DAL.Entities;

namespace DAL
{
    public class AppDbContext : DbContext
    {
        public DbSet<Match> Matches { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Tournament> Tournaments { get; set; }
        public DbSet<Player> Players { get; set; }
    }
}