using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Projeto_API.models;

namespace Projeto_API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<Professor> Professores { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Professor>().HasData(
                new List<Professor>(){
                    new Professor(){
                        Id = 1,
                        nome = "Carlos",
                        sobrenome = "Magno"
                    },
                    new Professor(){
                        Id = 2,
                        nome = "Mariana",
                        sobrenome = "Silveira"
                    },
                    new Professor(){
                        Id = 3,
                        nome = "Roberto",
                        sobrenome = "Fontes"
                    },
                    new Professor(){
                        Id = 4,
                        nome = "Ana",
                        sobrenome = "Carolina"
                    },
                    new Professor(){
                        Id = 5,
                        nome = "Lucas",
                        sobrenome = "Pereira"
                    }
                }
            );

            builder.Entity<Aluno>().HasData(
                new List<Aluno>(){
                    new Aluno(){
                        Id = 2023001,
                        nome = "João",
                        sobrenome = "Santos",
                        dataNasc = "15/03/2002",
                        professorId = 1,
                    },
                    new Aluno(){
                        Id = 2023002,
                        nome = "Maria",
                        sobrenome = "Oliveira",
                        dataNasc = "22/07/2001",
                        professorId = 2,
                    },
                    new Aluno(){
                        Id = 2023003,
                        nome = "Pedro",
                        sobrenome = "Costa",
                        dataNasc = "10/11/2000",
                        professorId = 3,
                    },
                    new Aluno(){
                        Id = 2023004,
                        nome = "Laura",
                        sobrenome = "Fernandes",
                        dataNasc = "05/05/2003",
                        professorId = 4,
                    },
                    new Aluno(){
                        Id = 2023005,
                        nome = "Gabriel",
                        sobrenome = "Martins",
                        dataNasc = "18/09/2002",
                        professorId = 5,
                    },
                    new Aluno(){
                        Id = 2023006,
                        nome = "Beatriz",
                        sobrenome = "Rocha",
                        dataNasc = "30/01/2001",
                        professorId = 1,
                    },
                    new Aluno(){
                        Id = 2023007,
                        nome = "Rafael",
                        sobrenome = "Lima",
                        dataNasc = "12/12/2000",
                        professorId = 2,
                    },
                    new Aluno(){
                        Id = 2023008,
                        nome = "Isabela",
                        sobrenome = "Souza",
                        dataNasc = "08/08/2003",
                        professorId = 3,
                    }
                }
            );
        }
    }
}