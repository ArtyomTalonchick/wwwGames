using System.Collections.Generic;
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

        [HttpGet]
        public ICollection<Team> GetAllTeams()
        {
            return db.Teams.ToList();
        }

        [HttpGet]
        public Team GetTeam(int? teamId)
        {
            User user = db.Users.Find(int.Parse(User.Identity.Name));
            if (teamId == null)
            {
                teamId = user.TeamId;
            }
            Team team = db.Teams.Include(t => t.Users).FirstOrDefault(t => t.Id == teamId);
            return team;
        }

        public string GetName(int id)
        {
            return GetTeam(id).Name;
        }

        public IActionResult AllTeams()
        {
            return View(GetAllTeams());
        }

        public IActionResult Team(int? id)
        {
            User user = db.Users.Find(int.Parse(User.Identity.Name));
            Role teamLeadRole = db.Roles.FirstOrDefault(r => r.Name == "teamLead");
            if (user.Role == teamLeadRole && (id == null || id == user.TeamId))
            {
                return RedirectToAction("EditTeam", "Team");
            }

            return View(GetTeam(id));
        }

        [Authorize(Roles = "teamLead")]
        public IActionResult EditTeam()
        {
            return View(GetTeam(null));
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