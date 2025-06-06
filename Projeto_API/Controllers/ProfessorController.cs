using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Projeto_API.Data;
using Projeto_API.models;

namespace Projeto_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ProfessorController : Controller
    {
        public IRepository _repo { get; }
        public ProfessorController(IRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await _repo.GetAllProfessoresAsync(true);
                return Ok(result);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou");
            }
        }

        [HttpGet("{professorId}")]
        public async Task<IActionResult> Get(int professorId)
        {
            try
            {
                var result = await _repo.GetProfessorAsyncById(professorId, true);
                return Ok(result);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou");
            }
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromForm] Professor model, [FromForm] IFormFile foto)
        {
            try
            {
                if (!calcularCPF(model.cpf))
                {
                    Console.WriteLine(model.cpf);
                    {
                        return BadRequest("Erro 01: CPF inválido.");
                    }
                }
                if (foto != null)
                {
                    // Define o caminho para salvar a imagem
                    var fileName = $"{Guid.NewGuid()}_{foto.FileName}";
                    var filePath = Path.Combine("wwwroot/images", fileName);

                    // Salva o arquivo no sistema de arquivos
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await foto.CopyToAsync(stream);
                    }
                    // Atualiza o caminho da foto no modelo
                    model.foto = $"/images/{fileName}";
                }

                

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
                    if (sqliteEx.SqliteErrorCode == 19) // Código genérico para constraint
                    {

                        if (sqliteEx.SqliteExtendedErrorCode == 2067) // SQLITE_CONSTRAINT_UNIQUE
                        {
                            if (sqliteEx.Message.Contains(".cpf", StringComparison.OrdinalIgnoreCase)) // 
                            {
                                return BadRequest("Erro 02:CPF já cadastrado.");
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

        [HttpPut("{professorId}")]
        public async Task<IActionResult> Put(int professorId, Professor model)
        {
            try
            {
                var professor = await _repo.GetProfessorAsyncById(professorId, false);
                if (professor == null) return NotFound();

                _repo.Update(model);
                if (await _repo.SaveChangesAsync())
                {
                    professor = await _repo.GetProfessorAsyncById(professorId, true);
                    return Created($"/api/professor/{model.Id}", professor);
                }
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou");
            }

            return BadRequest();
        }

        [HttpDelete("{professorId}")]
        public async Task<IActionResult> Delete(int professorId)
        {
            try
            {
                var professor = await _repo.GetProfessorAsyncById(professorId, false);
                if (professor == null) return NotFound();

                _repo.Delete(professor);
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