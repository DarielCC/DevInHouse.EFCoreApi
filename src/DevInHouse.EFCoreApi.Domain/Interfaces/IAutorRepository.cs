using DevInHouse.EFCoreApi.Core.Entities;

namespace DevInHouse.EFCoreApi.Domain.Interfaces
{
    public interface IAutorRepository
    {
        Task<Autor?> ObterPorIdAsync(int id);
        Task<Autor?> ObterPorNomeAsync(string nome);
        Task<IEnumerable<Autor>> ObterAutoresAsync();
        Task<IEnumerable<Autor>> ObterAutoresV2Async(string nome);
        Task<int> InserirAutorAsync(Autor autor);
    }
}
