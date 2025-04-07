using Microsoft.AspNetCore.Mvc;
using PlataformaJiujitsu.Models;
using PlataformaAPI.Data;

[Route("api/[controller]")]
[ApiController]
public class GraduacaoController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public GraduacaoController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public IActionResult CadastrarGraduacao([FromBody] Graduacao graduacaoDto)
    {
        if (graduacaoDto == null)
            return BadRequest("Dados inválidos");

        var esporte = _context.Esportes.Find(graduacaoDto.EsporteId);
        if (esporte == null)
            return NotFound("Esporte não encontrado");

        var graduacao = new Graduacao
        {
            Nome = graduacaoDto.Nome,
            EsporteId = graduacaoDto.EsporteId
        };

        _context.Graduacoes.Add(graduacao);
        _context.SaveChanges();

        return CreatedAtAction(nameof(GetGraduacao), new { id = graduacao.Id }, graduacao);
    }

    [HttpGet("{id}")]
    public IActionResult GetGraduacao(int id)
    {
        var graduacao = _context.Graduacoes.Find(id);
        if (graduacao == null)
            return NotFound();

        return Ok(graduacao);
    }

    [HttpGet]
    public IActionResult GetGraduacoes()
    {
        return Ok(_context.Graduacoes.ToList());
    }
}
