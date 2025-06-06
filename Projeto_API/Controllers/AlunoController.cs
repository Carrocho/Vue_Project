using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projeto_API.Data;
using Projeto_API.models;

namespace Projeto_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AlunoController : Controller
    {
        public IRepository _repo { get; }
        public AlunoController(IRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await _repo.GetAllAlunosAsync(true);
                return Ok(result);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou");
            }
        }

        [HttpGet("{AlunoId}")]
        public async Task<IActionResult> Get(int AlunoId)
        {
            try
            {
                var result = await _repo.GetAlunoAsyncById(AlunoId, true);
                return Ok(result);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou");
            }
        }

        [HttpGet("{AlunoId}/foto")]
        public async Task<IActionResult> GetFoto(int AlunoId)
        {
            var aluno = await _repo.GetAlunoAsyncById(AlunoId, false);
            if (aluno == null || aluno.foto == null) return NotFound();

            return File(aluno.foto, "image/jpeg");
        }

        [HttpGet("ByProfessor/{ProfessorId}")]
        public async Task<IActionResult> GetByProfessorId(int ProfessorId)
        {
            try
            {
                var result = await _repo.GetAlunosByProfessorId(ProfessorId, true);
                return Ok(result);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou");
            }
        }


        [HttpPost]
        public async Task<IActionResult> Post(Aluno model)
        {
            if (!calcularCPF(model.cpf))
                {
                    {
                        return BadRequest("Erro 01: CPF inválido.");
                    }
                }
            try
            {
                _repo.Add(model);
                if (await _repo.SaveChangesAsync())
                {
                    return Created($"/api/aluno/{model.Id}", model);
                }
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException ex)
            {
                if (ex.InnerException is Microsoft.Data.Sqlite.SqliteException sqliteEx)
                {
                    if (sqliteEx.SqliteErrorCode == 19)
                    {
                        if (sqliteEx.SqliteExtendedErrorCode == 2067) 
                        {
                            if (sqliteEx.Message.Contains(".cpf", StringComparison.OrdinalIgnoreCase)) // 
                            {
                                return BadRequest("Erro 02: CPF já cadastrado.");
                            }

                        }
                    }

                }

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou.");
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou");
            }

            return BadRequest();
        }

        [HttpPut("{AlunoId}")]
        public async Task<IActionResult> Put(int AlunoId, Aluno model)
        {
            try
            {
                var aluno = await _repo.GetAlunoAsyncById(AlunoId, false);
                if (aluno == null) return NotFound();

                _repo._context.Entry(aluno).State = EntityState.Detached;
                _repo.Update(model);


                if (await _repo.SaveChangesAsync())
                {
                    aluno = await _repo.GetAlunoAsyncById(AlunoId, true);
                    return Created($"/api/aluno/{model.Id}", aluno);
                }
                Console.WriteLine("\n => " + model.foto);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("\n => " + ex);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou");
            }
            return BadRequest();
        }

        [HttpDelete("{AlunoId}")]
        public async Task<IActionResult> Delete(int AlunoId)
        {
            try
            {
                var aluno = await _repo.GetAlunoAsyncById(AlunoId, false);
                if (aluno == null) return NotFound();

                _repo.Delete(aluno);
                if (await _repo.SaveChangesAsync())
                {
                    return Ok();
                }
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou");
            }

            return BadRequest();
        }

        private bool calcularCPF(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf))
                return false;

            var cpfApenasNumeros = new string(cpf.Where(char.IsDigit).ToArray());
            Console.WriteLine(cpfApenasNumeros);
            if (cpfApenasNumeros.Length != 11)
                return false;

            if (cpfApenasNumeros.Distinct().Count() == 1)
                return false;

            int[] numeros = new int[11];

            for (int i = 0; i < 11; i++)
            {
                numeros[i] = int.Parse(cpfApenasNumeros[i].ToString());
            }

            int soma = 0;
            for (int i = 0; i < 9; i++)
            {
                soma += numeros[i] * (10 - i);
            }
            int resto = soma % 11;
            int d1 = (resto < 2) ? 0 : (11 - resto);

            soma = 0;
            for (int i = 0; i < 9; i++) 
            {
                soma += numeros[i] * (11 - i); 
            }
            soma += d1 * 2; 

            resto = soma % 11;
            int d2 = (resto < 2) ? 0 : (11 - resto);

            return (numeros[10] == d2) && (numeros[9] == d1);
        }

    }
}