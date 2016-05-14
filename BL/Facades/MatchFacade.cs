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
            using (var writer = new StreamWriter("MatchLog.txt"))
            {
                writer.WriteLine(data);
            }
        }

        public void CreateMatch(MatchDTO match)
        {
            Match newMatch = Mapping.Mapper.Map<Match>(match);

            using (var context = new AppDbContext())
            {
                context.Database.Log = Logger;
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
                var specific =
                    context.Matches.Include(c => c.TeamA)
                        .Include(c => c.TeamB)
                        .Include(c => c.Tournament)
                        .FirstOrDefault(c => c.Id == id);
                return Mapping.Mapper.Map<MatchDTO>(specific);
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
    }
}