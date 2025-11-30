using ApiRestVideoGames.Models;
using ApiRestVideoGames.Persistence;
using ApiRestVideoGames.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiRestVideoGames.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImportController : ControllerBase
    {
        private readonly ImportService _importService;
        private readonly IVideoGameRepository _repo;

        public ImportController(ImportService importService, IVideoGameRepository repo)
        {
            _importService = importService;
            _repo = repo;
        }

        [HttpPost]
        public async Task<IActionResult> Import()
        {
            string path = Path.Combine(
                AppContext.BaseDirectory,
                "dataset",
                "vgsales.csv"
            );


            if (!System.IO.File.Exists(path))
                return NotFound($"No se encontró el archivo en: {path}");

            var items = _importService.ImportCsv(path);

            foreach (var item in items)
            {
                await _repo.AddVideoGameAsync(item);
            }

            return Ok(new { message = $"Importados {items.Count} videojuegos." });
        }
    }
}
