using Microsoft.AspNetCore.Mvc;
using PlataformaJiujitsu.Models; // Substitua pelo namespace do seu modelo de Academia
using PlataformaJiujitsu.Data; // Substitua pelo namespace do seu ApplicationDbContext
using System.Threading.Tasks;
using PlataformaAPI.Data;
using PlataformaJiujitsu.Data.Dtos;
using Microsoft.EntityFrameworkCore;

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

        // Endpoint para cadastrar uma nova academia
        [HttpPost("cadastrar")]
        public async Task<IActionResult> CadastrarAcademia([FromBody] CreateAcademiaDto dto)
        {
            if (string.IsNullOrEmpty(dto.Nome))
            {
                return BadRequest("O nome da academia é obrigatório.");
            }

            var academia = new Academia
            {
                Nome = dto.Nome,
                Endereco = dto.Endereco, // Pode ser vazio
                CNPJ = dto.CNPJ // Pode ser vazio
            };

            try
            {
                _context.Academias.Add(academia);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(CadastrarAcademia), new { id = academia.Id }, academia);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"Erro ao salvar a academia: {ex.Message}");
            }
        }
    }
}
