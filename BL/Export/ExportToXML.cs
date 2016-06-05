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

                TournamentXML tournamentXML = new TournamentXML
                {
                    Id = tournament.Id,
                    TournamentName = tournament.TournamentName,
                    TournamentSize = tournament.TournamentSize,
                    Matches = new List<string>()
                };

                foreach (var match in matches)
                {
                    var MatchXML = new MatchXML
                    {
                        Id = match.Id,
                        TeamAId = match.TeamAId,
                        TeamBId = match.TeamBId,
                        TeamA = teamFacade.GetSpecificTeam(match.TeamAId).TeamName,
                        TeamB = teamFacade.GetSpecificTeam(match.TeamBId).TeamName,
                        WinnerId = match.WinnerId
                    };
                    tournamentXML.Matches.Add(MatchXML.ToString());
                }


                System.Xml.Serialization.XmlSerializer writer =
                    new System.Xml.Serialization.XmlSerializer(typeof(TournamentXML));

                var path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + $"//{tournament.TournamentName}.xml";
                System.IO.FileStream file = System.IO.File.Create(path);

                writer.Serialize(file, tournamentXML);

                file.Close();
            }
        }
    }
}

