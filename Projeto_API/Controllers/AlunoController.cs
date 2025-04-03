using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Projeto_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AlunoController : Controller
    {
        public AlunoController(){

        }

        [HttpGet]
        public IActionResult get(){
            return Ok();
        }

        [HttpGet("{AlunoId}")]
        public IActionResult get(int alunoId){
            return Ok();
        }

        [HttpPost]
        public IActionResult post(){
            return Ok();
        }

        [HttpPut("{AlunoId}")]
        public IActionResult put(int alunoId){
            return Ok();
        }

        [HttpDelete("{AlunoId}")]
        public IActionResult delete(int alunoId){
            return Ok();
        }
    }
}