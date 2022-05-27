using DevInHouse.EFCoreApi.Core.Entities;
using DevInHouse.EFCoreApi.Domain.Interfaces;
using DevInHouse.EFCoreApi.Domain.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DevInHouse.EFCoreApi.Domain.Services
{
    public class AutorService : IAutorService
    {
        private readonly IAutorRepository _autorRepository;

        public AutorService(IAutorRepository autorRepository) => _autorRepository = autorRepository;

        public async Task<IEnumerable<Autor>> ObterAutoresAsync() => await _autorRepository.ObterAutoresAsync();
        public async Task<IEnumerable<Autor>> ObterAutoresV2Async(string nome) => await _autorRepository.ObterAutoresV2Async(nome);

        public async Task<int> CriarAutorAsync(string nome, string sobreNome)
        {
            if(string.IsNullOrEmpty(nome) || string.IsNullOrEmpty(sobreNome))
            {
                throw new ArgumentException("Nome e/ou Sobrenome não podem estar vazios");
            }

            var autor = await _autorRepository.ObterPorNomeAsync(nome);
            if (autor is not null)
            {
                throw new Exception("Autor já existe");
            }

            autor = new Autor(nome, sobreNome);

            return await _autorRepository.InserirAutorAsync(autor);
        }
    }
}
