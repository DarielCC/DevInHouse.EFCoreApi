using DevInHouse.EFCoreApi.Core.Entities;

namespace DevInHouse.EFCoreApi.Domain.Interfaces
{
    public interface IAutorRepository
    {
        Task<Autor?> ObterPorIdAsync(int id);
        Task<IEnumerable<Autor>> ObterAutoresAsync();
        Task<IEnumerable<Autor>> ObterAutoresV2Async(string nome);
    }
}
