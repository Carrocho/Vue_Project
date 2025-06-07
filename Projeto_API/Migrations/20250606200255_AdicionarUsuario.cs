using Microsoft.EntityFrameworkCore.Migrations;

namespace Projeto_API.Migrations
{
    public partial class AdicionarUsuario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    nomeUsuario = table.Column<string>(nullable: false),
                    senha = table.Column<string>(nullable: true),
                    isAdmin = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.nomeUsuario);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
