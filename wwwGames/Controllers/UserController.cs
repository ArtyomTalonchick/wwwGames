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
        private int id;

        public UserController(ContextDb context)
        {
            db = context;
        }

        public IActionResult AllUsers(int? teamId)
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
            ViewBag.teamId = teamId;
            return View(users);
        }

        public IActionResult Card(int? id)
        {
            if (id == null)
            {
                id = int.Parse(User.Identity.Name);
            }
            User user = db.Users.Find(id);

            return View(user);
        }

        [HttpGet]
        public IActionResult Edit()
        {
            int id = int.Parse(User.Identity.Name);
            User user = db.Users.Find(id);
            UserModel model = new UserModel { Name = user.Name, Email = user.Email, Password = user.Password };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserModel model)
        {
            if (ModelState.IsValid)
            {
                int id = int.Parse(User.Identity.Name);
                User user = db.Users.Find(id);
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