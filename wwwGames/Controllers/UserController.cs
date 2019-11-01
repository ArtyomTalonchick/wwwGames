using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System;
using System.Threading.Tasks;
using wwwGames.ViewModels;
using wwwGames.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace wwwGames.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        ContextDb db;

        public UserController(ContextDb context)
        {
            db = context;
        }

        [HttpGet]
        public ICollection<User> GetAllUsers(int? teamId)
        {
            ICollection<User> users;
            if (teamId == null)
            {
                users = db.Users.ToList();
            }
            else
            {
                users = db.Teams.Include(t => t.Users).FirstOrDefault(t => t.Id == teamId).Users;
            }
            foreach(var user in users)
            {
                user.Team = null;
            }
            return users;
        }

        [HttpGet]
        public User GetUser(int? userId)
        {
            if (userId == null)
            {
                userId = int.Parse(User.Identity.Name);
            }
            User user = db.Users.Find(userId);
            return user;
        }

        public ActionResult CardInfo()
        {
            return PartialView("CardInfo");
        }

        public IActionResult AllUsers(int? teamId)
        {
            ViewBag.teamId = teamId;
            return View(GetAllUsers(teamId));
        }

        public IActionResult Card(int? userId)
        {
            return View(GetUser(userId));
        }

        [HttpGet]
        public IActionResult Edit()
        {
            User user = GetUser(null);
            UserModel model = new UserModel { Name = user.Name, Email = user.Email, Password = user.Password };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserModel model)
        {
            if (ModelState.IsValid)
            {
                User user = GetUser(null);
                if (user != null)
                {
                    user.Name = model.Name;
                    user.Email = model.Email;
                    user.Password = model.Password;
                    await db.SaveChangesAsync();
                    return RedirectToAction("Card", "User");
                }
            }
            return View(model);
        }
    }
}