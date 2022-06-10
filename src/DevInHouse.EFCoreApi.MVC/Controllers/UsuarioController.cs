using DevInHouse.EFCoreApi.Application.ApplicationServices;
using DevInHouse.EFCoreApi.Application.ViewModels;
using Microsoft.AspNetCore.Authorization;
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
            return View(new UsuarioCreateViewModel());
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
                Microsoft.Extensions.Primitives.StringValues isAdmin = collection["IsAdmin"][0];

                var result = await _usuarioAppService.CriarUsuarioAsync(new UsuarioCreateViewModel()
                {
                    Email = email,
                    Senha = senha,
                    IsAdmin = Convert.ToBoolean(isAdmin)
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
