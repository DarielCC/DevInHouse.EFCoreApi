using DevInHouse.EFCoreApi.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DevInHouse.EFCoreApi.Application.ApplicationServices
{
    public interface IAutorApplicationService
    {
        Task<IEnumerable<AutorViewModel>> ObterAutoresAsync();
    }
}
