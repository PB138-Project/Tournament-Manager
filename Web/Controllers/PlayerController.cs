using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using BL.DTO;
using BL.Facades;

namespace Web.Controllers
{
    public class PlayerController : Controller
    {
     
            public ActionResult Players()
            {
            PlayerFacade playerFacade = new PlayerFacade();
            var model = playerFacade.GetAllPlayers();
                return View(model);
            }

            public ActionResult Create()
            {
                return View();
            }

            [HttpPost]
            public ActionResult Create(PlayerDTO player)
            {
            PlayerFacade playerFacade = new PlayerFacade();
            playerFacade.CreatePlayer(player);
                return RedirectToAction("Players");
            }

            public ActionResult Delete(int id)
            {
            PlayerFacade playerFacade = new PlayerFacade();
            playerFacade.DeletePlayer(id);
                return RedirectToAction("Players");
            }

            public ActionResult Edit(int id)
            {
            PlayerFacade playerFacade = new PlayerFacade();
            var player = playerFacade.GetSpecificPlayer(id);
                return View(player);
            }

            [HttpPost]
            public ActionResult Edit(int id, PlayerDTO player)
            {
            PlayerFacade playerFacade = new PlayerFacade();
            if (ModelState.IsValid)
                {
                    var originalPlayer = playerFacade.GetSpecificPlayer(id);
                    originalPlayer.Age = player.Age;
                    originalPlayer.Name = player.Name;

                    playerFacade.UpdatePlayer(originalPlayer);

                    return RedirectToAction("Players");
                }
                return View(player);
            }

        public List<string> DropDownListMaker()
        {
            var teamFacade = new TeamFacade();
            var list = teamFacade.GetAllTeams();
            List<string> dropDownList = list.Select(item => item.TeamName).ToList();
            return dropDownList;
        }
     }
}