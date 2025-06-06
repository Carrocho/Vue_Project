using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Projeto_API.models;

namespace Projeto_API.Data
{
    public class Repository : IRepository
    {
        public DataContext _context { get; }
        public Repository(DataContext context)
        {
            _context = context;
        }
        void IRepository.Add<T>(T entity)
        {
            _context.Add(entity);
        }

        void IRepository.Delete<T>(T entity)
        {
            _context.Remove(entity);
        }

        void IRepository.Update<T>(T entity)
        {
            _context.Update(entity);
        }

        async Task<bool> IRepository.SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() > 0);
        }

        //Alunos

        public async Task<Aluno[]> GetAllAlunosAsync(bool includeProfessor = false)
        {
            IQueryable<Aluno> query = _context.Alunos;
            if(includeProfessor){
                query = query.Include(a => a.professor);
            }

            query = query.AsNoTracking().OrderBy(a => a.Id);

            return await query.ToArrayAsync();
        }

        public async Task<Aluno> GetAlunoAsyncById(int alunoId, Boolean includeProfessor)
        {
            IQueryable<Aluno> query = _context.Alunos;
            if(includeProfessor){
                query = query.Include(a => a.professor);
            }

            query = query.AsNoTracking()
                .OrderBy(a => a.Id)
                .Where(aluno => aluno.Id == alunoId);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Aluno[]> GetAlunosByProfessorId(int professorId, bool includeProfessor)
        {
            IQueryable<Aluno> query = _context.Alunos;
            if(includeProfessor){
                query = query.Include(a => a.professor);
            }

            query = query.AsNoTracking()
                 .Where(aluno => aluno.professorId == professorId)
                 .OrderBy(a => a.Id);

            return await query.ToArrayAsync();
        }

        // Professores
        public async Task<Professor[]> GetAllProfessoresAsync(bool includeAluno)
        {
            IQueryable<Professor> query = _context.Professores;
            if(includeAluno){
                query = query.Include(p => p.alunos);
            }

            query = query.AsNoTracking().OrderBy(p => p.Id);

            return await query.ToArrayAsync();
        }

        public async Task<Professor> GetProfessorAsyncById(int professorId, bool includeAluno)
        {
            IQueryable<Professor> query = _context.Professores;
            if(includeAluno){
                query = query.Include(p => p.alunos);
            }

            query = query.AsNoTracking()
                .OrderBy(a => a.Id)
                .Where(professor => professor.Id == professorId);

            return await query.FirstOrDefaultAsync();
        }
    }
}