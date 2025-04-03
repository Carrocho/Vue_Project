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
                        nome = "Pericles",
                        sobrenome = "Pericles"
                    },
                    new Professor(){
                        Id = 2,
                        nome = "The",
                        sobrenome = "Rock"
                    },
                    new Professor(){
                        Id = 3,
                        nome = "Pspspsps",
                        sobrenome = "Da Silva"
                    }
                }
            );

            builder.Entity<Aluno>().HasData(
                new List<Aluno>(){
                    new Aluno(){
                        Id = 2023001,
                        nome = "Aluno",
                        sobrenome = "A",
                        dataNasc = "11/09/2001",
                        professorId = 1,
                    },
                    new Aluno(){
                        Id = 2023002,
                        nome = "Aluno",
                        sobrenome = "B",
                        dataNasc = "11/09/2001",
                        professorId = 2,
                    },
                    new Aluno(){
                        Id = 2023003,
                        nome = "Aluno",
                        sobrenome = "C",
                        dataNasc = "11/09/2001",
                        professorId = 3,
                    }
                }
            );


        }
    }
}