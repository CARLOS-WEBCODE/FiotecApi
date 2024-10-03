using FiotecApi.Data;
using FiotecApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FiotecApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LivrosController : ControllerBase
    {
        private readonly BibliotecaContext _context;

        public LivrosController(BibliotecaContext context)
        {
            _context = context;
        }

        // GET: api/livros
        [HttpGet]
        public async Task<IActionResult> GetLivros()
        {
            var livros = await _context.Livros.ToListAsync();
            return Ok(livros);
        }

        // GET: api/livros/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetLivro(int id)
        {
            var livro = await _context.Livros.FindAsync(id);
            if (livro == null)
            {
                return NotFound();
            }
            return Ok(livro);
        }

        // POST: api/livros
        [HttpPost("livro")]
        public async Task<IActionResult> CreateLivro([FromBody] Livro livro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Livros.Add(livro);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetLivro), new { id = livro.Id }, livro);
        }


        // PUT: api/livros/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLivro(int id, [FromBody] Livro livro)
        {
            if (id != livro.Id)
            {
                return BadRequest();
            }

            _context.Entry(livro).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/livros/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLivro(int id)
        {
            var livro = await _context.Livros.FindAsync(id);
            if (livro == null)
            {
                return NotFound();
            }

            _context.Livros.Remove(livro);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}