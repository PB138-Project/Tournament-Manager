using System.Web.Mvc;
using BL.DTO;
using BL.Facades;
using Web.Models;

namespace Web.Controllers
{

    //NOT FINISHED ; Don't review, please.
    public class TournamentController : Controller
    {
         TournamentFacade tournamentFacade = new TournamentFacade();

        [AllowAnonymous]
        public ActionResult Greet(string name)
        {
            return Content(name);
        }

        public ActionResult Redirect()
        {
            return Redirect("http://google.sk");
            //return RedirectToAction("Greet", "Information", new { name = "AHOJ"});
        }

        public ActionResult Index()
        {
            var model = new TournamentModel{TournamentName = "IEM Katowice" };
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(FormCollection collection)
        {
            var model = new TournamentModel { TournamentName = "IEM Malmo" };
            return View(model);
        }

        public ActionResult Customers()
        {
            var model = tournamentFacade.GetAllTournaments();
            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(TournamentDTO tournament)
        {
            tournamentFacade.CreateTournament(tournament);
            return RedirectToAction("Tournaments");
        }

        public ActionResult Delete(int id)
        {
            tournamentFacade.DeleteTournament(id);
            return RedirectToAction("Tournaments");
        }

        public ActionResult Edit(int id)
        {
            var tournament = tournamentFacade.GetSpecificTournament(id);
            return View(tournament);
        }

        [HttpPost]
        public ActionResult Edit(int id, TournamentDTO tournament)
        {
            if (ModelState.IsValid)
            {
                var originalTournament = tournamentFacade.GetSpecificTournament(id);
                originalTournament.Teams = tournament.Teams;
                originalTournament.Matches = tournament.Matches;
                tournamentFacade.UpdateTournament(originalTournament);
                return RedirectToAction("Customers");
            }
            return View(tournament);
        }
    }
}