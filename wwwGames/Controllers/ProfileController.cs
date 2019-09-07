using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace wwwGames.Controllers
{
    public class ProfileController : Controller
    {
        public IActionResult Card(int id)
        {
            ViewData["id"] = id;
            return View();
        }
        public IActionResult Edit(int id)
        {
            ViewData["id"] = id;
            return View();
        }
    }
}