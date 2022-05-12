using DevInHouse.EFCoreApi.Core.Entities;

namespace DevInHouse.EFCoreApi.Core.Interfaces
{
    public interface ILivroService
    {
        public Task<IEnumerable<Livro>> ObterLivrosAsync(string titulo);

        public Task<Livro>? ObterPorIdAsync(int id);

        public Task<int> CriarLivroAsync(string titulo, int categoriaId, int autorId, DateTime dataPublicacao, decimal preco);

        public Task AtualizarLivroAsync(int id, string titulo, int categoriaId, int autorId, DateTime dataPublicacao, decimal preco);

        public Task RemoverLivroAsync(int id);
    }
}