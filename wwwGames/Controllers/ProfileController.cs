using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using wwwGames.Models;

namespace wwwGames.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        ContextDb db;

        public ProfileController(ContextDb context)
        {
            db = context;
        }
        
        public IActionResult Card(int id)
        {
            return View(db.Users.ToList());
        }
        
        public IActionResult Edit(int id)
        {
            ViewData["id"] = id;
            return View();
        }
    }
}