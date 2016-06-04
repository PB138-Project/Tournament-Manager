using DAL.Entities;

namespace DAL.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DAL.AppDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DAL.AppDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            context.Teams.AddOrUpdate(
                p => p.Id,
                new Team { TeamName = "Korec" },
                new Team { TeamName = "Gonda" },
                new Team { TeamName = "Rydzi" },
                new Team { TeamName = "Repa" },
                new Team { TeamName = "Lampa" },
                new Team { TeamName = "Strom" },
                new Team { TeamName = "Ryza" },
                new Team { TeamName = "Tycinka" },
                new Team { TeamName = "Kebab" },
                new Team { TeamName = "Jablko" },
                new Team { TeamName = "Holub" },
                new Team { TeamName = "Kelimok" },
                new Team { TeamName = "Trnava" },
                new Team { TeamName = "Piatok" },
                new Team { TeamName = "Hlboko" },
                new Team { TeamName = "Janko" }
                );
            context.Tournaments.AddOrUpdate(
                p => p.Id,
                new Tournament { TournamentName = "Gocnik Cup 2k16", TournamentSize = 16 },
                new Tournament { TournamentName = "Korec Cup 2k16", TournamentSize = 8 },
                new Tournament { TournamentName = "Gonda Cup 2k16", TournamentSize = 4 },
                new Tournament { TournamentName = "Obsivac Cup 2k16", TournamentSize = 2 }
                );
        }
    }
}
