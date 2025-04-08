using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlataformaAPI.Data;
using PlataformaAPI.Models;
using PlataformaJiujitsu.Data.Dtos;
using PlataformaJiujitsu.Models;

namespace PlataformaJiujitsu.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AtletaController : ControllerBase
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly ApplicationDbContext _context;

        public AtletaController(UserManager<Usuario> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        [HttpPost("cadastrar-atleta")]
        public async Task<IActionResult> CadastrarAtleta(CreateAtletaDto dto)
        {
            // Cria o Identity User
            var usuario = new Usuario
            {
                UserName = dto.Email,
                Email = dto.Email,
                Nome = dto.Nome,
                DataNascimento = dto.DataNascimento,
                TipoUsuario = TipoUsuario.Atleta
            };

            var result = await _userManager.CreateAsync(usuario, dto.Senha);
            if (!result.Succeeded)
                return BadRequest(result.Errors);

            // Cria o Atleta
            var atleta = new Atleta
            {
                UsuarioId = usuario.Id,
                Graduacao = dto.Graduacao,
                Peso = dto.Peso,
                AcademiaId = dto.AcademiaId
            };

            _context.Atletas.Add(atleta);
            await _context.SaveChangesAsync();

            return Ok("Atleta cadastrado com sucesso!");
        }
        // Método para listar todos os atletas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Atleta>>> GetAtletas()
        {
            var atletas = await _context.Atletas.ToListAsync();
            return Ok(atletas);
        }

        [HttpPost("inscrever")]
        [Authorize]  // Garante que o usuário precisa estar logado
        public async Task<IActionResult> InscreverNoCampeonato([FromBody] InscricaoDto inscricaoDto)
        {
            // Verificar se o usuário está autenticado
            var usuarioAtual = await _userManager.GetUserAsync(User);  // Verifica o usuário logado

            if (usuarioAtual == null)
            {
                return Unauthorized("Você precisa estar logado para se inscrever.");
            }

            // Verificar se o usuário é um Atleta (verificando o campo TipoUsuario)
            if (usuarioAtual.TipoUsuario != TipoUsuario.Atleta)
            {
                return Unauthorized("Somente atletas podem se inscrever.");
            }

            // Verificar se o atleta existe no banco de dados
            var atleta = await _context.Atletas.FirstOrDefaultAsync(a => a.UsuarioId == usuarioAtual.Id);
            if (atleta == null)
            {
                return NotFound("Atleta não encontrado.");
            }

            // Verificar se o campeonato existe
            var campeonato = await _context.Campeonatos.FirstOrDefaultAsync(c => c.Id == inscricaoDto.CampeonatoId);
            if (campeonato == null)
            {
                return NotFound("Campeonato não encontrado.");
            }

            // Verificar se o atleta já está inscrito no campeonato
            var inscricaoExistente = await _context.Inscricoes
                .FirstOrDefaultAsync(i => i.AtletaId == atleta.Id && i.CampeonatoId == campeonato.Id);

            if (inscricaoExistente != null)
            {
                return BadRequest("O atleta já está inscrito neste campeonato.");
            }

            // Criar a inscrição
            var inscricao = new Inscricao
            {
                AtletaId = atleta.Id,
                CampeonatoId = campeonato.Id,
                DataInscricao = DateTime.Now
            };

            _context.Inscricoes.Add(inscricao);
            await _context.SaveChangesAsync();

            // Retornar a confirmação com ID da inscrição
            return Ok(new { Message = "Atleta inscrito com sucesso.", InscricaoId = inscricao.Id });
        }



    }
}
