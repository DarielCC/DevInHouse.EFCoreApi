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

        public async Task<IEnumerable<Livro>> ObterLivrosAsync(string titulo) => await
            _context.Livros
                .Include(p => p.Categoria)
                .Include(p => p.Autor)
                .Where(p => string.IsNullOrWhiteSpace(titulo) || p.Titulo.Contains(titulo))
                .ToListAsync();

        public async Task<Livro?> ObterPorTituloAsync(string titulo) => await
            _context.Livros
                .Where(p => string.IsNullOrWhiteSpace(titulo) || p.Titulo.Contains(titulo))
                .FirstOrDefaultAsync();

        public async Task<Livro?> ObterPorIdAsync(int id) => await
            _context.Livros
                .Include(p => p.Categoria)
                .Include(p => p.Autor)
                .FirstOrDefaultAsync(p => p.Id == id);

        public async Task<int> InserirLivroAsync(Livro livro)
        {
            _context.Livros.Add(livro);
            await SaveChangesAsync();
            return livro.Id;
        }

        public async Task AtualizarLivroAsync(Livro livro)
        {
            _context.Livros.Update(livro);
            await SaveChangesAsync();
        }

        public async Task RemoverLivroAsync(Livro livro)
        {
            _context.Livros.Remove(livro);
            await SaveChangesAsync();
        }

        private async Task SaveChangesAsync() => await _context.SaveChangesAsync();

    }
}