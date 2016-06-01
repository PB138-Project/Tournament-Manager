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

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
        //    base.OnModelCreating(modelBuilder);

        //    modelBuilder.Entity<Match>().HasRequired(c => c.TeamA).WithMany().WillCascadeOnDelete(false);
        //}
    }
}