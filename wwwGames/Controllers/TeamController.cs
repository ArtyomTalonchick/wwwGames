using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using wwwGames.Models;

namespace wwwGames.Controllers
{
    [Authorize]
    public class TeamController : Controller
    {
        ContextDb db;

        public TeamController(ContextDb context)
        {
            db = context;
        }

        public ActionResult TeamInfo()
        {
            return PartialView("TeamInfo");
        }

        public string GetName(int id)
        {
            Team team = db.Teams.FirstOrDefault(t => t.Id == id);
            return team.Name;
        }

        public IActionResult AllTeams()
        {
            return View(db.Teams.ToList());
        }

        public IActionResult Team(int? id)
        {
            User user = db.Users.Find(int.Parse(User.Identity.Name));
            if (id == null)
            {
                id = user.TeamId;
            }
            Team team = db.Teams.Include(t => t.Users).FirstOrDefault(t => t.Id == id);
            Role teamLeadRole = db.Roles.FirstOrDefault(r => r.Name == "teamLead");
            if (user.Role == teamLeadRole && id == user.TeamId)
            {
                return RedirectToAction("EditTeam", "Team", new { id });
            }

            return View(team);
        }

        [Authorize(Roles = "teamLead")]
        public IActionResult EditTeam(int? id)
        {
            User user = db.Users.Find(int.Parse(User.Identity.Name));
            if (id == null)
            {
                id = user.TeamId;
            }
            if (id != user.TeamId)
            {
                return RedirectToAction("Error", "Home");
            }

            Team team = db.Teams.Include(t => t.Users).FirstOrDefault(t => t.Id == id);
            return View(team);
        }

        [Authorize(Roles = "teamLead")]
        public ActionResult UsersTable()
        {
            return PartialView("UsersTable");
        }

        [Authorize(Roles = "teamLead")]
        public ActionResult CodesTable()
        {
            return PartialView("CodesTable");
        }
    }
}