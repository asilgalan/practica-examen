using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using PracticandoExamenViernes.Models;
using PracticandoExamenViernes.Repositories;

namespace PracticandoExamenViernes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComicsController : ControllerBase
    {

        private readonly RepositoryComics repository;
        public ComicsController(RepositoryComics repository)
        {

            this.repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<List<Comic>>> GetComics()
        {
            List<Comic> comics = await this.repository.GetComicsAsync();
            List<Comic> result = comics
                .Select(ContruirComic)
                .ToList();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Comic>> GetComic(int id)
        {
            Comic comic = await this.repository.GetComicAsync(id);
            if (comic == null)
            {
                return NotFound();
            }

            return Ok(ContruirComic(comic));
        }

        [HttpPost]
        public async Task<ActionResult> CreateComic(Comic comic)
        {
            await this.repository.CreateComicAsync(comic);

            return CreatedAtAction(nameof(GetComic), new { id = comic.Id }, ContruirComic(comic));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateComic(int id, Comic comic)
        {
            if (id != comic.Id)
            {
                return BadRequest();
            }

            Comic existing = await this.repository.GetComicAsync(id);
            if (existing == null)
            {
                return NotFound();
            }

            await this.repository.UpdateComicAsync(comic);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteComic(int id)
        {
            Comic comic = await this.repository.GetComicAsync(id);
            if (comic == null)
            {
                return NotFound();
            }

            await this.repository.RemoveComicAsync(id);
            return NoContent();
        }

        private Comic ContruirComic(Comic comic)
        {
            return new Comic
            {
                Id = comic.Id,
                Titulo = comic.Titulo,
                Descripcion = comic.Descripcion,
                Imagen = ContruirImagen(comic.Imagen)
            };
        }

        private string ContruirImagen(string imageName)
        {
  
            var baseUri = new Uri($"{Request.Scheme}://{Request.Host}{Request.PathBase}/");
            return new Uri(baseUri, $"images/{imageName}").ToString();
        }

    }
}
