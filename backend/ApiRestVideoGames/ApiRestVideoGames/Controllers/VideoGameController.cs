using ApiRestVideoGames.Models;
using ApiRestVideoGames.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiRestVideoGames.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]

    public class VideoGameController : ControllerBase
    {
        private readonly IVideoGameRepository _repo;

        public VideoGameController(IVideoGameRepository repo)
        {
            _repo = repo;
        }

        // GET api/VideoGame
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int limit = 500)
        {
            var games = await _repo.GetAllVideoGamesAsync();
            return Ok(games.Take(limit));
        }


        // GET api/VideoGame/24
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var item = await _repo.GetVideoGameAsync(id);
            if (item == null)
                return NotFound();

            return Ok(item);
        }

        // POST api/VideoGame
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] VideoGame game)
        {
            await _repo.AddVideoGameAsync(game);
            return Ok(new { message = "Videojuego insertado correctamente" });
        }

        // PUT api/VideoGame/24
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] VideoGame game)
        {
            var existing = await _repo.GetVideoGameAsync(id);
            if (existing == null)
                return NotFound();

            game.Rank = id;   // Aseguramos que modifica el correcto
            await _repo.UpdateVideoGameAsync(game);

            return Ok(new { message = "Videojuego actualizado" });
        }

        // DELETE api/VideoGame/24
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await _repo.GetVideoGameAsync(id);
            if (existing == null)
                return NotFound();

            await _repo.DeleteVideoGameAsync(id);
            return Ok(new { message = "Videojuego eliminado" });
        }
    }
}
