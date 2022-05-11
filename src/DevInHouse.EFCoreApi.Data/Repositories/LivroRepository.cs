using DevInHouse.EFCoreApi.Core.Entities;
using DevInHouse.EFCoreApi.Data.Context;
using DevInHouse.EFCoreApi.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DevInHouse.EFCoreApi.Data.Repositories
{
    public class LivroRepository : ILivroRepository
    {
        private readonly DataContext _context;

        public LivroRepository(DataContext context)
        {
            _context = context;
        }

        public IEnumerable<Livro> ObterLivros(string titulo) =>
            _context.Livros
                .Include(p => p.Categoria)
                .Include(p => p.Autor)
                .Where(p => string.IsNullOrWhiteSpace(titulo) || p.Titulo.Contains(titulo))
                .ToList();

        public Livro? ObterPorId(int id) =>
            _context.Livros
                .Include(p => p.Categoria)
                .Include(p => p.Autor)
                .FirstOrDefault(p => p.Id == id);

        public int InserirLivro(Livro livro)
        {
            _context.Livros.Add(livro);
            SaveChanges();
            return livro.Id;
        }

        public void AtualizarLivro(Livro livro)
        {
            _context.Livros.Update(livro);
            SaveChanges();
        }

        public void RemoverLivro(Livro livro)
        {
            _context.Livros.Remove(livro);
            SaveChanges();
        }

        private void SaveChanges() => _context.SaveChanges();
    }
}