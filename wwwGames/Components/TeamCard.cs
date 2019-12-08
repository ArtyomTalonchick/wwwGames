using Microsoft.AspNetCore.Mvc;
using System;
using wwwGames.Models;

namespace wwwGames.Components
{
    public class TeamCard : ViewComponent
    {
        public IViewComponentResult Invoke(Team team)
        {
            return View("TeamCard", team);
        }
    }
}
