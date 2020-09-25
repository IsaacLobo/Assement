using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Model;

namespace Amigo_Api.Data
{
    public class Amigo_ApiContext : DbContext
    {
        public Amigo_ApiContext (DbContextOptions<Amigo_ApiContext> options)
            : base(options)
        {
        }

        public Amigo_ApiContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=tcp:azure-db-infnet.database.windows.net,1433;Initial Catalog=dbTp03;Persist Security Info=False;User ID=administrador;Password=123456Isaac;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        }

        public DbSet<Model.Amigo> Amigo { get; set; }
    }
}
