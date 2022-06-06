using AutoMapper;
using DevInHouse.EFCoreApi.Application.ViewModels;
using DevInHouse.EFCoreApi.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevInHouse.EFCoreApi.Application.ApplicationServices
{
    public class LivroApplicationService : ILivroApplicationService
    {
        private readonly ILivroService _livroService;
        private readonly IMapper _mapper;

        public LivroApplicationService(ILivroService livroService, IMapper mapper)
        {
            _livroService = livroService;
            _mapper = mapper;   
        }

        public async Task<IEnumerable<LivroViewModel>> ObterLivrosAsync(string titulo)
        {
            var livros = await _livroService.ObterLivrosAsync(titulo);

            return _mapper.Map<IEnumerable<LivroViewModel>>(livros);
        }
    }
}
