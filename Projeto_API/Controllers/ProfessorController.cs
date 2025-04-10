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
    
    public class ProfessorController : Controller
    {
        public IRepository _repo { get; }
        public ProfessorController(IRepository repo){
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> Get(){
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
        public async Task<IActionResult> Get(int professorId){
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
        public async Task<IActionResult> Post(Professor model){
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

        [HttpPut("{professorId}")]
        public async Task<IActionResult> Put(int professorId, Professor model){
            try
            {
                var professor = await _repo.GetProfessorAsyncById(professorId, false);
                if(professor == null) return NotFound();

                _repo.Update(model);
                if(await _repo.SaveChangesAsync()){
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
        public async Task<IActionResult> Delete(int professorId){
            try
            {
                var professor = await _repo.GetProfessorAsyncById(professorId, false);
                if(professor == null) return NotFound();

                _repo.Delete(professor);
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