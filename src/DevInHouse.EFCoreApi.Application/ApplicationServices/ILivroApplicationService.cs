using DevInHouse.EFCoreApi.Application.ViewModels;

namespace DevInHouse.EFCoreApi.Application.ApplicationServices
{
    public interface ILivroApplicationService
    {
        Task<IEnumerable<LivroViewModel>> ObterLivrosAsync(string titulo);

        Task<LivroEditViewModel> ObterPorIdAsync(int id);

        Task<int> CriarLivroAsync(LivroCreateViewModel livroViewModel);

        Task EditarLivroAsync(LivroEditViewModel livroViewModel);

        Task<LivroCreateViewModel> InicializarLivroCreateViewModelAsync();

        Task<LivroEditViewModel> InicializarLivroEditViewModelAsync(int id);
    }
}
