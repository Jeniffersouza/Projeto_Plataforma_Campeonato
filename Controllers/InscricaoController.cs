using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlataformaAPI.Models;
using PlataformaJiujitsu.Models;
using PlataformaAPI.Data;

namespace PlataformaJiujitsu.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InscricaoController : ControllerBase
{
    private readonly UserManager<Usuario> _userManager;
    private readonly ApplicationDbContext _context;

    public InscricaoController(UserManager<Usuario> userManager, ApplicationDbContext context)
    {
        _userManager = userManager;
        _context = context;
    }

    [HttpPost("{campeonatoId}/inscrever")]
    [Authorize]
    public async Task<IActionResult> InscreverNoCampeonato(int campeonatoId)
    {
        var usuarioAtual = await _userManager.GetUserAsync(User);
        if (usuarioAtual == null)
            return Unauthorized("Você precisa estar logado para se inscrever.");

        if (usuarioAtual.TipoUsuario != TipoUsuario.Atleta)
            return Unauthorized("Somente atletas podem se inscrever.");

        var atleta = await _context.Atletas.FirstOrDefaultAsync(a => a.UsuarioId == usuarioAtual.Id);
        if (atleta == null)
            return NotFound("Atleta não encontrado.");

        var campeonato = await _context.Campeonatos.FirstOrDefaultAsync(c => c.Id == campeonatoId);
        if (campeonato == null)
            return NotFound("Campeonato não encontrado.");

        if (campeonato.Status != StatusCampeonato.EmAndamento) // Status correto
            return BadRequest("As inscrições para este campeonato estão encerradas.");

        bool inscrito = await _context.Inscricoes.AnyAsync(i => i.AtletaId == atleta.Id && i.CampeonatoId == campeonato.Id);
        if (inscrito)
            return BadRequest("O atleta já está inscrito neste campeonato.");

        var inscricao = new Inscricao
        {
            AtletaId = atleta.Id,
            CampeonatoId = campeonato.Id,
            DataInscricao = DateTime.UtcNow
        };

        _context.Inscricoes.Add(inscricao);
        await _context.SaveChangesAsync();

        return Ok(new { mensagem = "Atleta inscrito com sucesso!", InscricaoId = inscricao.Id });
    }
}
