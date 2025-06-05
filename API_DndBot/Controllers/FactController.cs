using API_DndBot.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API_DndBot.Controllers
{
    [ApiController]
    public class FactController : ControllerBase
    {
        private DndBotContext db;
        public FactController(DndBotContext db)
        {
            this.db = db;
        }
        [HttpGet]
        [Route("Facts")]
        public IQueryable GetFacts()
        {
            var fact = from e in db.Facts
                      select new
                      {
                         e.IdFact,
                         e.NameFact,
                         e.DescriptionFact
                      };
            return fact.AsQueryable();
        }
    }
}
