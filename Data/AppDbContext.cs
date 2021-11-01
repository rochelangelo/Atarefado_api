using Atarefado.Models;
using Microsoft.EntityFrameworkCore;

namespace Atarefado.Data{
    public class AppDbContext : DbContext{
        public DbSet<Tarefa> Tarefas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlite("DataSource=app.db;Cache=Shared");
    }
}