using DevInHouse.EFCoreApi.Core.Entities;
using DevInHouse.EFCoreApi.Data.Context;
using DevInHouse.EFCoreApi.Domain.Interfaces;

namespace DevInHouse.EFCoreApi.Data.Repositories
{
    public class AutorRepository : IAutorRepository
    {
        private readonly DataContext _context;

        public AutorRepository(DataContext context)
        {
            _context = context;
        }

        public Autor? ObterPorId(int id) =>
            _context.Autores
                .FirstOrDefault(p => p.Id == id);
    }
}
