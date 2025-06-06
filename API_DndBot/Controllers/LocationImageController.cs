using API_DndBot.Model;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace API_DndBot.Controllers
{
    [ApiController]
    public class LocationImageController : ControllerBase
    {
        private DndBotContext db;
        public LocationImageController(DndBotContext db)
        {
            this.db = db;
        }
        [HttpGet]
        [Route("LocationImages")]
        public IQueryable GetLocationImages()
        {
            var images = from e in db.LocationImages
                        select new
                        {
                           e.IdLocationImage,
                           e.NameImage,
                           e.IdLocation,
                           e.Source
                        };
            return images.AsQueryable();
        }
    }
}
