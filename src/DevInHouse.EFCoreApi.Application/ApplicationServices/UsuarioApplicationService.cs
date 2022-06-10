using DevInHouse.EFCoreApi.Application.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace DevInHouse.EFCoreApi.Application.ApplicationServices
{
    public class UsuarioApplicationService : IUsuarioApplicationService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UsuarioApplicationService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public async Task<IdentityResult> CriarUsuarioAsync(UsuarioCreateViewModel usuarioCreateViewModel)
        {
            IdentityUser? user = new IdentityUser()
            {
                UserName = usuarioCreateViewModel.Email,
                Email = usuarioCreateViewModel.Email                
            };

            var identityRole = new IdentityRole
            {
                Name = usuarioCreateViewModel.IsAdmin ? "Admin" : "User"
            };

            var role = _roleManager.Roles.FirstOrDefault(r => r.Name == identityRole.Name); 

            if(role is null)
                await _roleManager.CreateAsync(identityRole);


            var result = await _userManager.CreateAsync(user, usuarioCreateViewModel.Senha);

            await _userManager.AddToRoleAsync(user, identityRole.Name);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
            }

            return result;
        }

        public async Task<SignInResult> Login(LoginViewModel loginViewModel)
        {
            return await _signInManager.PasswordSignInAsync(loginViewModel.Email, loginViewModel.Senha, loginViewModel.RememberMe, false);
        }


        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
