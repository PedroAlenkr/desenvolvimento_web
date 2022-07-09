using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace mvc.Migrations
{
    public partial class ComprasModel_mig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "db_compras",
                columns: table => new
                {
                    protocolo = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    evento = table.Column<string>(type: "text", nullable: false),
                    qtd_ingressos = table.Column<int>(type: "integer", nullable: false),
                    dt_compra = table.Column<string>(type: "text", nullable: false),
                    dt_evento = table.Column<string>(type: "text", nullable: false),
                    cpf = table.Column<long>(type: "bigint", nullable: true),
                    pagamento = table.Column<string>(type: "text", nullable: false),
                    parcelas = table.Column<int>(type: "integer", nullable: false),
                    preco_total = table.Column<double>(type: "double precision", nullable: false),
                    id_evento = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_db_compras", x => x.protocolo);
                });

            migrationBuilder.CreateTable(
                name: "db_sessoes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    evento = table.Column<string>(type: "text", nullable: false),
                    local = table.Column<string>(type: "text", nullable: false),
                    data = table.Column<string>(type: "text", nullable: false),
                    inicio = table.Column<string>(type: "text", nullable: false),
                    fim = table.Column<string>(type: "text", nullable: false),
                    preco = table.Column<double>(type: "double precision", nullable: false),
                    assentos = table.Column<int>(type: "integer", nullable: false),
                    comprados = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_db_sessoes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "db_usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nome = table.Column<string>(type: "text", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    Senha = table.Column<string>(type: "text", nullable: false),
                    nascimento = table.Column<string>(type: "text", nullable: true),
                    cpf = table.Column<long>(type: "bigint", nullable: true),
                    sexo = table.Column<char>(type: "character(1)", nullable: true),
                    Tipo = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_db_usuarios", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "db_compras");

            migrationBuilder.DropTable(
                name: "db_sessoes");

            migrationBuilder.DropTable(
                name: "db_usuarios");
        }
    }
}
