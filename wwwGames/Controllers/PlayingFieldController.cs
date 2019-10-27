using Microsoft.AspNetCore.Mvc;

namespace wwwGames.Controllers
{
    public class PlayingFieldController : Controller
    {
        public IActionResult PlayingField(int id)
        {
            return View();
        }
    }
}