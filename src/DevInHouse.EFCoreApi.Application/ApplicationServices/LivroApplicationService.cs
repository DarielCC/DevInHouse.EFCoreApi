using AutoMapper;
using DevInHouse.EFCoreApi.Application.ViewModels;
using DevInHouse.EFCoreApi.Core.Entities;
using DevInHouse.EFCoreApi.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

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
            var livros = await _livroService.ObterLivrosAsync(titulo);

            return _mapper.Map<IEnumerable<LivroViewModel>>(livros);
        }

        public async Task<int> CriarLivroAsync(LivroCreateViewModel livroViewModel)
        {
            var livro = _mapper.Map<Livro>(livroViewModel);

            return await _livroService.CriarLivroAsync(livro);
        }

        public async Task<LivroCreateViewModel> InicializarLivroCreateViewModelAsync()
        {
            var autores = await _autorApplicationService.ObterAutoresAsync();
            var categorias = new List<CategoriaViewModel>()
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
    }
}
