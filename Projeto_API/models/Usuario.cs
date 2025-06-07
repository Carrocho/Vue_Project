using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projeto_API.models
{
    public class Usuario
    {
        public string nomeUsuario { get; set; }
        public string senha { get; set; }
        public bool isAdmin { get; set; }
    }
}