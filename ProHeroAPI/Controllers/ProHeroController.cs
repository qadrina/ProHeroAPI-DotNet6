using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProHeroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProHeroController : ControllerBase
    {
        // YOU DO NOT NEED THIS ANYMORE bcs it's already connected to the database
        //private static List<ProHero> heroes = new List<ProHero>
        //    {
        //        new ProHero {
        //            Id = 1,
        //            Name = "Eraserhead",
        //            FirstName = "Shouta",
        //            LastName = "Aizawa",
        //            Place = "Tokyo"
        //        }
        //    };

        private readonly DataContext _context;

        public ProHeroController(DataContext context)
        {
            _context = context;
        }

        // GET ALL HEROES FROM DATABASE
        [HttpGet]
        public async Task<ActionResult<List<ProHero>>> Get()
        {
            return Ok(await _context.ProHeroes.ToListAsync());
        }

        // GET INDIVIDUAL HEROES
        [HttpGet("{id}")]
        public async Task<ActionResult<ProHero>> Get(int id)
        {
            var hero = await _context.ProHeroes.FindAsync(id);
            if (hero == null)
                return BadRequest("Hero not found.");
            return Ok(hero);
        }

        // ADD HEROES
        [HttpPost]
        public async Task<ActionResult<List<ProHero>>> AddHero(ProHero hero)
        {
            _context.ProHeroes.Add(hero);
            await _context.SaveChangesAsync();
            return Ok(await _context.ProHeroes.ToListAsync());
        }

        // EDIT HERO
        [HttpPut]
        public async Task<ActionResult<List<ProHero>>> EditHero(ProHero request)
        {
            var dbHero = await _context.ProHeroes.FindAsync(request.Id);
            if (dbHero == null)
                return BadRequest("Hero not found.");

            dbHero.Name = request.Name;
            dbHero.FirstName = request.FirstName;
            dbHero.LastName = request.LastName;
            dbHero.Place = request.Place;

            await _context.SaveChangesAsync();

            return Ok(await _context.ProHeroes.ToListAsync());
        }

        // DELETE HERO
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<ProHero>>> Delete(int Id)
        {
            var dbHero = await _context.ProHeroes.FindAsync(Id);
            if (dbHero == null)
                return BadRequest("Hero not found.");

            _context.ProHeroes.Remove(dbHero);
            await _context.SaveChangesAsync();
            return Ok(await _context.ProHeroes.ToListAsync());
        }
    }
}
