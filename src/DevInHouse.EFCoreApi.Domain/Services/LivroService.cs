using DevInHouse.EFCoreApi.Core.Entities;
using DevInHouse.EFCoreApi.Core.Interfaces;
using DevInHouse.EFCoreApi.Domain.Interfaces;

namespace DevInHouse.EFCoreApi.Core.Services
{
    public class LivroService : ILivroService
    {
        private readonly IAutorRepository _autorRepository;
        private readonly ILivroRepository _livroRepository;

        public LivroService(ILivroRepository livroRepository, IAutorRepository autorRepository)
        {
            _autorRepository = autorRepository;
            _livroRepository = livroRepository;
        }

        public IEnumerable<Livro> ObterLivros(string titulo) => _livroRepository.ObterLivros(titulo);

        public Livro? ObterPorId(int id) => _livroRepository?.ObterPorId(id);

        public int CriarLivro(string titulo, int categoriaId, int autorId, DateTime dataPublicacao, decimal preco)
        {
            Autor? autor = _autorRepository.ObterPorId(autorId);
            if (autor is null)
            {
                throw new KeyNotFoundException("Autor não existe");
            }

            Livro? livro = new Livro(titulo, categoriaId, autorId, dataPublicacao, preco);

            if (livro is null)
            {
                throw new ArgumentNullException("Livro inválido");
            }

            if (livro.Titulo is null)
            {
                throw new ArgumentNullException("Título inválido");
            }

            return _livroRepository.InserirLivro(livro);
        }

        public void AtualizarLivro(int id, string titulo, int categoriaId, int autorId, DateTime dataPublicacao, decimal preco)
        {
            Livro? livro = _livroRepository.ObterPorId(id);

            if (livro is null)
            {
                throw new KeyNotFoundException("Livro não existe");
            }

            livro.AlterarDados(titulo, categoriaId, autorId, dataPublicacao, preco);
            _livroRepository.AtualizarLivro(livro);
        }

        public void RemoverLivro(int id)
        {
            Livro? livro = ObterPorId(id);
            if (livro == null)
            {
                throw new ArgumentException("O livro com o identificador informado não existe", "id");
            }

            _livroRepository.RemoverLivro(livro);
        }
    }
}