using Microsoft.EntityFrameworkCore.Migrations;

namespace Projeto_API.Migrations
{
    public partial class SeedDataUpdate2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Alunos",
                keyColumn: "Id",
                keyValue: 2023001);

            migrationBuilder.DeleteData(
                table: "Alunos",
                keyColumn: "Id",
                keyValue: 2023002);

            migrationBuilder.DeleteData(
                table: "Alunos",
                keyColumn: "Id",
                keyValue: 2023003);

            migrationBuilder.DeleteData(
                table: "Alunos",
                keyColumn: "Id",
                keyValue: 2023004);

            migrationBuilder.DeleteData(
                table: "Alunos",
                keyColumn: "Id",
                keyValue: 2023005);

            migrationBuilder.DeleteData(
                table: "Alunos",
                keyColumn: "Id",
                keyValue: 2023006);

            migrationBuilder.DeleteData(
                table: "Alunos",
                keyColumn: "Id",
                keyValue: 2023007);

            migrationBuilder.DeleteData(
                table: "Alunos",
                keyColumn: "Id",
                keyValue: 2023008);

            migrationBuilder.DeleteData(
                table: "Professores",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Professores",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Professores",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Professores",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Professores",
                keyColumn: "Id",
                keyValue: 5);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Professores",
                columns: new[] { "Id", "foto", "nome", "sobrenome" },
                values: new object[] { 1, "/images/dg5dz0zn.png", "Carlos", "Magno" });

            migrationBuilder.InsertData(
                table: "Professores",
                columns: new[] { "Id", "foto", "nome", "sobrenome" },
                values: new object[] { 2, null, "Mariana", "Silveira" });

            migrationBuilder.InsertData(
                table: "Professores",
                columns: new[] { "Id", "foto", "nome", "sobrenome" },
                values: new object[] { 3, null, "Roberto", "Fontes" });

            migrationBuilder.InsertData(
                table: "Professores",
                columns: new[] { "Id", "foto", "nome", "sobrenome" },
                values: new object[] { 4, null, "Ana", "Carolina" });

            migrationBuilder.InsertData(
                table: "Professores",
                columns: new[] { "Id", "foto", "nome", "sobrenome" },
                values: new object[] { 5, null, "Lucas", "Pereira" });

            migrationBuilder.InsertData(
                table: "Alunos",
                columns: new[] { "Id", "dataNasc", "foto", "nome", "professorId", "sobrenome" },
                values: new object[] { 2023001, "15/03/2002", null, "João", 1, "Santos" });

            migrationBuilder.InsertData(
                table: "Alunos",
                columns: new[] { "Id", "dataNasc", "foto", "nome", "professorId", "sobrenome" },
                values: new object[] { 2023006, "30/01/2001", null, "Beatriz", 1, "Rocha" });

            migrationBuilder.InsertData(
                table: "Alunos",
                columns: new[] { "Id", "dataNasc", "foto", "nome", "professorId", "sobrenome" },
                values: new object[] { 2023002, "22/07/2001", null, "Maria", 2, "Oliveira" });

            migrationBuilder.InsertData(
                table: "Alunos",
                columns: new[] { "Id", "dataNasc", "foto", "nome", "professorId", "sobrenome" },
                values: new object[] { 2023007, "12/12/2000", null, "Rafael", 2, "Lima" });

            migrationBuilder.InsertData(
                table: "Alunos",
                columns: new[] { "Id", "dataNasc", "foto", "nome", "professorId", "sobrenome" },
                values: new object[] { 2023003, "10/11/2000", null, "Pedro", 3, "Costa" });

            migrationBuilder.InsertData(
                table: "Alunos",
                columns: new[] { "Id", "dataNasc", "foto", "nome", "professorId", "sobrenome" },
                values: new object[] { 2023008, "08/08/2003", null, "Isabela", 3, "Souza" });

            migrationBuilder.InsertData(
                table: "Alunos",
                columns: new[] { "Id", "dataNasc", "foto", "nome", "professorId", "sobrenome" },
                values: new object[] { 2023004, "05/05/2003", null, "Laura", 4, "Fernandes" });

            migrationBuilder.InsertData(
                table: "Alunos",
                columns: new[] { "Id", "dataNasc", "foto", "nome", "professorId", "sobrenome" },
                values: new object[] { 2023005, "18/09/2002", null, "Gabriel", 5, "Martins" });
        }
    }
}
