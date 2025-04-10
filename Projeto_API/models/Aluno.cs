using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

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
    }
}