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
    public class ProfessorController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProfessorController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Listar todos os professores
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Professor>>> GetProfessores()
        {
            var professores = await _context.Professores.ToListAsync();
            return Ok(professores);
        }

        // Buscar professor por ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Professor>> GetProfessor(int id)
        {
            var professor = await _context.Professores.FindAsync(id);
            if (professor == null)
                return NotFound("Professor não encontrado.");

            return Ok(professor);
        }

        // Cadastrar um novo professor
        [HttpPost("cadastrar")]
        public async Task<IActionResult> CadastrarProfessor([FromBody] CreateProfessorDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Verifica se a academia existe, caso tenha sido informada
            if (dto.AcademiaId.HasValue)
            {
                var academiaExiste = await _context.Academias.AnyAsync(a => a.Id == dto.AcademiaId);
                if (!academiaExiste)
                    return BadRequest("A academia informada não existe.");
            }

            var professor = new Professor
            {
                Nome = dto.Nome,
                AcademiaId = dto.AcademiaId
            };

            try
            {
                _context.Professores.Add(professor);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetProfessor), new { id = professor.Id }, professor);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"Erro ao salvar o professor: {ex.Message}");
            }
        }
    }
}
