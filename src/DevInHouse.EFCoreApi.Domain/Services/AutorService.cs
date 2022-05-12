using DevInHouse.EFCoreApi.Core.Entities;
using DevInHouse.EFCoreApi.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevInHouse.EFCoreApi.Domain.Services
{
    public class AutorService : IAutorService
    {
        private readonly IAutorRepository _autorRepository;

        public AutorService(IAutorRepository autorRepository) => _autorRepository = autorRepository;

        public async Task<IEnumerable<Autor>> ObterAutoresAsync() => await _autorRepository.ObterAutoresAsync();
    }
}
