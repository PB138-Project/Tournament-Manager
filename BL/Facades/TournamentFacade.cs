using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using BL.DTO;
using DAL;
using DAL.Entities;

namespace BL.Facades
{
    public class TournamentFacade
    {
        public void Logger(string data)
        {
            using (var writer = new StreamWriter("C://Users/" + Environment.UserName + "/Desktop/TournamentLog.txt", true))
            {
                writer.WriteLine(data);
            }
        }

        public void CreateTournament(TournamentDTO tournament)
        {
            Tournament newTournament = Mapping.Mapper.Map<Tournament>(tournament);

            if (GetId(newTournament.TournamentName) != 0)
            {
                return;
            }
            using (var context = new AppDbContext())
            {
                context.Database.Log = Logger;
                if (newTournament.TournamentSize <= 2)
                {
                    newTournament.TournamentSize = 2;
                }

                if (newTournament.TournamentSize <= 4)
                {
                    newTournament.TournamentSize = 4;
                }

                if (newTournament.TournamentSize <= 8)
                {
                    newTournament.TournamentSize = 8;
                }

                if (newTournament.TournamentSize > 8)
                {
                    newTournament.TournamentSize = 16;
                }

                context.Tournaments.Add(newTournament);
                context.SaveChanges();
            }
        }

        public List<TournamentDTO> GetAllTournaments()
        {
            using (var context = new AppDbContext())
            {
                var tournaments = context.Tournaments.ToList();
                return tournaments.Select(e => Mapping.Mapper.Map<TournamentDTO>(e)).ToList();
            }
        }

        public TournamentDTO GetSpecificTournament(int id)
        {
            using (var context = new AppDbContext())
            {
                context.Database.Log = Logger;
                var specific =
                    context.Tournaments.FirstOrDefault(c => c.Id == id);
                return Mapping.Mapper.Map<TournamentDTO>(specific);
            }
        }

        public void UpdateTournament(TournamentDTO tournament)
        {
            var newTournament = Mapping.Mapper.Map<Tournament>(tournament);

            if (GetId(newTournament.TournamentName) != newTournament.Id)
            {
                return;
            }

            using (var context = new AppDbContext())
            {
                context.Database.Log = Logger;
                if (newTournament.TournamentSize <= 2)
                {
                    newTournament.TournamentSize = 2;
                }

                if (newTournament.TournamentSize <= 4)
                {
                    newTournament.TournamentSize = 4;
                }

                if (newTournament.TournamentSize <= 8)
                {
                    newTournament.TournamentSize = 8;
                }

                if (newTournament.TournamentSize > 8)
                {
                    newTournament.TournamentSize = 16;
                }
                context.Entry(newTournament).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void DeleteTournament(int id)
        {
            using (var context = new AppDbContext())
            {
                context.Database.Log = Logger;
                context.Tournaments.Remove(context.Tournaments.Find(id));
                context.SaveChanges();
            }
        }
        private int GetId(string name)
        {
            using (var context = new AppDbContext())
            {
                context.Database.Log = Logger;
                var tournament = context.Tournaments.FirstOrDefault(t => t.TournamentName.Equals(name));
                context.SaveChanges();
                return (tournament == null) ? 0 : tournament.Id;
            }
        }
    }
}