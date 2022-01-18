using MundoDoCinema.WebApi.Dominio;
using Microsoft.EntityFrameworkCore;

namespace MundoDoCinema.WebApi.Infraestrutura
{
    public class MundoDoCinemaDbContext : DbContext
    {
        public MundoDoCinemaDbContext(DbContextOptions options)
            : base(options)
        {

        }

        public DbSet<Sessao> Sessoes { get; set; }

        public DbSet<Filme> Filmes { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Filme>().ToTable("Filmes");
            modelBuilder.Entity<Filme>().HasKey(c => c.Id);
            modelBuilder.Entity<Filme>().Property(c => c.Titulo).HasColumnType("varchar(30)");
            modelBuilder.Entity<Filme>().Property(c => c.Duracao).HasColumnType("int");
            modelBuilder.Entity<Filme>().Property(c => c.Sinopse).HasColumnType("varchar(500)");

            modelBuilder.Entity<Sessao>().ToTable("Sessoes");
            modelBuilder.Entity<Sessao>().HasKey(a => a.Id);
            modelBuilder.Entity<Sessao>().Property(a => a.Dia).HasColumnType("date");
            modelBuilder.Entity<Sessao>().Property(a => a.Preco).HasColumnType("float");
            modelBuilder.Entity<Sessao>().Property(a => a.InicioHora).HasColumnType("int");
            modelBuilder.Entity<Sessao>().Property(a => a.InicioMinuto).HasColumnType("int");
            modelBuilder.Entity<Sessao>().Property(a => a.QuantidadeLugares).HasColumnType("int");
            modelBuilder.Entity<Sessao>().Property(a => a.QuantidadeLivres).HasColumnType("int");
            modelBuilder.Entity<Sessao>().Property(a => a.IdDoFilme).HasColumnType("uniqueidentifier");
            modelBuilder.Entity<Sessao>().Property("_hashConcorrencia").
                HasColumnName("token_concorrencia").
                HasConversion<string>().IsConcurrencyToken();
        }
    }
}