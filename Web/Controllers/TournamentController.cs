using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
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
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TournamentDTO tournamentDTO = db.TournamentDTOes.Find(id);
            if (tournamentDTO == null)
            {
                return HttpNotFound();
            }
            return View(tournamentDTO);
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
        public ActionResult Create([Bind(Include = "Id,TournamentName")] TournamentDTO tournamentDTO)
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
        public ActionResult Edit(int id, [Bind(Include = "Id")] TournamentDTO tournamentDTO)
        {
            var tournamentFacade = new TournamentFacade();
            if (ModelState.IsValid)
            {
                var originalTournament = tournamentFacade.GetSpecificTournament(id);
                originalTournament.TournamentName = tournamentDTO.TournamentName;
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
    }
}
