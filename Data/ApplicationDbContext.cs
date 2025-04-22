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

        // DbSets para as entidades específicas do seu aplicativo
        public DbSet<Campeonato> Campeonatos { get; set; }
        public DbSet<Academia> Academias { get; set; }
        public DbSet<Atleta> Atletas { get; set; }
        public DbSet<Inscricao> Inscricoes { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Professor> Professores { get; set; }
        public DbSet<Esporte> Esportes { get; set; }
        public DbSet<Graduacao> Graduacoes { get; set; }

        public DbSet<Categoria> Categoria { get; set; } // Adicionando Categoria no DbContext
        public DbSet<Chave> Chaves { get; set; }
        public DbSet<Luta> Lutas { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Definição do relacionamento entre Graduacao e Esporte
            modelBuilder.Entity<Graduacao>()
                .HasOne(g => g.Esporte)
                .WithMany() // Um esporte pode ter várias graduações
                .HasForeignKey(g => g.EsporteId)
                .OnDelete(DeleteBehavior.Cascade); // Se um esporte for excluído, suas graduações também serão

           
        }

    }
}
