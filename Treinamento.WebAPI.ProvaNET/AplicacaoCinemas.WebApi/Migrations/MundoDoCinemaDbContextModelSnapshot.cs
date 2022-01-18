
using System;
using MundoDoCinema.WebApi.Infraestrutura;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MundoDoCinema.WebApi.Migrations
{
    [DbContext(typeof(MundoDoCinemaDbContext))]
    partial class MundoDoCinemaDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.12")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MundoDoCinema.WebApi.Dominio.Filme", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Titulo")
                        .HasColumnType("varchar(30)");

                    b.Property<int>("Duracao")
                        .HasColumnType("int");

                    b.Property<string>("Sinopse")
                        .HasColumnType("varchar(500)");

                    b.HasKey("Id");

                    b.ToTable("Filmes");
                });

            modelBuilder.Entity("MundoDoCinema.WebApi.Dominio.Sessao", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Dia")
                        .HasColumnType("date");

                    b.Property<double>("Preco")
                        .HasColumnType("float");

                    b.Property<int>("InicioHora")
                        .HasColumnType("int");

                    b.Property<Guid>("IdDoFilme")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("InicioMinuto")
                        .HasColumnType("int");

                    b.Property<int>("QuantidadeLugares")
                        .HasColumnType("int");

                    b.Property<int>("QuantidadeLivres")
                        .HasColumnType("int");

                    b.Property<string>("_hashConcorrencia")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(500)")
                        .HasColumnName("token_concorrencia");

                    b.HasKey("Id");

                    b.ToTable("Sessoes");
                });
#pragma warning restore 612, 618
        }
    }
}
