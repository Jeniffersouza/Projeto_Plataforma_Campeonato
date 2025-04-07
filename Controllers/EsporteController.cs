using Microsoft.AspNetCore.Mvc;
using PlataformaJiujitsu.Models;
using PlataformaAPI.Data;

[Route("api/[controller]")]
[ApiController]
public class EsporteController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public EsporteController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public IActionResult CadastrarEsporte([FromBody] Esporte esporte)
    {
        if (esporte == null)
            return BadRequest("Dados inválidos");

        _context.Esportes.Add(esporte);
        _context.SaveChanges();

        return CreatedAtAction(nameof(GetEsporte), new { id = esporte.Id }, esporte);
    }

    [HttpGet("{id}")]
    public IActionResult GetEsporte(int id)
    {
        var esporte = _context.Esportes.Find(id);
        if (esporte == null)
            return NotFound();

        return Ok(esporte);
    }

    [HttpGet]
    public IActionResult GetEsportes()
    {
        return Ok(_context.Esportes.ToList());
    }
}
