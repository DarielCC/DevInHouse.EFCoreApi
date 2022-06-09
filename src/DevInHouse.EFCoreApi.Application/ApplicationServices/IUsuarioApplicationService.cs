using DevInHouse.EFCoreApi.Application.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace DevInHouse.EFCoreApi.Application.ApplicationServices
{
    public interface IUsuarioApplicationService
    {
        Task<IdentityResult> CriarUsuarioAsync(UsuarioCreateViewModel usuarioCreateViewModel);
    }
}
