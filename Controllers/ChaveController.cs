using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlataformaAPI.Data;
using PlataformaAPI.Models;
using PlataformaJiujitsu.Models;


[ApiController]
[Route("api/[controller]")]
public class ChaveController : ControllerBase
{
    private readonly UserManager<Usuario> _userManager;
    private readonly ApplicationDbContext _context;

    public ChaveController(UserManager<Usuario> userManager, ApplicationDbContext context)
    {
        _userManager = userManager;
        _context = context;
    }

    [HttpPost("{campeonatoId}/gerar-chave")]
    [Authorize]
    public async Task<IActionResult> GerarChave([FromRoute] int campeonatoId)
    {
        var usuarioAtual = await _userManager.GetUserAsync(User);

        if (usuarioAtual.TipoUsuario != TipoUsuario.Administrador)
            return Unauthorized("Somente administradores podem gerar chaves.");

        var campeonato = await _context.Campeonatos
            .Include(c => c.Categorias)
            .Include(c => c.Inscricoes)
                .ThenInclude(i => i.Atleta)
                    .ThenInclude(a => a.Usuario)
            .FirstOrDefaultAsync(c => c.Id == campeonatoId);

        if (campeonato == null)
            return NotFound("Campeonato não encontrado.");

        if (campeonato.Status != StatusCampeonato.EmAndamento)
            return BadRequest("O campeonato não está em andamento.");

        var inscricoes = campeonato.Inscricoes.ToList();

        // Verifica se há atleta ou usuário nulo
        foreach (var i in inscricoes)
        {
            if (i.Atleta == null || i.Atleta.Usuario == null || string.IsNullOrWhiteSpace(i.Atleta.Usuario.NomeCompleto))
                return BadRequest("Todos os atletas devem estar com seus dados completos e nome preenchido.");
        }

        var chaves = GerarChavesParaCampeonato(campeonato);

        if (!chaves.Any())
            return BadRequest("Não há inscrições suficientes para gerar chaves.");

        _context.Chaves.AddRange(chaves);
        await _context.SaveChangesAsync();

        return Ok(new
        {
            mensagem = "Chaves geradas com sucesso!",
            chaves = chaves.Select(chave => new
            {
                chave.CategoriaId,
                chave.Nome,
                AtletasInscritos = chave.Inscricoes.Select(i => new
                {
                    i.AtletaId,
                    Nome = i.Atleta.Usuario.NomeCompleto
                })
            })
        });
    }
    private List<Luta> GerarLutas(List<Inscricao> inscricoes)
    {
        var lutas = new List<Luta>();
        var atletas = inscricoes.Select(i => i.Atleta).ToList();

        // Embaralha os atletas para sorteio
        var random = new Random();
        atletas = atletas.OrderBy(a => random.Next()).ToList();

        for (int i = 0; i < atletas.Count - 1; i += 2)
        {
            var luta = new Luta
            {
                Atleta1Id = atletas[i].Id,
                Atleta2Id = atletas[i + 1].Id
                
            };

            lutas.Add(luta);
        }

        // Se número ímpar de atletas, o último avança automaticamente (bye)
        if (atletas.Count % 2 != 0)
        {
            var atletaSemLuta = atletas.Last();
            // Pode criar uma luta "bye" ou só marcar que ele avança
            lutas.Add(new Luta
            {
                Atleta1Id = atletaSemLuta.Id,
                Atleta2Id = atletaSemLuta.Id, // Representa avanço automático
                
            });
        }

        return lutas;
    }

    private List<Chave> GerarChavesParaCampeonato(Campeonato campeonato)
    {
        var chaves = new List<Chave>();
        var inscricoes = campeonato.Inscricoes.ToList();
        var categorias = campeonato.Categorias.ToList();

        foreach (var categoria in categorias)
        {
            var inscritosNaCategoria = inscricoes
                .Where(i => i.CategoriaId == categoria.Id)
                .ToList();

            if (inscritosNaCategoria.Count >= 2)
            {
                var chave = new Chave
                {
                    CategoriaId = categoria.Id,
                    Nome = $"Chave_{categoria.Id}",
                    Inscricoes = inscritosNaCategoria,
                    //Status = StatusChave.NaoIniciada, // ou Status = 0
                    Lutas = GerarLutas(inscritosNaCategoria)
                };

                chaves.Add(chave);
            }
        }

        return chaves;
    }

    [HttpGet("{campeonatoId}/chaves")]
    [Authorize]
    public async Task<IActionResult> GetChaves([FromRoute] int campeonatoId)
    {
        var campeonato = await _context.Campeonatos
            .Include(c => c.Categorias)
            .Include(c => c.Inscricoes)
                .ThenInclude(i => i.Atleta)
                    .ThenInclude(a => a.Usuario)
            .FirstOrDefaultAsync(c => c.Id == campeonatoId);

        if (campeonato == null)
            return NotFound("Campeonato não encontrado.");

        // Busca as chaves relacionadas através das inscrições
        var chaves = await _context.Chaves
            .Where(ch => ch.Inscricoes.Any(i => i.CampeonatoId == campeonatoId))
            .Include(ch => ch.Inscricoes)
                .ThenInclude(i => i.Atleta)
                    .ThenInclude(a => a.Usuario)
            .Include(ch => ch.Lutas)  // Se quiser incluir as lutas na resposta
            .ToListAsync();

        if (!chaves.Any())
            return NotFound("Não há chaves geradas para este campeonato.");

        var chavesResponse = chaves.Select(chave => new
        {
            chave.CategoriaId,
            chave.Nome,
            AtletasInscritos = chave.Inscricoes.Select(i => new
            {
                i.AtletaId,
                Nome = i.Atleta.Usuario.NomeCompleto
            }),
            Lutas = chave.Lutas.Select(l => new
            {
                l.Atleta1Id,
                l.Atleta2Id
            })
        });

        return Ok(new
        {
            campeonatoId = campeonato.Id,
            chaves = chavesResponse
        });
    }





}
