using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Projeto_API.models;
using SQLitePCL;

namespace Projeto_API.Data
{
    public interface IRepository
    {
        public DataContext _context { get; }
        //Geral
        void Add<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveChangesAsync();

        // Aluno
        Task<Aluno[]>GetAllAlunosAsync(Boolean includeProfessor);
        Task<Aluno>GetAlunoAsyncById(int alunoId, Boolean includeProfessor);
        Task<Aluno[]>GetAlunosByProfessorId(int professorId, Boolean includeProfessor);

        // Professor

        Task<Professor[]>GetAllProfessoresAsync(Boolean includeAluno);
        Task<Professor>GetProfessorAsyncById(int professorId, Boolean includeAluno);
    }
}