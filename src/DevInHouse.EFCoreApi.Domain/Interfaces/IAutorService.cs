using DevInHouse.EFCoreApi.Core.Entities;

namespace DevInHouse.EFCoreApi.Domain.Interfaces
{
    public interface IAutorService
    {
        Task<IEnumerable<Autor>> ObterAutoresAsync();
        Task<IEnumerable<Autor>> ObterAutoresV2Async(string nome);
    }
}
