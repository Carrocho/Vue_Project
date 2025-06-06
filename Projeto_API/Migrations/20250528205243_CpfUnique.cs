using Microsoft.EntityFrameworkCore.Migrations;

namespace Projeto_API.Migrations
{
    public partial class CpfUnique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Professores_cpf",
                table: "Professores",
                column: "cpf",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Alunos_cpf",
                table: "Alunos",
                column: "cpf",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Professores_cpf",
                table: "Professores");

            migrationBuilder.DropIndex(
                name: "IX_Alunos_cpf",
                table: "Alunos");
        }
    }
}
