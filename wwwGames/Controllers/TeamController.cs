using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public IActionResult AllTeams()
        {
            return View(db.Teams.ToList());
        }
        public IActionResult Team(int? id)
        {
            if (id == null)
            {
                id = int.Parse(User.Identity.Name);
            }

            Team team= db.Teams.Include(t => t.Users).FirstOrDefault(t => t.Id == id);

            return View(team);
        }
    }
}