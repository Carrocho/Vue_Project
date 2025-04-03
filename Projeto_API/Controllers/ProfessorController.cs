using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Projeto_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class ProfessorController : Controller
    {
        public ProfessorController(){

        }

        [HttpGet]
        public IActionResult get(){
            return Ok();
        }

        [HttpGet("{professorId}")]
        public IActionResult get(int professorId){
            return Ok();
        }

        [HttpPost]
        public IActionResult post(){
            return Ok();
        }

        [HttpPut("{professorId}")]
        public IActionResult put(int professorId){
            return Ok();
        }

        [HttpDelete("{professorId}")]
        public IActionResult delete(int professorId){
            return Ok();
        }
    }
}