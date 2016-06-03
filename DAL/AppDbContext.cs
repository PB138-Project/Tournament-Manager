using System.Data.Entity;
using DAL.Entities;
using DAL.IdentityEntities;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DAL
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, int, AppUserLogin, AppUserRole, AppUserClaim>
    {
        public AppDbContext() : base("Stalinove Slzy")
        {
        }

        public DbSet<Match> Matches { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Tournament> Tournaments { get; set; }
        public DbSet<Player> Players { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);         
        }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
        //    base.OnModelCreating(modelBuilder);

        //    modelBuilder.Entity<Match>().HasRequired(c => c.TeamA).WithMany().WillCascadeOnDelete(false);
        //}
    }
}