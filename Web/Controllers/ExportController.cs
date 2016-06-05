using BL.Facades;
using BL.Export;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class ExportController : Controller
    {
        private TournamentFacade tournamentFacade = new TournamentFacade();
        public ActionResult Index(int id)
        {
            var model = tournamentFacade.GetSpecificTournament(id);
            ExportToXML.WriteXML(model);
            return View(model);
        }
    }
}