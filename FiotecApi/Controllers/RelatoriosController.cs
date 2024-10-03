using FiotecApi.Data;
using FiotecApi.Dto;
using FiotecApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class RelatoriosController : ControllerBase
{
    private readonly BibliotecaContext _context;

    public RelatoriosController(BibliotecaContext context)
    {
        _context = context;
    }

    // 1. Listar todos os livros que estão disponíveis para empréstimo
    [HttpGet("livros-disponiveis")]
    public async Task<ActionResult<IEnumerable<Livro>>> GetLivrosDisponiveis()
    {
        var livrosDisponiveis = await _context.Livros
            .Where(l => l.Disponibilidade == true)
            .ToListAsync();

        return Ok(livrosDisponiveis);
    }

    // 2. Exibir todos os usuários que têm empréstimos ativos
    [HttpGet("emprestimos-ativos")]
    public async Task<ActionResult<IEnumerable<object>>> GetEmprestimosAtivos()
    {
        var emprestimosAtivos = await _context.Emprestimos
            .Include(e => e.Livros)
            .Include(e => e.Usuario) // Inclui o usuário associado
            .Where(e => e.DataDevolucao == null)
            .Select(e => new
            {
                e.Id,
                e.DataEmprestimo,
                e.DataDevolucao,
                UsuarioNome = e.Usuario.Nome, // Pega o nome do usuário
                Livros = e.Livros.Select(l => new
                {
                    l.Id,
                    l.Titulo,
                    l.Disponibilidade,
                    l.AutorId
                }).ToList()
            })
            .ToListAsync();

        return Ok(emprestimosAtivos);
    }

    // 3. Mostrar quais são os livros mais populares da biblioteca
    [HttpGet("livros-populares")]
    public async Task<ActionResult<IEnumerable<LivroPopularDto>>> GetLivrosPopulares()
    {
        var livrosPopulares = await _context.Emprestimos
            .SelectMany(e => e.Livros)
            .GroupBy(l => l.Id)
            .Select(g => new LivroPopularDto
            {
                LivroId = g.Key,
                Titulo = g.FirstOrDefault().Titulo,
                TotalEmprestimos = g.Count()
            })
            .OrderByDescending(l => l.TotalEmprestimos)
            .ToListAsync();

        return livrosPopulares;
    }

    // 4. Listar todos os empréstimos cujo prazo de devolução já expirou
    [HttpGet("emprestimos-expirados")]
    public async Task<ActionResult<IEnumerable<Emprestimo>>> GetEmprestimosExpirados()
    {
        var emprestimosExpirados = await _context.Emprestimos
            .Where(e => e.DataDevolucao == null && e.DataEmprestimo.AddDays(30) < DateTime.Now)
            .Include(e => e.Livros)
            .ToListAsync();

        return emprestimosExpirados;
    }

    // 5. Exibir o histórico completo de empréstimo de um usuário específico
    [HttpGet("historico-usuario/{usuarioId}")]
    public async Task<ActionResult<IEnumerable<Emprestimo>>> GetHistoricoEmprestimosUsuario(int usuarioId)
    {
        var historico = await _context.Emprestimos
            .Where(e => e.UsuarioId == usuarioId)
            .Include(e => e.Livros)
            .ToListAsync();

        return historico;
    }

    // 6. Exibir os livros que não foram emprestados nos últimos 12 meses
    [HttpGet("livros-nao-emprestados")]
    public async Task<ActionResult<IEnumerable<Livro>>> GetLivrosNaoEmprestadosNosUltimos12Meses()
    {
        var dataLimite = DateTime.Now.AddMonths(-12);
        var livrosNaoEmprestados = await _context.Livros
            .Where(l => !_context.Emprestimos.Any(e => e.Livros.Contains(l) && e.DataEmprestimo >= dataLimite))
            .ToListAsync();

        return livrosNaoEmprestados;
    }
}