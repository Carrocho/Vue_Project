using Microsoft.EntityFrameworkCore.Migrations;

namespace Projeto_API.Migrations
{
    public partial class fotoToString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Cria uma nova tabela com a estrutura atualizada
            migrationBuilder.CreateTable(
                name: "NewProfessor",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    nome = table.Column<string>(nullable: true),
                    sobrenome = table.Column<string>(nullable: true),
                    foto = table.Column<string>(nullable: true) // Agora é string
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewProfessor", x => x.Id);
                });

            // Copia os dados da tabela antiga para a nova
            migrationBuilder.Sql("INSERT INTO NewProfessor (Id, nome, sobrenome) SELECT Id, nome, sobrenome FROM Professores");

            // Remove a tabela antiga
            migrationBuilder.DropTable(name: "Professores");

            // Renomeia a nova tabela para o nome original
            migrationBuilder.RenameTable(name: "NewProfessor", newName: "Professores");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
