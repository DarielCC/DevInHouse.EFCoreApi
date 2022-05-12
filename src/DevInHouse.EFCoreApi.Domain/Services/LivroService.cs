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

        public async Task<IEnumerable<Livro>> ObterLivrosAsync(string titulo) => await _livroRepository.ObterLivrosAsync(titulo);

        public async Task<Livro>? ObterPorIdAsync(int id) => await _livroRepository?.ObterPorIdAsync(id);

        public async Task<int> CriarLivroAsync(string titulo, int categoriaId, int autorId, DateTime dataPublicacao, decimal preco)
        {
            Autor? autor = await _autorRepository.ObterPorIdAsync(autorId);
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

            return await _livroRepository.InserirLivroAsync(livro);
        }

        public async Task AtualizarLivroAsync(int id, string titulo, int categoriaId, int autorId, DateTime dataPublicacao, decimal preco)
        {
            Livro? livro = await _livroRepository.ObterPorIdAsync(id);

            if (livro is null)
            {
                throw new KeyNotFoundException("Livro não existe");
            }

            livro.AlterarDados(titulo, categoriaId, autorId, dataPublicacao, preco);
            await _livroRepository.AtualizarLivroAsync(livro);
        }

        public async Task RemoverLivroAsync(int id)
        {
            Livro? livro = await ObterPorIdAsync(id);
            if (livro == null)
            {
                throw new ArgumentException("O livro com o identificador informado não existe", "id");
            }

            await _livroRepository.RemoverLivroAsync(livro);
        }
    }
}