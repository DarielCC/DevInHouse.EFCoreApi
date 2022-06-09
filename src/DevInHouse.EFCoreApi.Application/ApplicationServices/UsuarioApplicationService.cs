using DevInHouse.EFCoreApi.Application.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace DevInHouse.EFCoreApi.Application.ApplicationServices
{
    public class UsuarioApplicationService : IUsuarioApplicationService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public UsuarioApplicationService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IdentityResult> CriarUsuarioAsync(UsuarioCreateViewModel usuarioCreateViewModel)
        {
            IdentityUser? user = new IdentityUser()
            {
                UserName = usuarioCreateViewModel.Email,
                Email = usuarioCreateViewModel.Email
            };

            IdentityResult? result = await _userManager.CreateAsync(user, usuarioCreateViewModel.Senha);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
            }

            return result;
        }
    }
}
