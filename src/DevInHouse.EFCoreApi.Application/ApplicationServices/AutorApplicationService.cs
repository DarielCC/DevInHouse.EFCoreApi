using AutoMapper;
using DevInHouse.EFCoreApi.Application.ViewModels;
using DevInHouse.EFCoreApi.Core.Entities;
using DevInHouse.EFCoreApi.Domain.Interfaces;

namespace DevInHouse.EFCoreApi.Application.ApplicationServices
{
    public class AutorApplicationService : IAutorApplicationService
    {
        private readonly IAutorService _autorService;
        private readonly IMapper _mapper;

        public AutorApplicationService(IAutorService autorService, IMapper mapper)
        {
            _autorService = autorService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AutorViewModel>> ObterAutoresAsync()
        {
            IEnumerable<Autor>? autores = await _autorService.ObterAutoresAsync();

            return _mapper.Map<IEnumerable<AutorViewModel>>(autores);
        }
    }
}
