using FiotecApi.Data;
using FiotecApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace FiotecApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AutoresController : ControllerBase
    {
        private readonly BibliotecaContext _context;

        public AutoresController(BibliotecaContext context)
        {
            _context = context;
        }

        // GET: api/autores
        [HttpGet]
        public async Task<IActionResult> GetAutores()
        {
            var autores = await _context.Autores.ToListAsync();
            return Ok(autores);
        }

        // GET: api/autores/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAutor(int id)
        {
            var autor = await _context.Autores.FindAsync(id);
            if (autor == null)
            {
                return NotFound();
            }
            return Ok(autor);
        }

        // POST: api/autores
        [HttpPost]
        public async Task<IActionResult> CreateAutor([FromBody] Autor autor)
        {
            if (autor == null)
            {
                return BadRequest();
            }

            _context.Autores.Add(autor);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAutor), new { id = autor.Id }, autor);
        }

        // PUT: api/autores/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAutor(int id, [FromBody] Autor autor)
        {
            if (id != autor.Id)
            {
                return BadRequest();
            }

            _context.Entry(autor).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/autores/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAutor(int id)
        {
            var autor = await _context.Autores.FindAsync(id);
            if (autor == null)
            {
                return NotFound();
            }

            _context.Autores.Remove(autor);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}