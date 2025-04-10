using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Projeto_API.models
{
    public class Professor
    {
        public int Id { get; set; }
        public string nome { get; set; }
        public string sobrenome { get; set; }
        public List<Aluno> alunos { get; set; }

    }
}