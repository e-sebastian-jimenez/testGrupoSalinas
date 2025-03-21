using Backend_Examen_Edgar_S_J_M.Domain;
using Microsoft.EntityFrameworkCore;
using System;

namespace Backend_Examen_Edgar_S_J_M.Persistence
{
    public class SqlServerDbContext : DbContext
    {
        public SqlServerDbContext(DbContextOptions<SqlServerDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
