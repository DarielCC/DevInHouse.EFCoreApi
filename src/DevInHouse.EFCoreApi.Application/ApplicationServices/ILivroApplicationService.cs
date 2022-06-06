using DevInHouse.EFCoreApi.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevInHouse.EFCoreApi.Application.ApplicationServices
{
    public interface ILivroApplicationService
    {
        Task<IEnumerable<LivroViewModel>> ObterLivrosAsync(string titulo);
    }
}
