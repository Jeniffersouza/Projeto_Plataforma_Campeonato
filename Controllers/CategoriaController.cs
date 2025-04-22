using Microsoft.AspNetCore.Mvc;
using PlataformaAPI.Data;
using PlataformaJiujitsu.Data;
using PlataformaJiujitsu.Data.Dtos;
using PlataformaJiujitsu.Models;
using System;


namespace PlataformaJiujitsu.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CategoriaController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult CadastrarCategoria([FromBody] CategoriaDto categoriaDto)
        {
            if (categoriaDto == null)
                return BadRequest("Dados inválidos");

            var campeonato = _context.Campeonatos.Find(categoriaDto.CampeonatoId);
            if (campeonato == null)
                return NotFound("Campeonato não encontrado");
            // Conversão segura de string para enum
            if (!Enum.TryParse<SexoEnum>(categoriaDto.Sexo, true, out var sexoConvertido))
                return BadRequest("Sexo inválido. Use 'Masculino' ou 'Feminino'.");

            var categoria = new Categoria
            {
                Nome = categoriaDto.Nome,
                PesoMinimo = categoriaDto.PesoMinimo,
                PesoMaximo = categoriaDto.PesoMaximo,
                FaixaEtaria = categoriaDto.FaixaEtaria, 
                Sexo = sexoConvertido,
                GraduacaoId = categoriaDto.GraduacaoId,
                CampeonatoId = categoriaDto.CampeonatoId
            };

            _context.Categoria.Add(categoria);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetCategoria), new { id = categoria.Id }, categoria);
        }

        [HttpGet("{id}")]
        public IActionResult GetCategoria(int id)
        {
            var categoria = _context.Categoria.Find(id);
            if (categoria == null)
                return NotFound();

            return Ok(categoria);
        }

        [HttpGet]
        public IActionResult GetCategorias()
        {
            return Ok(_context.Categoria.ToList());
        }
    }
}
