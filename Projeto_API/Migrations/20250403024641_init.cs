using Microsoft.EntityFrameworkCore.Migrations;

namespace Projeto_API.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Professores",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    nome = table.Column<string>(nullable: true),
                    sobrenome = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Professores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Alunos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    nome = table.Column<string>(nullable: true),
                    sobrenome = table.Column<string>(nullable: true),
                    dataNasc = table.Column<string>(nullable: true),
                    professorId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alunos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Alunos_Professores_professorId",
                        column: x => x.professorId,
                        principalTable: "Professores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Professores",
                columns: new[] { "Id", "nome", "sobrenome" },
                values: new object[] { 1, "Pericles", "Pericles" });

            migrationBuilder.InsertData(
                table: "Professores",
                columns: new[] { "Id", "nome", "sobrenome" },
                values: new object[] { 2, "The", "Rock" });

            migrationBuilder.InsertData(
                table: "Professores",
                columns: new[] { "Id", "nome", "sobrenome" },
                values: new object[] { 3, "Pspspsps", "Da Silva" });

            migrationBuilder.InsertData(
                table: "Alunos",
                columns: new[] { "Id", "dataNasc", "nome", "professorId", "sobrenome" },
                values: new object[] { 2023001, "11/09/2001", "Aluno", 1, "A" });

            migrationBuilder.InsertData(
                table: "Alunos",
                columns: new[] { "Id", "dataNasc", "nome", "professorId", "sobrenome" },
                values: new object[] { 2023002, "11/09/2001", "Aluno", 2, "B" });

            migrationBuilder.InsertData(
                table: "Alunos",
                columns: new[] { "Id", "dataNasc", "nome", "professorId", "sobrenome" },
                values: new object[] { 2023003, "11/09/2001", "Aluno", 3, "C" });

            migrationBuilder.CreateIndex(
                name: "IX_Alunos_professorId",
                table: "Alunos",
                column: "professorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Alunos");

            migrationBuilder.DropTable(
                name: "Professores");
        }
    }
}
