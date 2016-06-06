using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using BL.DTO;
using BL.Facades;
using Web.Models;

namespace Web.Controllers
{
    public class TournamentController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Tournament
        public ActionResult Tournaments()
        {
            var tournamentFacade = new TournamentFacade();
            var model = tournamentFacade.GetAllTournaments();
            return View(model);
        }

        // GET: Tournament/Details/5
        public ActionResult Details(int id)
        {
            var tournamentFacade = new TournamentFacade();
            var matchFacade = new MatchFacade();
            var tournament = tournamentFacade.GetSpecificTournament(id);
            var matches = matchFacade.GetMatchesByTournamentId(id);
            var tournamentModel = new TournamentModel()
            {
                Id = tournament.Id,
                Name = tournament.TournamentName,
                Size = tournament.TournamentSize,
                Teams = new string[tournament.TournamentSize],
                Matches = new MatchDTO[matches.Count]
            };
            int i=0;
            foreach (var match in matches)
            {
                tournamentModel.Matches[i] = match;
                i++;
            }


            return View(tournamentModel);
        }

        //Post: Tournament/Details/5
        [HttpPost]
        public ActionResult Details(TournamentModel tournamentModel)
        {
            var tournamentFacade = new TournamentFacade();
            var teamFacade = new TeamFacade();
            var matchFacade = new MatchFacade();
            var tournament = tournamentFacade.GetSpecificTournament(tournamentModel.Id);
            tournamentModel.Name = tournament.TournamentName;
            tournamentModel.Size = tournament.TournamentSize;
            //Control input data
            for (int i = 0; i < tournamentModel.Size; i++)
            {
                for (int j = i; j < tournamentModel.Teams.Length; j++)
                {
                    if (i != j)
                    {
                        if (tournamentModel.Teams[i].Equals(tournamentModel.Teams[j]))
                        {
                            return RedirectToAction("Details");
                        }
                    }
                }
            }
            //Set Tournament Id in first cycle
            if (tournamentModel.Teams.Length == tournamentModel.Size)
            {
                foreach (var name in tournamentModel.Teams)
                {
                    var team = teamFacade.GetSpecificTeam(name);
                    team.TournamentId = tournament.Id;
                    teamFacade.UpdateTeam(team);
                }
            }
            //Set Winners
            foreach(var match in matchFacade.SetWinners())
            {
                foreach(string name in tournamentModel.Teams)
                {
                    if (match.TeamA.TeamName.Equals(name))
                    {
                        match.WinnerId = match.TeamA.Id;
                    }
                    if (match.TeamB.TeamName.Equals(name))
                    {
                        match.WinnerId = match.TeamB.Id;
                    }
                    matchFacade.UpdateMatch(match);
                }                    
            }

            if (tournamentModel.Teams.Length == 1)
            {
                return RedirectToAction("Details");
            }

            for (int i = 0; i < tournamentModel.Teams.Length; i+=2)
            { 
                matchFacade.CreateMatch(teamFacade.GetSpecificTeam(tournamentModel.Teams[i]).Id,
                    teamFacade.GetSpecificTeam(tournamentModel.Teams[i + 1]).Id, tournamentModel.Id);

            }

            return RedirectToAction("Details");
        }


        // GET: Tournament/Winner -> last round
        public ActionResult Winner()
        {
            return View();
        }
        // GET: Tournament/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tournament/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( TournamentDTO tournamentDTO)
        {
            if (ModelState.IsValid)
            {
                var tournamentFacade = new TournamentFacade();
                tournamentFacade.CreateTournament(tournamentDTO);
                return RedirectToAction("Tournaments");
            }

            return View(tournamentDTO);
        }

        // GET: Tournament/Edit/5
        public ActionResult Edit(int id)
        {
            var tournamentFacade = new TournamentFacade();
            var tournament = tournamentFacade.GetSpecificTournament(id);
            return View(tournament);
        }

        // POST: Tournament/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, TournamentDTO tournamentDTO)
        {
            var tournamentFacade = new TournamentFacade();
            if (ModelState.IsValid)
            {
                var originalTournament = tournamentFacade.GetSpecificTournament(id);
                originalTournament.TournamentName = tournamentDTO.TournamentName;
                originalTournament.TournamentSize = tournamentDTO.TournamentSize;
                tournamentFacade.UpdateTournament(originalTournament);
                return RedirectToAction("Tournaments");
            }
            return View(tournamentDTO);
        }

        // GET: Tournament/Delete/5
        public ActionResult Delete(int id)
        {
            var tournamentFacade = new TournamentFacade();
            TournamentDTO tournamentDTO = tournamentFacade.GetSpecificTournament(id);
            if (tournamentDTO == null)
            {
                return HttpNotFound();
            }
            return View(tournamentDTO);
        }

        // POST: Tournament/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
           var tournamentFacade = new TournamentFacade();
            tournamentFacade.DeleteTournament(id);
            return RedirectToAction("Tournaments");
        }
        public List<string> DropDownListMaker()
        {
            var teamFacade = new TeamFacade();
            var list = teamFacade.GetAllUnusedTeams();
            List<string> dropDownList = list.Select(item => item.TeamName).ToList();
            return dropDownList;
        }
        public Tuple<string, string>[] GetTeamNames(TournamentModel model)
        {
            int maxSize = 0;
            foreach (var match in model.Matches)
            {
                if (maxSize < match.TeamA.TeamName.Length)
                {
                    maxSize = match.TeamA.TeamName.Length;
                }

                if (maxSize < match.TeamB.TeamName.Length)
                {
                    maxSize = match.TeamB.TeamName.Length;
                }
            }
            return model.Matches.Select(match => new Tuple<string, string>(GetName(match.TeamA.TeamName, maxSize), GetName(match.TeamA.TeamName, maxSize))).ToArray();
        }

        public string GetName(string name, int size)
        {
            StringBuilder result = new StringBuilder();
            int gap = size - name.Length;
            for (int i = 0; i < gap / 2 + gap % 2; ++i)
            {
                result.Append(" ");
            }
            result.Append(name);
            for (int i = 0; i < gap / 2; ++i)
            {
                result.Append(" ");
            }
            return result.ToString();
        }
    }
}
