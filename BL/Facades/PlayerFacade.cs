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
            using (var writer = new StreamWriter("C://Users/Filip/Desktop/PlayerLog.txt"))
            {
                writer.WriteLine(data);
            }
        }

        public void CreatePlayer(PlayerDTO player)
        {
            Player newPlayer = Mapping.Mapper.Map<Player>(player);

            using (var context = new AppDbContext())
            {
                context.Database.Log = Logger;
                context.Players.Add(newPlayer);
                context.SaveChanges();
            }
        }

        public List<PlayerDTO> GetAllPlayers()
        {
            using (var context = new AppDbContext())
            {
                var players = context.Players.ToList();
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
    }
}