using API_DndBot.Model;
using Microsoft.AspNetCore.Mvc;

namespace API_DndBot.Controllers
{
    [ApiController]
    public class GeneratorVibeController : ControllerBase
    {
        private DndBotContext db;
        public GeneratorVibeController(DndBotContext db)
        {
            this.db = db;
        }
        [HttpGet]
        [Route("Vibes")]
        public IQueryable GetGeneratorVibes()
        {
            var vibes = from e in db.GeneratorVibes
                        select new
                        {
                            e.IdVibes,
                            e.NameVibes,
                            e.TextVibes,
                            e.FromWho
                        };
            return vibes.AsQueryable();
        }
    }
}
