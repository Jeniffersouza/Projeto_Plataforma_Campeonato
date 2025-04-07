using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlataformaAPI.Data;
using PlataformaJiujitsu.Models;
using PlataformaJiujitsu.Data.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlataformaJiujitsu.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AcademiaController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AcademiaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Método para listar todas as academias
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Academia>>> GetAcademias()
        {
            var academias = await _context.Academias.ToListAsync();
            return Ok(academias);
        }

        // Método para buscar uma academia por ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Academia>> GetAcademia(int id)
        {
            var academia = await _context.Academias.FindAsync(id);
            if (academia == null)
                return NotFound("Academia não encontrada.");

            return Ok(academia);
        }

        // Endpoint para cadastrar uma nova academia
        [HttpPost("cadastrar")]
        public async Task<IActionResult> CadastrarAcademia([FromBody] CreateAcademiaDto dto)
        {
            if (string.IsNullOrEmpty(dto.Nome))
                return BadRequest("O nome da academia é obrigatório.");

   

            var academia = new Academia
            {
                Nome = dto.Nome,
                
            };

            try
            {
                _context.Academias.Add(academia);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetAcademia), new { id = academia.Id }, academia);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"Erro ao salvar a academia: {ex.Message}");
            }
        }
    }
}
