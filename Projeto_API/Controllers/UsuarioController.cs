using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Konscious.Security.Cryptography;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Projeto_API.Data;
using Projeto_API.models;

namespace Projeto_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class UsuarioController : Controller
    {
        public IRepository _repo { get; }
        public UsuarioController(IRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await _repo.GetAllUsuariosAsync();
                return Ok(result);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou");
            }
        }

        [HttpGet("{nomeUsuario}")]
        public async Task<IActionResult> Get(string nomeUsuario)
        {
            try
            {
                var result = await _repo.GetUsuarioAsyncByPK(nomeUsuario);
                return Ok(result);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou");
            }
        }


        [HttpPost]
        public async Task<IActionResult> Post(Usuario model)
        {
            try
            {
                model.senha = criarHashSenha(model.senha);
                _repo.Add(model);
                if (await _repo.SaveChangesAsync())
                {
                    return Created($"/api/usuario/{model.nomeUsuario}", model);
                }
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException ex)
            {
                if (ex.InnerException is Microsoft.Data.Sqlite.SqliteException sqliteEx)
                {
                    if (sqliteEx.SqliteErrorCode == 19) // Código genérico para constraint
                    {
                        if (sqliteEx.SqliteExtendedErrorCode == 1555) // SQLITE_CONSTRAINT_PRIMARYKEY
                        {
                            if (sqliteEx.Message.Contains(".nomeUsuario", StringComparison.OrdinalIgnoreCase))
                            {
                                return BadRequest("Erro 01:Nome já cadastrado.");
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

        [HttpPut("{nomeUsuario}")]
        public async Task<IActionResult> Put(string nomeUsuario, Usuario model)
        {
            try
            {
                model.senha = criarHashSenha(model.senha);
                var usuario = await _repo.GetUsuarioAsyncByPK(nomeUsuario);
                if (usuario == null) return NotFound();

                _repo.Update(model);
                if (await _repo.SaveChangesAsync())
                {
                    usuario = await _repo.GetUsuarioAsyncByPK(nomeUsuario);
                    return Created($"/api/usuario/{model.nomeUsuario}", usuario);
                }
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException ex)
            {
                if (ex.InnerException is Microsoft.Data.Sqlite.SqliteException sqliteEx)
                {
                    if (sqliteEx.SqliteErrorCode == 19) // Código genérico para constraint
                    {
                        if (sqliteEx.SqliteExtendedErrorCode == 1555) // SQLITE_CONSTRAINT_PRIMARYKEY
                        {
                            if (sqliteEx.Message.Contains(".nomeUsuario", StringComparison.OrdinalIgnoreCase))
                            {
                                return BadRequest("Erro 01:Nome já cadastrado.");
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

        [HttpDelete("{nomeUsuario}")]
        public async Task<IActionResult> Delete(string nomeUsuario)
        {
            try
            {
                var usuario = await _repo.GetUsuarioAsyncByPK(nomeUsuario);
                if (usuario == null) return NotFound();

                _repo.Delete(usuario);
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

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Usuario model)
        {
            if (model == null)
            {
                return BadRequest("Dados de login inválidos.");
            }
            
            var usuario = await _repo.GetUsuarioAsyncByPK(model.nomeUsuario);

            if (usuario == null)
            {
                return Unauthorized("Usuário ou senha inválidos.");
            }

            // Chama a função privada para verificar a senha
            bool senhaCorreta = verificarSenhaHash(model.senha, usuario);

            if (senhaCorreta)
            {
                return Ok(new
                {
                    message = "Login realizado com sucesso!",
                    usuario = new
                    {
                        nomeUsuario = usuario.nomeUsuario,
                        isAdmin = usuario.isAdmin
                    }
                });
            }
            else
            {
                // Senha incorreta
                return Unauthorized("Usuário ou senha inválidos.");
            }
        }

        private string criarHashSenha(string senha)
        {
            byte[] salt = new byte[8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            // Gera o hash da senha
            var argon2 = new Argon2id(Encoding.UTF8.GetBytes(senha));

            argon2.Salt = salt;
            argon2.DegreeOfParallelism = 8;
            argon2.Iterations = 4;          
            argon2.MemorySize = 1024 * 64; 


            // Converte para byte
            byte[] hash = argon2.GetBytes(32);

            // Converte para string, para salvar no banco
            string saltString = Convert.ToBase64String(salt);
            string hashString = Convert.ToBase64String(hash);

            return $"{saltString}:{hashString}";
        }

        private bool verificarSenhaHash(string senha, Usuario usuario)
        {
            var hashSalt = usuario.senha.Split(':');

            byte[] salt = Convert.FromBase64String(hashSalt[0]);
            byte[] hashArmazenado = Convert.FromBase64String(hashSalt[1]);

            var argon2 = new Argon2id(Encoding.UTF8.GetBytes(senha));

            argon2.Salt = salt;
            argon2.DegreeOfParallelism = 8;
            argon2.Iterations = 4;
            argon2.MemorySize = 1024 * 64;

            byte[] hashNovo = argon2.GetBytes(32);

            return CryptographicOperations.FixedTimeEquals(hashArmazenado, hashNovo);

        }

    }
}