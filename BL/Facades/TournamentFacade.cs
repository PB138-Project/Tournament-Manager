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

            using (var context = new AppDbContext())
            {
                context.Database.Log = Logger;
                context.Tournaments.Add(newTournament);
                context.SaveChanges();
            }
        }

        public List<TournamentDTO> GetAllTournaments()
        {
            using (var context = new AppDbContext())
            {
                var tournaments = context.Tournaments.Include(c => c.Teams).Include(c => c.Matches).ToList();
                return tournaments.Select(e => Mapping.Mapper.Map<TournamentDTO>(e)).ToList();
            }
        }

        public TournamentDTO GetSpecificTournament(int id)
        {
            using (var context = new AppDbContext())
            {
                context.Database.Log = Logger;
                var specific =
                    context.Tournaments.Include(c => c.Teams).Include(c => c.Matches).FirstOrDefault(c => c.Id == id);
                return Mapping.Mapper.Map<TournamentDTO>(specific);
            }
        }
        public void UpdateTournament(TournamentDTO tournament)
        {
            var newTournament = Mapping.Mapper.Map<Tournament>(tournament);

            using (var context = new AppDbContext())
            {
                context.Database.Log = Logger;
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
    }
}