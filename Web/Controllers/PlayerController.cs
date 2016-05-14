using System.Web.Mvc;
using BL.DTO;
using BL.Facades;

namespace Web.Controllers
{
    public class PlayerController : Controller
    {
            PlayerFacade playerFacade = new PlayerFacade();
            [AllowAnonymous]
            public ActionResult Greet(string name)
            {
                return Content(name);
            }

            public ActionResult Redirect()
            {
                return Redirect("http://google.sk");  
            }          

            public ActionResult Players()
            {
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
                playerFacade.CreatePlayer(player);
                return RedirectToAction("Players");
            }

            public ActionResult Delete(int id)
            {
                playerFacade.DeletePlayer(id);
                return RedirectToAction("Players");
            }

            public ActionResult Edit(int id)
            {
                var player = playerFacade.GetSpecificPlayer(id);
                return View(player);
            }

            [HttpPost]
            public ActionResult Edit(int id, PlayerDTO player)
            {
                if (ModelState.IsValid)
                {
                    var originalPlayer = playerFacade.GetSpecificPlayer(id);
                    originalPlayer.Team = player.Team;
                    originalPlayer.Team.TeamName = player.Team.TeamName;
                    originalPlayer.Age = player.Age;
                    originalPlayer.Name = player.Name;
                    originalPlayer.Surname = player.Surname;

                    playerFacade.UpdatePlayer(originalPlayer);

                    return RedirectToAction("Players");
                }
                return View(player);
            }
     }
}