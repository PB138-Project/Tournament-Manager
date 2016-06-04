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
    public class MatchFacade
    {
        public void Logger(string data)
        {
            using (var writer = new StreamWriter("C://Users/" + Environment.UserName + "/Desktop/PlayerLog.txt", true))
            {
                writer.WriteLine(data);
            }
        }

        public void CreateMatch(int Aid, int Bid, int Tid)
        {

            using (var context = new AppDbContext())
            {
                context.Database.Log = Logger;
                var newMatch = new Match
                {
                    TeamA = context.Teams.Find(Aid),
                    TeamB = context.Teams.Find(Bid),
                    Tournament = context.Tournaments.Find(Tid),
                    TeamAId = Aid,
                    TeamBId = Bid,
                    TournamentId = Tid,
                };
                context.Matches.Add(newMatch);
                context.SaveChanges();
            }
        }

        public List<MatchDTO> GetAllMatches()
        {
            using (var context = new AppDbContext())
            {
                var matches = context.Matches.ToList();
                return matches.Select(e => Mapping.Mapper.Map<MatchDTO>(e)).ToList();
            }
        }

        public MatchDTO GetSpecificMatch(int id)
        {
            using (var context = new AppDbContext())
            {
                context.Database.Log = Logger;
                var specific =context.Matches.FirstOrDefault(c => c.Id == id);
                specific.TeamA = context.Teams.Find(specific.TeamAId);
                specific.TeamB = context.Teams.Find(specific.TeamBId);
                var result = Mapping.Mapper.Map<MatchDTO>(specific);
                return result;
            }
        }

        public List<MatchDTO> SetWinners()
        {
            using (var context = new AppDbContext())
            {
                context.Database.Log = Logger;
                var matches = context.Matches.Where(m => m.WinnerId == null).ToList();
                foreach (var specific in matches)
                {
                    specific.TeamA = context.Teams.Find(specific.TeamAId);
                    specific.TeamB = context.Teams.Find(specific.TeamBId);
                }
                return matches.Select(e => Mapping.Mapper.Map<MatchDTO>(e)).ToList(); ;
            }

        }
        public void UpdateMatch(MatchDTO match)
        {
            Match newMatch = Mapping.Mapper.Map<Match>(match);

            using (var context = new AppDbContext())
            {
                context.Database.Log = Logger;
                context.Entry(newMatch).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void DeleteMatch(int id)
        {
            using (var context = new AppDbContext())
            {
                context.Database.Log = Logger;
                context.Matches.Remove(context.Matches.Find(id));
                context.SaveChanges();
            }
        }

        public List<MatchDTO> GetMatchesByTournamentId(int id)
        {
            using (var context = new AppDbContext())
            {
                context.Database.Log = Logger;
                var result = context.Matches.Where(c => c.TournamentId == id).ToList();
                foreach (var match in result)
                {
                    match.TeamA = context.Teams.Find(match.TeamAId);
                    match.TeamB = context.Teams.Find(match.TeamBId);
                }
                return result
                    .Select(e => Mapping.Mapper.Map<MatchDTO>(e)).ToList();
            }
        }
    }
}