using DevInHouse.EFCoreApi.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace DevInHouse.EFCoreApi.Data.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public DbSet<Livro> Livros { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Autor> Autores { get; set; }
    }
}