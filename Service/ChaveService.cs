using Microsoft.EntityFrameworkCore;
using PlataformaAPI.Data;
using PlataformaJiujitsu.Models;
using System;

public class ChaveService
{
    private readonly ApplicationDbContext _context;

    public ChaveService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ChaveDto> GerarChaveParaCategoria(int categoriaId)
    {
        var inscritos = await _context.Inscricoes
            .Where(i => i.CategoriaId == categoriaId)
            .Include(i => i.Atleta)
                .ThenInclude(a => a.Academia)
            .ToListAsync();

        if (inscritos.Count < 2)
            throw new Exception("Não há inscritos suficientes para formar uma chave");

        // Agrupa por academia para evitar confronto interno
        var atletas = inscritos.Select(i => i.Atleta).ToList();
        var chave = new Chave
        {
            Nome = "Chave Categoria " + categoriaId,
            CategoriaId = categoriaId,
            Status = "Gerada",
            Lutas = new List<Luta>()
        };

        var random = new Random();
        var disponiveis = new List<Atleta>(atletas);

        while (disponiveis.Count >= 2)
        {
            var atleta1 = disponiveis[0];
            var adversarios = disponiveis.Where(a => a.AcademiaId != atleta1.AcademiaId).ToList();

            if (!adversarios.Any())
            {
                // Se não houver adversário de outra academia, pula ou combina com qualquer um
                adversarios = disponiveis.Skip(1).ToList();
            }

            var atleta2 = adversarios[random.Next(adversarios.Count)];

            chave.Lutas.Add(new Luta
            {
                Atleta1Id = atleta1.Id,
                Atleta2Id = atleta2.Id
            });

            disponiveis.Remove(atleta1);
            disponiveis.Remove(atleta2);
        }

        await _context.Chaves.AddAsync(chave);
        await _context.SaveChangesAsync();

        return new ChaveDto
        {
            Id = chave.Id,
            Nome = chave.Nome,
            Status = chave.Status,
            CategoriaId = chave.CategoriaId,
            Lutas = chave.Lutas.Select(l => new LutaDto
            {
                Id = l.Id,
                Atleta1Id = l.Atleta1Id,
                Atleta2Id = l.Atleta2Id,
                PlacarAtleta1 = l.PlacarAtleta1,
                PlacarAtleta2 = l.PlacarAtleta2,
                Resultado = l.Resultado,
                Atleta1Nome = _context.Atletas.Include(a => a.Usuario).FirstOrDefault(a => a.Id == l.Atleta1Id)?.Usuario.NomeCompleto,
                Atleta2Nome = _context.Atletas.Include(a => a.Usuario).FirstOrDefault(a => a.Id == l.Atleta2Id)?.Usuario.NomeCompleto,
            }).ToList()
        };
    }
}
