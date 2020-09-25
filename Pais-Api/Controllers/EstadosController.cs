using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Model;
using Pais_Api.Data;

namespace Pais_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstadosController : ControllerBase
    {
        private readonly Pais_ApiContext _context;

        public EstadosController(Pais_ApiContext context)
        {
            _context = context;
        }

        // GET: api/Estados
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Estado>>> GetEstado()
        {
            var estados = _context.Estado.FromSqlRaw("Execute dbo.ListarEstados").ToList();

            return estados;
        }

        // GET: api/Estados/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Estado>> GetEstado(int id)
        {
            var estado = await _context.Estado.FindAsync(id);

            if (estado == null)
            {
                return NotFound();
            }

            return estado;
        }

        // PUT: api/Estados/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEstado(int id, Estado estado)
        {
            if (id != estado.EstadoId)
            {
                return BadRequest();
            }

            _context.Entry(estado).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EstadoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Estados
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Estado>> PostEstado(Estado estado)
        {
            try
            {
                var connectionString = "Server=tcp:azure-db-infnet.database.windows.net,1433;Initial Catalog=dbTp03;Persist Security Info=False;User ID=administrador;Password=123456Isaac;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
                using var connection = new SqlConnection(connectionString);
                var sp = "CriarEstado";
                var sqlCommand = new SqlCommand(sp, connection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@NomeEstado", estado.NomeEstado);
                //sqlCommand.Parameters.AddWithValue("@PaisId", estado.Pais.PaisId);
                

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

        // DELETE: api/Estados/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Estado>> DeleteEstado(int id)
        {
            var estado = new Estado();
            var connectionString = "Server=tcp:azure-db-infnet.database.windows.net,1433;Initial Catalog=dbTp03;Persist Security Info=False;User ID=administrador;Password=123456Isaac;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            using var connection = new SqlConnection(connectionString);
            string procedureName = "DeletarEstado";
            var sqlCommand = new SqlCommand(procedureName, connection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@EstadoId", id);
            await connection.OpenAsync();
            await sqlCommand.ExecuteNonQueryAsync();
            return NoContent();
        }

        private bool EstadoExists(int id)
        {
            return _context.Estado.Any(e => e.EstadoId == id);
        }
    }
}
