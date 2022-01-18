using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MundoDoCinema.WebApi.Migrations
{
    public partial class primeiramigracao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Filmes",
                columns: table => new
                {
                    Id      = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Titulo  = table.Column<string>(type: "varchar(30)", nullable: true),
                    Duracao = table.Column<int>(type: "int", nullable: false),
                    Sinopse = table.Column<string>(type: "varchar(500)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Filmes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sessoes",
                columns: table => new
                {
                    Id                 = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Dia                = table.Column<DateTime>(type: "date", nullable: false),
                    Preco              = table.Column<double>(type: "float", nullable: false),
                    InicioHora         = table.Column<int>(type: "int", nullable: false),
                    InicioMinuto       = table.Column<int>(type: "int", nullable: false),
                    QuantidadeLugares  = table.Column<int>(type: "int", nullable: false),
                    QuantidadeLivres   = table.Column<int>(type: "int", nullable: false),                    
                    IdDoFilme          = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    token_concorrencia = table.Column<string>(type: "nvarchar(500)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sessoes", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Filmes");

            migrationBuilder.DropTable(
                name: "Sessoes");
        }
    }
}
