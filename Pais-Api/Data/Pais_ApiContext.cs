using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Model;

namespace Pais_Api.Data
{
    public class Pais_ApiContext : DbContext
    {
        public Pais_ApiContext (DbContextOptions<Pais_ApiContext> options)
            : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=tcp:azure-db-infnet.database.windows.net,1433;Initial Catalog=dbTp03;Persist Security Info=False;User ID=administrador;Password=123456Isaac;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        }

        public DbSet<Model.Estado> Estado { get; set; }

        public DbSet<Model.Pais> Pais { get; set; }
    }
}
