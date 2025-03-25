using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PlataformaAPI.Models;
using PlataformaJiujitsu.Models;

namespace PlataformaAPI.Data
{
    public class ApplicationDbContext : IdentityDbContext<Usuario>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Campeonato> Campeonatos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Academia> Academias { get; set; }
        public DbSet<Atleta> Atletas { get; set; }
        public DbSet<Inscricao> Inscricoes { get; set; }
    }
}
