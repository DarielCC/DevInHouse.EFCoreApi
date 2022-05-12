using DevInHouse.EFCoreApi.Core.Entities;
using DevInHouse.EFCoreApi.Data.Context;
using DevInHouse.EFCoreApi.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DevInHouse.EFCoreApi.Data.Repositories
{
    public class AutorRepository : IAutorRepository
    {
        private readonly DataContext _context;

        public AutorRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Autor>>? ObterAutoresAsync() => await
            _context.Autores.ToListAsync();

        public async Task<Autor>? ObterPorIdAsync(int id) => await
            _context.Autores
                .FirstOrDefaultAsync(p => p.Id == id);
    }
}
