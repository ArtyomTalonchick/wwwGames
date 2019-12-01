using System.Collections.Generic;
using System.Linq;
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

        [HttpPost]
        public InvitationCode Create(int teamId, int game1)
        {
            //InvitationCode duplicationCode = db.InvitationCodes.FirstOrDefault(c => c.Code == code);
            //if (code != "" && duplicationCode == null)
            //{
            //    Team team = db.Teams.FirstOrDefault(t => t.Id == teamId);
            //    InvitationCode invitationCode = new InvitationCode { Team = team, Code = code };
            //    db.InvitationCodes.Add(invitationCode);
            //    db.SaveChanges();
            //    return invitationCode;
            //}
            //else
            //{
            //    return null;
            //}
            return null;
        }

        [HttpGet]
        public ICollection<Game> GetForUser(int? teamId)
        {
            ICollection<Game> games;
            if (teamId == null)
            {
                User user = db.Users.Find(int.Parse(User.Identity.Name));
                teamId = user.TeamId;
            }
            games = db.Games.Include(g => g.GameName).ToList().Where(g => g.TeamId1 == teamId || g.TeamId2 == teamId)?.ToList();
            return games;
        }

        public IActionResult List()
        {
            return View(GetForUser(null));
        }


        public IActionResult Room()
        {
            return View();
        }
    }
}