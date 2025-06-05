using API_DndBot.Model;
using Microsoft.AspNetCore.Mvc;

namespace API_DndBot.Controllers
{
    [ApiController]
    public class GeneratorLocationController : ControllerBase
    {
        private DndBotContext db;
        public GeneratorLocationController(DndBotContext db)
        {
            this.db = db;
        }
        [HttpGet]
        [Route("Locations")]
        public IQueryable GetGeneratorLocations()
        {
            var locs = from e in db.GeneratorLocations
                        select new
                        {
                            e.IdLocation,
                            e.NameLocation,
                            e.DescriptionLocation,
                            e.Setting,
                            e.FromWho
                        };
            return locs.AsQueryable();
        }
    }
}
