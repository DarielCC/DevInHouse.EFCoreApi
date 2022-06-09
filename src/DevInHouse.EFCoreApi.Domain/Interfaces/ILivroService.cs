using DevInHouse.EFCoreApi.Core.Entities;

namespace DevInHouse.EFCoreApi.Core.Interfaces
{
    public interface ILivroService
    {
        public Task<IEnumerable<Livro>> ObterLivrosAsync(string titulo);

        public Task<Livro>? ObterPorIdAsync(int id);

        public Task<int> CriarLivroAsync(Livro livro);

        public Task AtualizarLivroAsync(Livro livro);

        public Task RemoverLivroAsync(int id);
    }
}