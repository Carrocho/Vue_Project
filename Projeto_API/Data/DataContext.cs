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
        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Professor>()
                .HasIndex(p => p.cpf)
                .IsUnique();

            builder.Entity<Aluno>()
                .HasIndex(a => a.cpf)
                .IsUnique();

            builder.Entity<Usuario>()
                .HasKey(a => a.nomeUsuario);

            builder.Entity<Usuario>()
                .Property(a => a.isAdmin)
                .HasDefaultValue(false);
        }
    }
}