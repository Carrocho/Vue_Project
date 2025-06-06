using Microsoft.AspNetCore.Http;

namespace Projeto_API.models
{
    public class Aluno
    {
        
        public int Id { get; set; }
        public string nome { get; set; }
        public string sobrenome { get; set; }
        public string dataNasc { get; set; }
        public int professorId { get; set; }
        public Professor professor { get; set; }
        public byte[] foto { get; set; }
        public string cpf { get; set; }
    }
}