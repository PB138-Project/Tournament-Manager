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
    public class PlayerFacade
    {
        public void Logger(string data)
        {
            using (var writer = new StreamWriter("C://Users/" + Environment.UserName + "/Desktop/PlayerLog.txt", true))
            {
                writer.WriteLine(data);
            }
        }

        public void CreatePlayer(PlayerDTO player)
        {
            Player newPlayer = Mapping.Mapper.Map<Player>(player);

            if (GetId(newPlayer.Name) != 0)
            {
                return;
            }

            using (var context = new AppDbContext())
            {
                context.Database.Log = Logger;
                var team = context.Teams.FirstOrDefault(d => d.TeamName == player.Team.TeamName);
                if (team == null) throw new ArgumentNullException("Argument is null!");
                newPlayer.Team = team;               
                context.Players.Add(newPlayer);
                context.Entry(team).State = EntityState.Modified;
                context.Entry(newPlayer.Team).State = EntityState.Unchanged;
                context.SaveChanges();
            }
        }

        public List<PlayerDTO> GetAllPlayers()
        {
            using (var context = new AppDbContext())
            {
                var players = context.Players.Include(c => c.Team).ToList();
                return players.Select(e => Mapping.Mapper.Map<PlayerDTO>(e)).ToList();
            }
        }

        public PlayerDTO GetSpecificPlayer(int id)
        {
            using (var context = new AppDbContext())
            {
                context.Database.Log = Logger;
                var specific = context.Players.Include(c => c.Team).FirstOrDefault(c => c.Id == id);
                return Mapping.Mapper.Map<PlayerDTO>(specific);
            }
        }
        public void UpdatePlayer(PlayerDTO player)
        {
            var newPlayer = Mapping.Mapper.Map<Player>(player);

            if (GetId(newPlayer.Name) != newPlayer.Id && GetId(newPlayer.Name) != 0)
            {
                return;
            }

            using (var context = new AppDbContext())
            {
                context.Database.Log = Logger;
                context.Entry(newPlayer).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void DeletePlayer(int id)
        {
            using (var context = new AppDbContext())
            {
                context.Database.Log = Logger;
                context.Players.Remove(context.Players.Find(id));
                context.SaveChanges();
            }
        }

        private int GetId (string name)
        {
            using (var context = new AppDbContext())
            {
                context.Database.Log = Logger;
                var player = context.Players.FirstOrDefault(p => p.Name.Equals(name));
                context.SaveChanges();
                return (player == null) ? 0 : player.Id;
            }
        }
    }
}