using DevInHouse.EFCoreApi.Core.Entities;

namespace DevInHouse.EFCoreApi.Domain.Interfaces
{
    public interface IAutorRepository
    {
        Autor? ObterPorId(int id);
    }
}
