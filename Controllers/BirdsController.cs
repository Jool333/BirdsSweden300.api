using BirdsSweden300.api.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BirdsSweden300.api.ViewModels.Bird;
using BirdsSweden300.api.Entities;

namespace BirdsSweden300.api.Controllers
{
    [ApiController]
    [Route("api/v1/birds")]
    public class BirdsController : ControllerBase
    {
        private readonly BirdsContext _context;
        private readonly string _imageBaseUrl;
        public BirdsController(BirdsContext context, IConfiguration config)
        {
            _context = context;
            _imageBaseUrl = config.GetSection("apiImageUrl").Value;
        }

        [HttpGet()]
        public async Task<IActionResult> ListAll()
        {
                var result = await _context.Birds
                .Select(v => new
                {
                    Id = v.Id,
                    Name = v.Name,
                    Species = v.Species,
                    ImageUrl = _imageBaseUrl + v.ImageURL ?? "no-bird.png"
                })
                .ToListAsync();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _context.Birds
                .Select(v => new
                {
                    Id = v.Id,
                    Name = v.Name,
                    Species = v.Species,
                    Genus = v.Genus,
                    Family = v.Family,
                    Description = v.Description,
                    ImageUrl = _imageBaseUrl + v.ImageURL ?? "no-bird.png"
                })
                .SingleOrDefaultAsync(c => c.Id == id);

            return Ok(result);
        }


        [HttpPost("create")]
        public async Task<IActionResult> Add(BirdPostViewModel bird)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("All information är inte med i anropet");
            }

            if (await _context.Birds.SingleOrDefaultAsync(c => c.Name == bird.Name) is not null)
            {
                return BadRequest($"Arten {bird.Name} finns redan i systemet");
            }

            var birdToAdd = new Bird
            {
                Name = bird.Name,
                Species = bird.Species,
                Genus = bird.Genus,
                Family = bird.Family,
                Description = bird.Description
            };

            try
            {
                await _context.Birds.AddAsync(birdToAdd);

                if (await _context.SaveChangesAsync() > 0)
                {
                    // return StatusCode(201);
                    return CreatedAtAction(nameof(GetById), new { id = birdToAdd.Id },
                    new
                    {
                        Id = birdToAdd.Id,
                        Name = birdToAdd.Name,
                        Species = birdToAdd.Species,
                        Genus = birdToAdd.Genus,
                        Family = birdToAdd.Family,
                        ImgaeUrl = birdToAdd.ImageURL,
                        Description = birdToAdd.Description
                    });
                }

                return StatusCode(500, "Internal Server Error");
            }
            catch (Exception ex)
            {
                // loggning till en databas som hanterar debug information...
                Console.WriteLine(ex.Message);
                return StatusCode(500, "Internal Server Error");
            }

        }
        [HttpPatch("seen/{id}")]
        public async Task<IActionResult> MarkAsSeen(int id)
        {
            var birdToUpdate = await _context.Birds.FindAsync(id);
            if (birdToUpdate is null) return NotFound("Fågeln hittas inte");
            birdToUpdate.Seen = !birdToUpdate.Seen;

            _context.Birds.Update(birdToUpdate);
            if (await _context.SaveChangesAsync() > 0) return NoContent();

            return StatusCode(500,"Internt server error");
        }
    }
}