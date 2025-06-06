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
                    sobrenome = table.Column<string>(nullable: true),
                    foto = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Professores", x => x.Id);
                });

            migrationBuilder.Sql("INSERT INTO NewProfessor (Id, nome, sobrenome) SELECT Id, nome, sobrenome FROM Professor");

            migrationBuilder.DropTable(name: "Professor");

            migrationBuilder.RenameTable(name: "NewProfessor", newName: "Professor");

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
                values: new object[] { 1, "Carlos", "Magno", "/images/dg5dz0zn.png"});

            migrationBuilder.InsertData(
                table: "Professores",
                columns: new[] { "Id", "nome", "sobrenome" },
                values: new object[] { 2, "Mariana", "Silveira" });

            migrationBuilder.InsertData(
                table: "Professores",
                columns: new[] { "Id", "nome", "sobrenome" },
                values: new object[] { 3, "Roberto", "Fontes" });

            migrationBuilder.InsertData(
                table: "Professores",
                columns: new[] { "Id", "nome", "sobrenome" },
                values: new object[] { 4, "Ana", "Carolina" });

            migrationBuilder.InsertData(
                table: "Professores",
                columns: new[] { "Id", "nome", "sobrenome" },
                values: new object[] { 5, "Lucas", "Pereira" });

            migrationBuilder.InsertData(
                table: "Alunos",
                columns: new[] { "Id", "dataNasc", "nome", "professorId", "sobrenome" },
                values: new object[] { 2023001, "15/03/2002", "João", 1, "Santos" });

            migrationBuilder.InsertData(
                table: "Alunos",
                columns: new[] { "Id", "dataNasc", "nome", "professorId", "sobrenome" },
                values: new object[] { 2023006, "30/01/2001", "Beatriz", 1, "Rocha" });

            migrationBuilder.InsertData(
                table: "Alunos",
                columns: new[] { "Id", "dataNasc", "nome", "professorId", "sobrenome" },
                values: new object[] { 2023002, "22/07/2001", "Maria", 2, "Oliveira" });

            migrationBuilder.InsertData(
                table: "Alunos",
                columns: new[] { "Id", "dataNasc", "nome", "professorId", "sobrenome" },
                values: new object[] { 2023007, "12/12/2000", "Rafael", 2, "Lima" });

            migrationBuilder.InsertData(
                table: "Alunos",
                columns: new[] { "Id", "dataNasc", "nome", "professorId", "sobrenome" },
                values: new object[] { 2023003, "10/11/2000", "Pedro", 3, "Costa" });

            migrationBuilder.InsertData(
                table: "Alunos",
                columns: new[] { "Id", "dataNasc", "nome", "professorId", "sobrenome" },
                values: new object[] { 2023008, "08/08/2003", "Isabela", 3, "Souza" });

            migrationBuilder.InsertData(
                table: "Alunos",
                columns: new[] { "Id", "dataNasc", "nome", "professorId", "sobrenome" },
                values: new object[] { 2023004, "05/05/2003", "Laura", 4, "Fernandes" });

            migrationBuilder.InsertData(
                table: "Alunos",
                columns: new[] { "Id", "dataNasc", "nome", "professorId", "sobrenome" },
                values: new object[] { 2023005, "18/09/2002", "Gabriel", 5, "Martins" });

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
