using Microsoft.EntityFrameworkCore.Migrations;

namespace Projeto_API.Migrations
{
    public partial class AtualizaModelos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "cpf",
                table: "Professores",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "cpf",
                table: "Alunos",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "cpf",
                table: "Professores");

            migrationBuilder.DropColumn(
                name: "cpf",
                table: "Alunos");
        }
    }
}
