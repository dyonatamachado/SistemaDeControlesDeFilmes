using Microsoft.EntityFrameworkCore;
using SistemaDeControleDeFilmes.Entities;

namespace SistemaDeControleDeFilmes.Persistence
{
    public class FilmesContext : DbContext
    {
        public FilmesContext(DbContextOptions<FilmesContext> options)
            : base(options)
        {
            
        }

        public DbSet<Filme> Filmes { get; private set; }
        public DbSet<Usuario> Usuarios { get; private set; }
        public DbSet<Exibicao> Exibicoes { get; private set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Filme>(e => 
            {
                e.HasKey(f => f.Id);
                e.HasMany(f => f.Exibicoes)
                    .WithOne()
                    .HasForeignKey(e => e.FilmeId);
            });

            modelBuilder.Entity<Usuario>(e => 
            {                
                e.HasKey(u => u.Id);
                e.HasMany(u => u.Exibicoes)
                    .WithOne()
                    .HasForeignKey(e => e.UsuarioId);
            });

            modelBuilder.Entity<Exibicao>(e => 
            {
                e.HasKey(ex => ex.Id);
            });
        }
        
    }
}