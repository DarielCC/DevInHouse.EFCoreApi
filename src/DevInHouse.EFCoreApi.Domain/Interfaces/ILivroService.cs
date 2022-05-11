using DevInHouse.EFCoreApi.Core.Entities;

namespace DevInHouse.EFCoreApi.Core.Interfaces
{
    public interface ILivroService
    {
        public IEnumerable<Livro> ObterLivros(string titulo);

        public Livro? ObterPorId(int id);

        public int CriarLivro(string titulo, int categoriaId, int autorId, DateTime dataPublicacao, decimal preco);

        public void AtualizarLivro(int id, string titulo, int categoriaId, int autorId, DateTime dataPublicacao, decimal preco);

        public void RemoverLivro(int id);
    }
}