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
                new Team { TeamName = "Rýdzi" },
                new Team { TeamName = "Repa" },
                new Team { TeamName = "Gocník" },
                new Team { TeamName = "Drgoò" },
                new Team { TeamName = "Èekovský" },
                new Team { TeamName = "Polanský" },
                new Team { TeamName = "Obšivaè" },
                new Team { TeamName = "Pan Manh Tran" },
                new Team { TeamName = "Poliver" },
                new Team { TeamName = "Kika" },
                new Team { TeamName = "Jano" },
                new Team { TeamName = "Druhá Kika" },
                new Team { TeamName = "Športovka" },
                new Team { TeamName = "Pablo Escobar" }
                );
            context.Tournaments.AddOrUpdate(
                p => p.Id,
                new Tournament { TournamentName = "Gocnik Cup 2016", TournamentSize = 16 },
                new Tournament { TournamentName = "Bratiska Cup 2016", TournamentSize = 8 },
                new Tournament { TournamentName = "VUT Cup 2016", TournamentSize = 4 },
                new Tournament { TournamentName = "Colombia Invitational 2016", TournamentSize = 2 }
                );
        }
    }
}
