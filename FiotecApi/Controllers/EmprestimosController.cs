using FiotecApi.Data;
using FiotecApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class EmprestimosController : ControllerBase
{
    private readonly BibliotecaContext _context;

    public EmprestimosController(BibliotecaContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Emprestimo>>> GetEmprestimos()
    {
        return await _context.Emprestimos.Include(e => e.Livros).ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Emprestimo>> GetEmprestimo(int id)
    {
        var emprestimo = await _context.Emprestimos.Include(e => e.Livros).FirstOrDefaultAsync(e => e.Id == id);

        if (emprestimo == null)
        {
            return NotFound();
        }

        return emprestimo;
    }

    [HttpPost]
    public async Task<IActionResult> PostEmprestimo(Emprestimo emprestimo)
    {
        if (emprestimo == null)
        {
            return BadRequest();
        }

        await _context.Emprestimos.AddAsync(emprestimo);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetEmprestimo), new { id = emprestimo.Id }, emprestimo);
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> PutEmprestimo(int id, Emprestimo emprestimo)
    {
        if (id != emprestimo.Id)
        {
            return BadRequest();
        }

        _context.Entry(emprestimo).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!EmprestimoExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    [HttpPost("devolucao/{id}")]
    public async Task<IActionResult> Devolucao(int id)
    {
        var emprestimo = await _context.Emprestimos
            .Include(e => e.Livros)
            .FirstOrDefaultAsync(e => e.Id == id);

        if (emprestimo == null)
        {
            return NotFound();
        }

        emprestimo.DataDevolucao = DateTime.Now;

        foreach (var livro in emprestimo.Livros)
        {
            livro.Disponibilidade = true;
        }

        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool EmprestimoExists(int id)
    {
        return _context.Emprestimos.Any(e => e.Id == id);
    }
}