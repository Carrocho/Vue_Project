using Microsoft.EntityFrameworkCore.Migrations;

namespace Projeto_API.Migrations
{
    public partial class SeedDataUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
{
    // Remove os dados existentes
    migrationBuilder.Sql("DELETE FROM Professores");
    migrationBuilder.Sql("DELETE FROM Alunos");

    // Insere os novos dados
    migrationBuilder.InsertData(
        table: "Professores",
        columns: new[] { "Id", "nome", "sobrenome", "foto" },
        values: new object[,]
        {
            { 1, "Carlos", "Magno", "/images/dg5dz0zn.png" },
            { 2, "Mariana", "Silveira", null },
            { 3, "Roberto", "Fontes", null },
            { 4, "Ana", "Carolina", null },
            { 5, "Lucas", "Pereira", null }
        });

    migrationBuilder.InsertData(
        table: "Alunos",
        columns: new[] { "Id", "nome", "sobrenome", "dataNasc", "professorId" },
        values: new object[,]
        {
            { 2023001, "João", "Santos", "15/03/2002", 1 },
            { 2023002, "Maria", "Oliveira", "22/07/2001", 2 },
            { 2023003, "Pedro", "Costa", "10/11/2000", 3 },
            { 2023004, "Laura", "Fernandes", "05/05/2003", 4 },
            { 2023005, "Gabriel", "Martins", "18/09/2002", 5 },
            { 2023006, "Beatriz", "Rocha", "30/01/2001", 1 },
            { 2023007, "Rafael", "Lima", "12/12/2000", 2 },
            { 2023008, "Isabela", "Souza", "08/08/2003", 3 }
        });
}

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Professores",
                keyColumn: "Id",
                keyValue: 1,
                column: "foto",
                value: null);
        }
    }
}
