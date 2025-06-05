using Microsoft.AspNetCore.Mvc;

namespace API_DndBot.Controllers
{
    public class GameController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
