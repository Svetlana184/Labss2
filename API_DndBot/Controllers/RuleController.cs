using API_DndBot.Model;
using Microsoft.AspNetCore.Mvc;

namespace API_DndBot.Controllers
{
    [ApiController]
    public class RuleController : ControllerBase
    {
        private DndBotContext db;
        public RuleController(DndBotContext db)
        {
            this.db = db;
        }
        [HttpGet]
        [Route("Rules")]
        public IQueryable GetRules()
        {
            var rules = from e in db.Rules
                       select new
                       {
                           e.IdRule,
                           e.NameRule,
                           e.TypeOfRule,
                           e.Link
                       };
            return rules.AsQueryable();
        }
    }
}
