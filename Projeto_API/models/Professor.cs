using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace Projeto_API.models
{
    public class Professor
    {
        public int Id { get; set; }
        public string nome { get; set; }
        public string sobrenome { get; set; }
        public List<Aluno> alunos { get; set; }
        public string foto { get; set; }
        public string cpf { get; set; }
    }
}