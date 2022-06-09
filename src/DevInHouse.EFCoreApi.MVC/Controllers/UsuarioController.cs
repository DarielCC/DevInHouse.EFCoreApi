using DevInHouse.EFCoreApi.Application.ApplicationServices;
using DevInHouse.EFCoreApi.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DevInHouse.EFCoreApi.MVC.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IUsuarioApplicationService _usuarioAppService;

        public UsuarioController(IUsuarioApplicationService usuarioAppService)
        {
            _usuarioAppService = usuarioAppService;
        }

        // GET: UsuarioController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UsuarioController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormCollection collection)
        {
            try
            {
                Microsoft.Extensions.Primitives.StringValues email = collection["Email"];
                Microsoft.Extensions.Primitives.StringValues senha = collection["Senha"];

                Microsoft.AspNetCore.Identity.IdentityResult? result = await _usuarioAppService.CriarUsuarioAsync(new UsuarioCreateViewModel()
                {
                    Email = email,
                    Senha = senha
                });

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }

                foreach (Microsoft.AspNetCore.Identity.IdentityError? error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            catch
            {
                return View();
            }
            return View();
        }
    }
}
