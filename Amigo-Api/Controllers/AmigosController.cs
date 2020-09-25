using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Amigo_Api.Data;
using Model;
using Microsoft.Data.SqlClient;

namespace Amigo_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AmigosController : ControllerBase
    {
        private readonly Amigo_ApiContext _context;
        private readonly string _connectionString;

        public AmigosController(Amigo_ApiContext context)
        {
            _context = context;
        }

        // GET: api/Amigos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Amigo>>> GetAmigo()
        {

            var amigos = _context.Amigo.FromSqlRaw("Execute dbo.ListarAmigos").ToList();

            return amigos;
        }
        // GET: api/Amigos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Amigo>>> GetAmigo(int id)
        {
            var amigo = new List<Amigo>();
            var amigoId = new SqlParameter("@AmigoId", id);

            amigo = _context.Amigo.FromSqlRaw("Execute DetalharAmigo @AmigoId", amigoId).ToList();

            if (amigo == null)
            {
                return NoContent();
            }

            return amigo;
        }

        // PUT: api/Amigos/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAmigo(int id, Amigo amigo)
        {
           
            var nome = new SqlParameter("@Nome", amigo.Nome);
            var sobrenome = new SqlParameter("@Sobrenome", amigo.Sobrenome);
            var email = new SqlParameter("Email", amigo.Email);
            var telefone = new SqlParameter("@Telefone", amigo.Telefone);
            var aniversario = new SqlParameter("@Aniversario", amigo.Aniversario);

            var affected = _context.Database.ExecuteSqlRaw("Execute CriarAmigo @Nome, @Sobrenome, @Email, @Telefone, @Aniversario ",
                nome, sobrenome, email, telefone, aniversario);

            if (affected > 0)
            {
                return Created("Salvo Com sucesso", amigo);
            }
            else
            {
                throw new Exception();
            }

        }

        // POST: api/Amigos
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Amigo>> PostAmigo(Amigo amigo)
        {
            try
            {
                var connectionString = "Server=tcp:azure-db-infnet.database.windows.net,1433;Initial Catalog=dbTp03;Persist Security Info=False;User ID=administrador;Password=123456Isaac;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
                using var connection = new SqlConnection(connectionString);
                var sp = "CriarAmigo";
                var sqlCommand = new SqlCommand(sp, connection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                //sqlCommand.Parameters.AddWithValue("@AmigoId", amigo.AmigoId);
                sqlCommand.Parameters.AddWithValue("@Nome", amigo.Nome);
                sqlCommand.Parameters.AddWithValue("@Sobrenome", amigo.Sobrenome);
                sqlCommand.Parameters.AddWithValue("@Telefone", amigo.Telefone);
                sqlCommand.Parameters.AddWithValue("@Email", amigo.Email);
                sqlCommand.Parameters.AddWithValue("@Aniversario", amigo.Aniversario);

                try
                {
                    connection.Open();
                    sqlCommand.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    throw e;
                }

            }
            catch (Exception e)
            {

                throw e;
            }
            return NoContent(); 
        }

        // DELETE: api/Amigos/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Amigo>> DeleteAmigo(int id)
        {
            var amigo = new Amigo();
            var connectionString = "Server=tcp:azure-db-infnet.database.windows.net,1433;Initial Catalog=dbTp03;Persist Security Info=False;User ID=administrador;Password=123456Isaac;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            using var connection = new SqlConnection(connectionString);
            string procedureName = "DeletarAmigo";
            var sqlCommand = new SqlCommand(procedureName, connection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@AmigoId", id);
            await connection.OpenAsync();
            await sqlCommand.ExecuteNonQueryAsync();
            return NoContent();
        }

        private bool AmigoExists(int id)
        {
            return _context.Amigo.Any(e => e.AmigoId == id);
        }

    }
}
