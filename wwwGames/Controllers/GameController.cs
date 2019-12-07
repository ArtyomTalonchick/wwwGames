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
    public class GameController : Controller
    {

        ContextDb db;

        public GameController(ContextDb context)
        {
            db = context;
        }

        [HttpGet]
        public ICollection<GameName> GetAllGameNames()
        {
            return db.GameNames.ToList();
        }

        [HttpPost]
        [Authorize(Roles = "teamLead")]
        public async Task<Game> Create(int teamId, int gameNameId)
        {
            User user = db.Users.Find(int.Parse(User.Identity.Name));
            int teamId1 = user.TeamId;
            Game game = new Game { Team1Id = teamId1, Team2Id = teamId, GameNameId = gameNameId };
            db.Games.Add(game);
            await db.SaveChangesAsync();
            return game;
        }

        [HttpPut]
        [Authorize(Roles = "teamLead")]
        public async Task<bool> Remove(int id)
        {
            Game game = db.Games.FirstOrDefault(g => g.Id == id);
            if (game != null)
            {
                db.Games.Remove(game);
                await db.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        [HttpGet]
        public ICollection<Game> GetForUser(int? teamId)
        {
            if (teamId == null)
            {
                User user = db.Users.Find(int.Parse(User.Identity.Name));
                teamId = user.TeamId;
            }

            ICollection<Game> games = db.Games
                .Include(g => g.GameName)
                .ToList().Where(g => g.Team1Id == teamId || g.Team2Id == teamId)?.ToList();
            foreach (Game game in games)
            {
                if (game.Team1Id != teamId)
                {
                    int temp = game.Team1Id;
                    game.Team1Id = game.Team2Id;
                    game.Team2Id = temp;
                }
            }
            return games;
        }

        public IActionResult List()
        {
            User user = db.Users.Find(int.Parse(User.Identity.Name));
            Role teamLeadRole = db.Roles.FirstOrDefault(r => r.Name == "teamLead");
            ViewBag.isTeamLead = user.Role == teamLeadRole;

            return View(GetForUser(null));
        }


        public IActionResult Room()
        {
            return View();
        }

        [Authorize(Roles = "teamLead")]
        public ActionResult Create()
        {
            return PartialView("Create");
        }

        // temp action
        public IActionResult Hub()
        {
            return View();
        }
    }
}