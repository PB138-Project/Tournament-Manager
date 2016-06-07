using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data;
using BL.Facades;
using DAL;
using BL.DTO;

namespace BL.Export
{
    public class ExportToXML
    {
        public static void WriteXML(TournamentDTO tournament)
        {
            MatchFacade matchFacade = new MatchFacade();
            TeamFacade teamFacade = new TeamFacade();
            using (var context = new AppDbContext())
            {

                var matches = matchFacade.GetAllTournamentMatches(tournament.Id);

                Tournament tournamentXML = new Tournament
                {
                    Id = tournament.Id,
                    TournamentName = tournament.TournamentName,
                    TournamentSize = tournament.TournamentSize,
                    Matches = new List<Match>(),
                    Teams = new List<Team>()
                };
                int count = 0;
                foreach (var match in matches)
                {
                    var teamA = teamFacade.GetSpecificTeam(match.TeamAId).TeamName;
                    var teamB = teamFacade.GetSpecificTeam(match.TeamBId).TeamName;
                    var matchXML = new Match
                    {
                        Id = match.Id,
                        TeamA = teamA,
                        TeamB = teamB,
                        Winner = (match.WinnerId == null) ? "Unfinished Match" :
                        (match.WinnerId == match.TeamAId) ? teamA : teamB
                    };
                    if (count < tournament.TournamentSize / 2)
                    {
                        var team1 = new Team
                        {
                            Id = match.TeamAId,
                            Name = teamA,
                            Wins = 0,
                            Matches = 0,
                        };
                        var team2 = new Team
                        {
                            Id = match.TeamBId,
                            Name = teamB,
                            Wins = 0,
                            Matches = 0,
                        };
                        tournamentXML.Teams.Add(team1);
                        tournamentXML.Teams.Add(team2);
                        ++count;
                    }
                    tournamentXML.Matches.Add(matchXML);
                }
                foreach(var team in tournamentXML.Teams)
                {
                    foreach(var match in tournamentXML.Matches)
                    {
                        if (match.Winner.Equals(team.Name)) {
                            team.Wins++;
                        }
                        if (match.TeamA.Equals(team.Name) || match.TeamB.Equals(team.Name))
                        {
                            team.Matches++;
                        }
                    }
                    team.Ratio = $"{Math.Round(((double) team.Wins / team.Matches),3)*100}%";
                }


                System.Xml.Serialization.XmlSerializer writer =
                    new System.Xml.Serialization.XmlSerializer(typeof(Tournament));

                var path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + $"//{tournament.TournamentName}.xml";
                System.IO.FileStream file = System.IO.File.Create(path);

                writer.Serialize(file, tournamentXML);

                file.Close();
            }
        }
    }
}

