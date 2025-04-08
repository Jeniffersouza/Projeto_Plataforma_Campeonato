using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlataformaAPI.Data;
using PlataformaAPI.Data.Dtos;
using PlataformaAPI.Models;
using PlataformaJiujitsu.Models;

namespace PlataformaAPI.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class CampeonatoController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CampeonatoController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult AdicionaCampeonato([FromBody] CreateCampeonatoDto campeonatoDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Campeonato campeonato = _mapper.Map<Campeonato>(campeonatoDto);
            _context.Campeonatos.Add(campeonato);
            _context.SaveChanges();

            return CreatedAtAction(nameof(RecuperarCampeonatoPorId), new { id = campeonato.Id }, campeonato);
        }

        [HttpGet]
        public IEnumerable<ReadCampeonatoDto> RecuperarCampeonatos([FromQuery] int skip = 0, [FromQuery] int take = 50)
        {
            return _mapper.Map<List<ReadCampeonatoDto>>(_context.Campeonatos.Skip(skip).Take(take));
        }

        [HttpGet("{id}")]
        public IActionResult RecuperarCampeonatoPorId(int id)
        {
            var campeonato = _context.Campeonatos.FirstOrDefault(c => c.Id == id);

            if (campeonato == null)
            {
                return NotFound("Campeonato não encontrado.");
            }
            var campeonatoDto = _mapper.Map<ReadCampeonatoDto>(campeonato);

            return Ok(campeonatoDto);
        }

        [HttpPut("{id}")]
        public IActionResult AtualizaCampeonato(int id, [FromBody] UpdateCampeonatoDto campeonatoDto)
        {
            var campeonato = _context.Campeonatos.FirstOrDefault(
                campeonato => campeonato.Id == id);
            if (campeonato == null) return NotFound();
            _mapper.Map(campeonatoDto, campeonato);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletaCampeonato(int id)
        {
            var campeonato = _context.Campeonatos.FirstOrDefault(c => c.Id == id);

            if (campeonato == null)
            {
                return NotFound(new { mensagem = "Campeonato não encontrado." });
            }

            _context.Campeonatos.Remove(campeonato);
            _context.SaveChanges();

            return NoContent(); // Retorna 204 (No Content) para indicar que foi excluído com sucesso
        }
        [HttpGet("campeonato/{campeonatoId}/inscritos")]
        public async Task<IActionResult> GetAtletasInscritosNoCampeonato(int campeonatoId)
        {
            // Verificar se o campeonato existe
            var campeonato = await _context.Campeonatos.FirstOrDefaultAsync(c => c.Id == campeonatoId);
            if (campeonato == null)
            {
                return NotFound("Campeonato não encontrado.");
            }

            // Recuperar os atletas inscritos no campeonato
            var atletasInscritos = await _context.Inscricoes
                .Where(i => i.CampeonatoId == campeonatoId)
                .Select(i => new
                {
                    AtletaId = i.AtletaId,
                    NomeAtleta = i.Atleta.Usuario.Nome,
                    DataInscricao = i.DataInscricao
                })
                .ToListAsync();

            if (atletasInscritos.Count == 0)
            {
                return Ok("Nenhum atleta inscrito neste campeonato.");
            }

            return Ok(atletasInscritos);
        }
    }
}
