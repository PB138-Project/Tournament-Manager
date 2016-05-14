using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.Facades;
using BL.DTO;
using DAL;
using DAL.Entities;

namespace SimpleConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {

            //Testing the function CreateTeam in TeamFacade.

            var teamFacade = new TeamFacade();
            teamFacade.CreateTeam(new TeamDTO
            {
                Id = 1,
                TeamName = "Natus Vincere",
                Players = new List<PlayerDTO> {
                            new PlayerDTO {
                            Id = 1,
                            Name = "Ladislav",
                            Surname = "Kovács",
                            Age = 24
                }
                }
            });

            //Testing the function UpdateTeam in TeamFacade.
            teamFacade.UpdateTeam(new TeamDTO
            {
                Id = 1,
                TeamName = "Natuš Vincereš",
                Players = new List<PlayerDTO> {
                            new PlayerDTO {
                            Id = 1,
                            Name = "Lacko",
                            Surname = "Kovács",
                            Age = 24
                }
                }
            });
            //Testing the function GetSpecificTeam in TeamFacade.

            Console.WriteLine(teamFacade.GetSpecificTeam(1).ToString());

            //Testing the function DeleteTeam in TeamFacade (first, creates a new team, then removes it).

            teamFacade.CreateTeam(new TeamDTO
            {
                Id = 2,
                TeamName = "Virtus Pro",
                Players = new List<PlayerDTO> {
                            new PlayerDTO {
                            Id = 1,
                            Name = "Jaroslaw",
                            Surname = "Jarzabkowski",
                            Age = 28
                }
                }
            });

            teamFacade.DeleteTeam(2);

            //For log file, please see SimpleConsoleApp/bin/debug/TeamLog.txt.
        }
    }
}
