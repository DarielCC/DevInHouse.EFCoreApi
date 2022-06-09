using DevInHouse.EFCoreApi.Application.ViewModels;

namespace DevInHouse.EFCoreApi.Application.ApplicationServices
{
    public interface IAutorApplicationService
    {
        Task<IEnumerable<AutorViewModel>> ObterAutoresAsync();
    }
}
