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
    public class TeamController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Team
        public ActionResult Teams()
        {
            var teamFacade = new TeamFacade();
            var model = teamFacade.GetAllTeams();
            return View(model);
        }


        // GET: Team/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Team/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TeamName")] TeamDTO teamDTO)
        {
            if (ModelState.IsValid)
            {
                var teamFacade = new TeamFacade();
                teamFacade.CreateTeam(teamDTO);
                return RedirectToAction("Teams");
            }

            return View(teamDTO);
        }

        // GET: Team/Edit/5
        public ActionResult Edit(int id)
        {
            var teamFacade = new TeamFacade();
            var team = teamFacade.GetSpecificTeam(id);
            return View(team);
        }

        // POST: Team/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind(Include = "Id,TeamName")] TeamDTO teamDTO)
        {
            var teamFacade = new TeamFacade();
            if (ModelState.IsValid)
            {
                var originalTeam = teamFacade.GetSpecificTeam(id);
                originalTeam.TeamName = teamDTO.TeamName;
                teamFacade.UpdateTeam(originalTeam);
                return RedirectToAction("Teams");
            }
            return View(teamDTO);
        }

        // GET: Team/Delete/5
        public ActionResult Delete(int id)
        {
            var teamFacade = new TeamFacade();
            TeamDTO teamDTO = teamFacade.GetSpecificTeam(id);
            if (teamDTO == null)
            {
                return HttpNotFound();
            }
            return View(teamDTO);
        }

        // POST: Team/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var teamFacade = new TeamFacade();
            teamFacade.DeleteTeam(id);
            return RedirectToAction("Teams");
        }

    }
}
