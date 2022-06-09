using AutoMapper;
using DevInHouse.EFCoreApi.Application.ViewModels;
using DevInHouse.EFCoreApi.Core.Entities;
using DevInHouse.EFCoreApi.Core.Interfaces;

namespace DevInHouse.EFCoreApi.Application.ApplicationServices
{
    public class LivroApplicationService : ILivroApplicationService
    {
        private readonly IAutorApplicationService _autorApplicationService;
        private readonly ILivroService _livroService;
        private readonly IMapper _mapper;

        public LivroApplicationService(ILivroService livroService, IAutorApplicationService autorApplicationService, IMapper mapper)
        {
            _livroService = livroService;
            _autorApplicationService = autorApplicationService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<LivroViewModel>> ObterLivrosAsync(string titulo)
        {
            IEnumerable<Livro>? livros = await _livroService.ObterLivrosAsync(titulo);

            return _mapper.Map<IEnumerable<LivroViewModel>>(livros);
        }

        public async Task<LivroEditViewModel> ObterPorIdAsync(int id)
        {
            Livro? livro = await _livroService.ObterPorIdAsync(id);

            return _mapper.Map<LivroEditViewModel>(livro);
        }

        public async Task<int> CriarLivroAsync(LivroCreateViewModel livroViewModel)
        {
            Livro? livro = _mapper.Map<Livro>(livroViewModel);

            return await _livroService.CriarLivroAsync(livro);
        }

        public async Task EditarLivroAsync(LivroEditViewModel livroViewModel)
        {
            Livro? livro = _mapper.Map<Livro>(livroViewModel);

            await _livroService.AtualizarLivroAsync(livro);
        }

        public async Task<LivroCreateViewModel> InicializarLivroCreateViewModelAsync()
        {
            IEnumerable<AutorViewModel>? autores = await _autorApplicationService.ObterAutoresAsync();
            List<CategoriaViewModel>? categorias = new List<CategoriaViewModel>()
            {
                new CategoriaViewModel() { Id = 1, Nome = "Aventura" },
                new CategoriaViewModel() { Id = 2, Nome = "Romance" },
                new CategoriaViewModel() { Id = 3, Nome = "Terror" }
            };

            return new LivroCreateViewModel()
            {
                Autores = autores,
                Publicacao = DateTime.Now,
                Preco = decimal.Zero,
                Categorias = categorias
            };
        }


        public async Task<LivroEditViewModel> InicializarLivroEditViewModelAsync(int id)
        {
            IEnumerable<AutorViewModel>? autores = await _autorApplicationService.ObterAutoresAsync();
            List<CategoriaViewModel>? categorias = new List<CategoriaViewModel>()
            {
                new CategoriaViewModel() { Id = 1, Nome = "Aventura" },
                new CategoriaViewModel() { Id = 2, Nome = "Romance" },
                new CategoriaViewModel() { Id = 3, Nome = "Terror" }
            };

            Livro? livro = await _livroService.ObterPorIdAsync(id);

            return new LivroEditViewModel()
            {
                CategoriaId = livro.CategoriaId,
                AutorId = livro.AutorId,
                Autores = autores,
                Publicacao = livro.DataPublicacao,
                Preco = livro.Preco,
                Id = livro.Id,
                Titulo = livro.Titulo,
                Categorias = categorias
            };
        }
    }
}
