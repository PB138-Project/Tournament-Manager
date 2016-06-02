using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BL.DTO;
using DAL.Entities;
using DAL.IdentityEntities;

namespace BL
{
    public class Mapping
    {
        public static IMapper Mapper { get; }

        static Mapping()
        {
            var config = new MapperConfiguration(c =>
            {
                c.CreateMap<Player, PlayerDTO>().ReverseMap();
                c.CreateMap<Team, TeamDTO>().ReverseMap();
                c.CreateMap<Tournament, TournamentDTO>().ReverseMap();
                c.CreateMap<Match, MatchDTO>().ReverseMap();
                c.CreateMap<AppUser, UserDTO>().ReverseMap();
            });
            Mapper = config.CreateMapper();
        }
    }
}
