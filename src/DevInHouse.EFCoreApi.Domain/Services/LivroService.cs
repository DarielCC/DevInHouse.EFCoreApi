using DevInHouse.EFCoreApi.Core.Entities;
using DevInHouse.EFCoreApi.Core.Interfaces;
using DevInHouse.EFCoreApi.Domain.Interfaces;
using DevInHouse.EFCoreApi.Domain.Notifications;
using System.Net;

namespace DevInHouse.EFCoreApi.Core.Services
{
    public class LivroService : ILivroService
    {
        private readonly IAutorRepository _autorRepository;
        private readonly ILivroRepository _livroRepository;
        private readonly INotificacaoService _notificacaoService;

        public LivroService(ILivroRepository livroRepository, IAutorRepository autorRepository, INotificacaoService notificacaoService)
        {
            _autorRepository = autorRepository;
            _livroRepository = livroRepository;
            _notificacaoService = notificacaoService;
        }

        public async Task<IEnumerable<Livro>> ObterLivrosAsync(string titulo) => await _livroRepository.ObterLivrosAsync(titulo);

        public async Task<Livro>? ObterPorIdAsync(int id) => await _livroRepository?.ObterPorIdAsync(id);

        public async Task<int> CriarLivroAsync(Livro livro)
        {
            Autor? autor = await _autorRepository.ObterPorIdAsync(livro.AutorId);
            if (autor is null)
            {
                _notificacaoService.InserirNotificacao(new Notificacao()
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Mensagem = "Autor não existe"
                });
                return default;
            }

            Livro? livroPorTitulo = await _livroRepository.ObterPorTituloAsync(livro.Titulo);

            if (livroPorTitulo is not null)
            {
                _notificacaoService.InserirNotificacao(new Notificacao()
                {
                    StatusCode = HttpStatusCode.Ambiguous,
                    Mensagem = "Livro já existe com esse título"
                });
                return default;
            }

            FluentValidation.Results.ValidationResult? livroValidacao = livro.Validar();
            if (!livroValidacao.IsValid)
            {
                _notificacaoService.InserirNotificacoes(livroValidacao);
                return default;
            }

            return await _livroRepository.InserirLivroAsync(livro);
        }

        public async Task AtualizarLivroAsync(Livro livro)
        {
            if (livro is null)
            {
                throw new KeyNotFoundException("Livro não existe");
            }

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