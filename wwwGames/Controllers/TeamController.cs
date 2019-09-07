using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace wwwGames.Controllers
{
    public class TeamController : Controller
    {
        public IActionResult Team(int id)
        {
            ViewData["id"] = id;
            return View();
        }
    }
}