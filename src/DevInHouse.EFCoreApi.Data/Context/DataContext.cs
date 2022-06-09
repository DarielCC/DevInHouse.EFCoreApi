using DevInHouse.EFCoreApi.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace DevInHouse.EFCoreApi.Data.Context
{
    public class DataContext : IdentityDbContext
    {
        public DataContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Livro> Livros { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Autor> Autores { get; set; }
    }
}