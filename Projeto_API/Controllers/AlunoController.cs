using System;
using System.Collections.Generic;
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

    public class AlunoController : Controller
    {
        public IRepository _repo { get; }
        public AlunoController(IRepository repo){
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> Get(){
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
        public async Task<IActionResult> Get(int AlunoId){
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

        [HttpGet("ByProfessor/{ProfessorId}")]
        public async Task<IActionResult> GetByProfessorId(int ProfessorId){
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
        public async Task<IActionResult> Post(Aluno model){
            try
            {
                _repo.Add(model);
                if(await _repo.SaveChangesAsync()){
                    return Created($"/api/aluno/{model.Id}", model);
                }
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou");
            }

            return BadRequest();
        }

        [HttpPut("{AlunoId}")]
        public async Task<IActionResult> Put(int AlunoId, Aluno model){
            try
            {
                var aluno = await _repo.GetAlunoAsyncById(AlunoId, false);
                if(aluno == null) return NotFound();

                _repo.Update(model);
                if(await _repo.SaveChangesAsync()){
                    aluno = await _repo.GetAlunoAsyncById(AlunoId, true);
                    return Created($"/api/aluno/{model.Id}", aluno);
                }  
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou");
            }
            return BadRequest();
        }

        [HttpDelete("{AlunoId}")]
        public async Task<IActionResult> Delete(int AlunoId){
            try
            {
                var aluno = await _repo.GetAlunoAsyncById(AlunoId, false);
                if(aluno == null) return NotFound();

                _repo.Delete(aluno);
                if(await _repo.SaveChangesAsync()){
                    return Ok(); 
                }     
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou");
            }

            return BadRequest();
        }
    }
}