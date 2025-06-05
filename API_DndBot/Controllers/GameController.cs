using API_DndBot.Model;
using Microsoft.AspNetCore.Mvc;

namespace API_DndBot.Controllers
{
    [ApiController]
    public class GameController : ControllerBase
    {
        private DndBotContext db;
        public GameController(DndBotContext db)
        {
            this.db = db;
        }
        [HttpGet]
        [Route("Games")]
        public IQueryable GetGames()
        {
            var games = from e in db.Games
                        select new
                        {
                            e.IdGame,
                            e.NameGame,
                            e.DescriptionGame,
                            e.System,
                            e.Setting,
                            e.Vibes,
                            e.Genre,
                            e.Duration,
                            e.FromWho
                        };
            return games.AsQueryable();
        }
    }
}
