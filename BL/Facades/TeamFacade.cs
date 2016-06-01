using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using BL.DTO;

using DAL;
using DAL.Entities;

namespace BL.Facades
{
    public class TeamFacade
    {
        public void Logger(string data)
        {
            using (var writer = new StreamWriter("TeamLog.txt", true))
            {
                writer.WriteLine(data);
            }
        }

        public void CreateTeam(TeamDTO team)
        {
            Team newTeam = Mapping.Mapper.Map<Team>(team);

            using (var context = new AppDbContext())
            {
                context.Database.Log = Logger;
                context.Teams.Add(newTeam);
                context.Entry(newTeam.TeamName).State = EntityState.Unchanged;
                context.SaveChanges();
            }
        }

        public List<TeamDTO> GetAllTeams()
        {
            using (var context = new AppDbContext())
            {
                var teams = context.Teams.ToList();
                return teams.Select(e => Mapping.Mapper.Map<TeamDTO>(e)).ToList();
            }
        }

        public TeamDTO GetSpecificTeam(int id)
        {
            using (var context = new AppDbContext())
            {
                context.Database.Log = Logger;
                var specific = context.Teams.Include(c => c.Players).FirstOrDefault(c => c.Id == id);
                return Mapping.Mapper.Map<TeamDTO>(specific);
            }
        }
        public void UpdateTeam(TeamDTO team)
        {
            var newTeam = Mapping.Mapper.Map<Team>(team);

            using (var context = new AppDbContext())
            {
                context.Database.Log = Logger;
                context.Entry(newTeam).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void DeleteTeam(int id)
        {
            using (var context = new AppDbContext())
            {
                context.Database.Log = Logger;
                context.Teams.Remove(context.Teams.Find(id));
                context.SaveChanges();
            }
        }
    }
}