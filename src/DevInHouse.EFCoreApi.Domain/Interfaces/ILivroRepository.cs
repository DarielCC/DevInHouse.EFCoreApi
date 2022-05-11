using DevInHouse.EFCoreApi.Core.Entities;

namespace DevInHouse.EFCoreApi.Domain.Interfaces
{
    public interface ILivroRepository
    {
        IEnumerable<Livro> ObterLivros(string titulo);
        int InserirLivro(Livro livro);
        Livro? ObterPorId(int id);
        void AtualizarLivro(Livro livro);

        void RemoverLivro(Livro livro);
    }
}
