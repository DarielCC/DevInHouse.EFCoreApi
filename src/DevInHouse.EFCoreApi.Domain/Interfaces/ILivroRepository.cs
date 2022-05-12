using DevInHouse.EFCoreApi.Core.Entities;

namespace DevInHouse.EFCoreApi.Domain.Interfaces
{
    public interface ILivroRepository
    {
        Task<IEnumerable<Livro>> ObterLivrosAsync(string titulo);
        Task<int> InserirLivroAsync(Livro livro);
        Task<Livro>? ObterPorIdAsync(int id);
        Task AtualizarLivroAsync(Livro livro);

        Task RemoverLivroAsync(Livro livro);
    }
}
