using DevInHouse.EFCoreApi.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevInHouse.EFCoreApi.Domain.Interfaces
{
    public interface IAutorService
    {
        Task<IEnumerable<Autor>> ObterAutoresAsync();
        Task<IEnumerable<Autor>> ObterAutoresV2Async(string nome);
    }
}
