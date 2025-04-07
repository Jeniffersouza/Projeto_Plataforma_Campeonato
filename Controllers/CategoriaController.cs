using Microsoft.AspNetCore.Mvc;
using PlataformaAPI.Data;
using PlataformaJiujitsu.Data; // Adicionando a referência ao contexto do banco de dados
using PlataformaJiujitsu.Models;
using System;

[ApiController]
[Route("api/categorias")]
public class CategoriaController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public CategoriaController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public IActionResult CriarCategoria([FromBody] Categoria categoria)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        _context.Categorias.Add(categoria);
        _context.SaveChanges();

        return CreatedAtAction(nameof(ObterCategoriaPorId), new { id = categoria.Id }, categoria);
    }

    [HttpGet("{id}")]
    public IActionResult ObterCategoriaPorId(int id)
    {
        var categoria = _context.Categorias.Find(id);

        if (categoria == null)
        {
            return NotFound();
        }

        return Ok(categoria);
    }

    [HttpGet]
    public IActionResult ListarCategorias()
    {
        return Ok(_context.Categorias.ToList());
    }
}
